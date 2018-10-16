using Com.Danliris.Service.Finishing.Printing.Test.Controller.Utils;
using Com.Danliris.Service.Production.Lib.BusinessLogic.Interfaces.Master;
using Com.Danliris.Service.Production.Lib.Models.Master.Instruction;
using Com.Danliris.Service.Production.Lib.ViewModels.Master.Instruction;
using Com.Danliris.Service.Production.WebApi.Controllers.v1.Master;

namespace Com.Danliris.Service.Finishing.Printing.Test.Controllers
{
    public class InstructionControllerTest : BaseControllerTest<InstructionController, InstructionModel, InstructionViewModel, IInstructionFacade>
    {
    }
}
