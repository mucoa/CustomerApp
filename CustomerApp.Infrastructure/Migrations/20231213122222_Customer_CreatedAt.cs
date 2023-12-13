using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Customer_CreatedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "t_customer",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "t_permission",
                columns: new[] { "Id", "IsDisabled", "Name" },
                values: new object[] { 5, false, "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "t_permission",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "t_customer");
        }
    }
}
