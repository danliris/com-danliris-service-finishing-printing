using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Migrations
{
    public partial class DOSales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DOSalesItems",
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
                    Code = table.Column<string>(maxLength: 25, nullable: true),
                    ProductionOrderId = table.Column<int>(nullable: false),
                    ProductionOrderNo = table.Column<string>(maxLength: 25, nullable: true),
                    BuyerId = table.Column<int>(nullable: false),
                    BuyerCode = table.Column<string>(maxLength: 25, nullable: true),
                    BuyerName = table.Column<string>(maxLength: 250, nullable: true),
                    BuyerAddress = table.Column<string>(maxLength: 250, nullable: true),
                    BuyerType = table.Column<string>(maxLength: 25, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    PackingUom = table.Column<string>(maxLength: 25, nullable: true),
                    MaterialConstructionFinishId = table.Column<int>(nullable: false),
                    MaterialConstructionFinishName = table.Column<string>(maxLength: 250, nullable: true),
                    MaterialId = table.Column<int>(nullable: false),
                    Material = table.Column<string>(maxLength: 255, nullable: true),
                    MaterialWidthFinish = table.Column<string>(maxLength: 25, nullable: true),
                    Construction = table.Column<string>(maxLength: 300, nullable: true),
                    DeliveryType = table.Column<string>(maxLength: 25, nullable: true),
                    FinishedProductType = table.Column<string>(maxLength: 25, nullable: true),
                    Status = table.Column<string>(maxLength: 25, nullable: true),
                    Accepted = table.Column<bool>(nullable: false),
                    Declined = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOSalesItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DOSalesItemDetails",
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
                    UnitName = table.Column<string>(maxLength: 250, nullable: true),
                    UnitCode = table.Column<string>(maxLength: 100, nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    Weight = table.Column<double>(nullable: false),
                    Length = table.Column<double>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    DOSalesId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DOSalesItemDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DOSalesItemDetails_DOSalesItems_DOSalesId",
                        column: x => x.DOSalesId,
                        principalTable: "DOSalesItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DOSalesItemDetails_DOSalesId",
                table: "DOSalesItemDetails",
                column: "DOSalesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DOSalesItemDetails");

            migrationBuilder.DropTable(
                name: "DOSalesItems");
        }
    }
}
