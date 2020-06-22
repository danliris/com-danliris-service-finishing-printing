using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.ColorReceipt
{
    public class TechnicianModel : StandardEntity, IValidatableObject
    {
        [MaxLength(512)]
        public string Name { get; set; }
        public bool IsDefault { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
