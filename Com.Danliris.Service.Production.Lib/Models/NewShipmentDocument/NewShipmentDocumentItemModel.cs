using Com.Moonlay.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.NewShipmentDocument
{
    public class NewShipmentDocumentItemModel : StandardEntity, IValidatableObject
    {
        [MaxLength(250)]
        public string PackingReceiptCode { get; set; }
        public int PackingReceiptId { get; set; }
        public ICollection<NewShipmentDocumentPackingReceiptItemModel> PackingReceiptItems { get; set; }
        [MaxLength(250)]
        public string ReferenceNo { get; set; }
        [MaxLength(250)]
        public string ReferenceType { get; set; }
        public int ShipmentDocumentDetailId { get; set; }

        public int ItemIndex { get; set; }

        [ForeignKey("ShipmentDocumentDetailId")]
        public virtual NewShipmentDocumentDetailModel NewShipmentDocumentDetail { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new System.NotImplementedException();
        }
    }
}
