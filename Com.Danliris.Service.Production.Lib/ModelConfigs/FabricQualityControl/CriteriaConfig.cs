using Com.Danliris.Service.Finishing.Printing.Lib.Models.FabricQualityControl;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ModelConfigs.FabricQualityControl
{
    public class CriteriaConfig : IEntityTypeConfiguration<CriteriaModel>
    {
        public void Configure(EntityTypeBuilder<CriteriaModel> builder)
        {
        }
    }
}
