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
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "65acea3f-b525-4d64-a0f6-5d338172998f", "1", "Admin", "Admin" },
                    { "dd4436f5-fb55-4728-a3ce-48aefbe0683d", "2", "User", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "65acea3f-b525-4d64-a0f6-5d338172998f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dd4436f5-fb55-4728-a3ce-48aefbe0683d");
        }
    }
}
