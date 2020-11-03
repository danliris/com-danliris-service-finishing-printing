using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.EventOrganizer;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Test.DataUtils.MasterDataUtils
{
   public class EventOrganizerDataUtil : BaseDataUtil<EventOrganizerFacade, EventOrganizer>
    {
        public EventOrganizerDataUtil(EventOrganizerFacade facade) : base(facade)
        {
        }

        public override EventOrganizer GetNewData()
        {
            return new EventOrganizer()
            {
                ProcessArea = "Area Printing",
               Group="A",
               Kasubsie= "Kasubsie",
               Kasie= "Kasie",
               Code= "Code"
            };
        }

    }
}
