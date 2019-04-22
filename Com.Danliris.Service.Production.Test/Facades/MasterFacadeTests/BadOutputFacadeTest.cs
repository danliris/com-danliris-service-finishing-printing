using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.BadOutput;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.BadOutput;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils.MasterDataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Test.Facades.MasterFacadeTests
{
    public class BadOutputFacadeTest : BaseFacadeTest<ProductionDbContext, BadOutputFacade, BadOutputLogic, BadOutputModel, BadOutputDataUtil>
    {
        private const string ENTITY = "BadOutput";

        public BadOutputFacadeTest() : base(ENTITY)
        {
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(ProductionDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            BadOutputMachineLogic badOutputMachineLogic = new BadOutputMachineLogic(identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(BadOutputLogic)))
                .Returns(new BadOutputLogic(badOutputMachineLogic, identityService, dbContext));

            return serviceProviderMock;
        }
    }
}
