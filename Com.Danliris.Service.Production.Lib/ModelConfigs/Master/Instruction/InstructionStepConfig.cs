using Com.Danliris.Service.Production.Lib.Models.Master.Instruction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Production.Lib.ModelConfigs.Master.Instruction
{
    public class InstructionStepConfig : IEntityTypeConfiguration<InstructionStepModel>
    {
        public void Configure(EntityTypeBuilder<InstructionStepModel> builder)
        {
            builder.Property(b => b.Alias).HasMaxLength(100);
            builder.Property(b => b.Code).HasMaxLength(100);
            builder.Property(b => b.Process).HasMaxLength(500);
            builder.Property(b => b.ProcessArea).HasMaxLength(500);
            builder
                .HasMany(b => b.StepIndicators);
        }
    }
}
