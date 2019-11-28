using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Production.Lib.Models.Master.Instruction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Test.DataUtils.MasterDataUtils
{
    public class InstructionDataUtil : BaseDataUtil<InstructionFacade, InstructionModel>
    {
        public InstructionDataUtil(InstructionFacade facade) : base(facade)
        {
        }

        public override InstructionModel GetNewData()
        {
            InstructionModel model = new InstructionModel
            {
                Name = "test",
                Steps = new List<InstructionStepModel>
                {
                    new InstructionStepModel
                    {
                        StepIndicators = new List<InstructionStepIndicatorModel>
                        {
                            new InstructionStepIndicatorModel()
                        }
                    }
                }
            };
            return model;
        }
    }
}
