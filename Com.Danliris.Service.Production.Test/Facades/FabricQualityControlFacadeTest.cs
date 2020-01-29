using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.FabricQualityControl;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.FabricQualityControl;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.FabricQualityControl;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.FabricQualityControl;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.FabricQualityControl;
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

        [Fact]
        public async Task Get_Report()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            FabricQualityControlFacade facade = Activator.CreateInstance(typeof(FabricQualityControlFacade), serviceProvider, dbContext) as FabricQualityControlFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = facade.GetReport(1,25,null, 0, null, null, null, null, null, 0);

            Assert.NotNull(Response);
        }

        [Fact]
        public async Task Generate_Excel()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            FabricQualityControlFacade facade = Activator.CreateInstance(typeof(FabricQualityControlFacade), serviceProvider, dbContext) as FabricQualityControlFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = facade.GenerateExcel(null, 0, null, null, null, null, null, 0);

            Assert.NotNull(Response);
        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<FabricQualityControlProfile>();
            });
            var mapper = configuration.CreateMapper();

            FabricQualityControlViewModel vm = new FabricQualityControlViewModel { Id = 1 };
            FabricQualityControlModel model = mapper.Map<FabricQualityControlModel>(vm);

            Assert.Equal(vm.Id, model.Id);

        }
    }
}
