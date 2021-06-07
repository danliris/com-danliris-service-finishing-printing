using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.OperationalCost;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.OperationalCost;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.OperationalCost;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils.MasterDataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Facades.MasterFacadeTests
{
    public class OperationalCostFacadeTest : BaseFacadeTest<ProductionDbContext, OperationalCostFacade, OperationalCostLogic, OperationalCostModel, OperationalCostDataUtil>
    {
        private const string ENTITY = "OperationalCost";

        public OperationalCostFacadeTest() : base(ENTITY)
        {
        }

        [Fact]
        public virtual async void Get_All_WithKeywordYear()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            OperationalCostFacade facade = new OperationalCostFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = facade.Read(1, 25, "{}", new List<string>(), data.Year.ToString(), "{}");

            Assert.NotEmpty(Response.Data);
        }

        [Fact]
        public virtual async void Get_All_WithKeywordMonth()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            OperationalCostFacade facade = new OperationalCostFacade(serviceProvider, dbContext);

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

            var data = new OperationalCostViewModel()
            {

            };
            System.ComponentModel.DataAnnotations.ValidationContext validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(data, serviceProvider, null);
            var response = data.Validate(validationContext);

            Assert.NotEmpty(response);
        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<OperationalCostProfile>();
            });
            var mapper = configuration.CreateMapper();

            OperationalCostViewModel vm = new OperationalCostViewModel { Id = 1 };
            OperationalCostModel model = mapper.Map<OperationalCostModel>(vm);

            Assert.Equal(vm.Id, model.Id);

        }
    }
}
