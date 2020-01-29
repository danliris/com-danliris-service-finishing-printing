using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.MonitoringEvent;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.Machine;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Monitoring_Specification_Machine;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.Machine;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Monitoring_Specification_Machine;
using Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.MonitoringEvent;
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
    public class MonitoringEventReportControllerTest
    {
        protected MonitoringEventReportController GetController(Mock<IServiceProvider> serviceProviderMock, Mock<IMonitoringEventReportFacade> serviceMock, Mock<IMapper> mapperMock)
        {
            var user = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            user.Setup(u => u.Claims).Returns(claims);
            MonitoringEventReportController controller = (MonitoringEventReportController)Activator.CreateInstance(typeof(MonitoringEventReportController), serviceProviderMock.Object, mapperMock.Object, serviceMock.Object);
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
            var serviceMock = new Mock<IMonitoringEventReportFacade>();
            var mapperMock = new Mock<IMapper>();

            serviceMock.Setup(service => service.GetReport(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime?>(),
                It.IsAny<DateTime?>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(new Tuple<List<Lib.ViewModels.Monitoring_Event.MonitoringEventReportViewModel>, int>(new List<Lib.ViewModels.Monitoring_Event.MonitoringEventReportViewModel>(), 0));

            var controller = GetController(serviceProviderMock, serviceMock, mapperMock);

            var response = controller.GetReportAll(null, null, null, null, null, 1, 25);

            Assert.Equal((int)HttpStatusCode.OK, (int)response.GetType().GetProperty("StatusCode").GetValue(response, null));
        }

        [Fact]
        public void GetReportAll_InternalError()
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            var serviceMock = new Mock<IMonitoringEventReportFacade>();
            var mapperMock = new Mock<IMapper>();

            serviceMock.Setup(service => service.GetReport(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime?>(),
                It.IsAny<DateTime?>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>()))
                .Throws(new Exception("eer"));

            var controller = GetController(serviceProviderMock, serviceMock, mapperMock);

            var response = controller.GetReportAll(null, null, null, null, null, 1, 25);

            Assert.Equal((int)HttpStatusCode.InternalServerError, (int)response.GetType().GetProperty("StatusCode").GetValue(response, null));
        }


        [Fact]
        public void GetXls_Ok()
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            var serviceMock = new Mock<IMonitoringEventReportFacade>();
            var mapperMock = new Mock<IMapper>();

            serviceMock.Setup(service => service.GenerateExcel(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime?>(),
                It.IsAny<DateTime?>(), It.IsAny<int>()))
                .Returns(new MemoryStream());

            var controller = GetController(serviceProviderMock, serviceMock, mapperMock);

            var response = controller.GetXlsAll(null, null, null, null, null);

            Assert.NotNull(response);
        }

        [Fact]
        public void GetXls_InternalError()
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            var serviceMock = new Mock<IMonitoringEventReportFacade>();
            var mapperMock = new Mock<IMapper>();

            serviceMock.Setup(service => service.GenerateExcel(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime?>(),
                 It.IsAny<DateTime?>(), It.IsAny<int>()))
                 .Throws(new Exception("err"));

            var controller = GetController(serviceProviderMock, serviceMock, mapperMock);

            var response = controller.GetXlsAll(null, null, null, null, null);

            Assert.Equal((int)HttpStatusCode.InternalServerError, (int)response.GetType().GetProperty("StatusCode").GetValue(response, null));
        }

        [Fact]
        public void ByMachine_Ok()
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            var serviceMock = new Mock<IMonitoringEventReportFacade>();
            var mapperMock = new Mock<IMapper>();

            serviceMock.Setup(service => service.ReadByMachine(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(new List<Lib.Models.Master.Machine.MachineEventsModel>());
            mapperMock.Setup(map => map.Map<List<MachineEventViewModel>>(It.IsAny<List<MachineEventsModel>>()))
                .Returns(new List<MachineEventViewModel>());

            var controller = GetController(serviceProviderMock, serviceMock, mapperMock);

            var response = controller.ByMachine(null, 1);

            Assert.Equal((int)HttpStatusCode.OK, (int)response.GetType().GetProperty("StatusCode").GetValue(response, null));
        }

        [Fact]
        public void Get_Ok()
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            var serviceMock = new Mock<IMonitoringEventReportFacade>();
            var mapperMock = new Mock<IMapper>();

            serviceMock.Setup(service => service.ReadMonitoringSpecMachine(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>()))
                .Returns(new Lib.Models.Monitoring_Specification_Machine.MonitoringSpecificationMachineModel());
            mapperMock.Setup(map => map.Map<MonitoringSpecificationMachineViewModel>(It.IsAny<List<MonitoringSpecificationMachineModel>>()))
                .Returns(new MonitoringSpecificationMachineViewModel());
            var controller = GetController(serviceProviderMock, serviceMock, mapperMock);

            var response = controller.Get(1, "test", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

            Assert.Equal((int)HttpStatusCode.OK, (int)response.GetType().GetProperty("StatusCode").GetValue(response, null));
        }

        [Fact]
        public void Get_InternalError()
        {
            var serviceProviderMock = new Mock<IServiceProvider>();
            var serviceMock = new Mock<IMonitoringEventReportFacade>();
            var mapperMock = new Mock<IMapper>();

            serviceMock.Setup(service => service.ReadMonitoringSpecMachine(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>()))
                .Throws(new Exception("err"));
            mapperMock.Setup(map => map.Map<MonitoringSpecificationMachineViewModel>(It.IsAny<List<MonitoringSpecificationMachineModel>>()))
                .Returns(new MonitoringSpecificationMachineViewModel());
            var controller = GetController(serviceProviderMock, serviceMock, mapperMock);


            var response = controller.Get(1, "test", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));

            Assert.Equal((int)HttpStatusCode.InternalServerError, (int)response.GetType().GetProperty("StatusCode").GetValue(response, null));
        }

    }
}
