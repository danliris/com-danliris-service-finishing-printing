using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Production.Lib.Migrations
{
    public partial class FixReturToQCItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductionOrderName",
                table: "ReturToQCItems",
                newName: "ProductionOrderNo");

            migrationBuilder.RenameColumn(
                name: "UOM",
                table: "ReturToQCItemDetails",
                newName: "UOMUnit");

            migrationBuilder.AddColumn<string>(
                name: "ProductionOrderCode",
                table: "ReturToQCItems",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductionOrderCode",
                table: "ReturToQCItems");

            migrationBuilder.RenameColumn(
                name: "ProductionOrderNo",
                table: "ReturToQCItems",
                newName: "ProductionOrderName");

            migrationBuilder.RenameColumn(
                name: "UOMUnit",
                table: "ReturToQCItemDetails",
                newName: "UOM");
        }
    }
}
