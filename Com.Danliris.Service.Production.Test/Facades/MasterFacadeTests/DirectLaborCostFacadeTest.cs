using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.DirectLaborCost;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.DirectLaborCost;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.DirectLaborCost;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils.MasterDataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
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

        [Fact]
        public virtual void ValidateVM()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            DirectLaborCostFacade facade = new DirectLaborCostFacade(serviceProvider, dbContext);

            var data = new DirectLaborCostViewModel()
            {
                WageTotal = -1,
                LaborTotal = -1
            };
            System.ComponentModel.DataAnnotations.ValidationContext validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(data, serviceProvider, null);
            var response = data.Validate(validationContext);

            Assert.NotEmpty(response);
        }

        [Fact]
        public virtual async void Get_For_Cost_C()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            DirectLaborCostFacade facade = new DirectLaborCostFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = await  facade.GetForCostCalculation(data.Month, data.Year);

            Assert.NotNull(Response);
        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DirectLaborCostProfile>();
            });
            var mapper = configuration.CreateMapper();

            DirectLaborCostViewModel vm = new DirectLaborCostViewModel { Id = 1 };
            DirectLaborCostModel model = mapper.Map<DirectLaborCostModel>(vm);

            Assert.Equal(vm.Id, model.Id);

        }
    }
}
