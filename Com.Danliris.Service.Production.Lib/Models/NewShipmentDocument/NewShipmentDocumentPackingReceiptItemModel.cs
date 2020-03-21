using Com.Moonlay.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.NewShipmentDocument
{
    public class NewShipmentDocumentPackingReceiptItemModel : StandardEntity, IValidatableObject
    {
        [MaxLength(250)]
        public string ColorType { get; set; }
        [MaxLength(250)]
        public string DesignCode { get; set; }
        [MaxLength(250)]
        public string DesignNumber { get; set; }
        public double Length { get; set; }
        [MaxLength(250)]
        public string ProductCode { get; set; }
        public int ProductId { get; set; }
        [MaxLength(500)]
        public string ProductName { get; set; }
        public double Quantity { get; set; }
        [MaxLength(1000)]
        public string Remark { get; set; }
        public int UOMId { get; set; }
        [MaxLength(250)]
        public string UOMUnit { get; set; }
        public double Weight { get; set; }
        public int ShipmentDocumentItemId { get; set; }

        public int PackingReceiptItemIndex { get; set; }

        [ForeignKey("ShipmentDocumentItemId")]
        public virtual NewShipmentDocumentItemModel ShipmentDocumentItem { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new System.NotImplementedException();
        }
    }
}
