using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Production.Lib.Migrations
{
    public partial class updateModel_PackingReceipt_addDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Buyer",
                table: "PackingReceipt",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColorName",
                table: "PackingReceipt",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColorType",
                table: "PackingReceipt",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Construction",
                table: "PackingReceipt",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DesignCode",
                table: "PackingReceipt",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DesignNumber",
                table: "PackingReceipt",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaterialWidthFinish",
                table: "PackingReceipt",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderType",
                table: "PackingReceipt",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PackingUom",
                table: "PackingReceipt",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductionOrderNo",
                table: "PackingReceipt",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferenceNo",
                table: "PackingReceipt",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReferenceType",
                table: "PackingReceipt",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StorageCode",
                table: "PackingReceipt",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StorageDivisionCode",
                table: "PackingReceipt",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StorageDivisionName",
                table: "PackingReceipt",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StorageId",
                table: "PackingReceipt",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "StorageName",
                table: "PackingReceipt",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StorageUnitCode",
                table: "PackingReceipt",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StorageUnitName",
                table: "PackingReceipt",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "PackingReceipt",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PackingReceiptItem",
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
                    Product = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Length = table.Column<int>(nullable: false),
                    Weight = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    UomId = table.Column<int>(nullable: false),
                    IsDelivered = table.Column<bool>(nullable: false),
                    AvailableQuantity = table.Column<int>(nullable: false),
                    PackingReceiptModelId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackingReceiptItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackingReceiptItem_PackingReceipt_PackingReceiptModelId",
                        column: x => x.PackingReceiptModelId,
                        principalTable: "PackingReceipt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PackingReceiptItem_PackingReceiptModelId",
                table: "PackingReceiptItem",
                column: "PackingReceiptModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PackingReceiptItem");

            migrationBuilder.DropColumn(
                name: "Buyer",
                table: "PackingReceipt");

            migrationBuilder.DropColumn(
                name: "ColorName",
                table: "PackingReceipt");

            migrationBuilder.DropColumn(
                name: "ColorType",
                table: "PackingReceipt");

            migrationBuilder.DropColumn(
                name: "Construction",
                table: "PackingReceipt");

            migrationBuilder.DropColumn(
                name: "DesignCode",
                table: "PackingReceipt");

            migrationBuilder.DropColumn(
                name: "DesignNumber",
                table: "PackingReceipt");

            migrationBuilder.DropColumn(
                name: "MaterialWidthFinish",
                table: "PackingReceipt");

            migrationBuilder.DropColumn(
                name: "OrderType",
                table: "PackingReceipt");

            migrationBuilder.DropColumn(
                name: "PackingUom",
                table: "PackingReceipt");

            migrationBuilder.DropColumn(
                name: "ProductionOrderNo",
                table: "PackingReceipt");

            migrationBuilder.DropColumn(
                name: "ReferenceNo",
                table: "PackingReceipt");

            migrationBuilder.DropColumn(
                name: "ReferenceType",
                table: "PackingReceipt");

            migrationBuilder.DropColumn(
                name: "StorageCode",
                table: "PackingReceipt");

            migrationBuilder.DropColumn(
                name: "StorageDivisionCode",
                table: "PackingReceipt");

            migrationBuilder.DropColumn(
                name: "StorageDivisionName",
                table: "PackingReceipt");

            migrationBuilder.DropColumn(
                name: "StorageId",
                table: "PackingReceipt");

            migrationBuilder.DropColumn(
                name: "StorageName",
                table: "PackingReceipt");

            migrationBuilder.DropColumn(
                name: "StorageUnitCode",
                table: "PackingReceipt");

            migrationBuilder.DropColumn(
                name: "StorageUnitName",
                table: "PackingReceipt");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "PackingReceipt");
        }
    }
}
