using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MoviesRecommendationSystem.Data.Migrations
{
    public partial class EditorsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EditorId",
                table: "Movies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Editor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Editor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Editor_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Editor_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movies_EditorId",
                table: "Movies",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_Editor_UserId",
                table: "Editor",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Editor_UserId1",
                table: "Editor",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Editor_EditorId",
                table: "Movies",
                column: "EditorId",
                principalTable: "Editor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Editor_EditorId",
                table: "Movies");

            migrationBuilder.DropTable(
                name: "Editor");

            migrationBuilder.DropIndex(
                name: "IX_Movies_EditorId",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "EditorId",
                table: "Movies");
        }
    }
}
