using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Production.Lib.Models.Master.DurationEstimation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Test.DataUtils.MasterDataUtils
{
    public class DurationEstimationDataUtil : BaseDataUtil<DurationEstimationFacade, DurationEstimationModel>
    {
        public DurationEstimationDataUtil(DurationEstimationFacade facade) : base(facade)
        {
        }

        public override DurationEstimationModel GetNewData()
        {
            DurationEstimationModel model = new DurationEstimationModel
            {
                ProcessTypeCode ="test",
                Areas = new List<DurationEstimationAreaModel>
                {
                    new DurationEstimationAreaModel()
                }
            };
            return model;
        }
    }
}
