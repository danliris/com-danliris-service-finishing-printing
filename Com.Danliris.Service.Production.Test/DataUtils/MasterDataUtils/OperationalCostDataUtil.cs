using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.OperationalCost;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Test.DataUtils.MasterDataUtils
{
    public class OperationalCostDataUtil : BaseDataUtil<OperationalCostFacade, OperationalCostModel>
    {
        public OperationalCostDataUtil(OperationalCostFacade facade) : base(facade)
        {
        }

        public override OperationalCostModel GetNewData()
        {
            return new OperationalCostModel()
            {
                Month = DateTime.UtcNow.Month,
                Year = DateTime.UtcNow.Year
            };
        }
    }
}
