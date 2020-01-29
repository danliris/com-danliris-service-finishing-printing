using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils.MasterDataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.AutoMapperProfiles.Master;
using Com.Danliris.Service.Production.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Production.Lib.BusinessLogic.Implementations.Master.Step;
using Com.Danliris.Service.Production.Lib.Models.Master.Step;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.ViewModels.Master.Step;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Facades.MasterFacadeTests
{
    public class StepFacadeTest : BaseFacadeTest<ProductionDbContext, StepFacade, StepLogic, StepModel, StepDataUtil>
    {
        private const string ENTITY = "Step";

        public StepFacadeTest() : base(ENTITY)
        {
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(ProductionDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            StepIndicatorLogic StepIndicatorLogic = new StepIndicatorLogic(identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(StepLogic)))
                .Returns(new StepLogic(StepIndicatorLogic, identityService, dbContext));

            return serviceProviderMock;
        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<StepProfile>();
            });
            var mapper = configuration.CreateMapper();

            StepViewModel vm = new StepViewModel { Id = 1 };
            StepModel model = mapper.Map<StepModel>(vm);

            Assert.Equal(vm.Id, model.Id);

        }
    }
}
