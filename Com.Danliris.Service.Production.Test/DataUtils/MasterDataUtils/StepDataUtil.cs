using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Production.Lib.Models.Master.Step;
using System.Collections.Generic;

namespace Com.Danliris.Service.Finishing.Printing.Test.DataUtils.MasterDataUtils
{
    public class StepDataUtil : BaseDataUtil<StepFacade, StepModel>
    {
        public StepDataUtil(StepFacade facade) : base(facade)
        {
        }

        public override StepModel GetNewData()
        {
            StepModel model = new StepModel
            {
                StepIndicators = new List<StepIndicatorModel>
                {
                    new StepIndicatorModel()
                }
            };
            return model;
        }
    }
}
