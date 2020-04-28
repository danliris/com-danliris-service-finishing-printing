using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.ColorReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.ColorReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.ColorReceipt;
using Com.Danliris.Service.Finishing.Printing.Test.Controller.Utils;
using Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.ColorReceipt;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Controllers
{
    public class ColorReceiptControllerTest : BaseControllerTest<ColorReceiptController, ColorReceiptModel, ColorReceiptViewModel, IColorReceiptFacade>
    {
        public override async Task Post_WithoutException_ReturnCreated()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(s => s.Validate(It.IsAny<ColorReceiptViewModel>())).Verifiable();
            mocks.Facade.Setup(s => s.CreateAsync(It.IsAny<ColorReceiptModel>())).ReturnsAsync(1);
            mocks.Facade.Setup(s => s.CreateTechnician(It.IsAny<string>())).ReturnsAsync(new TechnicianModel());
            ColorReceiptController controller = GetController(mocks);
            var vm = ViewModel;
            vm.Technician = new TechnicianViewModel()
            {
                Id = 1,
                Name = "test"
            };
            vm.ChangeTechnician = true;
            var response = await controller.Post(vm);

            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.Created, statusCode);
        }

        [Fact]
        public virtual async Task GetDefaultTechnician_NotNullModel_ReturnOK()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(f => f.GetDefaultTechnician()).ReturnsAsync(new TechnicianModel());

            var controller = GetController(mocks);
            var response = await controller.GetDefaultTechnician();

            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.OK, statusCode);
        }


        [Fact]
        public virtual async Task GetDefaultTechnician_ThrowException_ReturnInternalServerError()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(f => f.GetDefaultTechnician()).ThrowsAsync(new Exception());

            var controller = GetController(mocks);
            var response = await controller.GetDefaultTechnician();

            int statusCode = GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);
        }
    }
}
