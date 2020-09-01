using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.LossEvent;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Test.DataUtils.MasterDataUtils
{
    public class LossEventDataUtil : BaseDataUtil<LossEventFacade, LossEventModel>
    {
        public LossEventDataUtil(LossEventFacade facade) : base(facade)
        {
        }

        public override LossEventModel GetNewData()
        {
            return new LossEventModel()
            {
                ProcessArea = "a",
                Losses = "losses",
                ProcessTypeCode = "code",
                ProcessTypeId = 1,
                ProcessTypeName = "na",
                OrderTypeCode = "c",
                OrderTypeId = 1,
                OrderTypeName = "ss"
            };
        }
    }
}
