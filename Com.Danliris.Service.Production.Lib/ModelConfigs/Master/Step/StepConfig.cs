using Com.Danliris.Service.Production.Lib.Models.Master.Step;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Production.Lib.ModelConfigs.Master.Step
{
    public class StepConfig : IEntityTypeConfiguration<StepModel>
    {
        public void Configure(EntityTypeBuilder<StepModel> builder)
        {
            builder.Property(b => b.Code).HasMaxLength(100);
            builder.Property(b => b.Alias).HasMaxLength(500);
            builder.Property(b => b.Process).HasMaxLength(500);
            builder.Property(b => b.ProcessArea).HasMaxLength(500);
            builder
                .HasMany(b => b.StepIndicators);
        }
    }
}
