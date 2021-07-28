using Microsoft.EntityFrameworkCore.Migrations;

namespace MoviesRecommendationSystem.Data.Migrations
{
    public partial class EditorUserIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Editors_UserId",
                table: "Editors");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Editors",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Editors_UserId",
                table: "Editors",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Editors_UserId",
                table: "Editors");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Editors",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Editors_UserId",
                table: "Editors",
                column: "UserId",
                unique: true);
        }
    }
}
