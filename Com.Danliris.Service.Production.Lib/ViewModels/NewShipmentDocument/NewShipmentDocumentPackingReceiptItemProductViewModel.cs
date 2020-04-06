using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.NewShipmentDocument
{
    public class NewShipmentDocumentPackingReceiptItemProductViewModel : BaseViewModel, IValidatableObject
    {
        public string ProductName { get; set; }
        public double? Quantity { get; set; }
        public double? Length { get; set; }
        public int? UOMId { get; set; }
        public string UOMUnit { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new System.NotImplementedException();
        }
    }
}
