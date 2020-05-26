using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.DyestuffChemicalUsageReceipt
{
    public class DyestuffChemicalUsageReceiptItemModel : StandardEntity, IValidatableObject
    {
        public DyestuffChemicalUsageReceiptItemModel()
        {
            DyestuffChemicalUsageReceiptItemDetails = new HashSet<DyestuffChemicalUsageReceiptItemDetailModel>();
        }

        [MaxLength(2048)]
        public string ColorCode { get; set; }

        public DateTimeOffset? Prod1Date { get; set; }

        public DateTimeOffset? Prod2Date { get; set; }

        public DateTimeOffset? Prod3Date { get; set; }

        public DateTimeOffset? Prod4Date { get; set; }

        public DateTimeOffset? Prod5Date { get; set; }

        public ICollection<DyestuffChemicalUsageReceiptItemDetailModel> DyestuffChemicalUsageReceiptItemDetails { get; set; }

        public int DyestuffChemicalUsageReceiptId { get; set; }

        [ForeignKey("DyestuffChemicalUsageReceiptId")]
        public DyestuffChemicalUsageReceiptModel DyestuffChemicalUsageReceipt { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
