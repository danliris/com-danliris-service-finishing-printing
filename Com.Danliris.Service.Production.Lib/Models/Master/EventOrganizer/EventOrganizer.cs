using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.EventOrganizer
{
    public class EventOrganizer : StandardEntity, IValidatableObject
    {
        public string Code { get; set; }

        [MaxLength(4096)]
        public string ProcessArea { get; set; }

        [MaxLength(4096)]
        public string Kasie { get; set; }
        [MaxLength(4096)]
        public string Kasubsie { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
