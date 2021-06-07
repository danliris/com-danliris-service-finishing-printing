using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Migrations
{
    public partial class ChangeProdToAdjsResepPemakaian : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Prod5Date",
                table: "DyestuffChemicalUsageReceiptItems",
                newName: "Adjs5Date");

            migrationBuilder.RenameColumn(
                name: "Prod4Date",
                table: "DyestuffChemicalUsageReceiptItems",
                newName: "Adjs4Date");

            migrationBuilder.RenameColumn(
                name: "Prod3Date",
                table: "DyestuffChemicalUsageReceiptItems",
                newName: "Adjs3Date");

            migrationBuilder.RenameColumn(
                name: "Prod2Date",
                table: "DyestuffChemicalUsageReceiptItems",
                newName: "Adjs2Date");

            migrationBuilder.RenameColumn(
                name: "Prod1Date",
                table: "DyestuffChemicalUsageReceiptItems",
                newName: "Adjs1Date");

            migrationBuilder.RenameColumn(
                name: "Prod5Quantity",
                table: "dyestuffChemicalUsageReceiptItemDetails",
                newName: "Adjs5Quantity");

            migrationBuilder.RenameColumn(
                name: "Prod4Quantity",
                table: "dyestuffChemicalUsageReceiptItemDetails",
                newName: "Adjs4Quantity");

            migrationBuilder.RenameColumn(
                name: "Prod3Quantity",
                table: "dyestuffChemicalUsageReceiptItemDetails",
                newName: "Adjs3Quantity");

            migrationBuilder.RenameColumn(
                name: "Prod2Quantity",
                table: "dyestuffChemicalUsageReceiptItemDetails",
                newName: "Adjs2Quantity");

            migrationBuilder.RenameColumn(
                name: "Prod1Quantity",
                table: "dyestuffChemicalUsageReceiptItemDetails",
                newName: "Adjs1Quantity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Adjs5Date",
                table: "DyestuffChemicalUsageReceiptItems",
                newName: "Prod5Date");

            migrationBuilder.RenameColumn(
                name: "Adjs4Date",
                table: "DyestuffChemicalUsageReceiptItems",
                newName: "Prod4Date");

            migrationBuilder.RenameColumn(
                name: "Adjs3Date",
                table: "DyestuffChemicalUsageReceiptItems",
                newName: "Prod3Date");

            migrationBuilder.RenameColumn(
                name: "Adjs2Date",
                table: "DyestuffChemicalUsageReceiptItems",
                newName: "Prod2Date");

            migrationBuilder.RenameColumn(
                name: "Adjs1Date",
                table: "DyestuffChemicalUsageReceiptItems",
                newName: "Prod1Date");

            migrationBuilder.RenameColumn(
                name: "Adjs5Quantity",
                table: "dyestuffChemicalUsageReceiptItemDetails",
                newName: "Prod5Quantity");

            migrationBuilder.RenameColumn(
                name: "Adjs4Quantity",
                table: "dyestuffChemicalUsageReceiptItemDetails",
                newName: "Prod4Quantity");

            migrationBuilder.RenameColumn(
                name: "Adjs3Quantity",
                table: "dyestuffChemicalUsageReceiptItemDetails",
                newName: "Prod3Quantity");

            migrationBuilder.RenameColumn(
                name: "Adjs2Quantity",
                table: "dyestuffChemicalUsageReceiptItemDetails",
                newName: "Prod2Quantity");

            migrationBuilder.RenameColumn(
                name: "Adjs1Quantity",
                table: "dyestuffChemicalUsageReceiptItemDetails",
                newName: "Prod1Quantity");
        }
    }
}
