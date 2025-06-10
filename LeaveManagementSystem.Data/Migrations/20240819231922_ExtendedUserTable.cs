using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExtendedUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "74f30c00-f192-4a39-8188-33be260d31c2",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1fd20d28-1791-408b-8b1a-80daae24143c", new DateOnly(1998, 3, 1), "Default", "Admin", "AQAAAAIAAYagAAAAELwkYy2b22RX6aAgd1I19zWcVFxxelAIYDvbgtqJK/5BwqiudruwpVqIdkgDm90A6w==", "9ac70eee-3cdf-48eb-9f2b-f0974bc6533a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "74f30c00-f192-4a39-8188-33be260d31c2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5806adb7-5dfa-42b5-8953-b6163cc88508", "AQAAAAIAAYagAAAAEOYAldzJeS42SckB3yGVbSD1jrUmS50KfuVM4jLaaXF3H9u87jDfxe5WkUlOBamqXg==", "23bb2d95-6e9d-4731-bcad-93705366519e" });
        }
    }
}
