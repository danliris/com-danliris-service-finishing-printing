using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.LossEvent;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.LossEvent;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.LossEvent;
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
    public class LossEventFacadeTest : BaseFacadeTest<ProductionDbContext, LossEventFacade, LossEventLogic, LossEventModel, LossEventDataUtil>
    {
        private const string ENTITY = "LossEvent";
        public LossEventFacadeTest() : base(ENTITY)
        {
        }

        [Fact]
        public virtual void ValidateVM()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            LossEventFacade facade = new LossEventFacade(serviceProvider, dbContext);

            var data = new LossEventViewModel();
            var validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(data));

            data.ProcessType = new Production.Lib.ViewModels.Integration.Master.ProcessTypeIntegrationViewModel();
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(data));
        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<LossEventProfile>();
            });
            var mapper = configuration.CreateMapper();

            LossEventViewModel vm = new LossEventViewModel { Id = 1 };
            LossEventModel model = mapper.Map<LossEventModel>(vm);

            Assert.Equal(vm.Id, model.Id);

            var vm2 = mapper.Map<LossEventViewModel>(model);

            Assert.Equal(vm2.Id, model.Id);

        }
    }
}
