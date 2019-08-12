using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.MachineType;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Danliris.Service.Production.Lib.ViewModels.Master.Step;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public double Electric { get; set; }
        public double Steam { get; set; }
        public double Water { get; set; }
        public double Solar { get; set; }
        public double LPG { get; set; }

        public UnitViewModel Unit { get; set; }
        public MachineTypeViewModel MachineType { get; set; }
        public ICollection<MachineEventViewModel> MachineEvents { get; set; }
        public ICollection<StepViewModel> Steps { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrWhiteSpace(Name))
                yield return new ValidationResult("Nama harus diisi", new List<string> { "Name" });

            if (string.IsNullOrEmpty(Process) || string.IsNullOrWhiteSpace(Process))
                yield return new ValidationResult("Proses harus diisi", new List<string> { "Process" });
        }
    }
}
