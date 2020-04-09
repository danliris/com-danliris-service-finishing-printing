using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Migrations
{
    public partial class RenameTableNameNewShipmentDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewShipmentDocumentDetailModel_NewShipmentDocumentModel_ShipmentDocumentId",
                table: "NewShipmentDocumentDetailModel");

            migrationBuilder.DropForeignKey(
                name: "FK_NewShipmentDocumentItemModel_NewShipmentDocumentDetailModel_ShipmentDocumentDetailId",
                table: "NewShipmentDocumentItemModel");

            migrationBuilder.DropForeignKey(
                name: "FK_NewShipmentDocumentPackingReceiptItemModel_NewShipmentDocumentItemModel_ShipmentDocumentItemId",
                table: "NewShipmentDocumentPackingReceiptItemModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewShipmentDocumentPackingReceiptItemModel",
                table: "NewShipmentDocumentPackingReceiptItemModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewShipmentDocumentModel",
                table: "NewShipmentDocumentModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewShipmentDocumentItemModel",
                table: "NewShipmentDocumentItemModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewShipmentDocumentDetailModel",
                table: "NewShipmentDocumentDetailModel");

            migrationBuilder.RenameTable(
                name: "NewShipmentDocumentPackingReceiptItemModel",
                newName: "NewShipmentDocumentPackingReceiptItems");

            migrationBuilder.RenameTable(
                name: "NewShipmentDocumentModel",
                newName: "NewShipmentDocuments");

            migrationBuilder.RenameTable(
                name: "NewShipmentDocumentItemModel",
                newName: "NewShipmentDocumentItems");

            migrationBuilder.RenameTable(
                name: "NewShipmentDocumentDetailModel",
                newName: "NewShipmentDocumentDetails");

            migrationBuilder.RenameIndex(
                name: "IX_NewShipmentDocumentPackingReceiptItemModel_ShipmentDocumentItemId",
                table: "NewShipmentDocumentPackingReceiptItems",
                newName: "IX_NewShipmentDocumentPackingReceiptItems_ShipmentDocumentItemId");

            migrationBuilder.RenameIndex(
                name: "IX_NewShipmentDocumentItemModel_ShipmentDocumentDetailId",
                table: "NewShipmentDocumentItems",
                newName: "IX_NewShipmentDocumentItems_ShipmentDocumentDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_NewShipmentDocumentDetailModel_ShipmentDocumentId",
                table: "NewShipmentDocumentDetails",
                newName: "IX_NewShipmentDocumentDetails_ShipmentDocumentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewShipmentDocumentPackingReceiptItems",
                table: "NewShipmentDocumentPackingReceiptItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewShipmentDocuments",
                table: "NewShipmentDocuments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewShipmentDocumentItems",
                table: "NewShipmentDocumentItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewShipmentDocumentDetails",
                table: "NewShipmentDocumentDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NewShipmentDocumentDetails_NewShipmentDocuments_ShipmentDocumentId",
                table: "NewShipmentDocumentDetails",
                column: "ShipmentDocumentId",
                principalTable: "NewShipmentDocuments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewShipmentDocumentItems_NewShipmentDocumentDetails_ShipmentDocumentDetailId",
                table: "NewShipmentDocumentItems",
                column: "ShipmentDocumentDetailId",
                principalTable: "NewShipmentDocumentDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewShipmentDocumentPackingReceiptItems_NewShipmentDocumentItems_ShipmentDocumentItemId",
                table: "NewShipmentDocumentPackingReceiptItems",
                column: "ShipmentDocumentItemId",
                principalTable: "NewShipmentDocumentItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NewShipmentDocumentDetails_NewShipmentDocuments_ShipmentDocumentId",
                table: "NewShipmentDocumentDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_NewShipmentDocumentItems_NewShipmentDocumentDetails_ShipmentDocumentDetailId",
                table: "NewShipmentDocumentItems");

            migrationBuilder.DropForeignKey(
                name: "FK_NewShipmentDocumentPackingReceiptItems_NewShipmentDocumentItems_ShipmentDocumentItemId",
                table: "NewShipmentDocumentPackingReceiptItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewShipmentDocuments",
                table: "NewShipmentDocuments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewShipmentDocumentPackingReceiptItems",
                table: "NewShipmentDocumentPackingReceiptItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewShipmentDocumentItems",
                table: "NewShipmentDocumentItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NewShipmentDocumentDetails",
                table: "NewShipmentDocumentDetails");

            migrationBuilder.RenameTable(
                name: "NewShipmentDocuments",
                newName: "NewShipmentDocumentModel");

            migrationBuilder.RenameTable(
                name: "NewShipmentDocumentPackingReceiptItems",
                newName: "NewShipmentDocumentPackingReceiptItemModel");

            migrationBuilder.RenameTable(
                name: "NewShipmentDocumentItems",
                newName: "NewShipmentDocumentItemModel");

            migrationBuilder.RenameTable(
                name: "NewShipmentDocumentDetails",
                newName: "NewShipmentDocumentDetailModel");

            migrationBuilder.RenameIndex(
                name: "IX_NewShipmentDocumentPackingReceiptItems_ShipmentDocumentItemId",
                table: "NewShipmentDocumentPackingReceiptItemModel",
                newName: "IX_NewShipmentDocumentPackingReceiptItemModel_ShipmentDocumentItemId");

            migrationBuilder.RenameIndex(
                name: "IX_NewShipmentDocumentItems_ShipmentDocumentDetailId",
                table: "NewShipmentDocumentItemModel",
                newName: "IX_NewShipmentDocumentItemModel_ShipmentDocumentDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_NewShipmentDocumentDetails_ShipmentDocumentId",
                table: "NewShipmentDocumentDetailModel",
                newName: "IX_NewShipmentDocumentDetailModel_ShipmentDocumentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewShipmentDocumentModel",
                table: "NewShipmentDocumentModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewShipmentDocumentPackingReceiptItemModel",
                table: "NewShipmentDocumentPackingReceiptItemModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewShipmentDocumentItemModel",
                table: "NewShipmentDocumentItemModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NewShipmentDocumentDetailModel",
                table: "NewShipmentDocumentDetailModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NewShipmentDocumentDetailModel_NewShipmentDocumentModel_ShipmentDocumentId",
                table: "NewShipmentDocumentDetailModel",
                column: "ShipmentDocumentId",
                principalTable: "NewShipmentDocumentModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewShipmentDocumentItemModel_NewShipmentDocumentDetailModel_ShipmentDocumentDetailId",
                table: "NewShipmentDocumentItemModel",
                column: "ShipmentDocumentDetailId",
                principalTable: "NewShipmentDocumentDetailModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NewShipmentDocumentPackingReceiptItemModel_NewShipmentDocumentItemModel_ShipmentDocumentItemId",
                table: "NewShipmentDocumentPackingReceiptItemModel",
                column: "ShipmentDocumentItemId",
                principalTable: "NewShipmentDocumentItemModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
