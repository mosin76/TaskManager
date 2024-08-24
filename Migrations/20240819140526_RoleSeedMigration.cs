using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace System.TaskItem.API.Migrations
{
    /// <inheritdoc />
    public partial class RoleSeedMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dea0b568-d421-4419-939c-97e53d3fb51a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fafaedf2-27b5-46f1-8fd5-1487b9181c58");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "20aeeaa9-b8bc-4aa5-8cd2-69104f3505da", "1", "Admin", "Admin" },
                    { "3e4c4684-bea3-477d-83df-5557dcd2af36", "2", "User", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "20aeeaa9-b8bc-4aa5-8cd2-69104f3505da");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3e4c4684-bea3-477d-83df-5557dcd2af36");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "dea0b568-d421-4419-939c-97e53d3fb51a", "2", "User", "User" },
                    { "fafaedf2-27b5-46f1-8fd5-1487b9181c58", "1", "Admin", "Admin" }
                });
        }
    }
}
