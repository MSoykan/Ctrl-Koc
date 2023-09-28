using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningManagementSystem.Web.Migrations
{
    /// <inheritdoc />
    public partial class CourseMaterialImageUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentPath",
                table: "Materials");

            migrationBuilder.RenameColumn(
                name: "ContentPath",
                table: "Assignments",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Materials",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Materials");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Assignments",
                newName: "ContentPath");

            migrationBuilder.AddColumn<string>(
                name: "ContentPath",
                table: "Materials",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
