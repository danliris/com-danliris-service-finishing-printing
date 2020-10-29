using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.EventOrganizer
{
 public   class EventOrganizerViewModel : BaseViewModel, IValidatableObject
    {
        public string Code { get; set; }
        public string ProcessArea { get; set; }

        public string Kasie { get; set; }
        public string Group { get; set; }

        public string Kasubsie { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(ProcessArea))
                yield return new ValidationResult("Area harus diisi", new List<string> { "ProcessArea" });

            if (string.IsNullOrEmpty(Kasie))
                yield return new ValidationResult("Kasie harus diisi", new List<string> { "Kasie" });

            if (string.IsNullOrEmpty(Kasubsie))
                yield return new ValidationResult("Kasubsie harus diisi", new List<string> { "Kasubsie" });

            if (string.IsNullOrEmpty(Group))
                yield return new ValidationResult("Group harus diisi", new List<string> { "Group" });
        }
    }
}
