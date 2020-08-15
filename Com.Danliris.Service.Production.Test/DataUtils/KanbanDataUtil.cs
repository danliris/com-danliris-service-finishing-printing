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

        public KanbanSnapshotModel GetKanbanSnapshot()
        {
            KanbanSnapshotModel kanbanSnapshot = new KanbanSnapshotModel()
            {
                Buyer = "Buyer",
                DOCreatedUtcMonth = DateTime.Now.Month,
                DOCreatedUtcYear = DateTime.Now.Year,
                DyeingBadOutputQty = 1,
                DyeingCartNumber = "",
                DyeingDay = 1,
                DyeingGoodOutputQty = 1,
                DyeingInputDate = 1,
                DyeingInputQty = 1,
                DyeingInputStepIndex = 1,
                DyeingKonstruksi = "DyeingKonstruksi",
                KanbanId = 1,
                PreTreatmentInputDate = DateTime.Now.Day,
                PreTreatmentOutputDate =DateTime.Now.Day,
                PreTreatmentInputStepIndex =1,
            };
            return  kanbanSnapshot;
        }

      
        public KanbanSnapshotModel GetKanbanSnapshotPreTreatmentOutputDateYesterday()
        {
            KanbanSnapshotModel kanbanSnapshot = new KanbanSnapshotModel()
            {
                Buyer = "Buyer",
                DOCreatedUtcMonth = DateTime.Now.Month,
                DOCreatedUtcYear = DateTime.Now.Year,
                DyeingBadOutputQty = 1,
                DyeingCartNumber = "",
                DyeingDay = 1,
                DyeingGoodOutputQty = 1,
                DyeingInputDate = 1,
                DyeingInputQty = 1,
                DyeingInputStepIndex = 1,
                DyeingKonstruksi = "DyeingKonstruksi",
                KanbanId = 1,
                PreTreatmentInputDate = DateTime.Now.AddDays(-1).Day,
                PreTreatmentOutputDate = DateTime.Now.AddDays(-1).Day,
                PreTreatmentInputStepIndex = 1,
               
            };
            return kanbanSnapshot;
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


        public  async Task<KanbanModel> GetNewDataAsyncToUpdate()
        {
            var oldKanban = await  this.GetTestData();
            MachineModel machine = await machineDataUtil.GetTestData();
            KanbanModel model = new KanbanModel
            {
           
                OldKanbanId = oldKanban.Id,
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
