using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.ShipmentDocument
{
    public class ShipmentDocumentPackingReceiptItemViewModel : BaseViewModel, IValidatableObject
    {
        public string ColorType { get; set; }
        public string DesignCode { get; set; }
        public string DesignNumber { get; set; }
        public double? Length { get; set; }
        public string ProductCode { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public double? Quantity { get; set; }
        public string Remark { get; set; }
        public int? UOMId { get; set; }
        public string UOMUnit { get; set; }
        public double? Weight { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new System.NotImplementedException();
        }
    }
}