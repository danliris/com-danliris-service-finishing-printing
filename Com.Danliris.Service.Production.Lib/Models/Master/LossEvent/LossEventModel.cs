using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.LossEvent
{
    public class LossEventModel : StandardEntity, IValidatableObject
    {
        [MaxLength(16)]
        public string Code { get; set; }
        public int ProcessTypeId { get; set; }
        [MaxLength(512)]
        public string ProcessTypeCode { get; set; }
        [MaxLength(2048)]
        public string ProcessTypeName { get; set; }

        [MaxLength(4096)]
        public string ProcessArea { get; set; }

        public int OrderTypeId { get; set; }
        [MaxLength(512)]
        public string OrderTypeCode { get; set; }
        [MaxLength(2048)]
        public string OrderTypeName { get; set; }

        [MaxLength(4096)]
        public string Losses { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
