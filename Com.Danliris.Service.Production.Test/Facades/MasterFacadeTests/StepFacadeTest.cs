using Com.Danliris.Service.Finishing.Printing.Test.DataUtils.MasterDataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Production.Lib.BusinessLogic.Implementations.Master.Step;
using Com.Danliris.Service.Production.Lib.Models.Master.Step;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Test.Facades.MasterFacadeTests
{
    public class StepFacadeTest : BaseFacadeTest<ProductionDbContext, StepFacade, StepLogic, StepModel, StepDataUtil>
    {
        private const string ENTITY = "Step";

        public StepFacadeTest() : base(ENTITY)
        {
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(ProductionDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            StepIndicatorLogic StepIndicatorLogic = new StepIndicatorLogic(identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(StepLogic)))
                .Returns(new StepLogic(StepIndicatorLogic, identityService, dbContext));

            return serviceProviderMock;
        }
    }
}
