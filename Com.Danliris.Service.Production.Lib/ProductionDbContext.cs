using Com.Danliris.Service.Finishing.Printing.Lib.ModelConfigs.FabricQualityControl;
using Com.Danliris.Service.Finishing.Printing.Lib.ModelConfigs.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.CostCalculation;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Daily_Operation;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.DOSales;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.FabricQualityControl;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.BadOutput;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.DirectLaborCost;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.Machine;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.MachineType;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.OperationalCost;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Monitoring_Event;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Monitoring_Specification_Machine;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Packing;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.PackingReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.ReturToQC;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.ShipmentDocument;
using Com.Danliris.Service.Production.Lib.ModelConfigs.Master.DurationEstimation;
using Com.Danliris.Service.Production.Lib.ModelConfigs.Master.Instruction;
using Com.Danliris.Service.Production.Lib.ModelConfigs.Master.Step;
using Com.Danliris.Service.Production.Lib.Models.Master.DurationEstimation;
using Com.Danliris.Service.Production.Lib.Models.Master.Instruction;
using Com.Danliris.Service.Production.Lib.Models.Master.Step;
using Com.Moonlay.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Com.Danliris.Service.Production.Lib
{
    public class ProductionDbContext : StandardDbContext
    {
        public ProductionDbContext(DbContextOptions<ProductionDbContext> options) : base(options)
        {
        }

        /* Master */
        public DbSet<StepIndicatorModel> StepIndicators { get; set; }
        public DbSet<StepModel> Steps { get; set; }
        public DbSet<InstructionStepIndicatorModel> InstructionStepIndicators { get; set; }
        public DbSet<InstructionStepModel> InstructionSteps { get; set; }
        public DbSet<InstructionModel> Instructions { get; set; }
        public DbSet<DurationEstimationModel> DurationEstimations { get; set; }
        public DbSet<DurationEstimationAreaModel> DurationEstimationAreas { get; set; }
        public DbSet<MachineTypeModel> MachineType { get; set; }
        public DbSet<MachineTypeIndicatorsModel> MachineTypeIndicators { get; set; }
        public DbSet<MachineModel> Machine { get; set; }
        public DbSet<MachineEventsModel> MachineEvents { get; set; }
        public DbSet<MachineStepModel> MachineSteps { get; set; }
        public DbSet<MonitoringSpecificationMachineModel> MonitoringSpecificationMachine { get; set; }
        public DbSet<MonitoringSpecificationMachineDetailsModel> MonitoringSpecificationMachineDetails { get; set; }
        public DbSet<KanbanModel> Kanbans { get; set; }
        public DbSet<KanbanInstructionModel> KanbanInstructions { get; set; }
        public DbSet<KanbanStepModel> KanbanSteps { get; set; }
        public DbSet<KanbanStepIndicatorModel> KanbanStepIndicators { get; set; }
        public DbSet<MonitoringEventModel> MonitoringEvent { get; set; }
        public DbSet<BadOutputModel> BadOutput { get; set; }
        public DbSet<BadOutputMachineModel> BadOutputMachine { get; set; }
        public DbSet<DailyOperationModel> DailyOperation { get; set; }
        public DbSet<DailyOperationBadOutputReasonsModel> DailyOperationBadOutputReasons { get; set; }
        public DbSet<DOSalesModel> DOSalesItems { get; set; }
        public DbSet<DOSalesDetailModel> DOSalesItemDetails { get; set; }
        public DbSet<PackingModel> Packings { get; set; }
        public DbSet<PackingDetailModel> PackingDetails { get; set; }
        public DbSet<FabricQualityControlModel> FabricQualityControls { get; set; }
        public DbSet<FabricGradeTestModel> FabricGradeTests { get; set; }
        public DbSet<CriteriaModel> Criterion { get; set; }
        public DbSet<PackingReceiptModel> PackingReceipt { get; set; }
        public DbSet<ReturToQCModel> ReturToQCs { get; set; }
        public DbSet<ReturToQCItemModel> ReturToQCItems { get; set; }
        public DbSet<ReturToQCItemDetailModel> ReturToQCItemDetails { get; set; }
        public DbSet<ShipmentDocumentModel> ShipmentDocuments { get; set; }
        public DbSet<ShipmentDocumentDetailModel> ShipmentDocumentDetails { get; set; }
        public DbSet<ShipmentDocumentItemModel> ShipmentDocumentItems { get; set; }
        public DbSet<ShipmentDocumentPackingReceiptItemModel> ShipmentDocumentPackingReceiptItems { get; set; }

        public DbSet<DirectLaborCostModel> DirectLaborCosts { get; set; }
        public DbSet<OperationalCostModel> OperationalCosts { get; set; }

        public DbSet<CostCalculationModel> CostCalculations { get; set; }
        public DbSet<CostCalculationMachineModel> CostCalculationMachines { get; set; }
        public DbSet<CostCalculationChemicalModel> CostCalculationChemicals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StepConfig());
            modelBuilder.ApplyConfiguration(new StepIndicatorConfig());
            modelBuilder.ApplyConfiguration(new InstructionConfig());
            modelBuilder.ApplyConfiguration(new InstructionStepConfig());
            modelBuilder.ApplyConfiguration(new InstructionStepIndicatorConfig());
            modelBuilder.ApplyConfiguration(new DurationEstimationConfig());
            modelBuilder.ApplyConfiguration(new DurationEstimationAreaConfig());
            modelBuilder.ApplyConfiguration(new KanbanConfig());
            modelBuilder.ApplyConfiguration(new KanbanInstructionConfig());
            modelBuilder.ApplyConfiguration(new KanbanStepConfig());
            modelBuilder.ApplyConfiguration(new KanbanStepIndicatorConfig());
            modelBuilder.ApplyConfiguration(new FabricQualityControlConfig());
            modelBuilder.ApplyConfiguration(new FabricGradeTestConfig());
            modelBuilder.ApplyConfiguration(new CriteriaConfig());
            base.OnModelCreating(modelBuilder);
        }
    }
}