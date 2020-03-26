using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Migrations
{
    public partial class AddKanbanSnapshotModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DOSalesItemDetails");

            migrationBuilder.DropTable(
                name: "DOSalesItems");

            migrationBuilder.CreateTable(
                name: "KanbanSnapshotModels",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KanbanId = table.Column<int>(nullable: false),
                    Buyer = table.Column<string>(maxLength: 1024, nullable: true),
                    SPPNo = table.Column<string>(maxLength: 512, nullable: true),
                    Konstruksi = table.Column<string>(maxLength: 2048, nullable: true),
                    Qty = table.Column<double>(nullable: false),
                    PreTreatmentStepIndex = table.Column<int>(nullable: false),
                    PreTreatmentKonstruksi = table.Column<string>(maxLength: 2048, nullable: true),
                    PreTreatmentInputQty = table.Column<double>(nullable: true),
                    PreTreatmentGoodOutputQty = table.Column<double>(nullable: true),
                    PreTreatmentBadOutputQty = table.Column<double>(nullable: true),
                    PreTreatmentDay = table.Column<int>(nullable: false),
                    DyeingStepIndex = table.Column<int>(nullable: false),
                    DyeingKonstruksi = table.Column<string>(maxLength: 2048, nullable: true),
                    DyeingInputQty = table.Column<double>(nullable: true),
                    DyeingGoodOutputQty = table.Column<double>(nullable: true),
                    DyeingBadOutputQty = table.Column<double>(nullable: true),
                    DyeingDay = table.Column<int>(nullable: false),
                    PrintingStepIndex = table.Column<int>(nullable: false),
                    PrintingKonstruksi = table.Column<string>(maxLength: 2048, nullable: true),
                    PrintingInputQty = table.Column<double>(nullable: true),
                    PrintingGoodOutputQty = table.Column<double>(nullable: true),
                    PrintingBadOutputQty = table.Column<double>(nullable: true),
                    PrintingDay = table.Column<int>(nullable: false),
                    FinishingStepIndex = table.Column<int>(nullable: false),
                    FinishingKonstruksi = table.Column<string>(maxLength: 2048, nullable: true),
                    FinishingInputQty = table.Column<double>(nullable: true),
                    FinishingGoodOutputQty = table.Column<double>(nullable: true),
                    FinishingBadOutputQty = table.Column<double>(nullable: true),
                    FinishingDay = table.Column<int>(nullable: false),
                    QCStepIndex = table.Column<int>(nullable: false),
                    QCKonstruksi = table.Column<string>(maxLength: 2048, nullable: true),
                    QCInputQty = table.Column<double>(nullable: true),
                    QCGoodOutputQty = table.Column<double>(nullable: true),
                    QCBadOutputQty = table.Column<double>(nullable: true),
                    QCDay = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KanbanSnapshotModels", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KanbanSnapshotModels");

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
