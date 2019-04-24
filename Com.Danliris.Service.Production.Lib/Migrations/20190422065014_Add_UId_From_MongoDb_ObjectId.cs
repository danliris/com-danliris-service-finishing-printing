using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Production.Lib.Migrations
{
    public partial class Add_UId_From_MongoDb_ObjectId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UId",
                table: "Steps",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                table: "ReturToQCs",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                table: "Packings",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                table: "PackingReceipt",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                table: "MonitoringSpecificationMachine",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                table: "MonitoringEvent",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                table: "MachineType",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                table: "Machine",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                table: "Kanbans",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                table: "Instructions",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                table: "FabricQualityControls",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                table: "DurationEstimations",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                table: "DailyOperation",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UId",
                table: "BadOutput",
                maxLength: 255,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ShipmentDocuments",
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
                    table.PrimaryKey("PK_ShipmentDocuments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShipmentDocumentDetails",
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
                    ShipmentDocumentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentDocumentDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipmentDocumentDetails_ShipmentDocuments_ShipmentDocumentId",
                        column: x => x.ShipmentDocumentId,
                        principalTable: "ShipmentDocuments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShipmentDocumentItems",
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
                    ShipmentDocumentDetailId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentDocumentItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipmentDocumentItems_ShipmentDocumentDetails_ShipmentDocumentDetailId",
                        column: x => x.ShipmentDocumentDetailId,
                        principalTable: "ShipmentDocumentDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShipmentDocumentPackingReceiptItems",
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
                    ShipmentDocumentItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShipmentDocumentPackingReceiptItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShipmentDocumentPackingReceiptItems_ShipmentDocumentItems_ShipmentDocumentItemId",
                        column: x => x.ShipmentDocumentItemId,
                        principalTable: "ShipmentDocumentItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentDocumentDetails_ShipmentDocumentId",
                table: "ShipmentDocumentDetails",
                column: "ShipmentDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentDocumentItems_ShipmentDocumentDetailId",
                table: "ShipmentDocumentItems",
                column: "ShipmentDocumentDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ShipmentDocumentPackingReceiptItems_ShipmentDocumentItemId",
                table: "ShipmentDocumentPackingReceiptItems",
                column: "ShipmentDocumentItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShipmentDocumentPackingReceiptItems");

            migrationBuilder.DropTable(
                name: "ShipmentDocumentItems");

            migrationBuilder.DropTable(
                name: "ShipmentDocumentDetails");

            migrationBuilder.DropTable(
                name: "ShipmentDocuments");

            migrationBuilder.DropColumn(
                name: "UId",
                table: "Steps");

            migrationBuilder.DropColumn(
                name: "UId",
                table: "ReturToQCs");

            migrationBuilder.DropColumn(
                name: "UId",
                table: "Packings");

            migrationBuilder.DropColumn(
                name: "UId",
                table: "PackingReceipt");

            migrationBuilder.DropColumn(
                name: "UId",
                table: "MonitoringSpecificationMachine");

            migrationBuilder.DropColumn(
                name: "UId",
                table: "MonitoringEvent");

            migrationBuilder.DropColumn(
                name: "UId",
                table: "MachineType");

            migrationBuilder.DropColumn(
                name: "UId",
                table: "Machine");

            migrationBuilder.DropColumn(
                name: "UId",
                table: "Kanbans");

            migrationBuilder.DropColumn(
                name: "UId",
                table: "Instructions");

            migrationBuilder.DropColumn(
                name: "UId",
                table: "FabricQualityControls");

            migrationBuilder.DropColumn(
                name: "UId",
                table: "DurationEstimations");

            migrationBuilder.DropColumn(
                name: "UId",
                table: "DailyOperation");

            migrationBuilder.DropColumn(
                name: "UId",
                table: "BadOutput");
        }
    }
}
