using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Migrations
{
    public partial class RemoveAdjs5DyestuffReceipt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adjs5Date",
                table: "DyestuffChemicalUsageReceiptItems");

            migrationBuilder.DropColumn(
                name: "Adjs5Quantity",
                table: "dyestuffChemicalUsageReceiptItemDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Adjs5Date",
                table: "DyestuffChemicalUsageReceiptItems",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Adjs5Quantity",
                table: "dyestuffChemicalUsageReceiptItemDetails",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
