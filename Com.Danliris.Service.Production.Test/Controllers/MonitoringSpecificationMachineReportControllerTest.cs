using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.MonitoringSpecificationMachine;
using Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.MonitoringSpecificationMachine;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Controllers
{
    public class MonitoringSpecificationMachineReportControllerTest
    {
        protected MonitoringSpecificationMachineReportController GetController(Mock<IServiceProvider> serviceProviderMock, Mock<IMonitoringSpecificationMachineReportFacade> serviceMock, Mock<IMapper> mapperMock)
        {
            var user = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            user.Setup(u => u.Claims).Returns(claims);
            MonitoringSpecificationMachineReportController controller = (MonitoringSpecificationMachineReportController)Activator.CreateInstance(typeof(MonitoringSpecificationMachineReportController), serviceProviderMock.Object, mapperMock.Object, serviceMock.Object);
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
        public void GetReportAll_Ok()
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            var serviceMock = new Mock<IMonitoringSpecificationMachineReportFacade>();
            var mapperMock = new Mock<IMapper>();

            serviceMock.Setup(service => service.GetReport(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime?>(),
                It.IsAny<DateTime?>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(new Tuple<List<Lib.ViewModels.Monitoring_Specification_Machine.MonitoringSpecificationMachineReportViewModel>, int>(new List<Lib.ViewModels.Monitoring_Specification_Machine.MonitoringSpecificationMachineReportViewModel>(), 0));

            var controller = GetController(serviceProviderMock, serviceMock, mapperMock);

            var response = controller.GetReportAll(0, null, null, null, 1, 25);

            Assert.Equal((int)HttpStatusCode.OK, (int)response.GetType().GetProperty("StatusCode").GetValue(response, null));
        }

        [Fact]
        public void GetReportAll_InternalError()
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            var serviceMock = new Mock<IMonitoringSpecificationMachineReportFacade>();
            var mapperMock = new Mock<IMapper>();

            serviceMock.Setup(service => service.GetReport(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime?>(),
                 It.IsAny<DateTime?>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>()))
                 .Throws(new Exception("eer"));

            var controller = GetController(serviceProviderMock, serviceMock, mapperMock);

            var response = controller.GetReportAll(0, null, null, null, 1, 25);

            Assert.Equal((int)HttpStatusCode.InternalServerError, (int)response.GetType().GetProperty("StatusCode").GetValue(response, null));
        }


        [Fact]
        public void GetXls_Ok()
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            var serviceMock = new Mock<IMonitoringSpecificationMachineReportFacade>();
            var mapperMock = new Mock<IMapper>();

            serviceMock.Setup(service => service.GenerateExcel(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime?>(),
                It.IsAny<DateTime?>(), It.IsAny<int>()))
                .Returns(new MemoryStream());

            var controller = GetController(serviceProviderMock, serviceMock, mapperMock);

            var response = controller.GetXlsAll(1, null, null, null);

            Assert.NotNull(response);
        }

        [Fact]
        public void GetXls_InternalError()
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            var serviceMock = new Mock<IMonitoringSpecificationMachineReportFacade>();
            var mapperMock = new Mock<IMapper>();

            serviceMock.Setup(service => service.GenerateExcel(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime?>(),
                It.IsAny<DateTime?>(), It.IsAny<int>()))
                 .Throws(new Exception("err"));

            var controller = GetController(serviceProviderMock, serviceMock, mapperMock);

            var response = controller.GetXlsAll(1, null, null, null);

            Assert.Equal((int)HttpStatusCode.InternalServerError, (int)response.GetType().GetProperty("StatusCode").GetValue(response, null));
        }
    }
}
