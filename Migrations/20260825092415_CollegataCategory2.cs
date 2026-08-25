using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList_MVC.Migrations
{
    /// <inheritdoc />
    public partial class CollegataCategory2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ToDos_CategoryId",
                table: "ToDos",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDos_Categories_CategoryId",
                table: "ToDos",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDos_Categories_CategoryId",
                table: "ToDos");

            migrationBuilder.DropIndex(
                name: "IX_ToDos_CategoryId",
                table: "ToDos");
        }
    }
}
