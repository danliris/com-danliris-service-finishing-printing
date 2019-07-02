using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.DirectLaborCost;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.DirectLaborCost;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils.MasterDataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Facades.MasterFacadeTests
{
    public class DirectLaborCostFacadeTest : BaseFacadeTest<ProductionDbContext, DirectLaborCostFacade, DirectLaborCostLogic, DirectLaborCostModel, DirectLaborCostDataUtil>
    {
        private const string ENTITY = "DirectLaborCost";
        public DirectLaborCostFacadeTest() : base(ENTITY)
        {
        }

        [Fact]
        public virtual async void Get_All_WithKeywordYear()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            DirectLaborCostFacade facade = new DirectLaborCostFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = facade.Read(1, 25, "{}", new List<string>(), data.Year.ToString(), "{}");

            Assert.NotEmpty(Response.Data);
        }

        [Fact]
        public virtual async void Get_All_WithKeywordMonth()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            DirectLaborCostFacade facade = new DirectLaborCostFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = facade.Read(1, 25, "{}", new List<string>(), CultureInfo.GetCultureInfo("en-ID").DateTimeFormat.GetMonthName(data.Month), "{}");

            Assert.NotEmpty(Response.Data);
        }
    }
}
