using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace System.TaskItem.API.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class updateTaskDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfileImage",
                table: "SprintTask",
                newName: "DueDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DueDate",
                table: "SprintTask",
                newName: "ProfileImage");
        }
    }
}
