using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.FabricQualityControl;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.FabricQualityControl;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.FabricQualityControl;
using Com.Danliris.Service.Finishing.Printing.Test.Controller.Utils;
using Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.FabricQualityControl;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Services.ValidateService;
using Com.Danliris.Service.Production.Lib.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Controllers
{
    public class FabricQualityControllerTest : BaseControllerTest<FabricQualityControlController, FabricQualityControlModel, FabricQualityControlViewModel, IFabricQualityControlFacade>
    {
        [Fact]
        public void GetReportPdf_WithoutException_ReturnOK()
        {
            var mockFacade = new Mock<IFabricQualityControlFacade>();
            mockFacade.Setup(f => f.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();
            FabricQualityControlViewModel vm = new FabricQualityControlViewModel()
            {
                DateIm = DateTimeOffset.UtcNow,
                FabricGradeTests = new List<FabricGradeTestViewModel>()
                {
                    new FabricGradeTestViewModel()
                }
            };
            mockMapper.Setup(x => x.Map<FabricQualityControlViewModel>(It.IsAny<FabricQualityControlModel>()))
                .Returns(vm);

            FabricQualityControlController controller = new FabricQualityControlController(mockIdentityService.Object, mockValidateService.Object, mockFacade.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = $"{It.IsAny<int>()}";

            var response = controller.GetPDF(It.IsAny<int>());
            Assert.NotNull(response.Result);
        }

        [Fact]
        public void GetReportPdf_WithException_ReturnError()
        {
            var mockFacade = new Mock<IFabricQualityControlFacade>();
            mockFacade.Setup(f => f.ReadByIdAsync(It.IsAny<int>()))
                 .Throws(new Exception());

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();

            FabricQualityControlController controller = new FabricQualityControlController(mockIdentityService.Object, mockValidateService.Object, mockFacade.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = $"{It.IsAny<int>()}";

            var response = controller.GetPDF(It.IsAny<int>());
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response.Result));
        }

        [Fact]
        public void GetReport_WithoutException_ReturnOK()
        {
            var mockFacade = new Mock<IFabricQualityControlFacade>();
            mockFacade.Setup(x => x.GetReport(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<int>()))
                .Returns(new ReadResponse<FabricQualityControlViewModel>(new List<FabricQualityControlViewModel>(), 0, new Dictionary<string, string>(), new List<string>()));

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();

            FabricQualityControlController controller = new FabricQualityControlController(mockIdentityService.Object, mockValidateService.Object, mockFacade.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = $"{It.IsAny<int>()}";

            var response = controller.GetReport(It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void GetReport_WithException_ReturnError()
        {
            var mockFacade = new Mock<IFabricQualityControlFacade>();
            mockFacade.Setup(x => x.GetReport(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<int>()))
                .Throws(new Exception());

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();

            FabricQualityControlController controller = new FabricQualityControlController(mockIdentityService.Object, mockValidateService.Object, mockFacade.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = $"{It.IsAny<int>()}";

            var response = controller.GetReport(It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void GetReportExcel_WithoutException_ReturnOK()
        {
            var mockFacade = new Mock<IFabricQualityControlFacade>();
            mockFacade.Setup(x => x.GenerateExcel(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<int>()))
                .Returns(new MemoryStream());

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();

            FabricQualityControlController controller = new FabricQualityControlController(mockIdentityService.Object, mockValidateService.Object, mockFacade.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = $"{It.IsAny<int>()}";

            var response = controller.GetXls(It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
            Assert.NotNull(response);
        }

        [Fact]
        public void GetReportExcel_WithException_ReturnError()
        {
            var mockFacade = new Mock<IFabricQualityControlFacade>();
            mockFacade.Setup(x => x.GenerateExcel(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<int>()))
                .Throws(new Exception());

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();

            FabricQualityControlController controller = new FabricQualityControlController(mockIdentityService.Object, mockValidateService.Object, mockFacade.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = $"{It.IsAny<int>()}";

            var response = controller.GetXls(It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>());
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }


        [Fact]
        public async Task GetForSPP_WithoutException_ReturnOK()
        {
            var mockFacade = new Mock<IFabricQualityControlFacade>();
            mockFacade.Setup(x => x.GetForSPP(It.IsAny<string>()))
                .ReturnsAsync(new List<FabricQCGradeTestsViewModel>());

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();

            FabricQualityControlController controller = new FabricQualityControlController(mockIdentityService.Object, mockValidateService.Object, mockFacade.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };

            var response = await controller.GetForSPP(It.IsAny<string>());
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task GetForSPP_WithException_ThrowError()
        {
            var mockFacade = new Mock<IFabricQualityControlFacade>();
            mockFacade.Setup(x => x.GetForSPP(It.IsAny<string>()))
                .Throws(new Exception());

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();

            FabricQualityControlController controller = new FabricQualityControlController(mockIdentityService.Object, mockValidateService.Object, mockFacade.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };

            var response = await controller.GetForSPP(It.IsAny<string>());
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
