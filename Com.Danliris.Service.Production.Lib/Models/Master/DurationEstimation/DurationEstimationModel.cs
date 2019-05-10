using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Production.Lib.Models.Master.DurationEstimation
{
    public class DurationEstimationModel : StandardEntity, IValidatableObject
    {
        [MaxLength(255)]
        public string UId { get; set; }
        public string Code { get; set; }
        public int OrderTypeId { get; set; }
        public string OrderTypeCode { get; set; }
        public string OrderTypeName { get; set; }
        public int ProcessTypeId { get; set; }
        public string ProcessTypeCode { get; set; }
        public string ProcessTypeName { get; set; }
        public ICollection<DurationEstimationAreaModel> Areas { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
