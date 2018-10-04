using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Production.Lib.Migrations
{
    public partial class AddReturToQCModule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReturToQCs",
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
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    DeliveryOrderNo = table.Column<string>(nullable: true),
                    Destination = table.Column<string>(nullable: true),
                    FinishedGoodCode = table.Column<string>(nullable: true),
                    IsVoid = table.Column<bool>(nullable: false),
                    MaterialId = table.Column<int>(nullable: false),
                    MaterialName = table.Column<string>(maxLength: 250, nullable: true),
                    MaterialCode = table.Column<string>(maxLength: 25, nullable: true),
                    MaterialWidthFinish = table.Column<string>(maxLength: 25, nullable: true),
                    Remark = table.Column<string>(maxLength: 500, nullable: true),
                    ReturNo = table.Column<string>(maxLength: 25, nullable: true),
                    MaterialConstructionId = table.Column<int>(nullable: false),
                    MaterialConstructionName = table.Column<string>(maxLength: 250, nullable: true),
                    MaterialConstructionCode = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturToQCs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReturToQCItems",
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
                    ReturToQCId = table.Column<int>(nullable: false),
                    ProductionOrderId = table.Column<int>(nullable: false),
                    ProductionOrderName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturToQCItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReturToQCItems_ReturToQCs_ReturToQCId",
                        column: x => x.ReturToQCId,
                        principalTable: "ReturToQCs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReturToQCItemDetails",
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
                    ReturToQCItemId = table.Column<int>(nullable: false),
                    ColorWay = table.Column<string>(nullable: true),
                    DesignCode = table.Column<string>(nullable: true),
                    DesignNumber = table.Column<int>(nullable: false),
                    Length = table.Column<double>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    ProductName = table.Column<int>(nullable: false),
                    QuantityBefore = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(maxLength: 500, nullable: true),
                    ReturQuantity = table.Column<int>(nullable: false),
                    StorageId = table.Column<int>(nullable: false),
                    UOM = table.Column<string>(nullable: true),
                    UOMId = table.Column<int>(nullable: false),
                    Weight = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturToQCItemDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReturToQCItemDetails_ReturToQCItems_ReturToQCItemId",
                        column: x => x.ReturToQCItemId,
                        principalTable: "ReturToQCItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReturToQCItemDetails_ReturToQCItemId",
                table: "ReturToQCItemDetails",
                column: "ReturToQCItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturToQCItems_ReturToQCId",
                table: "ReturToQCItems",
                column: "ReturToQCId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReturToQCItemDetails");

            migrationBuilder.DropTable(
                name: "ReturToQCItems");

            migrationBuilder.DropTable(
                name: "ReturToQCs");
        }
    }
}
