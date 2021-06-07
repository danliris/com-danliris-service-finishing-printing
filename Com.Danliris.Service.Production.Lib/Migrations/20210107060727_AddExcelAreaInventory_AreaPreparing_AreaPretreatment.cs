using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Migrations
{
    public partial class AddExcelAreaInventory_AreaPreparing_AreaPretreatment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Excel_AreaInventoryGreigeMovements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActivityIN = table.Column<string>(nullable: true),
                    DateIN = table.Column<DateTime>(nullable: true),
                    ProcessType = table.Column<string>(nullable: true),
                    ReceiptNo = table.Column<string>(nullable: true),
                    Construction = table.Column<string>(nullable: true),
                    Grade = table.Column<string>(nullable: true),
                    QtyPiece = table.Column<double>(nullable: true),
                    PieceNumber = table.Column<double>(nullable: true),
                    QtyMtr = table.Column<double>(nullable: true),
                    QtyYard = table.Column<double>(nullable: true),
                    ConvMtr = table.Column<double>(nullable: true),
                    ConvYard = table.Column<double>(nullable: true),
                    QtyTotalMtr = table.Column<double>(nullable: true),
                    QtyTotalYard = table.Column<double>(nullable: true),
                    Suplier = table.Column<string>(nullable: true),
                    FONo = table.Column<string>(nullable: true),
                    SCNo = table.Column<string>(nullable: true),
                    Operator = table.Column<string>(nullable: true),
                    Grade2 = table.Column<string>(nullable: true),
                    QtyMtr2 = table.Column<double>(nullable: true),
                    Diff = table.Column<double>(nullable: true),
                    ActivityOUT = table.Column<string>(nullable: true),
                    DateOUT = table.Column<DateTime>(nullable: true),
                    ReceiptNoteOut = table.Column<string>(nullable: true),
                    QtyTotal = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Excel_AreaInventoryGreigeMovements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Excel_AreaInventoryGreiges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: true),
                    OrderNo = table.Column<string>(nullable: true),
                    Material = table.Column<string>(nullable: true),
                    QtyOrder = table.Column<double>(nullable: true),
                    QtyCart = table.Column<double>(nullable: true),
                    Shift = table.Column<string>(nullable: true),
                    CartSeries = table.Column<string>(nullable: true),
                    Qty1 = table.Column<double>(nullable: true),
                    Grade1 = table.Column<string>(nullable: true),
                    Qty2 = table.Column<double>(nullable: true),
                    Grade2 = table.Column<string>(nullable: true),
                    Qty3 = table.Column<double>(nullable: true),
                    Grade3 = table.Column<string>(nullable: true),
                    QtyPlan = table.Column<double>(nullable: true),
                    QtyRealization = table.Column<double>(nullable: true),
                    ReturDate = table.Column<DateTime>(nullable: true),
                    QtyRetur = table.Column<double>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    QtyperCart = table.Column<double>(nullable: true),
                    CartNo = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Excel_AreaInventoryGreiges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Excel_AreaPreparings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Activity = table.Column<string>(nullable: true),
                    OrderType = table.Column<string>(nullable: true),
                    DateIN = table.Column<DateTime>(nullable: true),
                    Shift = table.Column<string>(nullable: true),
                    Group = table.Column<string>(nullable: true),
                    OrderNo = table.Column<string>(nullable: true),
                    Construction = table.Column<string>(nullable: true),
                    Motif = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    CartNo = table.Column<string>(nullable: true),
                    PieceNumber = table.Column<string>(nullable: true),
                    Grade = table.Column<string>(nullable: true),
                    QtyMtr = table.Column<double>(nullable: true),
                    DateSeal = table.Column<DateTime>(nullable: true),
                    DateSewing = table.Column<DateTime>(nullable: true),
                    DateRoll = table.Column<DateTime>(nullable: true),
                    TimeRoll = table.Column<TimeSpan>(nullable: true),
                    ActivityOut = table.Column<string>(nullable: true),
                    DateOut = table.Column<DateTime>(nullable: true),
                    ReceiptIN = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Excel_AreaPreparings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Excel_AreaPretreatmentChemicalUseds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Shift = table.Column<string>(nullable: true),
                    Group = table.Column<string>(nullable: true),
                    MachineName = table.Column<string>(nullable: true),
                    OrderNo = table.Column<string>(nullable: true),
                    Material = table.Column<string>(nullable: true),
                    ChemicalPrimalase = table.Column<double>(nullable: true),
                    ChemicalStabironAT2 = table.Column<double>(nullable: true),
                    ChemicalSizolTX = table.Column<double>(nullable: true),
                    ChemicalNaoh = table.Column<double>(nullable: true),
                    ChemicalNeoratePH = table.Column<double>(nullable: true),
                    ChemicalH2o2 = table.Column<double>(nullable: true),
                    ChemicalDsm60 = table.Column<double>(nullable: true),
                    ChemicalDjetpt = table.Column<double>(nullable: true),
                    ChemicalGamasol = table.Column<double>(nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Excel_AreaPretreatmentChemicalUseds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Excel_AreaPretreatmentConditionChecks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    Machine = table.Column<string>(nullable: true),
                    Time = table.Column<TimeSpan>(nullable: false),
                    OrderNo = table.Column<string>(nullable: true),
                    CartNo = table.Column<string>(nullable: true),
                    PressSat1 = table.Column<double>(nullable: false),
                    PressSat2 = table.Column<double>(nullable: false),
                    SpeedLB1 = table.Column<double>(nullable: false),
                    SpeedLB2 = table.Column<double>(nullable: false),
                    WasherTemp1 = table.Column<double>(nullable: false),
                    WasherTemp2 = table.Column<double>(nullable: false),
                    WasherTemp3 = table.Column<double>(nullable: false),
                    WasherTemp4 = table.Column<double>(nullable: false),
                    WasherTemp5 = table.Column<double>(nullable: false),
                    WasherTemp6 = table.Column<double>(nullable: false),
                    PressFM1 = table.Column<double>(nullable: false),
                    PressFM2 = table.Column<double>(nullable: false),
                    ChamberTempWater1 = table.Column<double>(nullable: false),
                    ChamberTempWater2 = table.Column<double>(nullable: false),
                    ChamberTempSteam1 = table.Column<double>(nullable: false),
                    ChamberTempSteam2 = table.Column<double>(nullable: false),
                    ChamberTiming1 = table.Column<double>(nullable: false),
                    ChamberTiming2 = table.Column<double>(nullable: false),
                    PolyStreamTemp1 = table.Column<double>(nullable: false),
                    PolyStreamTemp2 = table.Column<double>(nullable: false),
                    PolyStreamTemp3 = table.Column<double>(nullable: false),
                    PolyStreamTemp4 = table.Column<double>(nullable: false),
                    TransferPump1 = table.Column<double>(nullable: false),
                    TransferPump2 = table.Column<double>(nullable: false),
                    Operator = table.Column<string>(nullable: true),
                    Kasubsie = table.Column<string>(nullable: true),
                    Kasie = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Excel_AreaPretreatmentConditionChecks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Excel_AreaPretreatmentDiaryMachines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: true),
                    Shift = table.Column<string>(nullable: true),
                    Group = table.Column<string>(nullable: true),
                    MachineName = table.Column<string>(nullable: true),
                    OrderNo = table.Column<string>(nullable: true),
                    Material = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    QtyIn = table.Column<double>(nullable: true),
                    QtyOutBQ = table.Column<double>(nullable: true),
                    QtyOutBS = table.Column<double>(nullable: true),
                    CartNo = table.Column<string>(nullable: true),
                    WidthGreige = table.Column<double>(nullable: true),
                    ProcessType = table.Column<string>(nullable: true),
                    Speed = table.Column<double>(nullable: true),
                    StartTime = table.Column<TimeSpan>(nullable: true),
                    FinishTime = table.Column<TimeSpan>(nullable: true),
                    MotifCode = table.Column<string>(nullable: true),
                    Screen = table.Column<string>(nullable: true),
                    Ph = table.Column<double>(nullable: true),
                    PresureCD = table.Column<double>(nullable: true),
                    StenterTemp1 = table.Column<double>(nullable: true),
                    StenterTemp2 = table.Column<double>(nullable: true),
                    StenterTemp3 = table.Column<double>(nullable: true),
                    StenterTemp4 = table.Column<double>(nullable: true),
                    StenterTemp5 = table.Column<double>(nullable: true),
                    StenterTemp6 = table.Column<double>(nullable: true),
                    StenterTemp7 = table.Column<double>(nullable: true),
                    StenterTemp8 = table.Column<double>(nullable: true),
                    StenterTemp9 = table.Column<double>(nullable: true),
                    StenterTemp10 = table.Column<double>(nullable: true),
                    WasherTemp1 = table.Column<double>(nullable: true),
                    WasherTemp2 = table.Column<double>(nullable: true),
                    WasherTemp3 = table.Column<double>(nullable: true),
                    WasherTemp4 = table.Column<double>(nullable: true),
                    SaturatorTemp4 = table.Column<double>(nullable: true),
                    BurnerProcess = table.Column<double>(nullable: true),
                    SaturatorPress = table.Column<double>(nullable: true),
                    ResultBB = table.Column<string>(nullable: true),
                    FirePoint = table.Column<string>(nullable: true),
                    LoseMTR = table.Column<double>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Excel_AreaPretreatmentDiaryMachines", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Excel_AreaInventoryGreigeMovements");

            migrationBuilder.DropTable(
                name: "Excel_AreaInventoryGreiges");

            migrationBuilder.DropTable(
                name: "Excel_AreaPreparings");

            migrationBuilder.DropTable(
                name: "Excel_AreaPretreatmentChemicalUseds");

            migrationBuilder.DropTable(
                name: "Excel_AreaPretreatmentConditionChecks");

            migrationBuilder.DropTable(
                name: "Excel_AreaPretreatmentDiaryMachines");
        }
    }
}
