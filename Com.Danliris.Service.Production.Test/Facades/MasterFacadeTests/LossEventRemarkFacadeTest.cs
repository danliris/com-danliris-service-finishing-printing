using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.LossEventRemark;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.LossEventRemark;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.LossEventRemark;
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
    public class LossEventRemarkFacadeTest : BaseFacadeTest<ProductionDbContext, LossEventRemarkFacade, LossEventRemarkLogic, LossEventRemarkModel, LossEventRemarkDataUtil>
    {
        private const string ENTITY = "LossEventRemark";
        public LossEventRemarkFacadeTest() : base(ENTITY)
        {
        }

        [Fact]
        public virtual void ValidateVM()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            LossEventRemarkFacade facade = new LossEventRemarkFacade(serviceProvider, dbContext);

            var data = new LossEventRemarkViewModel();
            var validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(data));

            data.LossEventCategory = new Lib.ViewModels.Master.LossEventCategory.LossEventCategoryViewModel();
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(data));
        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<LossEventRemarkProfile>();
            });
            var mapper = configuration.CreateMapper();

            LossEventRemarkViewModel vm = new LossEventRemarkViewModel { Id = 1 };
            LossEventRemarkModel model = mapper.Map<LossEventRemarkModel>(vm);

            Assert.Equal(vm.Id, model.Id);

            var vm2 = mapper.Map<LossEventRemarkViewModel>(model);

            Assert.Equal(vm2.Id, model.Id);
        }
    }
}
