using Com.Danliris.Service.Finishing.Printing.Test.DataUtils.MasterDataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Production.Lib.BusinessLogic.Implementations.Master.DurationEstimation;
using Com.Danliris.Service.Production.Lib.Models.Master.DurationEstimation;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Test.Facades.MasterFacadeTests
{
    public class DurationEstimationFacadeTest : BaseFacadeTest<ProductionDbContext, DurationEstimationFacade, DurationEstimationLogic, DurationEstimationModel, DurationEstimationDataUtil>
    {
        private const string ENTITY = "DurationEstimation";

        public DurationEstimationFacadeTest() : base(ENTITY)
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
                .Setup(x => x.GetService(typeof(DurationEstimationLogic)))
                .Returns(new DurationEstimationLogic(identityService, dbContext));

            return serviceProviderMock;
        }
    }
}
