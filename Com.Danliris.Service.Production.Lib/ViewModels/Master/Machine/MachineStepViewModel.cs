using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.Machine
{
    public class MachineStepViewModel : BaseViewModel, IValidatableObject
    {
        public string Alias { get; set; }
        public string Code { get; set; }
        public string Process { get; set; }
        public string ProcessArea { get; set; }
        public int? StepId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
