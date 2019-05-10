using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.MachineType;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.MachineType;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils.MasterDataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;

namespace Com.Danliris.Service.Finishing.Printing.Test.Facades.MasterFacadeTests
{
    public class MachineTypeFacadeTest : BaseFacadeTest<ProductionDbContext, MachineTypeFacade, MachineTypeLogic, MachineTypeModel, MachineTypeDataUtil>
    {
        private const string ENTITY = "MachineType";

        public MachineTypeFacadeTest() : base(ENTITY)
        {
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(ProductionDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            MachineTypeIndicatorsLogic machineTypeIndicatorsLogic = new MachineTypeIndicatorsLogic(identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(MachineTypeLogic)))
                .Returns(new MachineTypeLogic(machineTypeIndicatorsLogic, identityService, dbContext));

            return serviceProviderMock;
        }
    }
}
