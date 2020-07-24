using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.DailyOperation;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Daily_Operation;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Kanban;
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

        public async Task<KanbanModel> GetKanban()
        {
            return await kanbanDataUtil.GetTestData();
        }

        public KanbanSnapshotModel GetKanbanSnapshot()
        {
            return kanbanDataUtil.GetKanbanSnapshot();
        }

        public DailyOperationModel GetNewDataInputPreTreatment(KanbanModel kanban)
        {
            DailyOperationModel model = new DailyOperationModel
            {
                KanbanId = kanban.Id,
                Type = "input",
                DateInput = DateTimeOffset.UtcNow,
                MachineId = kanban.Instruction.Steps.ElementAt(1).MachineId,
                StepId = kanban.Instruction.Steps.ElementAt(1).Id,
                StepProcess = kanban.Instruction.Steps.ElementAt(1).Process,
                BadOutputReasons = new List<DailyOperationBadOutputReasonsModel>
                {
                    new DailyOperationBadOutputReasonsModel()
                },
                Shift = "shift"
            };
            return model;
        }

        public DailyOperationModel GetNewDataInputDyeing(KanbanModel kanban)
        {

            DailyOperationModel model = new DailyOperationModel
            {
                KanbanId = kanban.Id,
                Type = "input",
                DateInput = DateTimeOffset.UtcNow,
                MachineId = kanban.Instruction.Steps.ElementAt(1).MachineId,
                StepId = kanban.Instruction.Steps.ElementAt(1).Id,
                StepProcess = kanban.Instruction.Steps.ElementAt(1).Process,
                BadOutputReasons = new List<DailyOperationBadOutputReasonsModel>
                {
                    new DailyOperationBadOutputReasonsModel()
                },
                Shift = "shift"
            };
            return model;
        }

        public DailyOperationModel GetNewDataInputPrinting(KanbanModel kanban)
        {

            DailyOperationModel model = new DailyOperationModel
            {
                KanbanId = kanban.Id,
                Type = "input",
                DateInput = DateTimeOffset.UtcNow,
                MachineId = kanban.Instruction.Steps.ElementAt(2).MachineId,
                StepId = kanban.Instruction.Steps.ElementAt(2).Id,
                StepProcess = kanban.Instruction.Steps.ElementAt(2).Process,
                BadOutputReasons = new List<DailyOperationBadOutputReasonsModel>
                {
                    new DailyOperationBadOutputReasonsModel()
                },
                Shift = "shift"
            };
            return model;
        }

        public DailyOperationModel GetNewDataInputFinishing(KanbanModel kanban)
        {

            DailyOperationModel model = new DailyOperationModel
            {
                KanbanId = kanban.Id,
                Type = "input",
                DateInput = DateTimeOffset.UtcNow,
                MachineId = kanban.Instruction.Steps.ElementAt(3).MachineId,
                StepId = kanban.Instruction.Steps.ElementAt(3).Id,
                StepProcess = kanban.Instruction.Steps.ElementAt(3).Process,
                BadOutputReasons = new List<DailyOperationBadOutputReasonsModel>
                {
                    new DailyOperationBadOutputReasonsModel()
                },
                Shift = "shift"
            };
            return model;
        }

        public DailyOperationModel GetNewDataInputQC(KanbanModel kanban)
        {

            DailyOperationModel model = new DailyOperationModel
            {
                KanbanId = kanban.Id,
                Type = "input",
                DateInput = DateTimeOffset.UtcNow,
                MachineId = kanban.Instruction.Steps.Last().MachineId,
                StepId = kanban.Instruction.Steps.Last().Id,
                StepProcess = kanban.Instruction.Steps.Last().Process,
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
