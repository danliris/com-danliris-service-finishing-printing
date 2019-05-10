using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.MonitoringSpecificationMachine;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.MonitoringSpecificationMachine;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Monitoring_Specification_Machine;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
