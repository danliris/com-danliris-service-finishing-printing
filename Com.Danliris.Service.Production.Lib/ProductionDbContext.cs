using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.Machine;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.MachineType;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Monitoring_Specification_Machine;
using Com.Danliris.Service.Production.Lib.ModelConfigs.Master.DurationEstimation;
using Com.Danliris.Service.Production.Lib.ModelConfigs.Master.Instruction;
using Com.Danliris.Service.Production.Lib.ModelConfigs.Master.Step;
using Com.Danliris.Service.Production.Lib.Models.Master.DurationEstimation;
using Com.Danliris.Service.Production.Lib.Models.Master.Instruction;
using Com.Danliris.Service.Production.Lib.Models.Master.Step;
using Com.Moonlay.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StepConfig());
            modelBuilder.ApplyConfiguration(new StepIndicatorConfig());
            modelBuilder.ApplyConfiguration(new InstructionConfig());
            modelBuilder.ApplyConfiguration(new InstructionStepConfig());
            modelBuilder.ApplyConfiguration(new InstructionStepIndicatorConfig());
            modelBuilder.ApplyConfiguration(new DurationEstimationConfig());
            modelBuilder.ApplyConfiguration(new DurationEstimationAreaConfig());
            base.OnModelCreating(modelBuilder);
        }
    }
}
