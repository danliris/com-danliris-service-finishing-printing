using Com.Danliris.Service.Finishing.Printing.Lib.Models.FabricQualityControl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ModelConfigs.FabricQualityControl
{
    public class FabricGradeTestConfig : IEntityTypeConfiguration<FabricGradeTestModel>
    {
        public void Configure(EntityTypeBuilder<FabricGradeTestModel> builder)
        {
            builder.Property(b => b.Grade).HasMaxLength(100);
            builder.Property(b => b.PcsNo).HasMaxLength(100);
            builder.Property(b => b.Type).HasMaxLength(100);
            builder
                .HasMany(b => b.Criteria)
                .WithOne(c => c.FabricGradeTest);
        }
    }
}
