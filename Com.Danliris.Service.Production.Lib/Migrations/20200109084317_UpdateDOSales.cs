using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Migrations
{
    public partial class UpdateDOSales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MetricUom",
                table: "DOSalesItems",
                newName: "StorageDivision");

            migrationBuilder.RenameColumn(
                name: "ImperialUom",
                table: "DOSalesItems",
                newName: "LengthUom");

            migrationBuilder.RenameColumn(
                name: "PackingQuantity",
                table: "DOSalesItemDetails",
                newName: "TotalPacking");

            migrationBuilder.RenameColumn(
                name: "MetricQuantity",
                table: "DOSalesItemDetails",
                newName: "TotalLengthConversion");

            migrationBuilder.RenameColumn(
                name: "ImperialQuantity",
                table: "DOSalesItemDetails",
                newName: "TotalLength");

            migrationBuilder.AddColumn<string>(
                name: "UnitRemark",
                table: "DOSalesItemDetails",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitRemark",
                table: "DOSalesItemDetails");

            migrationBuilder.RenameColumn(
                name: "StorageDivision",
                table: "DOSalesItems",
                newName: "MetricUom");

            migrationBuilder.RenameColumn(
                name: "LengthUom",
                table: "DOSalesItems",
                newName: "ImperialUom");

            migrationBuilder.RenameColumn(
                name: "TotalPacking",
                table: "DOSalesItemDetails",
                newName: "PackingQuantity");

            migrationBuilder.RenameColumn(
                name: "TotalLengthConversion",
                table: "DOSalesItemDetails",
                newName: "MetricQuantity");

            migrationBuilder.RenameColumn(
                name: "TotalLength",
                table: "DOSalesItemDetails",
                newName: "ImperialQuantity");
        }
    }
}
