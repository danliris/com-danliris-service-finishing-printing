using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.MonitoringSpecificationMachine;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.MonitoringSpecificationMachine;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.MonitoringSpecificationMachine;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Monitoring_Specification_Machine;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Monitoring_Specification_Machine;
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
    public class MonitoringSpecificationMachineFacadeTest : BaseFacadeTest<ProductionDbContext, MonitoringSpecificationMachineFacade, MonitoringSpecificationMachineLogic, MonitoringSpecificationMachineModel, MonitoringSpecificationMachineDataUtil>
    {
        private const string ENTITY = "MonitoringSpecificationMachine";

        public MonitoringSpecificationMachineFacadeTest() : base(ENTITY)
        {
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(ProductionDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            MonitoringSpecificationMachineDetailsLogic monitoringSpecificationMachineDetailsLogic = new MonitoringSpecificationMachineDetailsLogic(identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(MonitoringSpecificationMachineLogic)))
                .Returns(new MonitoringSpecificationMachineLogic(monitoringSpecificationMachineDetailsLogic, identityService, dbContext));

            return serviceProviderMock;
        }

        [Fact]
        public async void Get_Report()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            MonitoringSpecificationMachineFacade facade = Activator.CreateInstance(typeof(MonitoringSpecificationMachineFacade), serviceProvider, dbContext) as MonitoringSpecificationMachineFacade;
            MonitoringSpecificationMachineReportFacade reportFacade = new MonitoringSpecificationMachineReportFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = reportFacade.GetReport(data.MachineId, data.ProductionOrderNo, DateTime.MinValue, null, 1, 25, "{}", 7);
            Assert.NotEmpty(Response.Item1);
        }

        [Fact]
        public async void GenerateExcel()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            MonitoringSpecificationMachineFacade facade = Activator.CreateInstance(typeof(MonitoringSpecificationMachineFacade), serviceProvider, dbContext) as MonitoringSpecificationMachineFacade;
            MonitoringSpecificationMachineReportFacade reportFacade = new MonitoringSpecificationMachineReportFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = reportFacade.GenerateExcel(data.MachineId, data.ProductionOrderNo, DateTime.MinValue, null, 7);
            Assert.NotNull(Response);
        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MonitoringSpecificationMachineProfile>();
            });
            var mapper = configuration.CreateMapper();

            MonitoringSpecificationMachineViewModel vm = new MonitoringSpecificationMachineViewModel { Id = 1 };
            MonitoringSpecificationMachineModel model = mapper.Map<MonitoringSpecificationMachineModel>(vm);

            Assert.Equal(vm.Id, model.Id);

        }
    }
}
