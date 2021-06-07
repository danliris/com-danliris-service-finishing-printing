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

        public  FabricQualityControlModel GetNewDataFabricQualityControlModel()
        {
            FabricQualityControlModel model = new FabricQualityControlModel
            {
                Id =1,
                FabricGradeTests = new List<FabricGradeTestModel>
                {
                    new FabricGradeTestModel
                    {
                        FabricGradeTest=1,
                        FinalGradeTest =1,
                        FinalArea=1,
                        AvalLength=1,
                        FinalLength=1,
                        FabricQualityControlId=1,
                        FinalScore=1,
                        Grade ="A",
                        InitLength=1,
                        ItemIndex=1,
                        PcsNo ="1",
                        PointLimit=1,
                        PointSystem=1,
                        SampleLength=1,
                        Score=1,
                        Type="Type",
                        Width=1,
                        FabricQualityControl =new FabricQualityControlModel()
                        {
                            Buyer ="Buyer"
                        },
                        Criteria = new List<CriteriaModel>
                        {
                            new CriteriaModel()
                            {
                                Code ="Code",
                                Group ="Group"
                            }
                        }
                    }
                }
            };
            return model;
        }
    }
}
