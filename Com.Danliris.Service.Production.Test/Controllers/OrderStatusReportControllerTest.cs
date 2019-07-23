using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.OrderStatusReport;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.OrderStatusReports;
using Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.OrderStatusReport;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Controllers
{
    public class OrderStatusReportControllerTest
    {
        protected OrderStatusController GetController(Mock<IIdentityService> identityServiceMock, Mock<IOrderStatusReportService> serviceMock)
        {
            var user = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            user.Setup(u => u.Claims).Returns(claims);
            OrderStatusController controller = (OrderStatusController)Activator.CreateInstance(typeof(OrderStatusController), identityServiceMock.Object, serviceMock.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = user.Object
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["Authorization"] = "Bearer unittesttoken";
            controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = "0";
            controller.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");
            return controller;
        }

        [Fact]
        public async Task GetYearlyOrderStatusReport_Ok()
        {
            var identityServiceMock = new Mock<IIdentityService>();
            var serviceMock = new Mock<IOrderStatusReportService>();

            serviceMock.Setup(service => service.GetYearlyOrderStatusReport(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new List<YearlyOrderStatusReportViewModel>());

            var controller = GetController(identityServiceMock, serviceMock);

            var response = await controller.GetYearlyReport(0, 0);

            Assert.Equal((int)HttpStatusCode.OK, (int)response.GetType().GetProperty("StatusCode").GetValue(response, null));
        }

        [Fact]
        public async Task GetYearlyOrderStatusReport_FailInternalServerError()
        {
            var identityServiceMock = new Mock<IIdentityService>();
            var serviceMock = new Mock<IOrderStatusReportService>();

            serviceMock.Setup(service => service.GetYearlyOrderStatusReport(It.IsAny<int>(), It.IsAny<int>())).ThrowsAsync(new Exception());

            var controller = GetController(identityServiceMock, serviceMock);

            var response = await controller.GetYearlyReport(0, 0);

            Assert.Equal((int)HttpStatusCode.InternalServerError, (int)response.GetType().GetProperty("StatusCode").GetValue(response, null));
        }

        [Fact]
        public async Task GetYearlyOrderStatusReportExcel_Ok()
        {
            var identityServiceMock = new Mock<IIdentityService>();
            var serviceMock = new Mock<IOrderStatusReportService>();

            serviceMock.Setup(service => service.GetYearlyOrderStatusReportExcel(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new MemoryStream());

            var controller = GetController(identityServiceMock, serviceMock);

            var response = await controller.GetYearlyReportXls(0, 0);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetYearlyOrderStatusReportExcel_FailInternalServerError()
        {
            var identityServiceMock = new Mock<IIdentityService>();
            var serviceMock = new Mock<IOrderStatusReportService>();

            serviceMock.Setup(service => service.GetYearlyOrderStatusReportExcel(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ThrowsAsync(new Exception());

            var controller = GetController(identityServiceMock, serviceMock);

            var response = await controller.GetYearlyReportXls(0, 0);

            Assert.Equal((int)HttpStatusCode.InternalServerError, (int)response.GetType().GetProperty("StatusCode").GetValue(response, null));
        }

        [Fact]
        public async Task GetMonthlyOrderStatusReport_Ok()
        {
            var identityServiceMock = new Mock<IIdentityService>();
            var serviceMock = new Mock<IOrderStatusReportService>();

            serviceMock.Setup(service => service.GetMonthlyOrderStatusReport(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new List<MonthlyOrderStatusReportViewModel>());

            var controller = GetController(identityServiceMock, serviceMock);

            var response = await controller.GetMonthlyReport(0, 0, 0);

            Assert.Equal((int)HttpStatusCode.OK, (int)response.GetType().GetProperty("StatusCode").GetValue(response, null));
        }

        [Fact]
        public async Task GetMonthlyOrderStatusReport_FailInternalServerError()
        {
            var identityServiceMock = new Mock<IIdentityService>();
            var serviceMock = new Mock<IOrderStatusReportService>();

            serviceMock.Setup(service => service.GetMonthlyOrderStatusReport(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ThrowsAsync(new Exception());

            var controller = GetController(identityServiceMock, serviceMock);

            var response = await controller.GetMonthlyReport(0, 0, 0);

            Assert.Equal((int)HttpStatusCode.InternalServerError, (int)response.GetType().GetProperty("StatusCode").GetValue(response, null));
        }

        [Fact]
        public async Task GetMonthlyOrderStatusReportExcel_Ok()
        {
            var identityServiceMock = new Mock<IIdentityService>();
            var serviceMock = new Mock<IOrderStatusReportService>();

            serviceMock.Setup(service => service.GetMonthlyOrderStatusReportExcel(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new MemoryStream());

            var controller = GetController(identityServiceMock, serviceMock);

            var response = await controller.GetMonthlyReportXls(0, 0, 0);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetMonthlyOrderStatusReportExcel_FailInternalServerError()
        {
            var identityServiceMock = new Mock<IIdentityService>();
            var serviceMock = new Mock<IOrderStatusReportService>();

            serviceMock.Setup(service => service.GetMonthlyOrderStatusReportExcel(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ThrowsAsync(new Exception());

            var controller = GetController(identityServiceMock, serviceMock);

            var response = await controller.GetMonthlyReportXls(0, 0, 0);

            Assert.Equal((int)HttpStatusCode.InternalServerError, (int)response.GetType().GetProperty("StatusCode").GetValue(response, null));
        }

        [Fact]
        public async Task GetOrderStatusReportByOrderId_Ok()
        {
            var identityServiceMock = new Mock<IIdentityService>();
            var serviceMock = new Mock<IOrderStatusReportService>();

            serviceMock.Setup(service => service.GetProductionOrderStatusReport(It.IsAny<int>())).ReturnsAsync(new List<ProductionOrderStatusReportViewModel>());

            var controller = GetController(identityServiceMock, serviceMock);

            var response = await controller.GetByOrderId(0);

            Assert.Equal((int)HttpStatusCode.OK, (int)response.GetType().GetProperty("StatusCode").GetValue(response, null));
        }

        [Fact]
        public async Task GetOrderStatusReportByOrderId_FailInternalServerError()
        {
            var identityServiceMock = new Mock<IIdentityService>();
            var serviceMock = new Mock<IOrderStatusReportService>();

            serviceMock.Setup(service => service.GetProductionOrderStatusReport(It.IsAny<int>())).ThrowsAsync(new Exception());

            var controller = GetController(identityServiceMock, serviceMock);

            var response = await controller.GetByOrderId(0);

            Assert.Equal((int)HttpStatusCode.InternalServerError, (int)response.GetType().GetProperty("StatusCode").GetValue(response, null));
        }

        [Fact]
        public async Task GetOrderStatusReportByOrderIdExcel_Ok()
        {
            var identityServiceMock = new Mock<IIdentityService>();
            var serviceMock = new Mock<IOrderStatusReportService>();

            serviceMock.Setup(service => service.GetProductionOrderStatusReportExcel(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new MemoryStream());

            var controller = GetController(identityServiceMock, serviceMock);

            var response = await controller.GetByOrderIdXls(0);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetOrderStatusReportByOrderIdExcel_FailInternalServerError()
        {
            var identityServiceMock = new Mock<IIdentityService>();
            var serviceMock = new Mock<IOrderStatusReportService>();

            serviceMock.Setup(service => service.GetProductionOrderStatusReportExcel(It.IsAny<int>(), It.IsAny<int>())).ThrowsAsync(new Exception());

            var controller = GetController(identityServiceMock, serviceMock);

            var response = await controller.GetByOrderIdXls(0);

            Assert.Equal((int)HttpStatusCode.InternalServerError, (int)response.GetType().GetProperty("StatusCode").GetValue(response, null));
        }
    }
}
