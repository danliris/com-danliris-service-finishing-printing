using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.PackingReceipt
{
    public class PackingReceiptItemViewModel : BaseViewModel, IValidatableObject
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
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
