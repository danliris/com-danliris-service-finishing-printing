using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.MachineType;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Danliris.Service.Production.Lib.ViewModels.Master.Step;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.Machine
{
    public class MachineViewModel : BaseViewModel, IValidatableObject
    {
        public string UId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Process { get; set; }
        public string Manufacture { get; set; }
        public int? Year { get; set; }
        public string Condition { get; set; }
        public int? MonthlyCapacity { get; set; }

        public UnitViewModel Unit { get; set; }
        public MachineTypeViewModel MachineType { get; set; }
        public ICollection<MachineEventViewModel> MachineEvents { get; set; }
        public ICollection<MachineStepViewModel> MachineSteps { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Name))
                yield return new ValidationResult("Nama harus diisi", new List<string> { "Name" });

        }
    }
}
