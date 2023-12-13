using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CustomerApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_User_Role_Permission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_permission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "t_role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDisabled = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "t_user",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_user", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "t_role_permission",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_role_permission", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_t_role_permission_t_permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "t_permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_role_permission_t_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "t_role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "t_user_role",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_user_role", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_t_user_role_t_role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "t_role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_user_role_t_user_UserId",
                        column: x => x.UserId,
                        principalTable: "t_user",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "t_permission",
                columns: new[] { "Id", "IsDisabled", "Name" },
                values: new object[,]
                {
                    { 1, false, "CreateCustomer" },
                    { 2, false, "DeleteCustomer" },
                    { 3, false, "UpdateCustomer" },
                    { 4, false, "GetOrders" }
                });

            migrationBuilder.InsertData(
                table: "t_role",
                columns: new[] { "Id", "IsDisabled", "Name" },
                values: new object[,]
                {
                    { 1, false, "Administrator" },
                    { 2, false, "StandartUser" }
                });

            migrationBuilder.InsertData(
                table: "t_role_permission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 4, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_role_permission_PermissionId",
                table: "t_role_permission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_t_user_role_RoleId",
                table: "t_user_role",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_role_permission");

            migrationBuilder.DropTable(
                name: "t_user_role");

            migrationBuilder.DropTable(
                name: "t_permission");

            migrationBuilder.DropTable(
                name: "t_role");

            migrationBuilder.DropTable(
                name: "t_user");
        }
    }
}
