using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Migrations
{
    public partial class RenameTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dyestuffChemicalUsageReceiptItemDetails_DyestuffChemicalUsageReceiptItems_DyestuffChemicalUsageReceiptItemId",
                table: "dyestuffChemicalUsageReceiptItemDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dyestuffChemicalUsageReceiptItemDetails",
                table: "dyestuffChemicalUsageReceiptItemDetails");

            migrationBuilder.RenameTable(
                name: "dyestuffChemicalUsageReceiptItemDetails",
                newName: "DyestuffChemicalUsageReceiptItemDetails");

            migrationBuilder.RenameIndex(
                name: "IX_dyestuffChemicalUsageReceiptItemDetails_DyestuffChemicalUsageReceiptItemId",
                table: "DyestuffChemicalUsageReceiptItemDetails",
                newName: "IX_DyestuffChemicalUsageReceiptItemDetails_DyestuffChemicalUsageReceiptItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DyestuffChemicalUsageReceiptItemDetails",
                table: "DyestuffChemicalUsageReceiptItemDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DyestuffChemicalUsageReceiptItemDetails_DyestuffChemicalUsageReceiptItems_DyestuffChemicalUsageReceiptItemId",
                table: "DyestuffChemicalUsageReceiptItemDetails",
                column: "DyestuffChemicalUsageReceiptItemId",
                principalTable: "DyestuffChemicalUsageReceiptItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DyestuffChemicalUsageReceiptItemDetails_DyestuffChemicalUsageReceiptItems_DyestuffChemicalUsageReceiptItemId",
                table: "DyestuffChemicalUsageReceiptItemDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DyestuffChemicalUsageReceiptItemDetails",
                table: "DyestuffChemicalUsageReceiptItemDetails");

            migrationBuilder.RenameTable(
                name: "DyestuffChemicalUsageReceiptItemDetails",
                newName: "dyestuffChemicalUsageReceiptItemDetails");

            migrationBuilder.RenameIndex(
                name: "IX_DyestuffChemicalUsageReceiptItemDetails_DyestuffChemicalUsageReceiptItemId",
                table: "dyestuffChemicalUsageReceiptItemDetails",
                newName: "IX_dyestuffChemicalUsageReceiptItemDetails_DyestuffChemicalUsageReceiptItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dyestuffChemicalUsageReceiptItemDetails",
                table: "dyestuffChemicalUsageReceiptItemDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_dyestuffChemicalUsageReceiptItemDetails_DyestuffChemicalUsageReceiptItems_DyestuffChemicalUsageReceiptItemId",
                table: "dyestuffChemicalUsageReceiptItemDetails",
                column: "DyestuffChemicalUsageReceiptItemId",
                principalTable: "DyestuffChemicalUsageReceiptItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
