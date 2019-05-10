using Com.Danliris.Service.Finishing.Printing.Test.DataUtils.MasterDataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Production.Lib.BusinessLogic.Implementations.Master.Instruction;
using Com.Danliris.Service.Production.Lib.Models.Master.Instruction;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Moq;
using System;

namespace Com.Danliris.Service.Finishing.Printing.Test.Facades.MasterFacadeTests
{
    public class InstructionFacadeTest : BaseFacadeTest<ProductionDbContext, InstructionFacade, InstructionLogic, InstructionModel, InstructionDataUtil>
    {
        private const string ENTITY = "Instruction";

        public InstructionFacadeTest() : base(ENTITY)
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
                .Setup(x => x.GetService(typeof(InstructionLogic)))
                .Returns(new InstructionLogic(identityService, dbContext));

            return serviceProviderMock;
        }
    }
}
