using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.Daily_Operation
{
    public class DailyOperationModel
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
    }
}
