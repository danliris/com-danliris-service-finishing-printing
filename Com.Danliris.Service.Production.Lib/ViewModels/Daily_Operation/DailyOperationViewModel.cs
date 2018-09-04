using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.Machine;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Danliris.Service.Production.Lib.ViewModels.Master.Step;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Daily_Operation
{
    public class DailyOperationViewModel : BaseViewModel, IValidatableObject
    {
        public string Code { get; set; }
        public string Type { get; set; }
        public string Shift { get; set; }
        public DateTimeOffset? DateInput { get; set; }
        public double? TimeInput { get; set; }
        public double? Input { get; set; }
        public DateTimeOffset? DateOutput { get; set; }
        public double? TimeOutput { get; set; }
        public double? GoodOutput { get; set; }
        public double? BadOutput { get; set; }

        //step
        public MachineStepViewModel Step { get; set; }

        //kanban
        public KanbanViewModel Kanban { get; set; }

        //machine
        public MachineViewModel Machine { get; set; }

        public ICollection<DailyOperationBadOutputReasonsViewModel> BadOutputReasons { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(this.Type))
                yield return new ValidationResult("harus diisi", new List<string> { "Type" });

            if (string.IsNullOrEmpty(this.Shift))
                yield return new ValidationResult("harus diisi", new List<string> { "Shift" });

            if (this.Type == "input")
            {
                if (this.DateInput == null)
                {
                    yield return new ValidationResult("harus diisi", new List<string> { "Shift" });
                }

                if (this.DateInput > DateTime.Now)
                {
                    yield return new ValidationResult("date input lebih dari hari ini", new List<string> { "DateInput" });
                }

                if (this.DateInput > DateTime.Now)
                {
                    yield return new ValidationResult("date input lebih dari hari ini", new List<string> { "DateInput" });
                }
            }
        }
    }
}
