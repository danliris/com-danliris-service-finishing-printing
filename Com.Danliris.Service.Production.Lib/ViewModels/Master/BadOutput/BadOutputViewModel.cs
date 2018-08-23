using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.BadOutput
{
    public class BadOutputViewModel : BaseViewModel, IValidatableObject
    {
        public string Code { get; set; }
        public string Reason { get; set; }
        public ICollection<BadOutputMachineViewModel> MachineDetails { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Reason))
                yield return new ValidationResult("harus diisi", new List<string> { "Reason" });
        }
    }
}
