using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.Machine;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Test.DataUtils.MasterDataUtils
{
    public class MachineEventDataUtil : BaseDataUtil<MachineEventFacade, MachineEventsModel>
    {
        public MachineEventDataUtil(MachineEventFacade facade) : base(facade)
        {
        }
    }
}
