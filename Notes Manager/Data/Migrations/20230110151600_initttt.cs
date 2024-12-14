using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notes_Manager.Data.Migrations
{
    public partial class initttt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Notes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_CategoryId",
                table: "Notes",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Category_CategoryId",
                table: "Notes",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Category_CategoryId",
                table: "Notes");

            migrationBuilder.DropIndex(
                name: "IX_Notes_CategoryId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Notes");
        }
    }
}
