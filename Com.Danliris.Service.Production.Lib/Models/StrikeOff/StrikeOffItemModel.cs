using Com.Danliris.Service.Finishing.Printing.Lib.Models.ColorReceipt;
using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.StrikeOff
{
    public class StrikeOffItemModel : StandardEntity, IValidatableObject
    {
        public StrikeOffItemModel()
        {
            ColorReceiptItems = new HashSet<ColorReceiptItemModel>();
        }

        public int ColorReceiptId { get; set; }
        [MaxLength(2048)]
        public string ColorReceiptColorCode { get; set; }

        public int StrikeOffId { get; set; }

        [ForeignKey("StrikeOffId")]
        public StrikeOffModel StrikeOff { get; set; }

        [NotMapped]
        public ICollection<ColorReceiptItemModel> ColorReceiptItems { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
