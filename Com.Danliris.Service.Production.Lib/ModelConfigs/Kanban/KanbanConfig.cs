using Com.Danliris.Service.Finishing.Printing.Lib.Models.Kanban;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ModelConfigs.Kanban
{
    public class KanbanConfig : IEntityTypeConfiguration<KanbanModel>
    {
        public void Configure(EntityTypeBuilder<KanbanModel> builder)
        {
            builder.Property(b => b.CartCartNumber).HasMaxLength(100);
            builder.Property(b => b.CartCode).HasMaxLength(100);
            builder.Property(b => b.CartUomUnit).HasMaxLength(100);
            builder.Property(b => b.Code).HasMaxLength(100);
            builder.Property(b => b.FinishWidth).HasMaxLength(100);
            builder.Property(b => b.Grade).HasMaxLength(100);
            builder.Property(b => b.ProductionOrderHandlingStandard).HasMaxLength(100);
            builder.Property(b => b.ProductionOrderMaterialCode).HasMaxLength(100);
            builder.Property(b => b.ProductionOrderMaterialConstructionCode).HasMaxLength(100);
            builder.Property(b => b.ProductionOrderMaterialConstructionName).HasMaxLength(500);
            builder.Property(b => b.ProductionOrderMaterialName).HasMaxLength(500);
            builder.Property(b => b.ProductionOrderOrderNo).HasMaxLength(100);
            builder.Property(b => b.ProductionOrderOrderTypeCode).HasMaxLength(100);
            builder.Property(b => b.ProductionOrderOrderTypeName).HasMaxLength(250);
            builder.Property(b => b.ProductionOrderProcessTypeCode).HasMaxLength(100);
            builder.Property(b => b.ProductionOrderProcessTypeName).HasMaxLength(250);
            builder.Property(b => b.ProductionOrderYarnMaterialCode).HasMaxLength(100);
            builder.Property(b => b.ProductionOrderYarnMaterialName).HasMaxLength(250);
            builder.Property(b => b.SelectedProductionOrderDetailColorRequest).HasMaxLength(250);
            builder.Property(b => b.SelectedProductionOrderDetailColorTemplate).HasMaxLength(250);
            builder.Property(b => b.SelectedProductionOrderDetailColorTypeCode).HasMaxLength(100);
            builder.Property(b => b.SelectedProductionOrderDetailColorTypeName).HasMaxLength(250);
            builder.Property(b => b.SelectedProductionOrderDetailUomUnit).HasMaxLength(250);
            builder
                .HasOne(b => b.Instruction)
                .WithOne(c => c.Kanban)
                .HasForeignKey<KanbanModel>("InstructionId");
        }
    }
}
