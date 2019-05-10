using Com.Danliris.Service.Finishing.Printing.Lib.Models.FabricQualityControl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ModelConfigs.FabricQualityControl
{
    public class FabricQualityControlConfig : IEntityTypeConfiguration<FabricQualityControlModel>
    {
        public void Configure(EntityTypeBuilder<FabricQualityControlModel> builder)
        {
            builder.Property(b => b.Buyer).HasMaxLength(250);
            builder.Property(b => b.CartNo).HasMaxLength(250);
            builder.Property(b => b.Code).HasMaxLength(25);
            builder.Property(b => b.Color).HasMaxLength(250);
            builder.Property(b => b.Construction).HasMaxLength(250);
            builder.Property(b => b.Group).HasMaxLength(250);
            builder.Property(b => b.KanbanCode).HasMaxLength(25);
            builder.Property(b => b.MachineNoIm).HasMaxLength(250);
            builder.Property(b => b.OperatorIm).HasMaxLength(250);
            builder.Property(b => b.PackingInstruction).HasMaxLength(500);
            builder.Property(b => b.ProductionOrderNo).HasMaxLength(250);
            builder.Property(b => b.ProductionOrderType).HasMaxLength(250);
            builder.Property(b => b.ShiftIm).HasMaxLength(250);
            builder.Property(b => b.Uom).HasMaxLength(250);
            builder
                .HasMany(b => b.FabricGradeTests)
                .WithOne(c => c.FabricQualityControl);
        }
    }
}
