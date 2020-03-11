using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils.MasterDataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.AutoMapperProfiles.Master;
using Com.Danliris.Service.Production.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Production.Lib.BusinessLogic.Implementations.Master.DurationEstimation;
using Com.Danliris.Service.Production.Lib.Models.Master.DurationEstimation;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.ViewModels.Master.DurationEstimation;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Facades.MasterFacadeTests
{
    public class DurationEstimationFacadeTest : BaseFacadeTest<ProductionDbContext, DurationEstimationFacade, DurationEstimationLogic, DurationEstimationModel, DurationEstimationDataUtil>
    {
        private const string ENTITY = "DurationEstimation";

        public DurationEstimationFacadeTest() : base(ENTITY)
        {
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(ProductionDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(DurationEstimationLogic)))
                .Returns(new DurationEstimationLogic(identityService, dbContext));

            return serviceProviderMock;
        }

        [Fact]
        public async void GetProcessType()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            DurationEstimationFacade facade = Activator.CreateInstance(typeof(DurationEstimationFacade), serviceProvider, dbContext) as DurationEstimationFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = facade.ReadByProcessType(data.ProcessTypeCode);

            Assert.NotNull(Response);
        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DurationEstimationProfile>();
            });
            var mapper = configuration.CreateMapper();

            DurationEstimationViewModel vm = new DurationEstimationViewModel { Id = 1 };
            DurationEstimationModel model = mapper.Map<DurationEstimationModel>(vm);

            Assert.Equal(vm.Id, model.Id);

        }

    }
}
