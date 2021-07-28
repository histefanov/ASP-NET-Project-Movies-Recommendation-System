using Microsoft.EntityFrameworkCore.Migrations;

namespace MoviesRecommendationSystem.Data.Migrations
{
    public partial class FixEditorToUserRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserWatchlistMovies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserWatchlistMovies",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWatchlistMovies", x => new { x.UserId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_UserWatchlistMovies_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserWatchlistMovies_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserWatchlistMovies_MovieId",
                table: "UserWatchlistMovies",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWatchlistMovies_UserId1",
                table: "UserWatchlistMovies",
                column: "UserId1");
        }
    }
}
