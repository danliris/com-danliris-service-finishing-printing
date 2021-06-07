using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.ReturToQC;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.ReturToQC;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.ReturToQC;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.ReturToQC;
using Com.Danliris.Service.Finishing.Printing.Lib.Services.HttpClientService;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.ReturToQC;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Facades
{
    public class ReturToQCFacadeTest : BaseFacadeTest<ProductionDbContext, ReturToQCFacade, ReturToQCLogic, ReturToQCModel, ReturToQCDataUtil>
    {
        private const string ENTITY = "ReturToQC";

        public ReturToQCFacadeTest() : base(ENTITY)
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
                .Setup(x => x.GetService(typeof(ReturToQCLogic)))
                .Returns(new ReturToQCLogic(identityService, dbContext));

            return serviceProviderMock;
        }

        [Fact]
        public async Task CreateAsync_Return_Succes()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            var facade = Activator.CreateInstance(typeof(ReturToQCFacade), serviceProvider, dbContext) as ReturToQCFacade;
           
            var data =  DataUtil(facade, dbContext).GetNewData();

            var result=await facade.CreateAsync(data);
            Assert.NotEqual(0, result);
          
        }

        [Fact]
        public async void CreateAsync_Duplicate_Key_ThrowsException()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            var facade = Activator.CreateInstance(typeof(ReturToQCFacade), serviceProvider, dbContext) as ReturToQCFacade;
            var data = await DataUtil(facade, dbContext).GetTestData();

            await Assert.ThrowsAsync<System.ArgumentException>(() => facade.CreateAsync(data));

        }

        [Fact]
        public async void DeleteAsync_succes()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            var facade = Activator.CreateInstance(typeof(ReturToQCFacade), serviceProvider, dbContext) as ReturToQCFacade;
            var data = await DataUtil(facade, dbContext).GetTestData();

           var result= await facade.DeleteAsync(data.Id);
            Assert.NotEqual(0, result);
        }

        [Fact]
        public async void DeleteAsync_ThrowsException()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            var facade = Activator.CreateInstance(typeof(ReturToQCFacade), serviceProvider, dbContext) as ReturToQCFacade;
            var data = await DataUtil(facade, dbContext).GetTestData();
            await Assert.ThrowsAsync<System.NullReferenceException>(() => facade.DeleteAsync(0));

        }

        [Fact]
        public async void UpdateAsync_ThrowsException()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            var facade = Activator.CreateInstance(typeof(ReturToQCFacade), serviceProvider, dbContext) as ReturToQCFacade;
            var data = await DataUtil(facade, dbContext).GetTestData();
            await Assert.ThrowsAsync<System.NullReferenceException>(() => facade.UpdateAsync(1,null));

        }

        [Fact]
        public async void UpdateAsync_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            var facade = Activator.CreateInstance(typeof(ReturToQCFacade), serviceProvider, dbContext) as ReturToQCFacade;
            var data = await DataUtil(facade, dbContext).GetTestData();
            var newData = DataUtil(facade, dbContext).GetNewData();
            await facade.UpdateAsync(data.Id, newData);

        }


        [Fact]
        public async void GetReport()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            var facade = Activator.CreateInstance(typeof(ReturToQCFacade), serviceProvider, dbContext) as ReturToQCFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();
            
            var Response = facade.GetReport(1, 25, DateTime.MinValue, DateTime.MaxValue, data.ReturToQCItems.FirstOrDefault().ProductionOrderNo, data.ReturNo, data.Destination, data.DeliveryOrderNo, 7);

            Assert.NotEmpty(Response.Data);
        }

        [Fact]
        public async void GetReport_With_DateTo_DateFrom_isNull()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            var facade = Activator.CreateInstance(typeof(ReturToQCFacade), serviceProvider, dbContext) as ReturToQCFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = facade.GetReport(1, 25, null,null, null, null, null, null, 7);

            Assert.NotNull(Response.Data);
        }

        [Fact]
        public async void GetReport_With_DateFrom_isNull()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            var facade = Activator.CreateInstance(typeof(ReturToQCFacade), serviceProvider, dbContext) as ReturToQCFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = facade.GetReport(1, 25, null, DateTime.MaxValue, null, null, null, null, 7);

            Assert.NotNull(Response.Data);
        }

        [Fact]
        public async void GetReport_With_DateTo_isNull()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            var facade = Activator.CreateInstance(typeof(ReturToQCFacade), serviceProvider, dbContext) as ReturToQCFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = facade.GetReport(1, 25, DateTime.MinValue, null, null, null, null, null, 7);

            Assert.NotNull(Response.Data);
        }


        [Fact]
        public async void GenerateExcel()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            var facade = Activator.CreateInstance(typeof(ReturToQCFacade), serviceProvider, dbContext) as ReturToQCFacade;
            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = facade.GenerateExcel(DateTime.MinValue, DateTime.MaxValue, null, null, null, null, 7);

            Assert.NotNull(Response);
        }

        [Fact]
        public  void GenerateExcel_When_EmptyData_ReturnSucces()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            var facade = Activator.CreateInstance(typeof(ReturToQCFacade), serviceProvider, dbContext) as ReturToQCFacade;
            
            var Response = facade.GenerateExcel(DateTime.MinValue, DateTime.MaxValue, null, null, null, null, 7);

            Assert.NotNull(Response);
        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ReturToQCProfile>();
            });
            var mapper = configuration.CreateMapper();

            ReturToQCViewModel vm = new ReturToQCViewModel { Id = 1 };
            ReturToQCModel model = mapper.Map<ReturToQCModel>(vm);

            Assert.Equal(vm.Id, model.Id);

        }
    }
}
