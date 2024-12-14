using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notes_Manager.Data.Migrations
{
    public partial class inittttt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Category_CategoryId",
                table: "Notes");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Category_CategoryId",
                table: "Notes",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Category_CategoryId",
                table: "Notes");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Category_CategoryId",
                table: "Notes",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }
    }
}
