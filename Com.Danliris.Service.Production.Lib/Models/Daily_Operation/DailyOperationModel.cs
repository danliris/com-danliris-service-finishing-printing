using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.Daily_Operation
{
    public class DailyOperationModel : StandardEntity, IValidatableObject
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
        public int StepId { get; set; }
        public int StepProcess { get; set; }

        //kanban
        public int KanbanId { get; set; }
        public int KanbanCode { get; set; }

        //machine
        public int MachineId { get; set; }
        public string MachineCode { get; set; }

        public ICollection<DailyOperationBadOutputReasonsModel> BadOutputReasons { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
