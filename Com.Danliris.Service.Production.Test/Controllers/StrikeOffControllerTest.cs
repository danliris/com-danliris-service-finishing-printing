using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.StrikeOff;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.StrikeOff;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.StrikeOff;
using Com.Danliris.Service.Finishing.Printing.Test.Controller.Utils;
using Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.StrikeOff;
using Com.Danliris.Service.Production.Lib.Utilities;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Controllers
{
    public class StrikeOffControllerTest : BaseControllerTest<StrikeOffController, StrikeOffModel, StrikeOffViewModel, IStrikeOffFacade>
    {
        [Fact]
        public void GetForConsumptionReceipt_WithoutException_ReturnOK()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(f => f.ReadForUsageReceipt(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new ReadResponse<StrikeOffConsumptionViewModel>(new List<StrikeOffConsumptionViewModel>(), 0, new Dictionary<string, string>(), new List<string>()));


            StrikeOffController controller = GetController(mocks);
            var response = controller.GetUsageReceipt();

            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public void GetForConsumptionReceipt_ReadThrowException_ReturnInternalServerError()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(f => f.ReadForUsageReceipt(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());

            StrikeOffController controller = GetController(mocks);
            var response = controller.GetUsageReceipt();

            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }
    }
}
