using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.DOSales;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.DOSales;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.DOSales;
using Com.Danliris.Service.Finishing.Printing.Lib.Services.HttpClientService;
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
    public class DOSalesFacadeTest : BaseFacadeTest<ProductionDbContext, DOSalesFacade, DOSalesLogic, DOSalesModel, DOSalesDataUtil>
    {
        private const string ENTITY = "DOSales";

        public DOSalesFacadeTest() : base(ENTITY)
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
                .Setup(x => x.GetService(typeof(DOSalesLogic)))
                .Returns(new DOSalesLogic(identityService, dbContext));

            return serviceProviderMock;
        }

        [Fact]
        public async void GetReport()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            var facade = Activator.CreateInstance(typeof(DOSalesFacade), serviceProvider, dbContext) as DOSalesFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = facade.GetReport(1, 25, data.Code, data.ProductionOrderId, null, null, 7);

            Assert.NotNull(Response);
        }

        [Fact]
        public async void GetDOSalesDetail()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            var facade = Activator.CreateInstance(typeof(DOSalesFacade), serviceProvider, dbContext) as DOSalesFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = await facade.GetDOSalesDetail("");

            Assert.Null(Response);
        }
    }
}
