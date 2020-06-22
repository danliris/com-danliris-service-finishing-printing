using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Migrations
{
    public partial class AddDyeStuffChemicalUsageReceipt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DyestuffChemicalUsageReceipts",
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
                    ProductionOrderId = table.Column<long>(nullable: false),
                    ProductionOrderOrderNo = table.Column<string>(maxLength: 256, nullable: true),
                    ProductionOrderOrderQuantity = table.Column<double>(nullable: false),
                    ProductionOrderMaterialId = table.Column<long>(nullable: false),
                    ProductionOrderMaterialName = table.Column<string>(maxLength: 1024, nullable: true),
                    ProductionOrderMaterialConstructionId = table.Column<long>(nullable: false),
                    ProductionOrderMaterialConstructionName = table.Column<string>(maxLength: 1024, nullable: true),
                    ProductionOrderMaterialWidth = table.Column<string>(maxLength: 1024, nullable: true),
                    StrikeOffId = table.Column<int>(nullable: false),
                    StrikeOffCode = table.Column<string>(maxLength: 128, nullable: true),
                    StrikeOffType = table.Column<string>(maxLength: 256, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DyestuffChemicalUsageReceipts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DyestuffChemicalUsageReceiptItems",
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
                    ColorCode = table.Column<string>(maxLength: 2048, nullable: true),
                    DyestuffChemicalUsageReceiptId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DyestuffChemicalUsageReceiptItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DyestuffChemicalUsageReceiptItems_DyestuffChemicalUsageReceipts_DyestuffChemicalUsageReceiptId",
                        column: x => x.DyestuffChemicalUsageReceiptId,
                        principalTable: "DyestuffChemicalUsageReceipts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dyestuffChemicalUsageReceiptItemDetails",
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
                    Index = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 2048, nullable: true),
                    ReceiptQuantity = table.Column<double>(nullable: false),
                    Prod1Date = table.Column<DateTimeOffset>(nullable: false),
                    Prod1Quantity = table.Column<double>(nullable: false),
                    Prod2Date = table.Column<DateTimeOffset>(nullable: false),
                    Prod2Quantity = table.Column<double>(nullable: false),
                    Prod3Date = table.Column<DateTimeOffset>(nullable: false),
                    Prod3Quantity = table.Column<double>(nullable: false),
                    Prod4Date = table.Column<DateTimeOffset>(nullable: false),
                    Prod4Quantity = table.Column<double>(nullable: false),
                    Prod5Date = table.Column<DateTimeOffset>(nullable: false),
                    Prod5Quantity = table.Column<double>(nullable: false),
                    DyestuffChemicalUsageReceiptItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dyestuffChemicalUsageReceiptItemDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dyestuffChemicalUsageReceiptItemDetails_DyestuffChemicalUsageReceiptItems_DyestuffChemicalUsageReceiptItemId",
                        column: x => x.DyestuffChemicalUsageReceiptItemId,
                        principalTable: "DyestuffChemicalUsageReceiptItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_dyestuffChemicalUsageReceiptItemDetails_DyestuffChemicalUsageReceiptItemId",
                table: "dyestuffChemicalUsageReceiptItemDetails",
                column: "DyestuffChemicalUsageReceiptItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DyestuffChemicalUsageReceiptItems_DyestuffChemicalUsageReceiptId",
                table: "DyestuffChemicalUsageReceiptItems",
                column: "DyestuffChemicalUsageReceiptId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dyestuffChemicalUsageReceiptItemDetails");

            migrationBuilder.DropTable(
                name: "DyestuffChemicalUsageReceiptItems");

            migrationBuilder.DropTable(
                name: "DyestuffChemicalUsageReceipts");
        }
    }
}
