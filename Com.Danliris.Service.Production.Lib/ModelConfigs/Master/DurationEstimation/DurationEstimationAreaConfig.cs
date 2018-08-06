using Com.Danliris.Service.Production.Lib.Models.Master.DurationEstimation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Production.Lib.ModelConfigs.Master.DurationEstimation
{
    public class DurationEstimationAreaConfig : IEntityTypeConfiguration<DurationEstimationAreaModel>
    {
        public void Configure(EntityTypeBuilder<DurationEstimationAreaModel> builder)
        {
            builder.Property(b => b.Name).HasMaxLength(100);
        }
    }
}
