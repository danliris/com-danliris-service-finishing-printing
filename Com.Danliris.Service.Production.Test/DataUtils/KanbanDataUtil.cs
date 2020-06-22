using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.Machine;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils.MasterDataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Test.DataUtils
{
    public class KanbanDataUtil : BaseDataUtil<KanbanFacade, KanbanModel>
    {
        MachineDataUtil machineDataUtil;

        public KanbanDataUtil(MachineDataUtil machineDataUtil, KanbanFacade facade) : base(facade)
        {
            this.machineDataUtil = machineDataUtil;
        }

        public override async Task<KanbanModel> GetNewDataAsync()
        {
            MachineModel machine = await machineDataUtil.GetTestData();
            KanbanModel model = new KanbanModel
            {
                CartCartNumber = "11",
                Instruction = new KanbanInstructionModel
                {
                    Steps = new List<KanbanStepModel>
                    {
                        new KanbanStepModel
                        {
                            Machine = new MachineModel
                            {
                                Id = machine.Id
                            },
                            MachineId = machine.Id,
                            StepIndicators = new List<KanbanStepIndicatorModel>
                            {
                                new KanbanStepIndicatorModel()
                            },
                            ProcessArea = "area pre treatment"
                        },
                        new KanbanStepModel
                        {
                            Machine = new MachineModel
                            {
                                Id = machine.Id
                            },
                            MachineId = machine.Id,
                            StepIndicators = new List<KanbanStepIndicatorModel>
                            {
                                new KanbanStepIndicatorModel()
                            },
                            ProcessArea = "area dyeing"
                        },
                        new KanbanStepModel
                        {
                            Machine = new MachineModel
                            {
                                Id = machine.Id
                            },
                            MachineId = machine.Id,
                            StepIndicators = new List<KanbanStepIndicatorModel>
                            {
                                new KanbanStepIndicatorModel()
                            },
                            ProcessArea = "area printing"
                        },
                        new KanbanStepModel
                        {
                            Machine = new MachineModel
                            {
                                Id = machine.Id
                            },
                            MachineId = machine.Id,
                            StepIndicators = new List<KanbanStepIndicatorModel>
                            {
                                new KanbanStepIndicatorModel()
                            },
                            ProcessArea = "area finishing"
                        },
                        new KanbanStepModel
                        {
                            Machine = new MachineModel
                            {
                                Id = machine.Id
                            },
                            MachineId = machine.Id,
                            StepIndicators = new List<KanbanStepIndicatorModel>
                            {
                                new KanbanStepIndicatorModel()
                            },
                            ProcessArea = "area qc"
                        },
                    }
                }
            };
            return model;
        }
    }
}
