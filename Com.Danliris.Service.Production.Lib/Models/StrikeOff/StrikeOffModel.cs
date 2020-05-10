using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.StrikeOff
{
    public class StrikeOffModel : StandardEntity, IValidatableObject
    {

        public StrikeOffModel()
        {
            StrikeOffItems = new HashSet<StrikeOffItemModel>();
        }

        [MaxLength(128)]
        public string Code { get; set; }
        [MaxLength(4096)]
        public string Remark { get; set; }

        public ICollection<StrikeOffItemModel> StrikeOffItems { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
