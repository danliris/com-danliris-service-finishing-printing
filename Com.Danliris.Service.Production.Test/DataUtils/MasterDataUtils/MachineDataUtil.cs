using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.Machine;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using System.Collections.Generic;

namespace Com.Danliris.Service.Finishing.Printing.Test.DataUtils.MasterDataUtils
{
    public class MachineDataUtil : BaseDataUtil<MachineFacade, MachineModel>
    {
        public MachineDataUtil(MachineFacade facade) : base(facade)
        {
        }

        public override MachineModel GetNewData()
        {
            MachineModel model = new MachineModel
            {
                MachineEvents = new List<MachineEventsModel>
                {
                    new MachineEventsModel()
                },
                MachineSteps = new List<MachineStepModel>
                {
                    new MachineStepModel()
                }
            };
            return model;
        }
    }
}
