using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.EventOrganizer;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.EventOrganizer;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.EventOrganizer;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils.MasterDataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Facades.MasterFacadeTests
{
  public  class EventOrganizerFacadeTest : BaseFacadeTest<ProductionDbContext, EventOrganizerFacade, EventOrganizerLogic, EventOrganizer, EventOrganizerDataUtil>
    {
        private const string ENTITY = "Instruction";
        public EventOrganizerFacadeTest() : base(ENTITY)
        {
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
