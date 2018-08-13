using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.Machine
{
    public class MachineEventViewModel : BaseViewModel, IValidatableObject
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string No { get; set; }
        public string Category { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
