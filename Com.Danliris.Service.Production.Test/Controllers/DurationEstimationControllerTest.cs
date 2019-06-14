using Com.Danliris.Service.Finishing.Printing.Test.Controller.Utils;
using Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.Master;
using Com.Danliris.Service.Production.Lib.BusinessLogic.Interfaces.Master;
using Com.Danliris.Service.Production.Lib.Models.Master.DurationEstimation;
using Com.Danliris.Service.Production.Lib.ViewModels.Master.DurationEstimation;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Controllers
{
    public class DurationEstimationControllerTest : BaseControllerTest<DurationEstimationController, DurationEstimationModel, DurationEstimationViewModel, IDurationEstimationFacade>
    {
        [Fact]
        public void GetByProcessTypeCode_OK()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(s => s.ReadByProcessType(It.IsAny<string>())).Returns(new DurationEstimationModel());
            mocks.Mapper.Setup(s => s.Map<DurationEstimationViewModel>(It.IsAny<DurationEstimationModel>())).Returns(new DurationEstimationViewModel());

            var result = GetController(mocks).GetByProcessType("a");
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(result));
        }

        [Fact]
        public void GetByProcessTypeCode_Exception()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(s => s.ReadByProcessType(It.IsAny<string>())).Throws(new Exception("err"));
            mocks.Mapper.Setup(s => s.Map<DurationEstimationViewModel>(It.IsAny<DurationEstimationModel>())).Returns(new DurationEstimationViewModel());

            var result = GetController(mocks).GetByProcessType("a");
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(result));
        }
    }
}
