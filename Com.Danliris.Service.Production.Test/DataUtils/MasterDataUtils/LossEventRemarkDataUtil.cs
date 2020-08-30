using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.LossEventRemark;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Test.DataUtils.MasterDataUtils
{
    public class LossEventRemarkDataUtil : BaseDataUtil<LossEventRemarkFacade, LossEventRemarkModel>
    {
        public LossEventRemarkDataUtil(LossEventRemarkFacade facade) : base(facade)
        {
        }

        public override LossEventRemarkModel GetNewData()
        {
            return new LossEventRemarkModel()
            {
                ProductionLossCode = "c",
                Remark = "r",
                LossEventId = 1,
                LossEventCode = "c",
                LossEventCategoryLossesCategory = "c",
                LossEventCategoryCode = "c",
                LossEventCategoryId = 1,
                LossEventLosses = "losses",
                LossEventProcessTypeCode = "code",
                LossEventProcessTypeId = 1,
                LossEventProcessTypeName = "na",
                LossEventOrderTypeCode = "c",
                LossEventOrderTypeId = 1,
                LossEventOrderTypeName = "ss"
            }
        }
    }
}
