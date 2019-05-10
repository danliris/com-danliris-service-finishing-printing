using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.FabricQualityControl;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.FabricQualityControl;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.FabricQualityControl;
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
    public class FabricQualityControlFacadeTest : BaseFacadeTest<ProductionDbContext, FabricQualityControlFacade, FabricQualityControlLogic, FabricQualityControlModel, FabricQualityControlDataUtil>
    {
        private const string ENTITY = "FabricQualityControl";

        public FabricQualityControlFacadeTest() : base(ENTITY)
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
                .Setup(x => x.GetService(typeof(FabricQualityControlLogic)))
                .Returns(new FabricQualityControlLogic(identityService, dbContext));

            return serviceProviderMock;
        }
    }
}
