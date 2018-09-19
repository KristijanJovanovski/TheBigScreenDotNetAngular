using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using TheBigScreen.DataAccess;
using TheBigScreen.DataAccess.Repositories;
using TheBigScreen.Entities.Interfaces;
using TheBigScreen.Services.Impl;
using TheBigScreen.Services.Interfaces;
using TMDbLib.Client;
using TraktApiSharp;

namespace TheBigScreen.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TheBigScreenDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("TheBigScreenConnection")));

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
//                    .WithOrigins("http://localhost:4200");
                    .AllowAnyOrigin();
            }));

            //            services.AddSignalR();

            services.AddSingleton(new TraktClient(Configuration.GetSection("Trakt")["ClientId"],
                Configuration.GetSection("Trakt")["ClientSecret"]));

            services.AddSingleton(new TMDbClient(Configuration.GetSection("TMDB")["APIKey"]));

            services.AddAutoMapper();


            var mvcCoreBuilder = services.AddMvcCore();

            mvcCoreBuilder
                .AddFormatterMappings()
                .AddJsonFormatters()
                .AddCors()
                .AddApiExplorer();



            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITraktMovieRepository, TraktMovieRepository>();
            services.AddScoped<IWatchedMovieRepository, WatchedMovieRepository>();
            services.AddScoped<IBookmarkedMovieRepository, BookmarkedMovieRepository>();
            services.AddScoped<IRatedMovieRepository, RatedMovieRepository>();

            // Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMovieService, MovieService>();


            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {Title = "My API", Version = "v1"});
            });



            string domain = $"https://{Configuration["Auth0:Domain"]}/";
            string apiIdentifier = Configuration["Auth0:ApiIdentifier"];
//            string apiSecret = Configuration["Auth0:ApiSecret"];

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = domain;
                options.Audience = apiIdentifier;
//                options.Events = new JwtBearerEvents
//                {
//                    OnTokenValidated = context =>
//                    {
//                        // Grab the raw value of the token, and store it as a claim so we can retrieve it again later in the request pipeline
//                        // Have a look at the ValuesController.UserInformation() method to see how to retrieve it and use it to retrieve the
//                        // user's information from the /userinfo endpoint
//                        if (context.SecurityToken is JwtSecurityToken token)
//                        {
//                            if (context.Principal.Identity is ClaimsIdentity identity)
//                            {
//                                identity.AddClaim(new Claim("access_token", token.RawData));
//                            }
//                        }
//
//                        return Task.FromResult(0);
//                    }
//                };
            });
            services.AddAuthorization(options =>
            {
//                options.AddPolicy("read:movies",
//                    policy => policy.Requirements.Add(new HasScopeRequirement("read:movies", domain)));
                options.AddPolicy("read:userdetails",
                    policy => policy.Requirements.Add(new HasScopeRequirement("read:userdetails", domain)));
            });
            // register the scope authorization handler
            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");

            //            app.UseSignalR(routes =>
            //            {
            //                routes.MapHub<ChatHub>("sync");
            //            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
