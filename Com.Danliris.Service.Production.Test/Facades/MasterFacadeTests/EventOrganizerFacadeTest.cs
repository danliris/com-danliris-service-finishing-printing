using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.EventOrganizer;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.EventOrganizer;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils.MasterDataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Test.Facades.MasterFacadeTests
{
  public  class EventOrganizerFacadeTest : BaseFacadeTest<ProductionDbContext, EventOrganizerFacade, EventOrganizerLogic, EventOrganizer, EventOrganizerDataUtil>
    {
        private const string ENTITY = "Instruction";
        public EventOrganizerFacadeTest() : base(ENTITY)
        {
        }


    }
}
