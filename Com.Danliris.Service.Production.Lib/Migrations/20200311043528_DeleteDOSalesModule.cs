using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Migrations
{
    public partial class DeleteDOSalesModule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DOSalesItemDetails");

            migrationBuilder.DropTable(
                name: "DOSalesItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DOSalesItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Accepted = table.Column<bool>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    AutoIncreament = table.Column<long>(nullable: false),
                    BuyerAddress = table.Column<string>(maxLength: 1000, nullable: true),
                    BuyerCode = table.Column<string>(maxLength: 255, nullable: true),
                    BuyerId = table.Column<int>(nullable: false),
                    BuyerNPWP = table.Column<string>(maxLength: 255, nullable: true),
                    BuyerName = table.Column<string>(maxLength: 255, nullable: true),
                    BuyerType = table.Column<string>(maxLength: 255, nullable: true),
                    Code = table.Column<string>(maxLength: 255, nullable: true),
                    Construction = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    DOSalesDate = table.Column<DateTimeOffset>(nullable: false),
                    DOSalesNo = table.Column<string>(maxLength: 255, nullable: true),
                    DOSalesType = table.Column<string>(maxLength: 255, nullable: true),
                    Declined = table.Column<bool>(nullable: false),
                    DeletedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 255, nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    DestinationBuyerAddress = table.Column<string>(maxLength: 1000, nullable: true),
                    DestinationBuyerCode = table.Column<string>(maxLength: 255, nullable: true),
                    DestinationBuyerId = table.Column<int>(nullable: false),
                    DestinationBuyerNPWP = table.Column<string>(maxLength: 255, nullable: true),
                    DestinationBuyerName = table.Column<string>(maxLength: 255, nullable: true),
                    DestinationBuyerType = table.Column<string>(maxLength: 255, nullable: true),
                    Disp = table.Column<int>(nullable: false),
                    HeadOfStorage = table.Column<string>(maxLength: 255, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModifiedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 255, nullable: false),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    LengthUom = table.Column<string>(maxLength: 255, nullable: true),
                    Material = table.Column<string>(maxLength: 255, nullable: true),
                    MaterialConstructionFinishId = table.Column<int>(nullable: false),
                    MaterialConstructionFinishName = table.Column<string>(maxLength: 255, nullable: true),
                    MaterialId = table.Column<int>(nullable: false),
                    MaterialWidthFinish = table.Column<string>(maxLength: 255, nullable: true),
                    Op = table.Column<int>(nullable: false),
                    PackingUom = table.Column<string>(maxLength: 255, nullable: true),
                    ProductionOrderId = table.Column<int>(nullable: false),
                    ProductionOrderNo = table.Column<string>(maxLength: 255, nullable: true),
                    Remark = table.Column<string>(maxLength: 1000, nullable: true),
                    Sc = table.Column<int>(nullable: false),
                    Status = table.Column<string>(maxLength: 255, nullable: true),
                    StorageDivision = table.Column<string>(maxLength: 255, nullable: true),
                    StorageId = table.Column<int>(nullable: false),
                    StorageName = table.Column<string>(maxLength: 255, nullable: true),
                    UId = table.Column<string>(maxLength: 255, nullable: true)
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
                    CreatedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
                    CreatedUtc = table.Column<DateTime>(nullable: false),
                    DOSalesId = table.Column<int>(nullable: false),
                    DeletedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    DeletedBy = table.Column<string>(maxLength: 255, nullable: false),
                    DeletedUtc = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LastModifiedAgent = table.Column<string>(maxLength: 255, nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 255, nullable: false),
                    LastModifiedUtc = table.Column<DateTime>(nullable: false),
                    TotalLength = table.Column<double>(nullable: false),
                    TotalLengthConversion = table.Column<double>(nullable: false),
                    TotalPacking = table.Column<double>(nullable: false),
                    UnitCode = table.Column<string>(maxLength: 255, nullable: true),
                    UnitName = table.Column<string>(maxLength: 255, nullable: true),
                    UnitRemark = table.Column<string>(maxLength: 255, nullable: true)
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
    }
}
