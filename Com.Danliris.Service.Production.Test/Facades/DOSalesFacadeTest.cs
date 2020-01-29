using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.DOSales;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.DOSales;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.DOSales;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib;
using System;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Facades
{
    public class DOSalesFacadeTest : BaseFacadeTest<ProductionDbContext, DOSalesFacade, DOSalesLogic, DOSalesModel, DOSalesDataUtil>
    {
        private const string ENTITY = "DOSales";

        public DOSalesFacadeTest() : base(ENTITY)
        {
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
