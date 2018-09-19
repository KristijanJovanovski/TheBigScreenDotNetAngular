using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TheBigScreen.Entities;
using TheBigScreen.Entities.Entities;


namespace TheBigScreen.DataAccess
{
    public class TheBigScreenDbContext : DbContext
    {

        public TheBigScreenDbContext(DbContextOptions<TheBigScreenDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<TraktMovie> TraktMovies { get; set; }
        public DbSet<WatchedMovie> WatchedMovies { get; set; }
        public DbSet<RatedMovie> RatedMovies { get; set; }
        public DbSet<BookmarkedMovie> BookmarkedMovies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TraktMovie>()
                .ToTable("trakt_movies");

            modelBuilder.Entity<TraktMovie>()
                .Property(t => t.TraktId)
                .ValueGeneratedNever();


            modelBuilder.Entity<User>()
                .ToTable("users")
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .ValueGeneratedNever();



            modelBuilder.Entity<BookmarkedMovie>()
                .ToTable("bookmarked_movies");

            modelBuilder.Entity<BookmarkedMovie>()
                .HasKey(bm => new {bm.UserId, bm.MovieId});

            modelBuilder.Entity<BookmarkedMovie>()
                .HasOne(bm => bm.User)
                .WithMany(m => m.BookmarkedMovies)
                .HasForeignKey(bm => bm.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BookmarkedMovie>()
                .HasOne(bm => bm.Movie)
                .WithMany(m => m.BookmarkedUsers)
                .HasForeignKey(bm => bm.MovieId)
                .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<RatedMovie>()
                .ToTable("rated_movies");

            modelBuilder.Entity<RatedMovie>()
                .HasKey(rm => new {rm.UserId, rm.MovieId});

            modelBuilder.Entity<RatedMovie>()
                .HasOne(rm => rm.User)
                .WithMany(m => m.RatedMovies)
                .HasForeignKey(rm => rm.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RatedMovie>()
                .HasOne(rm => rm.Movie)
                .WithMany(m => m.RatedUsers)
                .HasForeignKey(rm => rm.MovieId)
                .OnDelete(DeleteBehavior.Cascade);



            modelBuilder.Entity<WatchedMovie>()
                .ToTable("watched_movies");

            modelBuilder.Entity<WatchedMovie>()
                .HasKey(wm => new {wm.UserId, wm.MovieId});

            modelBuilder.Entity<WatchedMovie>()
                .HasOne(wm => wm.User)
                .WithMany(m => m.WatchedMovies)
                .HasForeignKey(wm => wm.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WatchedMovie>()
                .HasOne(wm => wm.Movie)
                .WithMany(m => m.WatchedUsers)
                .HasForeignKey(wm => wm.MovieId)
                .OnDelete(DeleteBehavior.Cascade);
        }


    }
}
