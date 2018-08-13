using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Production.Lib.Models.Master.DurationEstimation
{
    public class DurationEstimationAreaModel : StandardEntity, IValidatableObject
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public int DurationEstimationId { get; set; }
        public virtual DurationEstimationModel DurationEstimation { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
