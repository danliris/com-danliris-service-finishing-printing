using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.ColorReceipt
{
    public class ColorReceiptModel : StandardEntity, IValidatableObject
    {
        public ColorReceiptModel()
        {
            ColorReceiptItems = new HashSet<ColorReceiptItemModel>();
        }

        [MaxLength(512)]
        public string ColorName { get; set; }
        [MaxLength(2048)]
        public string ColorCode { get; set; }
        public int TechnicianId { get; set; }
        [MaxLength(512)]
        public string TechnicianName { get; set; }
        [MaxLength(4096)]
        public string Remark { get; set; }

        public ICollection<ColorReceiptItemModel> ColorReceiptItems { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
