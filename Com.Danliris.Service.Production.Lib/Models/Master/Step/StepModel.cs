using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Production.Lib.Models.Master.Step
{
    public class StepModel : StandardEntity, IValidatableObject
    {
        [MaxLength(255)]
        public string UId { get; set; }
        public string Alias { get; set; }
        public string Code { get; set; }
        public string Process { get; set; }
        public string ProcessArea { get; set; }
        public ICollection<StepIndicatorModel> StepIndicators { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
