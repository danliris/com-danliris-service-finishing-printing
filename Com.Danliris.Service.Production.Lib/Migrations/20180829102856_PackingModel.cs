using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Production.Lib.Migrations
{
    public partial class PackingModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PackingDetails",
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
                    Lot = table.Column<string>(maxLength: 250, nullable: true),
                    Grade = table.Column<string>(maxLength: 100, nullable: true),
                    Weight = table.Column<int>(nullable: false),
                    Length = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackingDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Packings",
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
                    Code = table.Column<string>(maxLength: 25, nullable: true),
                    ProductionOrderId = table.Column<int>(nullable: false),
                    ProductionOrderNo = table.Column<string>(maxLength: 25, nullable: true),
                    OrderType = table.Column<string>(maxLength: 25, nullable: true),
                    SalesContractNo = table.Column<string>(maxLength: 25, nullable: true),
                    DesignCode = table.Column<string>(maxLength: 250, nullable: true),
                    DesignNumber = table.Column<string>(maxLength: 250, nullable: true),
                    BuyerId = table.Column<int>(nullable: false),
                    BuyerCode = table.Column<string>(maxLength: 25, nullable: true),
                    BuyerName = table.Column<string>(maxLength: 250, nullable: true),
                    BuyerAddress = table.Column<string>(maxLength: 250, nullable: true),
                    BuyerType = table.Column<string>(maxLength: 25, nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    PackingUom = table.Column<string>(maxLength: 25, nullable: true),
                    ColorCode = table.Column<string>(maxLength: 250, nullable: true),
                    ColorName = table.Column<string>(maxLength: 250, nullable: true),
                    ColorType = table.Column<string>(maxLength: 250, nullable: true),
                    MaterialConstructionFinishId = table.Column<int>(nullable: false),
                    MaterialConstructionFinishName = table.Column<string>(maxLength: 250, nullable: true),
                    MaterialId = table.Column<int>(nullable: false),
                    Material = table.Column<string>(maxLength: 25, nullable: true),
                    MaterialWidthFinish = table.Column<string>(maxLength: 25, nullable: true),
                    Construction = table.Column<string>(maxLength: 25, nullable: true),
                    DeliveryType = table.Column<string>(maxLength: 25, nullable: true),
                    FinishedProductType = table.Column<string>(maxLength: 25, nullable: true),
                    Motif = table.Column<string>(maxLength: 250, nullable: true),
                    Status = table.Column<string>(maxLength: 25, nullable: true),
                    Accepted = table.Column<bool>(nullable: false),
                    Declined = table.Column<bool>(nullable: false),
                    PackingDetailId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Packings_PackingDetails_PackingDetailId",
                        column: x => x.PackingDetailId,
                        principalTable: "PackingDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Packings_PackingDetailId",
                table: "Packings",
                column: "PackingDetailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Packings");

            migrationBuilder.DropTable(
                name: "PackingDetails");
        }
    }
}
