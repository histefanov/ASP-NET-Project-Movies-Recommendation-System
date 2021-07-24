using Microsoft.EntityFrameworkCore.Migrations;

namespace MoviesRecommendationSystem.Data.Migrations
{
    public partial class EditorsTableFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Editor_AspNetUsers_UserId",
                table: "Editor");

            migrationBuilder.DropForeignKey(
                name: "FK_Editor_AspNetUsers_UserId1",
                table: "Editor");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Editor_EditorId",
                table: "Movies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Editor",
                table: "Editor");

            migrationBuilder.RenameTable(
                name: "Editor",
                newName: "Editors");

            migrationBuilder.RenameIndex(
                name: "IX_Editor_UserId1",
                table: "Editors",
                newName: "IX_Editors_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Editor_UserId",
                table: "Editors",
                newName: "IX_Editors_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Editors",
                table: "Editors",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Editors_AspNetUsers_UserId",
                table: "Editors",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Editors_AspNetUsers_UserId1",
                table: "Editors",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Editors_EditorId",
                table: "Movies",
                column: "EditorId",
                principalTable: "Editors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Editors_AspNetUsers_UserId",
                table: "Editors");

            migrationBuilder.DropForeignKey(
                name: "FK_Editors_AspNetUsers_UserId1",
                table: "Editors");

            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Editors_EditorId",
                table: "Movies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Editors",
                table: "Editors");

            migrationBuilder.RenameTable(
                name: "Editors",
                newName: "Editor");

            migrationBuilder.RenameIndex(
                name: "IX_Editors_UserId1",
                table: "Editor",
                newName: "IX_Editor_UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Editors_UserId",
                table: "Editor",
                newName: "IX_Editor_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Editor",
                table: "Editor",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Editor_AspNetUsers_UserId",
                table: "Editor",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Editor_AspNetUsers_UserId1",
                table: "Editor",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Editor_EditorId",
                table: "Movies",
                column: "EditorId",
                principalTable: "Editor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
