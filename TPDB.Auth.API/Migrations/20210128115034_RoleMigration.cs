using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TPDB.Auth.API.Migrations
{
    public partial class RoleMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Users_AccountId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_AccountId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Roles");

            migrationBuilder.CreateTable(
                name: "AccountRole",
                columns: table => new
                {
                    AccountsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RolesID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRole", x => new { x.AccountsId, x.RolesID });
                    table.ForeignKey(
                        name: "FK_AccountRole_Roles_RolesID",
                        column: x => x.RolesID,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountRole_Users_AccountsId",
                        column: x => x.AccountsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountRole_RolesID",
                table: "AccountRole",
                column: "RolesID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountRole");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Roles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_AccountId",
                table: "Roles",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Users_AccountId",
                table: "Roles",
                column: "AccountId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
