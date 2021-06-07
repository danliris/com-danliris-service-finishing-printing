using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.Machine;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.Machine;
using Com.Danliris.Service.Finishing.Printing.Test.Controller.Utils;
using Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.Master;
using Com.Danliris.Service.Production.Lib.Utilities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Controllers
{
    public class MachineControllerTest : BaseControllerTest<MachineController, MachineModel, MachineViewModel, IMachineFacade>
    {
        [Fact]
        public virtual void GetDyeingPrinting_WithoutException_ReturnOK()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(f => f.GetDyeingPrintingMachine(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>())).Returns(new ReadResponse<MachineModel>(new List<MachineModel>(), 0, new Dictionary<string, string>(), new List<string>()));
            mocks.Mapper.Setup(f => f.Map<List<MachineViewModel>>(It.IsAny<List<MachineModel>>())).Returns(ViewModels);
            MachineController controller = GetController(mocks);
            IActionResult response = controller.GetDyeingPrintingMachine();

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public virtual void GetDyeingPrinting_ReadThrowException_ReturnInternalServerError()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(f => f.GetDyeingPrintingMachine(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());

            MachineController controller = GetController(mocks);
            IActionResult response = controller.GetDyeingPrintingMachine();
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
