using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.DirectLaborCost;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.DirectLaborCost;
using Com.Danliris.Service.Finishing.Printing.Test.Controller.Utils;
using Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.Master;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Controllers
{
    public class DirectLaborCostControllerTest : BaseControllerTest<DirectLaborCostController, DirectLaborCostModel, DirectLaborCostViewModel, IDirectLaborCostFacade>
    {
        [Fact]
        public async Task GetCostCalculation_Return_OK()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(s => s.GetForCostCalculation(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new DirectLaborCostModel());
            mocks.Mapper.Setup(s => s.Map<DirectLaborCostViewModel>(It.IsAny<DirectLaborCostModel>())).Returns(new DirectLaborCostViewModel());

            var result = await GetController(mocks).Get(1, 1);
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(result));
        }

        [Fact]
        public async Task GetCostCalculation_Return_InternalServerError()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(s => s.GetForCostCalculation(It.IsAny<int>(), It.IsAny<int>())).Throws(new Exception());
            mocks.Mapper.Setup(s => s.Map<DirectLaborCostViewModel>(It.IsAny<DirectLaborCostModel>())).Returns(new DirectLaborCostViewModel());

            var result = await GetController(mocks).Get(1, 1);
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(result));
        }
    }
}
