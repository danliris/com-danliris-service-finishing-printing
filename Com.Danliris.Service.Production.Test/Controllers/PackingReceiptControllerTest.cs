using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.PackingReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.PackingReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.PackingReceipt;
using Com.Danliris.Service.Finishing.Printing.Test.Controller.Utils;
using Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.PackingReceipt;

namespace Com.Danliris.Service.Finishing.Printing.Test.Controllers
{
    public class PackingReceiptControllerTest : BaseControllerTest<PackingReceiptController, PackingReceiptModel, PackingReceiptViewModel, IPackingReceiptFacade>
    {
    }
}
