using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.Packing
{
    public class PackingDetailModel : StandardEntity, IValidatableObject
    {
        [MaxLength(250)]
        public string Lot { get; set; }
        [MaxLength(100)]
        public string Grade { get; set; }
        public int Weight { get; set; }
        public int Length { get; set; }
        public int Quantity { get; set; }
        [MaxLength(500)]
        public string Remark { get; set; }

        public int PackingId { get; set; }

        [ForeignKey("PackingId")]
        public virtual PackingModel Packing { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
