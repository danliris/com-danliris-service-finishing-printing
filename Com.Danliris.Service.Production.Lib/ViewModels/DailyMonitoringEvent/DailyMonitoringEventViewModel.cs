using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.Machine;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DailyMonitoringEvent
{
    public class DailyMonitoringEventViewModel : BaseViewModel, IValidatableObject
    {
        public DailyMonitoringEventViewModel()
        {
            DailyMonitoringEventProductionOrderItems = new HashSet<DailyMonitoringEventProductionOrderItemViewModel>();
            DailyMonitoringEventLossEventItems = new HashSet<DailyMonitoringEventLossEventItemViewModel>();
        }
        public string Code { get; set; }

        public MachineViewModel Machine { get; set; }
        

        public DateTimeOffset? Date { get; set; }
        public string Shift { get; set; }
        public string Group { get; set; }

        public ProcessTypeIntegrationViewModel ProcessType { get; set; }

        public string ProcessArea { get; set; }

        public string Kasie { get; set; }
        public string Kasubsie { get; set; }
        public string ElectricMechanic { get; set; }
        public string Notes { get; set; }

        public IEnumerable<DailyMonitoringEventProductionOrderItemViewModel> DailyMonitoringEventProductionOrderItems { get; set; }
        public IEnumerable<DailyMonitoringEventLossEventItemViewModel> DailyMonitoringEventLossEventItems { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Machine == null || Machine.Id == 0)
                yield return new ValidationResult("Mesin harus diisi", new List<string> { "Machine" });

            if (!Date.HasValue)
                yield return new ValidationResult("Tanggal harus diisi", new List<string> { "Date" });

            if (string.IsNullOrEmpty(Shift))
                yield return new ValidationResult("Jam Kerja harus diisi", new List<string> { "Shift" });

            if (string.IsNullOrEmpty(Group))
                yield return new ValidationResult("Group harus diisi", new List<string> { "Group" });

            if (string.IsNullOrEmpty(ProcessArea))
                yield return new ValidationResult("Area harus diisi", new List<string> { "ProcessArea" });

            if (string.IsNullOrEmpty(Kasie))
                yield return new ValidationResult("Kasie harus diisi", new List<string> { "Kasie" });

            if (string.IsNullOrEmpty(Kasubsie))
                yield return new ValidationResult("Kasubsie harus diisi", new List<string> { "Kasubsie" });

            if (string.IsNullOrEmpty(ElectricMechanic))
                yield return new ValidationResult("Mekanik/Elektrik harus diisi", new List<string> { "ElectricMechanic" });
        }
    }
}
