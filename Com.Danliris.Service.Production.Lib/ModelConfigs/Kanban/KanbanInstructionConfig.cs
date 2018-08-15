using Com.Danliris.Service.Finishing.Printing.Lib.Models.Kanban;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ModelConfigs.Kanban
{
    public class KanbanInstructionConfig : IEntityTypeConfiguration<KanbanInstructionModel>
    {
        public void Configure(EntityTypeBuilder<KanbanInstructionModel> builder)
        {
            builder.Property(b => b.Code).HasMaxLength(100);
            builder.Property(b => b.Name).HasMaxLength(500);
            builder
                .HasOne(b => b.Kanban)
                .WithOne(c => c.Instruction)
                .HasForeignKey<KanbanInstructionModel>("KanbanId");
            builder
                .HasMany(b => b.Steps)
                .WithOne(b => b.Instruction);
        }
    }
}
