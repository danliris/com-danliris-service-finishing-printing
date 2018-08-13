using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.Machine
{
    public class MachineStepModel : StandardEntity, IValidatableObject
    {
        public int StepId { get; set; }
        public string Alias { get; set; }
        public string Code { get; set; }
        public string Process { get; set; }
        public string ProcessArea { get; set; }
        public int MachineId { get; set; }
        public virtual MachineModel Machine { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
