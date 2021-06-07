using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.DyestuffChemicalUsageReceipt
{
    public class DyestuffChemicalUsageReceiptModel : StandardEntity, IValidatableObject
    {

        public DyestuffChemicalUsageReceiptModel()
        {
            DyestuffChemicalUsageReceiptItems = new HashSet<DyestuffChemicalUsageReceiptItemModel>();
        }

        public long ProductionOrderId { get; set; }

        [MaxLength(256)]
        public string ProductionOrderOrderNo { get; set; }

        public double ProductionOrderOrderQuantity { get; set; }

        public long ProductionOrderMaterialId { get; set; }

        [MaxLength(1024)]
        public string ProductionOrderMaterialName { get; set; }

        public long ProductionOrderMaterialConstructionId { get; set; }

        [MaxLength(1024)]
        public string ProductionOrderMaterialConstructionName { get; set; }

        [MaxLength(1024)]
        public string ProductionOrderMaterialWidth { get; set; }

        public int StrikeOffId { get; set; }

        [MaxLength(128)]
        public string StrikeOffCode { get; set; }

        [MaxLength(256)]
        public string StrikeOffType { get; set; }

        public DateTimeOffset Date { get; set; }

        [MaxLength(128)]
        public string RepeatedProductionOrderNo { get; set; }

        public ICollection<DyestuffChemicalUsageReceiptItemModel> DyestuffChemicalUsageReceiptItems { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
