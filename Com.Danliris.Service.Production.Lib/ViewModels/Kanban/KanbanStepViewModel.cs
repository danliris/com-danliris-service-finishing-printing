using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.Machine;
using Com.Danliris.Service.Production.Lib.ViewModels.Master.Step;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Kanban
{
    public class KanbanStepViewModel : StepViewModel
    {
        public DateTimeOffset Deadline { get; set; }
        public int SelectedIndex { get; set; }
        public MachineViewModel Machine { get; set; }
    }
}
