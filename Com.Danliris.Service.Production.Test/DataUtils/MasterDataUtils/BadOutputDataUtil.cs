using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.BadOutput;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Test.DataUtils.MasterDataUtils
{
    public class BadOutputDataUtil : BaseDataUtil<BadOutputFacade, BadOutputModel>
    {
        public BadOutputDataUtil(BadOutputFacade facade) : base(facade)
        {
        }

        public override BadOutputModel GetNewData()
        {
            BadOutputModel model = new BadOutputModel
            {
                MachineDetails = new List<BadOutputMachineModel>
                {
                    new BadOutputMachineModel()
                }
            };
            return model;
        }
    }
}
