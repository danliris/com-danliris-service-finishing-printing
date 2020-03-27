using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.DailyOperation;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Daily_Operation;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Test.DataUtils
{
    public class DailyOperationDataUtil : BaseDataUtil<DailyOperationFacade, DailyOperationModel>
    {
        KanbanDataUtil kanbanDataUtil;
        public DailyOperationDataUtil(KanbanDataUtil kanbanDataUtil, DailyOperationFacade facade) : base(facade)
        {
            this.kanbanDataUtil = kanbanDataUtil;
        }

        public override async Task<DailyOperationModel> GetNewDataAsync()
        {
            var kanbanData = await kanbanDataUtil.GetTestData();

            DailyOperationModel model = new DailyOperationModel
            {
                KanbanId = kanbanData.Id,
                Type = "input",
                DateInput = DateTimeOffset.UtcNow,
                MachineId = kanbanData.Instruction.Steps.First().MachineId,
                StepId = kanbanData.Instruction.Steps.First().Id,
                StepProcess = kanbanData.Instruction.Steps.First().Process,
                BadOutputReasons = new List<DailyOperationBadOutputReasonsModel>
                {
                    new DailyOperationBadOutputReasonsModel()
                },
                Shift = "shift"
            };
            return model;
        }

        public async Task<DailyOperationModel> GetNewDataInputDyeing()
        {
            var kanbanData = await kanbanDataUtil.GetTestData();

            DailyOperationModel model = new DailyOperationModel
            {
                KanbanId = kanbanData.Id,
                Type = "input",
                DateInput = DateTimeOffset.UtcNow,
                MachineId = kanbanData.Instruction.Steps.ElementAt(1).MachineId,
                StepId = kanbanData.Instruction.Steps.ElementAt(1).Id,
                StepProcess = kanbanData.Instruction.Steps.ElementAt(1).Process,
                BadOutputReasons = new List<DailyOperationBadOutputReasonsModel>
                {
                    new DailyOperationBadOutputReasonsModel()
                },
                Shift = "shift"
            };
            return model;
        }

        public DailyOperationModel GetNewDataOut(DailyOperationModel dataInput)
        {

            DailyOperationModel model = new DailyOperationModel
            {
                KanbanId = dataInput.KanbanId,
                Type = "output",
                DateOutput = DateTimeOffset.UtcNow,
                StepId = dataInput.StepId,
                MachineId = dataInput.MachineId,
                StepProcess = dataInput.StepProcess,
                BadOutputReasons = new List<DailyOperationBadOutputReasonsModel>
                {
                    new DailyOperationBadOutputReasonsModel()
                }
            };
            return model;
        }
    }
}
