using Microsoft.EntityFrameworkCore.Migrations;

namespace MoviesRecommendationSystem.Data.Migrations
{
    public partial class RemoveStudioEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Studios_StudioId",
                table: "Movies");

            migrationBuilder.DropForeignKey(
                name: "FK_Series_Studios_StudioId",
                table: "Series");

            migrationBuilder.DropTable(
                name: "Studios");

            migrationBuilder.DropIndex(
                name: "IX_Series_StudioId",
                table: "Series");

            migrationBuilder.DropIndex(
                name: "IX_Movies_StudioId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "StudioId",
                table: "Movies");

            migrationBuilder.AddColumn<string>(
                name: "Studio",
                table: "Series",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Studio",
                table: "Movies",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Studio",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "Studio",
                table: "Movies");

            migrationBuilder.AddColumn<int>(
                name: "StudioId",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Studios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studios", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Series_StudioId",
                table: "Series",
                column: "StudioId");

            migrationBuilder.CreateIndex(
                name: "IX_Movies_StudioId",
                table: "Movies",
                column: "StudioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Studios_StudioId",
                table: "Movies",
                column: "StudioId",
                principalTable: "Studios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Series_Studios_StudioId",
                table: "Series",
                column: "StudioId",
                principalTable: "Studios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
