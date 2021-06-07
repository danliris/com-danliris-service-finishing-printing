using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.DyestuffChemicalUsageReceipt
{
    public class DyestuffChemicalUsageReceiptItemDetailModel : StandardEntity, IValidatableObject
    {
        public int Index { get; set; }

        [MaxLength(2048)]
        public string Name { get; set; }

        public double ReceiptQuantity { get; set; }

        public double Adjs1Quantity { get; set; }

        public double Adjs2Quantity { get; set; }

        public double Adjs3Quantity { get; set; }

        public double Adjs4Quantity { get; set; }

        public int DyestuffChemicalUsageReceiptItemId { get; set; }

        [ForeignKey("DyestuffChemicalUsageReceiptItemId")]
        public DyestuffChemicalUsageReceiptItemModel DyestuffChemicalUsageReceiptItem { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
