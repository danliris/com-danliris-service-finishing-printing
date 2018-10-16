using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Kanban;
using Com.Danliris.Service.Finishing.Printing.Test.Controller.Utils;
using Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.Kanban;

namespace Com.Danliris.Service.Finishing.Printing.Test.Controllers
{
    public class KanbanControllerTest : BaseControllerTest<KanbanController, KanbanModel, KanbanViewModel, IKanbanFacade>
    {
    }
}
