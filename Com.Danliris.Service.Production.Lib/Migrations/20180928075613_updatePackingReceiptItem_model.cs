using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Production.Lib.Migrations
{
    public partial class updatePackingReceiptItem_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PackingReceiptItem_PackingReceipt_PackingReceiptModelId",
                table: "PackingReceiptItem");

            migrationBuilder.DropIndex(
                name: "IX_PackingReceiptItem_PackingReceiptModelId",
                table: "PackingReceiptItem");

            migrationBuilder.DropColumn(
                name: "PackingReceiptModelId",
                table: "PackingReceiptItem");

            migrationBuilder.AddColumn<int>(
                name: "PackingReceiptId",
                table: "PackingReceiptItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PackingReceiptItem_PackingReceiptId",
                table: "PackingReceiptItem",
                column: "PackingReceiptId");

            migrationBuilder.AddForeignKey(
                name: "FK_PackingReceiptItem_PackingReceipt_PackingReceiptId",
                table: "PackingReceiptItem",
                column: "PackingReceiptId",
                principalTable: "PackingReceipt",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PackingReceiptItem_PackingReceipt_PackingReceiptId",
                table: "PackingReceiptItem");

            migrationBuilder.DropIndex(
                name: "IX_PackingReceiptItem_PackingReceiptId",
                table: "PackingReceiptItem");

            migrationBuilder.DropColumn(
                name: "PackingReceiptId",
                table: "PackingReceiptItem");

            migrationBuilder.AddColumn<int>(
                name: "PackingReceiptModelId",
                table: "PackingReceiptItem",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PackingReceiptItem_PackingReceiptModelId",
                table: "PackingReceiptItem",
                column: "PackingReceiptModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_PackingReceiptItem_PackingReceipt_PackingReceiptModelId",
                table: "PackingReceiptItem",
                column: "PackingReceiptModelId",
                principalTable: "PackingReceipt",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
