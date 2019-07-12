using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.DirectLaborCost;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Test.DataUtils.MasterDataUtils
{
    public class DirectLaborCostDataUtil : BaseDataUtil<DirectLaborCostFacade, DirectLaborCostModel>
    {
        public DirectLaborCostDataUtil(DirectLaborCostFacade facade) : base(facade)
        {
        }

        public override DirectLaborCostModel GetNewData()
        {
            return new DirectLaborCostModel() {
                LaborTotal = 1,
                WageTotal = 1,
                Month = DateTime.UtcNow.Month,
                Year = DateTime.UtcNow.Year
            };
        }
    }
}
