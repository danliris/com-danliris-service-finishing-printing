using Com.Danliris.Service.Production.Lib.Models.Master.Instruction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Production.Lib.ModelConfigs.Master.Instruction
{
    public class InstructionStepIndicatorConfig : IEntityTypeConfiguration<InstructionStepIndicatorModel>
    {
        public void Configure(EntityTypeBuilder<InstructionStepIndicatorModel> builder)
        {
            builder.Property(b => b.Name).HasMaxLength(100);
            builder.Property(b => b.Uom).HasMaxLength(100);
            builder.Property(b => b.Value).HasMaxLength(150);
        }
    }
}
