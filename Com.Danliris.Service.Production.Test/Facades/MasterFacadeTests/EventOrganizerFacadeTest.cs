using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.EventOrganizer;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.EventOrganizer;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.EventOrganizer;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils.MasterDataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Facades.MasterFacadeTests
{
  public  class EventOrganizerFacadeTest : BaseFacadeTest<ProductionDbContext, EventOrganizerFacade, EventOrganizerLogic, EventOrganizer, EventOrganizerDataUtil>
    {
        private const string ENTITY = "Instruction";
        public EventOrganizerFacadeTest() : base(ENTITY)
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
                .Setup(x => x.GetService(typeof(EventOrganizerLogic)))
                .Returns(new EventOrganizerLogic(identityService, dbContext));

            return serviceProviderMock;
        }

        [Fact]
        public  async Task ReadByGroupArea_Return_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            EventOrganizerFacade facade = new EventOrganizerFacade(serviceProvider, dbContext);
            var data = await DataUtil(facade, dbContext).GetTestData();
            var response =await facade.ReadByGroupArea(data.ProcessArea,data.Group);
            Assert.True(0 < response.Id);
        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EventOrganizerProfil>();
            });
            var mapper = configuration.CreateMapper();

            EventOrganizerViewModel vm = new EventOrganizerViewModel { Id = 1 };
            EventOrganizer model = mapper.Map<EventOrganizer>(vm);

            Assert.Equal(vm.Id, model.Id);

        }


    }
}
