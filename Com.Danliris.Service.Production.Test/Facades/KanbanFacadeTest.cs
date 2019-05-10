using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.Machine;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Kanban;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils.MasterDataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Test.Facades
{
    public class KanbanFacadeTest : BaseFacadeTest<ProductionDbContext, KanbanFacade, KanbanLogic, KanbanModel, KanbanDataUtil>
    {
        private const string ENTITY = "Kanban";

        public KanbanFacadeTest() : base(ENTITY)
        {
        }

        protected override KanbanDataUtil DataUtil(KanbanFacade facade, ProductionDbContext dbContext = null)
        {
            IServiceProvider serviceProvider = GetServiceProviderMock(dbContext).Object;

            MachineFacade machineFacade = new MachineFacade(serviceProvider, dbContext);
            MachineDataUtil machineDataUtil = new MachineDataUtil(machineFacade);

            KanbanDataUtil dataUtil = new KanbanDataUtil(machineDataUtil, facade);

            return dataUtil;
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(ProductionDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            MachineEventLogic machineEventLogic = new MachineEventLogic(identityService, dbContext);
            MachineStepLogic machineStepLogic = new MachineStepLogic(identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(MachineLogic)))
                .Returns(new MachineLogic(machineEventLogic, machineStepLogic, identityService, dbContext));

            serviceProviderMock
                .Setup(x => x.GetService(typeof(KanbanLogic)))
                .Returns(new KanbanLogic(identityService, dbContext));

            return serviceProviderMock;
        }
    }
}
