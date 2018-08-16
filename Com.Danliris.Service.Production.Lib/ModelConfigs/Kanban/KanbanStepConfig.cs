using Com.Danliris.Service.Finishing.Printing.Lib.Models.Kanban;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ModelConfigs.Kanban
{
    public class KanbanStepConfig : IEntityTypeConfiguration<KanbanStepModel>
    {
        public void Configure(EntityTypeBuilder<KanbanStepModel> builder)
        {
            builder.Property(b => b.Alias).HasMaxLength(100);
            builder.Property(b => b.Code).HasMaxLength(100);
            builder.Property(b => b.Process).HasMaxLength(500);
            builder.Property(b => b.ProcessArea).HasMaxLength(500);
            builder
                .HasMany(b => b.StepIndicators)
                .WithOne(c => c.Step);
        }
    }
}
