using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Com.Danliris.Service.Production.Lib.Models.Master.Step
{
    public class StepIndicatorModel : StandardEntity, IValidatableObject
    {
        public string Name { get; set; }
        public string Uom { get; set; }
        public double Value { get; set; }
        public int StepId { get; set; }
        public virtual StepModel Step { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}