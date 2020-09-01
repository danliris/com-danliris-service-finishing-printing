using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.LossEventCategory;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Test.DataUtils.MasterDataUtils
{
    public class LossEventCategoryDataUtil : BaseDataUtil<LossEventCategoryFacade, LossEventCategoryModel>
    {
        public LossEventCategoryDataUtil(LossEventCategoryFacade facade) : base(facade)
        {
        }

        public override LossEventCategoryModel GetNewData()
        {
            return new LossEventCategoryModel()
            {
                LossEventProcessArea = "c",
                LossesCategory = "c",
                LossEventCode = "c",
                LossEventId = 1,
                LossEventLosses = "losses",
                LossEventProcessTypeCode = "code",
                LossEventProcessTypeId = 1,
                LossEventProcessTypeName = "na",
                LossEventOrderTypeCode = "c",
                LossEventOrderTypeId = 1,
                LossEventOrderTypeName = "ss"
            };
        }
    }
}
