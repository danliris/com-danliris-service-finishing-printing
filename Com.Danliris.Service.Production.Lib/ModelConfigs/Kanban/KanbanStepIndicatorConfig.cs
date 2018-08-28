using Com.Danliris.Service.Finishing.Printing.Lib.Models.Kanban;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ModelConfigs.Kanban
{
    public class KanbanStepIndicatorConfig : IEntityTypeConfiguration<KanbanStepIndicatorModel>
    {
        public void Configure(EntityTypeBuilder<KanbanStepIndicatorModel> builder)
        {
            builder.Property(b => b.Name).HasMaxLength(100);
            builder.Property(b => b.Uom).HasMaxLength(100);
            builder.Property(b => b.Value).HasMaxLength(150);
            builder
                .HasOne(b => b.Step)
                .WithMany(b => b.StepIndicators);
        }
    }
}
