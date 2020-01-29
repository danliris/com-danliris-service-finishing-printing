using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Migrations
{
    public partial class ChangedInDOSales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "DOSalesItemDetails");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "DOSalesItemDetails");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "DOSalesItems",
                newName: "DOSalesDate");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "DOSalesItems",
                newName: "MetricUom");

            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "DOSalesItemDetails",
                newName: "PackingQuantity");

            migrationBuilder.RenameColumn(
                name: "Length",
                table: "DOSalesItemDetails",
                newName: "MetricQuantity");

            migrationBuilder.AlterColumn<string>(
                name: "ProductionOrderNo",
                table: "DOSalesItems",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaterialWidthFinish",
                table: "DOSalesItems",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Material",
                table: "DOSalesItems",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuyerNPWP",
                table: "DOSalesItems",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DOSalesNo",
                table: "DOSalesItems",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DestinationBuyerAddress",
                table: "DOSalesItems",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DestinationBuyerCode",
                table: "DOSalesItems",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DestinationBuyerId",
                table: "DOSalesItems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DestinationBuyerNPWP",
                table: "DOSalesItems",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DestinationBuyerName",
                table: "DOSalesItems",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DestinationBuyerType",
                table: "DOSalesItems",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Disp",
                table: "DOSalesItems",
                maxLength: 25,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImperialUom",
                table: "DOSalesItems",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Op",
                table: "DOSalesItems",
                maxLength: 25,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "DOSalesItems",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sc",
                table: "DOSalesItems",
                maxLength: 25,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "ImperialQuantity",
                table: "DOSalesItemDetails",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "UnitCode",
                table: "DOSalesItemDetails",
                maxLength: 250,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyerNPWP",
                table: "DOSalesItems");

            migrationBuilder.DropColumn(
                name: "DOSalesNo",
                table: "DOSalesItems");

            migrationBuilder.DropColumn(
                name: "DestinationBuyerAddress",
                table: "DOSalesItems");

            migrationBuilder.DropColumn(
                name: "DestinationBuyerCode",
                table: "DOSalesItems");

            migrationBuilder.DropColumn(
                name: "DestinationBuyerId",
                table: "DOSalesItems");

            migrationBuilder.DropColumn(
                name: "DestinationBuyerNPWP",
                table: "DOSalesItems");

            migrationBuilder.DropColumn(
                name: "DestinationBuyerName",
                table: "DOSalesItems");

            migrationBuilder.DropColumn(
                name: "DestinationBuyerType",
                table: "DOSalesItems");

            migrationBuilder.DropColumn(
                name: "Disp",
                table: "DOSalesItems");

            migrationBuilder.DropColumn(
                name: "ImperialUom",
                table: "DOSalesItems");

            migrationBuilder.DropColumn(
                name: "Op",
                table: "DOSalesItems");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "DOSalesItems");

            migrationBuilder.DropColumn(
                name: "Sc",
                table: "DOSalesItems");

            migrationBuilder.DropColumn(
                name: "ImperialQuantity",
                table: "DOSalesItemDetails");

            migrationBuilder.DropColumn(
                name: "UnitCode",
                table: "DOSalesItemDetails");

            migrationBuilder.RenameColumn(
                name: "MetricUom",
                table: "DOSalesItems",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "DOSalesDate",
                table: "DOSalesItems",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "PackingQuantity",
                table: "DOSalesItemDetails",
                newName: "Weight");

            migrationBuilder.RenameColumn(
                name: "MetricQuantity",
                table: "DOSalesItemDetails",
                newName: "Length");

            migrationBuilder.AlterColumn<string>(
                name: "ProductionOrderNo",
                table: "DOSalesItems",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaterialWidthFinish",
                table: "DOSalesItems",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Material",
                table: "DOSalesItems",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "DOSalesItemDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "DOSalesItemDetails",
                maxLength: 500,
                nullable: true);
        }
    }
}
