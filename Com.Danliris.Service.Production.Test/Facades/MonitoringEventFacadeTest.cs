using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.MonitoringEvent;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.MonitoringEvent;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.MonitoringEvent;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Monitoring_Event;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Monitoring_Event;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Facades
{
    public class MonitoringEventFacadeTest : BaseFacadeTest<ProductionDbContext, MonitoringEventFacade, MonitoringEventLogic, MonitoringEventModel, MonitoringEventDataUtil>
    {
        private const string ENTITY = "MonitoringEvent";

        public MonitoringEventFacadeTest() : base(ENTITY)
        {
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(ProductionDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(MonitoringEventLogic)))
                .Returns(new MonitoringEventLogic(identityService, dbContext));

            return serviceProviderMock;
        }

        [Fact]
        public async void Get_Report()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            MonitoringEventFacade facade = Activator.CreateInstance(typeof(MonitoringEventFacade), serviceProvider, dbContext) as MonitoringEventFacade;
            MonitoringEventReportFacade reportFacade = new MonitoringEventReportFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = reportFacade.GetReport(null, null, null, DateTime.MinValue, null, 1, 25, "{}", 7);
            Assert.NotEmpty(Response.Item1);
        }

        [Fact]
        public async void GenerateExcel()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            MonitoringEventFacade facade = Activator.CreateInstance(typeof(MonitoringEventFacade), serviceProvider, dbContext) as MonitoringEventFacade;
            MonitoringEventReportFacade reportFacade = new MonitoringEventReportFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = reportFacade.GenerateExcel(null, null, null, DateTime.MinValue, null, 7);
            Assert.NotNull(Response);
        }

        [Fact]
        public async void ReadByMachine()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            MonitoringEventFacade facade = Activator.CreateInstance(typeof(MonitoringEventFacade), serviceProvider, dbContext) as MonitoringEventFacade;
            MonitoringEventReportFacade reportFacade = new MonitoringEventReportFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = reportFacade.ReadByMachine(null, data.MachineId);
            Assert.NotNull(Response);
        }

        [Fact]
        public async void ReadByMachineSpec()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            MonitoringEventFacade facade = Activator.CreateInstance(typeof(MonitoringEventFacade), serviceProvider, dbContext) as MonitoringEventFacade;
            MonitoringEventReportFacade reportFacade = new MonitoringEventReportFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = reportFacade.ReadMonitoringSpecMachine(data.MachineId, data.ProductionOrderOrderNo, DateTime.MaxValue);
            Assert.Null(Response);
        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MonitoringEventProfile>();
            });
            var mapper = configuration.CreateMapper();

            MonitoringEventViewModel vm = new MonitoringEventViewModel { Id = 1 };
            MonitoringEventModel model = mapper.Map<MonitoringEventModel>(vm);

            Assert.Equal(vm.Id, model.Id);

        }
    }
}
