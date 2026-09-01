using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList_MVC.Migrations
{
    /// <inheritdoc />
    public partial class CollegataCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "ToDos",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ToDos");
        }
    }
}
