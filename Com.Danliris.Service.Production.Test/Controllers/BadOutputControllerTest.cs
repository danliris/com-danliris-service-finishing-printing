using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.BadOutput;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.BadOutput;
using Com.Danliris.Service.Finishing.Printing.Test.Controller.Utils;
using Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.Master;

namespace Com.Danliris.Service.Finishing.Printing.Test.Controllers
{
    public class BadOutputControllerTest : BaseControllerTest<BadOutputController, BadOutputModel, BadOutputViewModel, IBadOutputFacade>
    {
    }
}
