using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System.Collections.Generic;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Kanban
{
    public class KanbanInstructionViewModel : BaseViewModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public List<KanbanStepViewModel> Steps { get; set; }
    }
}
