using Microsoft.EntityFrameworkCore.Migrations;

namespace TPDB.Resource.API.Migrations
{
    public partial class ForImportMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commodities_Ports_PortId1",
                table: "Commodities");

            migrationBuilder.DropIndex(
                name: "IX_Commodities_PortId1",
                table: "Commodities");

            migrationBuilder.DropColumn(
                name: "PortId1",
                table: "Commodities");

            migrationBuilder.AddColumn<bool>(
                name: "ForImport",
                table: "Commodities",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForImport",
                table: "Commodities");

            migrationBuilder.AddColumn<int>(
                name: "PortId1",
                table: "Commodities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Commodities_PortId1",
                table: "Commodities",
                column: "PortId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Commodities_Ports_PortId1",
                table: "Commodities",
                column: "PortId1",
                principalTable: "Ports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
