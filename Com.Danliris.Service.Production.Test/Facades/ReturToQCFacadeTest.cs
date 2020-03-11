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
using System.Text;
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
        public async void GetReport()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            var facade = Activator.CreateInstance(typeof(ReturToQCFacade), serviceProvider, dbContext) as ReturToQCFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = facade.GetReport(1, 25, DateTime.MinValue, DateTime.MaxValue, null, null, null, null, 7);

            Assert.NotEmpty(Response.Data);
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
