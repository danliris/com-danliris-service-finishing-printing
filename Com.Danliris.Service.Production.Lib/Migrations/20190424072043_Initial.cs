using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BadOutput",
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
                    Code = table.Column<string>(nullable: true),
                    Reason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BadOutput", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DurationEstimations",
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
                    Code = table.Column<string>(maxLength: 100, nullable: true),
                    OrderTypeId = table.Column<int>(nullable: false),
                    OrderTypeCode = table.Column<string>(maxLength: 100, nullable: true),
                    OrderTypeName = table.Column<string>(maxLength: 500, nullable: true),
                    ProcessTypeId = table.Column<int>(nullable: false),
                    ProcessTypeCode = table.Column<string>(maxLength: 100, nullable: true),
                    ProcessTypeName = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DurationEstimations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FabricQualityControls",
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
                    Buyer = table.Column<string>(maxLength: 250, nullable: true),
                    CartNo = table.Column<string>(maxLength: 250, nullable: true),
                    Code = table.Column<string>(maxLength: 25, nullable: true),
                    Color = table.Column<string>(maxLength: 250, nullable: true),
                    Construction = table.Column<string>(maxLength: 250, nullable: true),
                    DateIm = table.Column<DateTimeOffset>(nullable: false),
                    Group = table.Column<string>(maxLength: 250, nullable: true),
                    IsUsed = table.Column<bool>(nullable: false),
                    KanbanCode = table.Column<string>(maxLength: 25, nullable: true),
                    KanbanId = table.Column<int>(nullable: false),
                    MachineNoIm = table.Column<string>(maxLength: 250, nullable: true),
                    OperatorIm = table.Column<string>(maxLength: 250, nullable: true),
                    OrderQuantity = table.Column<double>(nullable: false),
                    PackingInstruction = table.Column<string>(maxLength: 500, nullable: true),
                    PointLimit = table.Column<double>(nullable: false),
                    PointSystem = table.Column<double>(nullable: false),
                    ProductionOrderNo = table.Column<string>(maxLength: 250, nullable: true),
                    ProductionOrderType = table.Column<string>(maxLength: 250, nullable: true),
                    ShiftIm = table.Column<string>(maxLength: 250, nullable: true),
                    Uom = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FabricQualityControls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Instructions",
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
                    Code = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kanbans",
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
                    BadOutput = table.Column<double>(nullable: false),
                    CartCartNumber = table.Column<string>(maxLength: 100, nullable: true),
                    CartCode = table.Column<string>(maxLength: 100, nullable: true),
                    CartQty = table.Column<double>(nullable: false),
                    CartPcs = table.Column<double>(nullable: false),
                    CartUomUnit = table.Column<string>(maxLength: 100, nullable: true),
                    Code = table.Column<string>(maxLength: 100, nullable: true),
                    CurrentQty = table.Column<double>(nullable: false),
                    CurrentStepIndex = table.Column<int>(nullable: false),
                    GoodOutput = table.Column<double>(nullable: false),
                    Grade = table.Column<string>(maxLength: 100, nullable: true),
                    InstructionId = table.Column<int>(nullable: false),
                    IsBadOutput = table.Column<bool>(nullable: false),
                    IsComplete = table.Column<bool>(nullable: false),
                    IsInactive = table.Column<bool>(nullable: false),
                    IsReprocess = table.Column<bool>(nullable: false),
                    OldKanbanId = table.Column<int>(nullable: false),
                    ProductionOrderDeliveryDate = table.Column<DateTimeOffset>(nullable: false),
                    ProductionOrderBuyerId = table.Column<int>(nullable: false),
                    ProductionOrderBuyerCode = table.Column<string>(nullable: true),
                    ProductionOrderBuyerName = table.Column<string>(nullable: true),
                    ProductionOrderId = table.Column<int>(nullable: false),
                    ProductionOrderOrderNo = table.Column<string>(maxLength: 100, nullable: true),
                    ProductionOrderSalesContractNo = table.Column<string>(nullable: true),
                    ProductionOrderOrderTypeId = table.Column<int>(nullable: false),
                    ProductionOrderOrderTypeCode = table.Column<string>(maxLength: 100, nullable: true),
                    ProductionOrderOrderTypeName = table.Column<string>(maxLength: 250, nullable: true),
                    ProductionOrderProcessTypeId = table.Column<int>(nullable: false),
                    ProductionOrderProcessTypeCode = table.Column<string>(maxLength: 100, nullable: true),
                    ProductionOrderProcessTypeName = table.Column<string>(maxLength: 250, nullable: true),
                    ProductionOrderMaterialId = table.Column<int>(nullable: false),
                    ProductionOrderMaterialCode = table.Column<string>(maxLength: 100, nullable: true),
                    ProductionOrderMaterialName = table.Column<string>(maxLength: 500, nullable: true),
                    ProductionOrderMaterialConstructionId = table.Column<int>(nullable: false),
                    ProductionOrderMaterialConstructionCode = table.Column<string>(maxLength: 100, nullable: true),
                    ProductionOrderMaterialConstructionName = table.Column<string>(maxLength: 500, nullable: true),
                    ProductionOrderYarnMaterialId = table.Column<int>(nullable: false),
                    ProductionOrderYarnMaterialCode = table.Column<string>(maxLength: 100, nullable: true),
                    ProductionOrderYarnMaterialName = table.Column<string>(maxLength: 250, nullable: true),
                    ProductionOrderHandlingStandard = table.Column<string>(maxLength: 100, nullable: true),
                    FinishWidth = table.Column<string>(maxLength: 100, nullable: true),
                    SelectedProductionOrderDetailId = table.Column<int>(nullable: false),
                    SelectedProductionOrderDetailColorRequest = table.Column<string>(maxLength: 250, nullable: true),
                    SelectedProductionOrderDetailColorTemplate = table.Column<string>(maxLength: 250, nullable: true),
                    SelectedProductionOrderDetailColorTypeCode = table.Column<string>(maxLength: 100, nullable: true),
                    SelectedProductionOrderDetailColorTypeName = table.Column<string>(maxLength: 250, nullable: true),
                    SelectedProductionOrderDetailQuantity = table.Column<double>(nullable: false),
                    SelectedProductionOrderDetailUomUnit = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kanbans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Machine",
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
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Process = table.Column<string>(nullable: true),
                    Manufacture = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: false),
                    Condition = table.Column<string>(nullable: true),
                    MonthlyCapacity = table.Column<int>(nullable: false),
                    UnitId = table.Column<int>(nullable: false),
                    UnitCode = table.Column<string>(nullable: true),
                    UnitName = table.Column<string>(nullable: true),
                    UnitDivisionId = table.Column<string>(nullable: true),
                    UnitDivisionName = table.Column<string>(nullable: true),
                    MachineTypeId = table.Column<int>(nullable: false),
                    MachineTypeCode = table.Column<string>(nullable: true),
                    MachineTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machine", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MachineType",
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
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MonitoringEvent",
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
                    Code = table.Column<string>(nullable: true),
                    DateStart = table.Column<DateTimeOffset>(nullable: false),
                    DateEnd = table.Column<DateTimeOffset>(nullable: false),
                    TimeInMilisStart = table.Column<double>(nullable: false),
                    TimeInMilisEnd = table.Column<double>(nullable: false),
                    CartNumber = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true),
                    MachineId = table.Column<int>(nullable: false),
                    MachineName = table.Column<string>(nullable: true),
                    ProductionOrderId = table.Column<int>(nullable: false),
                    ProductionOrderOrderNo = table.Column<string>(nullable: true),
                    ProductionOrderDeliveryDate = table.Column<string>(nullable: true),
                    ProductionOrderDetailCode = table.Column<string>(nullable: true),
                    ProductionOrderDetailColorRequest = table.Column<string>(nullable: true),
                    ProductionOrderDetailColorTemplate = table.Column<string>(nullable: true),
                    ProductionOrderDetailColorTypeId = table.Column<string>(nullable: true),
                    ProductionOrderDetailColorType = table.Column<string>(nullable: true),
                    ProductionOrderDetailQuantity = table.Column<double>(nullable: false),
                    MachineEventId = table.Column<int>(nullable: false),
                    MachineEventCode = table.Column<string>(nullable: true),
                    MachineEventName = table.Column<string>(nullable: true),
                    MachineEventCategory = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitoringEvent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MonitoringSpecificationMachine",
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
                    Code = table.Column<string>(nullable: true),
                    DateTimeInput = table.Column<DateTimeOffset>(nullable: false),
                    CartNumber = table.Column<string>(nullable: true),
                    MachineId = table.Column<int>(nullable: false),
                    MachineName = table.Column<string>(nullable: true),
                    ProductionOrderId = table.Column<int>(nullable: false),
                    ProductionOrderNo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitoringSpecificationMachine", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PackingReceipt",
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
                    Code = table.Column<string>(nullable: true),
                    Date = table.Column<DateTimeOffset>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    Accepted = table.Column<bool>(nullable: false),
                    Declined = table.Column<bool>(nullable: false),
                    IsVoid = table.Column<bool>(nullable: false),
                    PackingCode = table.Column<string>(nullable: true),
                    PackingId = table.Column<int>(nullable: false),
                    StorageId = table.Column<int>(nullable: false),
                    StorageCode = table.Column<string>(nullable: true),
                    StorageName = table.Column<string>(nullable: true),
                    StorageUnitName = table.Column<string>(nullable: true),
                    StorageUnitCode = table.Column<string>(nullable: true),
                    StorageDivisionName = table.Column<string>(nullable: true),
                    StorageDivisionCode = table.Column<string>(nullable: true),
                    ReferenceNo = table.Column<string>(nullable: true),
                    ReferenceType = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    ProductionOrderNo = table.Column<string>(nullable: true),
                    Buyer = table.Column<string>(nullable: true),
                    ColorName = table.Column<string>(nullable: true),
                    Construction = table.Column<string>(nullable: true),
                    MaterialWidthFinish = table.Column<string>(nullable: true),
                    PackingUom = table.Column<string>(nullable: true),
                    OrderType = table.Column<string>(nullable: true),
                    ColorType = table.Column<string>(nullable: true),
                    DesignCode = table.Column<string>(nullable: true),
                    DesignNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackingReceipt", x => x.Id);
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
                    UId = table.Column<string>(maxLength: 255, nullable: true),
                    Code = table.Column<string>(maxLength: 25, nullable: true),
                    ProductionOrderId = table.Column<int>(nullable: false),
                    ProductionOrderNo = table.Column<string>(maxLength: 25, nullable: true),
                    OrderTypeId = table.Column<int>(nullable: false),
                    OrderTypeCode = table.Column<string>(maxLength: 25, nullable: true),
                    OrderTypeName = table.Column<string>(maxLength: 25, nullable: true),
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
                    Construction = table.Column<string>(maxLength: 300, nullable: true),
                    DeliveryType = table.Column<string>(maxLength: 25, nullable: true),
                    FinishedProductType = table.Column<string>(maxLength: 25, nullable: true),
                    Motif = table.Column<string>(maxLength: 250, nullable: true),
                    Status = table.Column<string>(maxLength: 25, nullable: true),
                    Accepted = table.Column<bool>(nullable: false),
                    Declined = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packings", x => x.Id);
                });

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
                    UId = table.Column<string>(maxLength: 255, nullable: true),
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
                name: "Steps",
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
                    Alias = table.Column<string>(maxLength: 500, nullable: true),
                    Code = table.Column<string>(maxLength: 100, nullable: true),
                    Process = table.Column<string>(maxLength: 500, nullable: true),
                    ProcessArea = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Steps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BadOutputMachine",
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
                    MachineId = table.Column<int>(nullable: false),
                    MachineName = table.Column<string>(nullable: true),
                    MachineCode = table.Column<string>(nullable: true),
                    BadOutputId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BadOutputMachine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BadOutputMachine_BadOutput_BadOutputId",
                        column: x => x.BadOutputId,
                        principalTable: "BadOutput",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DurationEstimationAreas",
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
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Duration = table.Column<int>(nullable: false),
                    DurationEstimationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DurationEstimationAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DurationEstimationAreas_DurationEstimations_DurationEstimationId",
                        column: x => x.DurationEstimationId,
                        principalTable: "DurationEstimations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FabricGradeTests",
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
                    AvalLength = table.Column<double>(nullable: false),
                    FabricGradeTest = table.Column<double>(nullable: false),
                    FinalArea = table.Column<double>(nullable: false),
                    FinalGradeTest = table.Column<double>(nullable: false),
                    FinalLength = table.Column<double>(nullable: false),
                    FinalScore = table.Column<double>(nullable: false),
                    Grade = table.Column<string>(maxLength: 100, nullable: true),
                    InitLength = table.Column<double>(nullable: false),
                    PcsNo = table.Column<string>(maxLength: 100, nullable: true),
                    PointLimit = table.Column<double>(nullable: false),
                    PointSystem = table.Column<double>(nullable: false),
                    SampleLength = table.Column<double>(nullable: false),
                    Score = table.Column<double>(nullable: false),
                    Type = table.Column<string>(maxLength: 100, nullable: true),
                    Width = table.Column<double>(nullable: false),
                    FabricQualityControlId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FabricGradeTests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FabricGradeTests_FabricQualityControls_FabricQualityControlId",
                        column: x => x.FabricQualityControlId,
                        principalTable: "FabricQualityControls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstructionSteps",
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
                    Alias = table.Column<string>(maxLength: 100, nullable: true),
                    Code = table.Column<string>(maxLength: 100, nullable: true),
                    Process = table.Column<string>(maxLength: 500, nullable: true),
                    ProcessArea = table.Column<string>(maxLength: 500, nullable: true),
                    InstructionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructionSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstructionSteps_Instructions_InstructionId",
                        column: x => x.InstructionId,
                        principalTable: "Instructions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KanbanInstructions",
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
                    Code = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 500, nullable: true),
                    KanbanId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KanbanInstructions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KanbanInstructions_Kanbans_KanbanId",
                        column: x => x.KanbanId,
                        principalTable: "Kanbans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyOperation",
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
                    Code = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Shift = table.Column<string>(nullable: true),
                    DateInput = table.Column<DateTimeOffset>(nullable: true),
                    TimeInput = table.Column<double>(nullable: true),
                    Input = table.Column<double>(nullable: true),
                    DateOutput = table.Column<DateTimeOffset>(nullable: true),
                    TimeOutput = table.Column<double>(nullable: true),
                    GoodOutput = table.Column<double>(nullable: true),
                    BadOutput = table.Column<double>(nullable: true),
                    StepId = table.Column<int>(nullable: false),
                    StepProcess = table.Column<string>(nullable: true),
                    KanbanId = table.Column<int>(nullable: false),
                    KanbanCode = table.Column<string>(nullable: true),
                    MachineId = table.Column<int>(nullable: false),
                    MachineCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyOperation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyOperation_Kanbans_KanbanId",
                        column: x => x.KanbanId,
                        principalTable: "Kanbans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DailyOperation_Machine_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MachineEvents",
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
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    No = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    MachineId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MachineEvents_Machine_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MachineSteps",
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
                    StepId = table.Column<int>(nullable: false),
                    Alias = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Process = table.Column<string>(nullable: true),
                    ProcessArea = table.Column<string>(nullable: true),
                    MachineId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MachineSteps_Machine_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MachineTypeIndicators",
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
                    Indicator = table.Column<string>(nullable: true),
                    DataType = table.Column<string>(nullable: true),
                    DefaultValue = table.Column<string>(nullable: true),
                    Uom = table.Column<string>(nullable: true),
                    MachineTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineTypeIndicators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MachineTypeIndicators_MachineType_MachineTypeId",
                        column: x => x.MachineTypeId,
                        principalTable: "MachineType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonitoringSpecificationMachineDetails",
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
                    Indicator = table.Column<string>(nullable: true),
                    DataType = table.Column<string>(nullable: true),
                    DefaultValue = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    Uom = table.Column<string>(nullable: true),
                    MonitoringSpecificationMachineId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonitoringSpecificationMachineDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonitoringSpecificationMachineDetails_MonitoringSpecificationMachine_MonitoringSpecificationMachineId",
                        column: x => x.MonitoringSpecificationMachineId,
                        principalTable: "MonitoringSpecificationMachine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    ProductCode = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    Length = table.Column<int>(nullable: false),
                    Weight = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    UomId = table.Column<int>(nullable: false),
                    Uom = table.Column<string>(nullable: true),
                    IsDelivered = table.Column<bool>(nullable: false),
                    AvailableQuantity = table.Column<int>(nullable: false),
                    PackingReceiptId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackingReceiptItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackingReceiptItem_PackingReceipt_PackingReceiptId",
                        column: x => x.PackingReceiptId,
                        principalTable: "PackingReceipt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    Remark = table.Column<string>(maxLength: 500, nullable: true),
                    PackingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PackingDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PackingDetails_Packings_PackingId",
                        column: x => x.PackingId,
                        principalTable: "Packings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    ProductionOrderNo = table.Column<string>(nullable: true),
                    ProductionOrderCode = table.Column<string>(nullable: true)
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
                name: "StepIndicators",
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
                    Name = table.Column<string>(maxLength: 500, nullable: true),
                    Uom = table.Column<string>(maxLength: 500, nullable: true),
                    Value = table.Column<string>(maxLength: 150, nullable: true),
                    StepId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StepIndicators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StepIndicators_Steps_StepId",
                        column: x => x.StepId,
                        principalTable: "Steps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Criterion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Group = table.Column<string>(nullable: true),
                    Index = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ScoreA = table.Column<double>(nullable: false),
                    ScoreB = table.Column<double>(nullable: false),
                    ScoreC = table.Column<double>(nullable: false),
                    ScoreD = table.Column<double>(nullable: false),
                    FabricGradeTestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Criterion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Criterion_FabricGradeTests_FabricGradeTestId",
                        column: x => x.FabricGradeTestId,
                        principalTable: "FabricGradeTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstructionStepIndicators",
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
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Uom = table.Column<string>(maxLength: 100, nullable: true),
                    Value = table.Column<string>(maxLength: 150, nullable: true),
                    StepId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstructionStepIndicators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstructionStepIndicators_InstructionSteps_StepId",
                        column: x => x.StepId,
                        principalTable: "InstructionSteps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KanbanSteps",
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
                    Alias = table.Column<string>(maxLength: 100, nullable: true),
                    Code = table.Column<string>(maxLength: 100, nullable: true),
                    Deadline = table.Column<DateTimeOffset>(nullable: false),
                    Process = table.Column<string>(maxLength: 500, nullable: true),
                    ProcessArea = table.Column<string>(maxLength: 500, nullable: true),
                    InstructionId = table.Column<int>(nullable: false),
                    MachineId = table.Column<int>(nullable: false),
                    SelectedIndex = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KanbanSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KanbanSteps_KanbanInstructions_InstructionId",
                        column: x => x.InstructionId,
                        principalTable: "KanbanInstructions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KanbanSteps_Machine_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machine",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DailyOperationBadOutputReasons",
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
                    Length = table.Column<double>(nullable: true),
                    Action = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    BadOutputId = table.Column<int>(nullable: false),
                    BadOutputCode = table.Column<string>(nullable: true),
                    BadOutputReason = table.Column<string>(nullable: true),
                    MachineId = table.Column<int>(nullable: false),
                    MachineCode = table.Column<string>(nullable: true),
                    MachineName = table.Column<string>(nullable: true),
                    DailyOperationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyOperationBadOutputReasons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyOperationBadOutputReasons_DailyOperation_DailyOperationId",
                        column: x => x.DailyOperationId,
                        principalTable: "DailyOperation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    DesignNumber = table.Column<string>(nullable: true),
                    Length = table.Column<double>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    ProductCode = table.Column<string>(nullable: true),
                    QuantityBefore = table.Column<double>(nullable: false),
                    Remark = table.Column<string>(maxLength: 500, nullable: true),
                    ReturQuantity = table.Column<double>(nullable: false),
                    StorageId = table.Column<int>(nullable: false),
                    StorageName = table.Column<string>(nullable: true),
                    StorageCode = table.Column<string>(nullable: true),
                    UOMUnit = table.Column<string>(nullable: true),
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
                name: "KanbanStepIndicators",
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
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Uom = table.Column<string>(maxLength: 100, nullable: true),
                    Value = table.Column<string>(maxLength: 150, nullable: true),
                    StepId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KanbanStepIndicators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KanbanStepIndicators_KanbanSteps_StepId",
                        column: x => x.StepId,
                        principalTable: "KanbanSteps",
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
                name: "IX_BadOutputMachine_BadOutputId",
                table: "BadOutputMachine",
                column: "BadOutputId");

            migrationBuilder.CreateIndex(
                name: "IX_Criterion_FabricGradeTestId",
                table: "Criterion",
                column: "FabricGradeTestId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyOperation_KanbanId",
                table: "DailyOperation",
                column: "KanbanId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyOperation_MachineId",
                table: "DailyOperation",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_DailyOperationBadOutputReasons_DailyOperationId",
                table: "DailyOperationBadOutputReasons",
                column: "DailyOperationId");

            migrationBuilder.CreateIndex(
                name: "IX_DurationEstimationAreas_DurationEstimationId",
                table: "DurationEstimationAreas",
                column: "DurationEstimationId");

            migrationBuilder.CreateIndex(
                name: "IX_FabricGradeTests_FabricQualityControlId",
                table: "FabricGradeTests",
                column: "FabricQualityControlId");

            migrationBuilder.CreateIndex(
                name: "IX_InstructionStepIndicators_StepId",
                table: "InstructionStepIndicators",
                column: "StepId");

            migrationBuilder.CreateIndex(
                name: "IX_InstructionSteps_InstructionId",
                table: "InstructionSteps",
                column: "InstructionId");

            migrationBuilder.CreateIndex(
                name: "IX_KanbanInstructions_KanbanId",
                table: "KanbanInstructions",
                column: "KanbanId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KanbanStepIndicators_StepId",
                table: "KanbanStepIndicators",
                column: "StepId");

            migrationBuilder.CreateIndex(
                name: "IX_KanbanSteps_InstructionId",
                table: "KanbanSteps",
                column: "InstructionId");

            migrationBuilder.CreateIndex(
                name: "IX_KanbanSteps_MachineId",
                table: "KanbanSteps",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineEvents_MachineId",
                table: "MachineEvents",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineSteps_MachineId",
                table: "MachineSteps",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineTypeIndicators_MachineTypeId",
                table: "MachineTypeIndicators",
                column: "MachineTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MonitoringSpecificationMachineDetails_MonitoringSpecificationMachineId",
                table: "MonitoringSpecificationMachineDetails",
                column: "MonitoringSpecificationMachineId");

            migrationBuilder.CreateIndex(
                name: "IX_PackingDetails_PackingId",
                table: "PackingDetails",
                column: "PackingId");

            migrationBuilder.CreateIndex(
                name: "IX_PackingReceiptItem_PackingReceiptId",
                table: "PackingReceiptItem",
                column: "PackingReceiptId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturToQCItemDetails_ReturToQCItemId",
                table: "ReturToQCItemDetails",
                column: "ReturToQCItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturToQCItems_ReturToQCId",
                table: "ReturToQCItems",
                column: "ReturToQCId");

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

            migrationBuilder.CreateIndex(
                name: "IX_StepIndicators_StepId",
                table: "StepIndicators",
                column: "StepId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BadOutputMachine");

            migrationBuilder.DropTable(
                name: "Criterion");

            migrationBuilder.DropTable(
                name: "DailyOperationBadOutputReasons");

            migrationBuilder.DropTable(
                name: "DurationEstimationAreas");

            migrationBuilder.DropTable(
                name: "InstructionStepIndicators");

            migrationBuilder.DropTable(
                name: "KanbanStepIndicators");

            migrationBuilder.DropTable(
                name: "MachineEvents");

            migrationBuilder.DropTable(
                name: "MachineSteps");

            migrationBuilder.DropTable(
                name: "MachineTypeIndicators");

            migrationBuilder.DropTable(
                name: "MonitoringEvent");

            migrationBuilder.DropTable(
                name: "MonitoringSpecificationMachineDetails");

            migrationBuilder.DropTable(
                name: "PackingDetails");

            migrationBuilder.DropTable(
                name: "PackingReceiptItem");

            migrationBuilder.DropTable(
                name: "ReturToQCItemDetails");

            migrationBuilder.DropTable(
                name: "ShipmentDocumentPackingReceiptItems");

            migrationBuilder.DropTable(
                name: "StepIndicators");

            migrationBuilder.DropTable(
                name: "BadOutput");

            migrationBuilder.DropTable(
                name: "FabricGradeTests");

            migrationBuilder.DropTable(
                name: "DailyOperation");

            migrationBuilder.DropTable(
                name: "DurationEstimations");

            migrationBuilder.DropTable(
                name: "InstructionSteps");

            migrationBuilder.DropTable(
                name: "KanbanSteps");

            migrationBuilder.DropTable(
                name: "MachineType");

            migrationBuilder.DropTable(
                name: "MonitoringSpecificationMachine");

            migrationBuilder.DropTable(
                name: "Packings");

            migrationBuilder.DropTable(
                name: "PackingReceipt");

            migrationBuilder.DropTable(
                name: "ReturToQCItems");

            migrationBuilder.DropTable(
                name: "ShipmentDocumentItems");

            migrationBuilder.DropTable(
                name: "Steps");

            migrationBuilder.DropTable(
                name: "FabricQualityControls");

            migrationBuilder.DropTable(
                name: "Instructions");

            migrationBuilder.DropTable(
                name: "KanbanInstructions");

            migrationBuilder.DropTable(
                name: "Machine");

            migrationBuilder.DropTable(
                name: "ReturToQCs");

            migrationBuilder.DropTable(
                name: "ShipmentDocumentDetails");

            migrationBuilder.DropTable(
                name: "Kanbans");

            migrationBuilder.DropTable(
                name: "ShipmentDocuments");
        }
    }
}
