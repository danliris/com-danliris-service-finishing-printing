using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.Packing;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Packing;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Packing;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Packing;
using Com.Danliris.Service.Finishing.Printing.Lib.Services.HttpClientService;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Packing;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Facades
{
    public class PackingFacadeTest : BaseFacadeTest<ProductionDbContext, PackingFacade, PackingLogic, PackingModel, PackingDataUtil>
    {
        private const string ENTITY = "Packing";

        public PackingFacadeTest() : base(ENTITY)
        {
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(ProductionDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IHttpClientService)))
                .Returns(new HttpClientTestService());

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(PackingLogic)))
                .Returns(new PackingLogic(identityService, dbContext));

            return serviceProviderMock;
        }

        [Fact]
        public async void GenerateExcel()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            var facade = Activator.CreateInstance(typeof(PackingFacade), serviceProvider, dbContext) as PackingFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = facade.GenerateExcel(data.Code, data.ProductionOrderId, null, null, 7);

            Assert.NotNull(Response);
        }

        [Fact]
        public async void GenerateExcelQCGudang()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            var facade = Activator.CreateInstance(typeof(PackingFacade), serviceProvider, dbContext) as PackingFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = facade.GenerateExcelQCGudang(null, null, 7);

            Assert.NotNull(Response);
        }

        [Fact]
        public async void GetReport()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            var facade = Activator.CreateInstance(typeof(PackingFacade), serviceProvider, dbContext) as PackingFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = facade.GetReport(1, 25, data.Code, data.ProductionOrderId, null, null, 7);

            Assert.NotNull(Response);
        }

        [Fact]
        public async void GetPackingDetail()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            var facade = Activator.CreateInstance(typeof(PackingFacade), serviceProvider, dbContext) as PackingFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = await facade.GetPackingDetail("");

            Assert.Null(Response);
        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PackingProfile>();
            });
            var mapper = configuration.CreateMapper();

            PackingViewModel vm = new PackingViewModel { Id = 1 };
            PackingModel model = mapper.Map<PackingModel>(vm);

            Assert.Equal(vm.Id, model.Id);

        }
    }
}
