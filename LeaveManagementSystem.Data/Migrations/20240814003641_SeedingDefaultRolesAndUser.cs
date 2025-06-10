using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaveManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDefaultRolesAndUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "084d4bda-bbec-4140-ae2c-4e59f6ff0b44", null, "Supervisor", "SUPERVISOR" },
                    { "4ac2cb24-f0cb-424d-8c97-76337e0d9404", null, "Administrator", "ADMINISTRATOR" },
                    { "5d2f419b-1e2e-425c-b0c0-eeedbed738f5", null, "Employee", "EMPLOYEE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "74f30c00-f192-4a39-8188-33be260d31c2", 0, "5806adb7-5dfa-42b5-8953-b6163cc88508", "admin@localhost.com", true, false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEOYAldzJeS42SckB3yGVbSD1jrUmS50KfuVM4jLaaXF3H9u87jDfxe5WkUlOBamqXg==", null, false, "23bb2d95-6e9d-4731-bcad-93705366519e", false, "admin@localhost.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "4ac2cb24-f0cb-424d-8c97-76337e0d9404", "74f30c00-f192-4a39-8188-33be260d31c2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "084d4bda-bbec-4140-ae2c-4e59f6ff0b44");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5d2f419b-1e2e-425c-b0c0-eeedbed738f5");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4ac2cb24-f0cb-424d-8c97-76337e0d9404", "74f30c00-f192-4a39-8188-33be260d31c2" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4ac2cb24-f0cb-424d-8c97-76337e0d9404");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "74f30c00-f192-4a39-8188-33be260d31c2");
        }
    }
}
