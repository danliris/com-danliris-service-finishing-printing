using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.Machine;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.Machine;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils.MasterDataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Test.Facades.MasterFacadeTests
{
    public class MachineEventFacadeTest : BaseFacadeTest<ProductionDbContext, MachineEventFacade, MachineEventLogic, MachineEventsModel, MachineEventDataUtil>
    {
        private const string ENTITY = "MachineEvent";
        public MachineEventFacadeTest() : base(ENTITY)
        {

        }

    }
}
