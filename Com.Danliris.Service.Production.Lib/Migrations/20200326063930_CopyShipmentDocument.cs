using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Migrations
{
    public partial class CopyShipmentDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NewShipmentDocumentModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
                    CreatedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 255, nullable: false),
                    LastModifiedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 255, nullable: false),
                    DeletedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    UId = table.Column<string>(maxLength: 255, nullable: true),
                    BuyerId = table.Column<int>(nullable: false),
                    BuyerAddress = table.Column<string>(nullable: true),
                    BuyerCity = table.Column<string>(maxLength: 250, nullable: true),
                    BuyerCode = table.Column<string>(maxLength: 125, nullable: true),
                    BuyerContact = table.Column<string>(maxLength: 250, nullable: true),
                    BuyerCountry = table.Column<string>(maxLength: 250, nullable: true),
                    BuyerName = table.Column<string>(nullable: true),
                    BuyerNPWP = table.Column<string>(maxLength: 250, nullable: true),
                    BuyerTempo = table.Column<string>(maxLength: 250, nullable: true),
                    BuyerType = table.Column<string>(maxLength: 250, nullable: true),
                    Code = table.Column<string>(maxLength: 250, nullable: true),
                    DeliveryCode = table.Column<string>(maxLength: 250, nullable: true),
                    DeliveryDate = table.Column<DateTimeOffset>(nullable: false),
                    DeliveryReference = table.Column<string>(maxLength: 250, nullable: true),
                    IsVoid = table.Column<bool>(nullable: false),
                    ProductIdentity = table.Column<string>(maxLength: 250, nullable: true),
                    ShipmentNumber = table.Column<string>(maxLength: 250, nullable: true),
                    StorageId = table.Column<int>(nullable: false),
                    StorageCode = table.Column<string>(maxLength: 250, nullable: true),
                    StorageName = table.Column<string>(maxLength: 250, nullable: true),
                    StorageDescription = table.Column<string>(maxLength: 1000, nullable: true),
                    StorageUnitCode = table.Column<string>(maxLength: 250, nullable: true),
                    StorageUnitName = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewShipmentDocumentModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewShipmentDocumentDetailModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
                    CreatedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 255, nullable: false),
                    LastModifiedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 255, nullable: false),
                    DeletedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    ProductionOrderColorType = table.Column<string>(maxLength: 250, nullable: true),
                    ProductionOrderDesignCode = table.Column<string>(maxLength: 250, nullable: true),
                    ProductionOrderDesignNumber = table.Column<string>(maxLength: 250, nullable: true),
                    ProductionOrderId = table.Column<int>(nullable: false),
                    ProductionOrderNo = table.Column<string>(maxLength: 250, nullable: true),
                    ProductionOrderType = table.Column<string>(maxLength: 250, nullable: true),
                    ShipmentDocumentId = table.Column<int>(nullable: false),
                    DetailIndex = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewShipmentDocumentDetailModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewShipmentDocumentDetailModel_NewShipmentDocumentModel_ShipmentDocumentId",
                        column: x => x.ShipmentDocumentId,
                        principalTable: "NewShipmentDocumentModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewShipmentDocumentItemModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
                    CreatedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 255, nullable: false),
                    LastModifiedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 255, nullable: false),
                    DeletedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    PackingReceiptCode = table.Column<string>(maxLength: 250, nullable: true),
                    PackingReceiptId = table.Column<int>(nullable: false),
                    ReferenceNo = table.Column<string>(maxLength: 250, nullable: true),
                    ReferenceType = table.Column<string>(maxLength: 250, nullable: true),
                    ShipmentDocumentDetailId = table.Column<int>(nullable: false),
                    ItemIndex = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewShipmentDocumentItemModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewShipmentDocumentItemModel_NewShipmentDocumentDetailModel_ShipmentDocumentDetailId",
                        column: x => x.ShipmentDocumentDetailId,
                        principalTable: "NewShipmentDocumentDetailModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewShipmentDocumentPackingReceiptItemModel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
                    CreatedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 255, nullable: false),
                    LastModifiedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 255, nullable: false),
                    DeletedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    ColorType = table.Column<string>(maxLength: 250, nullable: true),
                    DesignCode = table.Column<string>(maxLength: 250, nullable: true),
                    DesignNumber = table.Column<string>(maxLength: 250, nullable: true),
                    Length = table.Column<double>(nullable: false),
                    ProductCode = table.Column<string>(maxLength: 250, nullable: true),
                    ProductId = table.Column<int>(nullable: false),
                    ProductName = table.Column<string>(maxLength: 500, nullable: true),
                    Quantity = table.Column<double>(nullable: false),
                    Remark = table.Column<string>(maxLength: 1000, nullable: true),
                    UOMId = table.Column<int>(nullable: false),
                    UOMUnit = table.Column<string>(maxLength: 250, nullable: true),
                    Weight = table.Column<double>(nullable: false),
                    ShipmentDocumentItemId = table.Column<int>(nullable: false),
                    PackingReceiptItemIndex = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewShipmentDocumentPackingReceiptItemModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewShipmentDocumentPackingReceiptItemModel_NewShipmentDocumentItemModel_ShipmentDocumentItemId",
                        column: x => x.ShipmentDocumentItemId,
                        principalTable: "NewShipmentDocumentItemModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewShipmentDocumentDetailModel_ShipmentDocumentId",
                table: "NewShipmentDocumentDetailModel",
                column: "ShipmentDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_NewShipmentDocumentItemModel_ShipmentDocumentDetailId",
                table: "NewShipmentDocumentItemModel",
                column: "ShipmentDocumentDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_NewShipmentDocumentPackingReceiptItemModel_ShipmentDocumentItemId",
                table: "NewShipmentDocumentPackingReceiptItemModel",
                column: "ShipmentDocumentItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NewShipmentDocumentPackingReceiptItemModel");

            migrationBuilder.DropTable(
                name: "NewShipmentDocumentItemModel");

            migrationBuilder.DropTable(
                name: "NewShipmentDocumentDetailModel");

            migrationBuilder.DropTable(
                name: "NewShipmentDocumentModel");
        }
    }
}
