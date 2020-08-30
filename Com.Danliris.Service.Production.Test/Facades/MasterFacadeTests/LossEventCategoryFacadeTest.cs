using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.LossEventCategory;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.LossEventCategory;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.LossEventCategory;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils.MasterDataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.ValidateService;
using Com.Danliris.Service.Production.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Facades.MasterFacadeTests
{
    public class LossEventCategoryFacadeTest : BaseFacadeTest<ProductionDbContext, LossEventCategoryFacade, LossEventCategoryLogic, LossEventCategoryModel, LossEventCategoryDataUtil>
    {
        private const string ENTITY = "LossEventCategory";
        public LossEventCategoryFacadeTest() : base(ENTITY)
        {
        }

        [Fact]
        public virtual void ValidateVM()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            LossEventCategoryFacade facade = new LossEventCategoryFacade(serviceProvider, dbContext);

            var data = new LossEventCategoryViewModel();
            var validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(data));
        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<LossEventCategoryProfile>();
            });
            var mapper = configuration.CreateMapper();

            LossEventCategoryViewModel vm = new LossEventCategoryViewModel { Id = 1 };
            LossEventCategoryModel model = mapper.Map<LossEventCategoryModel>(vm);

            Assert.Equal(vm.Id, model.Id);

        }
    }
}
