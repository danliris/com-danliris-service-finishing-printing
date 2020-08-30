using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.LossEventCategory
{
    public class LossEventCategoryModel : StandardEntity, IValidatableObject
    {
        [MaxLength(16)]
        public string Code { get; set; }

        public int LossEventProcessTypeId { get; set; }
        [MaxLength(512)]
        public string LossEventProcessTypeCode { get; set; }
        [MaxLength(2048)]
        public string LossEventProcessTypeName { get; set; }

        public int LossEventOrderTypeId { get; set; }
        [MaxLength(512)]
        public string LossEventOrderTypeCode { get; set; }
        [MaxLength(2048)]
        public string LossEventOrderTypeName { get; set; }

        public int LossEventId { get; set; }
        [MaxLength(16)]
        public string LossEventCode { get; set; }
        [MaxLength(4096)]
        public string LossEventLosses { get; set; }

        [MaxLength(4096)]
        public string LossesCategory { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
