using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TheBigScreen.DataAccess.Migrations
{
    public partial class Initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "trakt_movies",
                columns: table => new
                {
                    TraktId = table.Column<int>(nullable: false),
                    Certification = table.Column<string>(nullable: true),
                    Homepage = table.Column<string>(nullable: true),
                    ImdbId = table.Column<string>(nullable: true),
                    LanguageCode = table.Column<string>(nullable: true),
                    Overview = table.Column<string>(nullable: true),
                    Rating = table.Column<float>(nullable: true),
                    Released = table.Column<DateTime>(nullable: true),
                    Runtime = table.Column<int>(nullable: true),
                    Slug = table.Column<string>(nullable: false),
                    Tagline = table.Column<string>(nullable: true),
                    Title = table.Column<string>(maxLength: 255, nullable: false),
                    TmdbId = table.Column<int>(nullable: true),
                    Trailer = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    Votes = table.Column<int>(nullable: true),
                    Year = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trakt_movies", x => x.TraktId);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    Avatar = table.Column<string>(maxLength: 150, nullable: true),
                    Email = table.Column<string>(maxLength: 30, nullable: false),
                    FirstName = table.Column<string>(maxLength: 20, nullable: false),
                    Gender = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(maxLength: 20, nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Username = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "bookmarked_movies",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    MovieId = table.Column<int>(nullable: false),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    DateBookmarked = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookmarked_movies", x => new { x.UserId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_bookmarked_movies_trakt_movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "trakt_movies",
                        principalColumn: "TraktId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_bookmarked_movies_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rated_movies",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    MovieId = table.Column<int>(nullable: false),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    DateRated = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Rate = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rated_movies", x => new { x.UserId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_rated_movies_trakt_movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "trakt_movies",
                        principalColumn: "TraktId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rated_movies_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "watched_movies",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    MovieId = table.Column<int>(nullable: false),
                    AddedDate = table.Column<DateTime>(nullable: false),
                    DateWatched = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_watched_movies", x => new { x.UserId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_watched_movies_trakt_movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "trakt_movies",
                        principalColumn: "TraktId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_watched_movies_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bookmarked_movies_MovieId",
                table: "bookmarked_movies",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_rated_movies_MovieId",
                table: "rated_movies",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_users_Username",
                table: "users",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_watched_movies_MovieId",
                table: "watched_movies",
                column: "MovieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bookmarked_movies");

            migrationBuilder.DropTable(
                name: "rated_movies");

            migrationBuilder.DropTable(
                name: "watched_movies");

            migrationBuilder.DropTable(
                name: "trakt_movies");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
