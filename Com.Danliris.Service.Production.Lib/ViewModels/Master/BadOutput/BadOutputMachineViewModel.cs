using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.BadOutput
{
    public class BadOutputMachineViewModel : BaseViewModel, IValidatableObject
    {
        public int MachineId { get; set; }
        public string MachineName { get; set; }
        public string MachineCode { get; set; }

        public int BadOutputId { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
