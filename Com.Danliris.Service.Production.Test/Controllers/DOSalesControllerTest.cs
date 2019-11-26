using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.DOSales;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.DOSales;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DOSales;
using Com.Danliris.Service.Finishing.Printing.Test.Controller.Utils;
using Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.DOSales;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Services.ValidateService;
using Com.Danliris.Service.Production.Lib.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Controllers
{
    public class DOSalesControllerTest : BaseControllerTest<DOSalesController, DOSalesModel, DOSalesViewModel, IDOSalesFacade>
    {
        [Fact]
        public void GetReport_WithoutException_ReturnOK()
        {
            var mockFacade = new Mock<IDOSalesFacade>();
            mockFacade.Setup(x => x.GetReport(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<int>()))
                .Returns(new ReadResponse<DOSalesViewModel>(new List<DOSalesViewModel>(), 0, new Dictionary<string, string>(), new List<string>()));

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();

            DOSalesController controller = new DOSalesController(mockIdentityService.Object, mockValidateService.Object, mockFacade.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = $"{It.IsAny<int>()}";

            var response = controller.GetReport(It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<string>(), It.IsAny<int>());
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void GetReport_WithException_ReturnError()
        {
            var mockFacade = new Mock<IDOSalesFacade>();
            mockFacade.Setup(x => x.GetReport(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<int>()))
                .Throws(new Exception());

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();

            DOSalesController controller = new DOSalesController(mockIdentityService.Object, mockValidateService.Object, mockFacade.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = $"{It.IsAny<int>()}";

            var response = controller.GetReport(It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<string>(), It.IsAny<int>());
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async void GetDOSalesDetail_OK()
        {
            var mocks = GetMocks();
            DOSalesModel model = new DOSalesModel()
            {
                DOSalesDetails = new List<DOSalesDetailModel>() { new DOSalesDetailModel() { DOSales = new DOSalesModel()} }

            };
            mocks.Mapper.Setup(f => f.Map<List<DOSalesViewModel>>(It.IsAny<List<DOSalesModel>>())).Returns(ViewModels);
            mocks.Facade.Setup(x => x.GetDOSalesDetail(It.IsAny<string>())).Returns(Task.FromResult(model.DOSalesDetails.FirstOrDefault()));

            var controller = GetController(mocks);

            var response = await controller.GetDOSalesDetail("");
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async void GetDOSalesDetail_NotFound()
        {
            var mocks = GetMocks();
            DOSalesModel model = new DOSalesModel()
            {
                DOSalesDetails = new List<DOSalesDetailModel>() { new DOSalesDetailModel() { DOSales = new DOSalesModel() } }

            };
            mocks.Mapper.Setup(f => f.Map<List<DOSalesViewModel>>(It.IsAny<List<DOSalesModel>>())).Returns(ViewModels);
            mocks.Facade.Setup(x => x.GetDOSalesDetail(It.IsAny<string>())).Returns(Task.FromResult(default(DOSalesDetailModel)));

            var controller = GetController(mocks);

            var response = await controller.GetDOSalesDetail("");
            Assert.Equal((int)HttpStatusCode.NotFound, GetStatusCode(response));
        }

        [Fact]
        public async void GetDOSalesDetail_Exception()
        {
            var mocks = GetMocks();
            DOSalesModel model = new DOSalesModel()
            {
                DOSalesDetails = new List<DOSalesDetailModel>() { new DOSalesDetailModel() { DOSales = new DOSalesModel() } }

            };
            mocks.Mapper.Setup(f => f.Map<List<DOSalesViewModel>>(It.IsAny<List<DOSalesModel>>())).Returns(ViewModels);
            mocks.Facade.Setup(x => x.GetDOSalesDetail(It.IsAny<string>())).Throws(new Exception(""));

            var controller = GetController(mocks);

            var response = await controller.GetDOSalesDetail("");
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
