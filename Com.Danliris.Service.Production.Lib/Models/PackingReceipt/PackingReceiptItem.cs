using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.PackingReceipt
{
    public class PackingReceiptItem : StandardEntity, IValidatableObject
    {
        public string Product { get; set; }
        public string ProductCode { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Length { get; set; }
        public int Weight { get; set; }
        public string Remark { get; set; }
        public string Notes { get; set; }
        public int UomId { get; set; }
        public string Uom { get; set; }
        public bool IsDelivered { get; set; }
        public int AvailableQuantity { get; set; }
        public int PackingReceiptId { get; set; }
        public virtual PackingReceiptModel PackingReceipt { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
