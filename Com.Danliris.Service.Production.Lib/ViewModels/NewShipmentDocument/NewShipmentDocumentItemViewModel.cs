using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.NewShipmentDocument
{
    public class NewShipmentDocumentItemViewModel : BaseViewModel, IValidatableObject
    {
        public string PackingReceiptCode { get; set; }
        public int? PackingReceiptId { get; set; }
        public List<NewShipmentDocumentPackingReceiptItemViewModel> PackingReceiptItems { get; set; }
        public string ReferenceNo { get; set; }
        public string ReferenceType { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new System.NotImplementedException();
        }
    }
}