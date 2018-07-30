using Com.Danliris.Service.Production.Lib.Models.Master.DurationEstimation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Production.Lib.ModelConfigs.Master.DurationEstimation
{
    public class DurationEstimationConfig : IEntityTypeConfiguration<DurationEstimationModel>
    {
        public void Configure(EntityTypeBuilder<DurationEstimationModel> builder)
        {
            builder.Property(b => b.Code).HasMaxLength(100);
            builder.Property(b => b.OrderTypeCode).HasMaxLength(100);
            builder.Property(b => b.OrderTypeName).HasMaxLength(500);
            builder.Property(b => b.ProcessTypeCode).HasMaxLength(100);
            builder.Property(b => b.ProcessTypeName).HasMaxLength(500);
            builder
                .HasMany(b => b.Areas);
        }
    }
}
