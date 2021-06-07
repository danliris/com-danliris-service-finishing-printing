using Com.Danliris.Service.Finishing.Printing.Lib.Models.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.Machine;
using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.Daily_Operation
{
    public class DailyOperationModel : StandardEntity, IValidatableObject
    {
        [MaxLength(255)]
        public string UId { get; set; }
        [MaxLength(255)]
        public string Code { get; set; }
        [MaxLength(255)]
        public string Type { get; set; }
        [MaxLength(255)]
        public string Shift { get; set; }
        public DateTimeOffset? DateInput { get; set; }
        public double? TimeInput { get; set; }
        public double? Input { get; set; }
        public DateTimeOffset? DateOutput { get; set; }
        public double? TimeOutput { get; set; }
        public double? GoodOutput { get; set; }
        public double? BadOutput { get; set; }
        public int KanbanStepIndex { get; set; }

        //step
        public int StepId { get; set; }
        [MaxLength(255)]
        public string StepProcess { get; set; }

        //kanban
        public int KanbanId { get; set; }
        [MaxLength(255)]
        public string KanbanCode { get; set; }
        public virtual KanbanModel Kanban { get; set; }

        //machine
        public int MachineId { get; set; }
        [MaxLength(255)]
        public string MachineCode { get; set; }

        public virtual MachineModel Machine { get; set; }

        public virtual ICollection<DailyOperationBadOutputReasonsModel> BadOutputReasons { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
