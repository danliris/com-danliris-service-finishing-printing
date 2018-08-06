using Com.Moonlay.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Production.Lib.Models.Master.Instruction
{
    public class InstructionStepModel : StandardEntity, IValidatableObject
    {
        public string Alias { get; set; }
        public string Code { get; set; }
        public string Process { get; set; }
        public string ProcessArea { get; set; }
        public int InstructionId { get; set; }
        public virtual InstructionModel Instruction { get; set; }
        public ICollection<InstructionStepIndicatorModel> StepIndicators { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
