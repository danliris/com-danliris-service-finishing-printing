using Com.Danliris.Service.Production.Lib.Models.Master.Step;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Production.Lib.ModelConfigs.Master.Step
{
    public class StepIndicatorConfig : IEntityTypeConfiguration<StepIndicatorModel>
    {
        public void Configure(EntityTypeBuilder<StepIndicatorModel> builder)
        {
            builder.Property(b => b.Name).HasMaxLength(500);
            builder.Property(b => b.Uom).HasMaxLength(500);
            builder.Property(b => b.Value).HasMaxLength(150);
        }
    }
}
