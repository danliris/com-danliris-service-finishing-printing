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
using System.Threading.Tasks;
using Xunit;

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

        [Fact]
        public async Task GetForSPP_WithoutNo()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            FabricQualityControlFacade facade = Activator.CreateInstance(typeof(FabricQualityControlFacade), serviceProvider, dbContext) as FabricQualityControlFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = await facade.GetForSPP(null);

            Assert.NotNull(Response);
        }

        [Fact]
        public async Task GetJoinKanban_WithNo()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            FabricQualityControlFacade facade = Activator.CreateInstance(typeof(FabricQualityControlFacade), serviceProvider, dbContext) as FabricQualityControlFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = await facade.GetForSPP("a");

            Assert.NotNull(Response);
        }
    }
}
