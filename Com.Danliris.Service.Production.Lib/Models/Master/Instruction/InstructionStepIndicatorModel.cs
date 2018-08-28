using Com.Moonlay.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Production.Lib.Models.Master.Instruction
{
    public class InstructionStepIndicatorModel : StandardEntity, IValidatableObject
    {
        public string Name { get; set; }
        public string Uom { get; set; }
        public string Value { get; set; }
        public int StepId { get; set; }
        public virtual InstructionStepModel Step { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
