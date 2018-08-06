using Com.Danliris.Service.Production.Lib.Models.Master.Instruction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Production.Lib.ModelConfigs.Master.Instruction
{
    public class InstructionConfig : IEntityTypeConfiguration<InstructionModel>
    {
        public void Configure(EntityTypeBuilder<InstructionModel> builder)
        {
            builder.Property(b => b.Code).HasMaxLength(100);
            builder.Property(b => b.Name).HasMaxLength(500);
            builder
                .HasMany(b => b.Steps);
        }
    }
}
