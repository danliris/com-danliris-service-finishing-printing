using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.FabricQualityControl;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.FabricQualityControl;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Test.DataUtils
{
    public class FabricQualityControlDataUtil : BaseDataUtil<FabricQualityControlFacade, FabricQualityControlModel>
    {
        public FabricQualityControlDataUtil(FabricQualityControlFacade facade) : base(facade)
        {
        }

        public override FabricQualityControlModel GetNewData()
        {
            FabricQualityControlModel model = new FabricQualityControlModel
            {
                FabricGradeTests = new List<FabricGradeTestModel>
                {
                    new FabricGradeTestModel
                    {
                        Criteria = new List<CriteriaModel>
                        {
                            new CriteriaModel()
                        }
                    }
                }
            };
            return model;
        }
    }
}
