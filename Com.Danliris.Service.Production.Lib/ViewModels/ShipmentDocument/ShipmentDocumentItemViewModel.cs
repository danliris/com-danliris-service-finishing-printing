using Com.Danliris.Service.Finishing.Printing.Lib.Models.ShipmentDocument;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.ShipmentDocument
{
    public class ShipmentDocumentItemViewModel : BaseViewModel, IValidatableObject
    {
        public string PackingReceiptCode { get; set; }
        public int? PackingReceiptId { get; set; }
        public List<ShipmentDocumentPackingReceiptItemViewModel> PackingReceiptItems { get; set; }
        public string ReferenceNo { get; set; }
        public string ReferenceType { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new System.NotImplementedException();
        }
    }
}