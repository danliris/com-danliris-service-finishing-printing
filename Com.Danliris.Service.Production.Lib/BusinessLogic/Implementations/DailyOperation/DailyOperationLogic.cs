using Com.Danliris.Service.Finishing.Printing.Lib.Models.Daily_Operation;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Daily_Operation;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.DailyOperation
{
    public class DailyOperationLogic : BaseLogic<DailyOperationModel>
    {
        private const string UserAgent = "production-service";
        private DailyOperationBadOutputReasonsLogic DailyOperationBadOutputReasonsLogic;
        private readonly DbSet<KanbanModel> DbSetKanban;
        private readonly ProductionDbContext DbContext;

        public DailyOperationLogic(DailyOperationBadOutputReasonsLogic dailyOperationBadOutputReasonsLogic, IIdentityService identityService, ProductionDbContext dbContext) : base(identityService, dbContext)
        {
            this.DbContext = dbContext;
            this.DbSetKanban = DbContext.Set<KanbanModel>();
            this.DailyOperationBadOutputReasonsLogic = dailyOperationBadOutputReasonsLogic;
        }

        public override void CreateModel(DailyOperationModel model)
        {

            if (model.Type == "output")
            {
                foreach (DailyOperationBadOutputReasonsModel item in model.BadOutputReasons)
                {
                    EntityExtension.FlagForCreate(item, IdentityService.Username, UserAgent);
                }
            }

            this.SetKanbanCreate(model);

            model.Kanban = null;
            model.Machine = null;

            base.CreateModel(model);
        }

        public override async Task DeleteModel(int id)
        {
            var model = await ReadModelById(id);

            if (model.Type == "output")
            {
                // string flag = "delete";
                foreach (var item in model.BadOutputReasons)
                {
                    EntityExtension.FlagForDelete(item, IdentityService.Username, UserAgent);
                }
            }

            this.SetKanbanDelete(model);
            model.Kanban = null;
            model.Machine = null;
            EntityExtension.FlagForDelete(model, IdentityService.Username, UserAgent, true);
            DbSet.Update(model);
        }

        public override async Task UpdateModelAsync(int id, DailyOperationModel model)
        {
            if (model.Type == "output")
            {
                // string flag = "update";
                HashSet<int> detailId = DailyOperationBadOutputReasonsLogic.DataId(id);
                foreach (var itemId in detailId)
                {
                    DailyOperationBadOutputReasonsModel data = model.BadOutputReasons.FirstOrDefault(prop => prop.Id.Equals(itemId));
                    if (data == null)
                        await DailyOperationBadOutputReasonsLogic.DeleteModel(itemId);
                    else
                    {
                        await DailyOperationBadOutputReasonsLogic.UpdateModelAsync(itemId, data);
                    }

                    foreach (DailyOperationBadOutputReasonsModel item in model.BadOutputReasons)
                    {
                        if (item.Id == 0)
                            DailyOperationBadOutputReasonsLogic.CreateModel(item);
                    }
                }
                // this.UpdateKanban(model, flag);
            }

            this.SetKanbanUpdate(model);
            model.Kanban = null;
            model.Machine = null;

            EntityExtension.FlagForUpdate(model, IdentityService.Username, UserAgent);
            DbSet.Update(model);
        }

        public override Task<DailyOperationModel> ReadModelById(int id)
        {
            return DbSet.Include(d => d.Machine)
                .Include(d => d.Kanban)
                    .ThenInclude(d => d.Instruction)
                        .ThenInclude(d => d.Steps)
                .Include(d => d.BadOutputReasons)
                .FirstOrDefaultAsync(d => d.Id.Equals(id) && d.IsDeleted.Equals(false));
        }

        //public void UpdateKanban(DailyOperationModel model, string flag)
        //{
        //	KanbanModel kanban = this.DbSetKanban.Where(k => k.Id.Equals(model.KanbanId)).SingleOrDefault();

        //	int currentStepIndex = (flag == "create" ? kanban.CurrentStepIndex += 1 : flag == "update" ? kanban.CurrentStepIndex : kanban.CurrentStepIndex -= 1);

        //	kanban.CurrentQty = model.GoodOutput != null ? (double)model.GoodOutput : 0;
        //	kanban.CurrentStepIndex = currentStepIndex;
        //	kanban.GoodOutput = model.GoodOutput != null ? (double)model.GoodOutput : 0;
        //	kanban.BadOutput = model.BadOutput != null ? (double)model.GoodOutput : 0;

        //	EntityExtension.FlagForUpdate(kanban, IdentityService.Username, UserAgent);
        //	DbSetKanban.Update(kanban);
        //}

        public void SetKanbanCreate(DailyOperationModel model)
        {
            var selectedKanban = this.DbSetKanban.Where(kanban => kanban.Id == model.KanbanId).SingleOrDefault();
            // var selectedKanbanInstruction = this.DbContext.KanbanInstructions.Where(kanbanInstruction => kanbanInstruction.KanbanId == selectedKanban.Id).SingleOrDefault();

            if (model.Type.ToUpper() == "INPUT")
            {
                selectedKanban.CurrentStepIndex += 1;
                selectedKanban.CurrentQty = model.Input.GetValueOrDefault();
                model.KanbanStepIndex = selectedKanban.CurrentStepIndex;
            }
            else if (model.Type.ToUpper() == "OUTPUT")
            {
                model.KanbanStepIndex = selectedKanban.CurrentStepIndex;
                selectedKanban.CurrentQty = model.GoodOutput.GetValueOrDefault() + model.BadOutput.GetValueOrDefault();
                selectedKanban.GoodOutput = model.GoodOutput.GetValueOrDefault();
                selectedKanban.BadOutput = model.BadOutput.GetValueOrDefault();
            }
            selectedKanban.FlagForUpdate(IdentityService.Username, UserAgent);
            DbContext.Kanbans.Update(selectedKanban);
        }

        public void SetKanbanUpdate(DailyOperationModel model)
        {
            var selectedKanban = this.DbSetKanban.Where(kanban => kanban.Id == model.KanbanId).SingleOrDefault();
            // var selectedKanbanInstruction = this.DbContext.KanbanInstructions.Where(kanbanInstruction => kanbanInstruction.KanbanId == selectedKanban.Id).SingleOrDefault();

            if (model.Type.ToUpper() == "INPUT")
            {
                selectedKanban.CurrentQty = model.Input.GetValueOrDefault();
            }
            else if (model.Type.ToUpper() == "OUTPUT")
            {
                selectedKanban.CurrentQty = model.GoodOutput.GetValueOrDefault() + model.BadOutput.GetValueOrDefault();
                selectedKanban.GoodOutput = model.GoodOutput.GetValueOrDefault();
                selectedKanban.BadOutput = model.BadOutput.GetValueOrDefault();
            }
            selectedKanban.FlagForUpdate(IdentityService.Username, UserAgent);
            DbContext.Kanbans.Update(selectedKanban);
        }

        public void SetKanbanDelete(DailyOperationModel model)
        {
            var selectedKanban = this.DbSetKanban.Where(kanban => kanban.Id == model.KanbanId).SingleOrDefault();

            var previousState = GetPreviousState(model);

            if (previousState != null)
            {
                if (previousState.Type.ToUpper() == "INPUT")
                {
                    selectedKanban.CurrentStepIndex -= 1;
                    selectedKanban.CurrentQty = previousState.Input.GetValueOrDefault();
                }
                else if (previousState.Type.ToUpper() == "OUTPUT")
                {
                    selectedKanban.CurrentQty = previousState.GoodOutput.GetValueOrDefault() + previousState.BadOutput.GetValueOrDefault();
                    selectedKanban.GoodOutput = previousState.GoodOutput.GetValueOrDefault();
                    selectedKanban.BadOutput = previousState.BadOutput.GetValueOrDefault();
                }
            }
            selectedKanban.FlagForUpdate(IdentityService.Username, UserAgent);
            DbContext.Kanbans.Update(selectedKanban);
        }

        public DailyOperationModel GetPreviousState(DailyOperationModel model)
        {
            if (model.Type.ToUpper() == "INPUT")
            {
                return DbSet.Where(dailyOperation => dailyOperation.KanbanId == model.KanbanId && dailyOperation.KanbanStepIndex == model.KanbanStepIndex - 1 && model.Type.ToUpper() == "OUTPUT").SingleOrDefault();
            }
            else
            {
                return DbSet.Where(dailyOperation => dailyOperation.KanbanId == model.KanbanId && dailyOperation.KanbanStepIndex == model.KanbanStepIndex && model.Type.ToUpper() == "INPUT").SingleOrDefault();
            }
        }

        //public HashSet<int> hasInput(DailyOperationViewModel vm)
        //{
        //	return new HashSet<int>(DbSet.Where(d => d.Kanban.Id == vm.Kanban.Id && d.Type == vm.Type && d.StepId == vm.Step.StepId).Select(d => d.Id));
        //}

        public DailyOperationModel GetInputDataForCurrentOutput(DailyOperationViewModel vm)
        {
            return DbSet.FirstOrDefault(s => s.KanbanId == vm.Kanban.Id && s.Type.ToLower() == "input" && s.KanbanStepIndex == vm.Kanban.CurrentStepIndex.GetValueOrDefault());
        }

        public bool ValidateCreateOutputDataCheckCurrentInput(DailyOperationViewModel vm)
        {
            return !DbSet.Any(s => s.KanbanId == vm.Kanban.Id && s.Type.ToLower() == "input" && s.KanbanStepIndex == vm.Kanban.CurrentStepIndex.GetValueOrDefault());
        }

        public bool ValidateCreateOutputDataCheckDuplicate(DailyOperationViewModel vm)
        {
            return DbSet.Any(s => s.KanbanId == vm.Kanban.Id && s.Type.ToLower() == "output" && s.KanbanStepIndex == vm.Kanban.CurrentStepIndex.GetValueOrDefault());
        }

        public bool ValidateCreateInputDataCheckPreviousOutput(DailyOperationViewModel vm)
        {
            if (vm.Kanban.CurrentStepIndex == 0)
                return false;

            return !DbSet.Any(s => s.KanbanId == vm.Kanban.Id && s.Type.ToLower() == "output" && (s.KanbanStepIndex == vm.Kanban.CurrentStepIndex.GetValueOrDefault()));
        }

        public bool ValidateCreateInputDataCheckDuplicate(DailyOperationViewModel vm)
        {

            return DbSet.Any(s => s.KanbanId == vm.Kanban.Id && s.Type.ToLower() == "input" && s.KanbanStepIndex == (vm.Kanban.CurrentStepIndex.GetValueOrDefault() + 1));
        }

        public void CreateSnapshot(DailyOperationModel model)
        {
            var selectedKanban = DbSetKanban.Include(s => s.Instruction).ThenInclude(s => s.Steps)
                .Where(kanban => kanban.Id == model.KanbanId).SingleOrDefault();
            var stepData = selectedKanban.Instruction.Steps.FirstOrDefault(s => s.Process == model.StepProcess);
            int stepIndex = stepData.StepIndex;
            string konstruksi = string.Format("{0} / {1} / {2}", selectedKanban.ProductionOrderMaterialName, selectedKanban.ProductionOrderMaterialConstructionName, selectedKanban.FinishWidth);
            var snapshotData = DbContext.KanbanSnapshots
                    .Where(s => s.KanbanId == model.KanbanId && s.DOCreatedUtcMonth == model.CreatedUtc.Month && s.DOCreatedUtcYear == model.CreatedUtc.Year);

            if (stepData.ProcessArea.ToLower() == "area pre treatment")
            {
                if (model.Type.ToLower() == "input")
                {
                    var inputData = snapshotData.FirstOrDefault(s => s.PreTreatmentInputDate == model.CreatedUtc.Day);
                    if (inputData == null)
                    {
                        KanbanSnapshotModel newModel = new KanbanSnapshotModel()
                        {
                            Buyer = selectedKanban.ProductionOrderBuyerName,
                            DOCreatedUtcMonth = model.CreatedUtc.Month,
                            DOCreatedUtcYear = model.CreatedUtc.Year,
                            KanbanId = model.KanbanId,
                            Konstruksi = konstruksi,
                            Qty = selectedKanban.SelectedProductionOrderDetailQuantity,
                            SPPNo = selectedKanban.ProductionOrderOrderNo,
                            PreTreatmentInputDate = model.CreatedUtc.Day,
                            PreTreatmentInputQty = model.Input,
                            PreTreatmentKonstruksi = konstruksi,
                            PreTreatmentCartNumber = selectedKanban.CartCartNumber,
                            PreTreatmentInputStepIndex = stepIndex,
                            PreTreatmentDay = 1

                        };
                        DbContext.KanbanSnapshots.Add(newModel);
                    }
                    else
                    {
                        if (inputData.PreTreatmentInputDate == 0 || model.CreatedUtc.Day < inputData.PreTreatmentInputDate)
                        {
                            inputData.PreTreatmentKonstruksi = konstruksi;
                            inputData.DOCreatedUtcMonth = model.CreatedUtc.Month;
                            inputData.DOCreatedUtcYear = model.CreatedUtc.Year;
                            inputData.PreTreatmentInputDate = model.CreatedUtc.Day;
                            inputData.PreTreatmentInputQty = model.Input;
                            inputData.PreTreatmentInputStepIndex = stepIndex;
                            inputData.PreTreatmentDay = 1;
                            inputData.PreTreatmentCartNumber = selectedKanban.CartCartNumber;
                        }
                    }
                }
                else
                {
                    var outputData = snapshotData.FirstOrDefault(s => s.PreTreatmentOutputDate == model.CreatedUtc.Day);
                    var stepIndexexPreTreatment = selectedKanban.Instruction.Steps.Where(s => s.ProcessArea.ToLower() == "area pre treatment").Select(s => s.StepIndex);
                    var inputData = DbContext.KanbanSnapshots
                        .Where(s => s.KanbanId == model.KanbanId && s.DOCreatedUtcMonth == model.CreatedUtc.Month && s.DOCreatedUtcYear == model.CreatedUtc.Year && stepIndexexPreTreatment.Contains(s.PreTreatmentInputStepIndex))
                        .OrderBy(s => s.PreTreatmentInputStepIndex).FirstOrDefault();
                    if (outputData == null)
                    {
                        if (inputData != null)
                        {
                            if (model.CreatedUtc.Day == inputData.PreTreatmentInputDate)
                            {
                                inputData.PreTreatmentKonstruksi = konstruksi;
                                inputData.PreTreatmentBadOutputQty = model.BadOutput;
                                inputData.PreTreatmentGoodOutputQty = model.GoodOutput;
                                inputData.PreTreatmentOutputDate = model.CreatedUtc.Day;
                                inputData.PreTreatmentCartNumber = selectedKanban.CartCartNumber;
                                inputData.PreTreatmentOutputStepIndex = stepIndex;
                                inputData.PreTreatmentDay = Math.Abs(model.CreatedUtc.Day - inputData.PreTreatmentInputDate) + 1;
                            }
                            else
                            {
                                KanbanSnapshotModel newModel = new KanbanSnapshotModel()
                                {
                                    Buyer = selectedKanban.ProductionOrderBuyerName,
                                    DOCreatedUtcMonth = model.CreatedUtc.Month,
                                    DOCreatedUtcYear = model.CreatedUtc.Year,
                                    KanbanId = model.KanbanId,
                                    Konstruksi = konstruksi,
                                    Qty = selectedKanban.SelectedProductionOrderDetailQuantity,
                                    SPPNo = selectedKanban.ProductionOrderOrderNo,
                                    PreTreatmentDay = Math.Abs(model.CreatedUtc.Day - inputData.PreTreatmentInputDate) + 1,
                                    PreTreatmentInputQty = inputData.PreTreatmentInputQty,
                                    PreTreatmentKonstruksi = konstruksi,
                                    PreTreatmentBadOutputQty = model.BadOutput,
                                    PreTreatmentGoodOutputQty = model.GoodOutput,
                                    PreTreatmentOutputStepIndex = stepIndex,
                                    PreTreatmentCartNumber = selectedKanban.CartCartNumber,
                                    PreTreatmentOutputDate = model.CreatedUtc.Day

                                };
                                DbContext.KanbanSnapshots.Add(newModel);
                            }

                        }

                    }
                    else
                    {
                        if (outputData.PreTreatmentOutputDate <= model.CreatedUtc.Day)
                        {
                            outputData.PreTreatmentKonstruksi = konstruksi;
                            outputData.DOCreatedUtcMonth = model.CreatedUtc.Month;
                            outputData.DOCreatedUtcYear = model.CreatedUtc.Year;
                            outputData.PreTreatmentOutputDate = model.CreatedUtc.Day;
                            outputData.PreTreatmentBadOutputQty = model.BadOutput;
                            outputData.PreTreatmentGoodOutputQty = model.GoodOutput;
                            outputData.PreTreatmentOutputStepIndex = stepIndex;
                            outputData.PreTreatmentCartNumber = selectedKanban.CartCartNumber;
                            outputData.PreTreatmentDay = Math.Abs(model.CreatedUtc.Day - inputData.PreTreatmentInputDate) + 1;
                        }
                    }
                }
            }
            else if (stepData.ProcessArea.ToLower() == "area dyeing")
            {
                if (model.Type.ToLower() == "input")
                {
                    var inputData = snapshotData.FirstOrDefault(s => s.DyeingInputDate == model.CreatedUtc.Day
                                                                    || s.PreTreatmentOutputDate == model.CreatedUtc.Day);
                    if (inputData == null)
                    {
                        KanbanSnapshotModel newModel = new KanbanSnapshotModel()
                        {
                            Buyer = selectedKanban.ProductionOrderBuyerName,
                            DOCreatedUtcMonth = model.CreatedUtc.Month,
                            DOCreatedUtcYear = model.CreatedUtc.Year,
                            KanbanId = model.KanbanId,
                            Konstruksi = konstruksi,
                            Qty = selectedKanban.SelectedProductionOrderDetailQuantity,
                            SPPNo = selectedKanban.ProductionOrderOrderNo,
                            DyeingInputDate = model.CreatedUtc.Day,
                            DyeingInputQty = model.Input,
                            DyeingKonstruksi = konstruksi,
                            DyeingCartNumber = selectedKanban.CartCartNumber,
                            DyeingInputStepIndex = stepIndex,
                            DyeingDay = 1

                        };
                        DbContext.KanbanSnapshots.Add(newModel);
                    }
                    else
                    {
                        if (inputData.DyeingInputDate == 0 || model.CreatedUtc.Day < inputData.DyeingInputDate)
                        {
                            inputData.DyeingKonstruksi = konstruksi;
                            inputData.DOCreatedUtcMonth = model.CreatedUtc.Month;
                            inputData.DOCreatedUtcYear = model.CreatedUtc.Year;
                            inputData.DyeingInputDate = model.CreatedUtc.Day;
                            inputData.DyeingInputQty = model.Input;
                            inputData.DyeingInputStepIndex = stepIndex;
                            inputData.DyeingCartNumber = selectedKanban.CartCartNumber;
                            inputData.DyeingDay = 1;
                        }
                    }
                }
                else
                {
                    var outputData = snapshotData.FirstOrDefault(s => s.DyeingOutputDate == model.CreatedUtc.Day);
                    var stepIndexexDyeing = selectedKanban.Instruction.Steps.Where(s => s.ProcessArea.ToLower() == "area dyeing").Select(s => s.StepIndex);
                    var inputData = DbContext.KanbanSnapshots
                        .Where(s => s.KanbanId == model.KanbanId && s.DOCreatedUtcMonth == model.CreatedUtc.Month && s.DOCreatedUtcYear == model.CreatedUtc.Year && stepIndexexDyeing.Contains(s.DyeingInputStepIndex))
                        .OrderBy(s => s.DyeingInputStepIndex).FirstOrDefault();
                    if (outputData == null)
                    {
                        if (inputData != null)
                        {
                            if (model.CreatedUtc.Day == inputData.DyeingInputDate)
                            {
                                inputData.DyeingKonstruksi = konstruksi;
                                inputData.DyeingBadOutputQty = model.BadOutput;
                                inputData.DyeingGoodOutputQty = model.GoodOutput;
                                inputData.DyeingOutputDate = model.CreatedUtc.Day;
                                inputData.DyeingCartNumber = selectedKanban.CartCartNumber;
                                inputData.DyeingOutputStepIndex = stepIndex;
                                inputData.DyeingDay = Math.Abs(model.CreatedUtc.Day - inputData.DyeingInputDate) + 1;
                            }
                            else
                            {
                                KanbanSnapshotModel newModel = new KanbanSnapshotModel()
                                {
                                    Buyer = selectedKanban.ProductionOrderBuyerName,
                                    DOCreatedUtcMonth = model.CreatedUtc.Month,
                                    DOCreatedUtcYear = model.CreatedUtc.Year,
                                    KanbanId = model.KanbanId,
                                    Konstruksi = konstruksi,
                                    Qty = selectedKanban.SelectedProductionOrderDetailQuantity,
                                    SPPNo = selectedKanban.ProductionOrderOrderNo,
                                    DyeingDay = Math.Abs(model.CreatedUtc.Day - inputData.DyeingInputDate) + 1,
                                    DyeingInputQty = inputData.DyeingInputQty,
                                    DyeingKonstruksi = konstruksi,
                                    DyeingBadOutputQty = model.BadOutput,
                                    DyeingOutputStepIndex = stepIndex,
                                    DyeingGoodOutputQty = model.GoodOutput,
                                    DyeingCartNumber = selectedKanban.CartCartNumber,
                                    DyeingOutputDate = model.CreatedUtc.Day

                                };
                                DbContext.KanbanSnapshots.Add(newModel);
                            }

                        }

                    }
                    else
                    {
                        if (outputData.DyeingOutputDate <= model.CreatedUtc.Day)
                        {
                            outputData.DyeingKonstruksi = konstruksi;
                            outputData.DOCreatedUtcMonth = model.CreatedUtc.Month;
                            outputData.DOCreatedUtcYear = model.CreatedUtc.Year;
                            outputData.DyeingOutputDate = model.CreatedUtc.Day;
                            outputData.DyeingBadOutputQty = model.BadOutput;
                            outputData.DyeingGoodOutputQty = model.GoodOutput;
                            outputData.DyeingOutputStepIndex = stepIndex;
                            outputData.DyeingCartNumber = selectedKanban.CartCartNumber;
                            outputData.DyeingDay = Math.Abs(model.CreatedUtc.Day - inputData.DyeingInputDate) + 1;
                        }
                    }
                }
            }
            else if (stepData.ProcessArea.ToLower() == "area printing")
            {
                if (model.Type.ToLower() == "input")
                {
                    var inputData = snapshotData.FirstOrDefault(s => s.PrintingInputDate == model.CreatedUtc.Day ||
                                                                    s.DyeingOutputDate == model.CreatedUtc.Day ||
                                                                    s.PreTreatmentOutputDate == model.CreatedUtc.Day);
                    if (inputData == null)
                    {
                        KanbanSnapshotModel newModel = new KanbanSnapshotModel()
                        {
                            Buyer = selectedKanban.ProductionOrderBuyerName,
                            DOCreatedUtcMonth = model.CreatedUtc.Month,
                            DOCreatedUtcYear = model.CreatedUtc.Year,
                            KanbanId = model.KanbanId,
                            Konstruksi = konstruksi,
                            Qty = selectedKanban.SelectedProductionOrderDetailQuantity,
                            SPPNo = selectedKanban.ProductionOrderOrderNo,
                            PrintingInputDate = model.CreatedUtc.Day,
                            PrintingInputQty = model.Input,
                            PrintingKonstruksi = konstruksi,
                            PrintingCartNumber = selectedKanban.CartCartNumber,
                            PrintingInputStepIndex = stepIndex,
                            PrintingDay = 1

                        };
                        DbContext.KanbanSnapshots.Add(newModel);
                    }
                    else
                    {
                        if (inputData.PrintingInputDate == 0 || model.CreatedUtc.Day < inputData.PrintingInputDate)
                        {
                            inputData.PrintingKonstruksi = konstruksi;
                            inputData.DOCreatedUtcMonth = model.CreatedUtc.Month;
                            inputData.DOCreatedUtcYear = model.CreatedUtc.Year;
                            inputData.PrintingInputDate = model.CreatedUtc.Day;
                            inputData.PrintingInputQty = model.Input;
                            inputData.PrintingInputStepIndex = stepIndex;
                            inputData.PrintingCartNumber = selectedKanban.CartCartNumber;
                            inputData.PrintingDay = 1;
                        }
                    }
                }
                else
                {
                    var outputData = snapshotData.FirstOrDefault(s => s.PrintingOutputDate == model.CreatedUtc.Day);
                    var stepIndexexPrinting = selectedKanban.Instruction.Steps.Where(s => s.ProcessArea.ToLower() == "area printing").Select(s => s.StepIndex);
                    var inputData = DbContext.KanbanSnapshots
                        .Where(s => s.KanbanId == model.KanbanId && s.DOCreatedUtcMonth == model.CreatedUtc.Month && s.DOCreatedUtcYear == model.CreatedUtc.Year && stepIndexexPrinting.Contains(s.PrintingInputStepIndex))
                        .OrderBy(s => s.PrintingInputStepIndex).FirstOrDefault();
                    if (outputData == null)
                    {
                        if (inputData != null)
                        {
                            if (model.CreatedUtc.Day == inputData.PrintingInputDate)
                            {
                                inputData.PrintingKonstruksi = konstruksi;
                                inputData.PrintingBadOutputQty = model.BadOutput;
                                inputData.PrintingGoodOutputQty = model.GoodOutput;
                                inputData.PrintingOutputDate = model.CreatedUtc.Day;
                                inputData.PrintingCartNumber = selectedKanban.CartCartNumber;
                                inputData.PrintingOutputStepIndex = stepIndex;
                                inputData.PrintingDay = Math.Abs(model.CreatedUtc.Day - inputData.PrintingInputDate) + 1;
                            }
                            else
                            {
                                KanbanSnapshotModel newModel = new KanbanSnapshotModel()
                                {
                                    Buyer = selectedKanban.ProductionOrderBuyerName,
                                    DOCreatedUtcMonth = model.CreatedUtc.Month,
                                    DOCreatedUtcYear = model.CreatedUtc.Year,
                                    KanbanId = model.KanbanId,
                                    Konstruksi = konstruksi,
                                    Qty = selectedKanban.SelectedProductionOrderDetailQuantity,
                                    SPPNo = selectedKanban.ProductionOrderOrderNo,
                                    PrintingOutputStepIndex = stepIndex,
                                    PrintingDay = Math.Abs(model.CreatedUtc.Day - inputData.PrintingInputDate) + 1,
                                    PrintingInputQty = inputData.PrintingInputQty,
                                    PrintingKonstruksi = konstruksi,
                                    PrintingBadOutputQty = model.BadOutput,
                                    PrintingGoodOutputQty = model.GoodOutput,
                                    PrintingCartNumber = selectedKanban.CartCartNumber,
                                    PrintingOutputDate = model.CreatedUtc.Day

                                };
                                DbContext.KanbanSnapshots.Add(newModel);
                            }

                        }

                    }
                    else
                    {
                        if (outputData.PrintingOutputDate <= model.CreatedUtc.Day)
                        {
                            outputData.PrintingKonstruksi = konstruksi;
                            outputData.DOCreatedUtcMonth = model.CreatedUtc.Month;
                            outputData.DOCreatedUtcYear = model.CreatedUtc.Year;
                            outputData.PrintingOutputStepIndex = stepIndex;
                            outputData.PrintingOutputDate = model.CreatedUtc.Day;
                            outputData.PrintingBadOutputQty = model.BadOutput;
                            outputData.PrintingGoodOutputQty = model.GoodOutput;
                            outputData.PrintingCartNumber = selectedKanban.CartCartNumber;
                            outputData.PrintingDay = Math.Abs(model.CreatedUtc.Day - inputData.PrintingInputDate) + 1;
                        }
                    }
                }
            }
            else if (stepData.ProcessArea.ToLower() == "area finishing")
            {
                if (model.Type.ToLower() == "input")
                {
                    var inputData = snapshotData.FirstOrDefault(s => s.FinishingInputDate == model.CreatedUtc.Day ||
                                                                    s.PrintingOutputDate == model.CreatedUtc.Day ||
                                                                    s.DyeingOutputDate == model.CreatedUtc.Day ||
                                                                    s.PreTreatmentOutputDate == model.CreatedUtc.Day);
                    if (inputData == null)
                    {
                        KanbanSnapshotModel newModel = new KanbanSnapshotModel()
                        {
                            Buyer = selectedKanban.ProductionOrderBuyerName,
                            DOCreatedUtcMonth = model.CreatedUtc.Month,
                            DOCreatedUtcYear = model.CreatedUtc.Year,
                            KanbanId = model.KanbanId,
                            Konstruksi = konstruksi,
                            Qty = selectedKanban.SelectedProductionOrderDetailQuantity,
                            SPPNo = selectedKanban.ProductionOrderOrderNo,
                            FinishingInputDate = model.CreatedUtc.Day,
                            FinishingInputQty = model.Input,
                            FinishingKonstruksi = konstruksi,
                            FinishingInputStepIndex = stepIndex,
                            FinishingCartNumber = selectedKanban.CartCartNumber,
                            FinishingDay = 1

                        };
                        DbContext.KanbanSnapshots.Add(newModel);
                    }
                    else
                    {
                        if (inputData.FinishingInputDate == 0 || model.CreatedUtc.Day < inputData.FinishingInputDate)
                        {
                            inputData.FinishingKonstruksi = konstruksi;
                            inputData.DOCreatedUtcMonth = model.CreatedUtc.Month;
                            inputData.DOCreatedUtcYear = model.CreatedUtc.Year;
                            inputData.FinishingInputDate = model.CreatedUtc.Day;
                            inputData.FinishingInputQty = model.Input;
                            inputData.FinishingInputStepIndex = stepIndex;
                            inputData.FinishingCartNumber = selectedKanban.CartCartNumber;
                            inputData.FinishingDay = 1;
                        }
                    }
                }
                else
                {
                    var outputData = snapshotData.FirstOrDefault(s => s.FinishingOutputDate == model.CreatedUtc.Day);
                    var stepIndexexFinishing = selectedKanban.Instruction.Steps.Where(s => s.ProcessArea.ToLower() == "area finishing").Select(s => s.StepIndex);
                    var inputData = DbContext.KanbanSnapshots
                        .Where(s => s.KanbanId == model.KanbanId && s.DOCreatedUtcMonth == model.CreatedUtc.Month && s.DOCreatedUtcYear == model.CreatedUtc.Year && stepIndexexFinishing.Contains(s.FinishingInputStepIndex))
                        .OrderBy(s => s.FinishingInputStepIndex).FirstOrDefault();
                    if (outputData == null)
                    {
                        if (inputData != null)
                        {
                            if (model.CreatedUtc.Day == inputData.FinishingInputDate)
                            {
                                inputData.FinishingKonstruksi = konstruksi;
                                inputData.FinishingBadOutputQty = model.BadOutput;
                                inputData.FinishingGoodOutputQty = model.GoodOutput;
                                inputData.FinishingOutputDate = model.CreatedUtc.Day;
                                inputData.FinishingCartNumber = selectedKanban.CartCartNumber;
                                inputData.FinishingOutputStepIndex = stepIndex;
                                inputData.FinishingDay = Math.Abs(model.CreatedUtc.Day - inputData.FinishingInputDate) + 1;
                            }
                            else
                            {
                                KanbanSnapshotModel newModel = new KanbanSnapshotModel()
                                {
                                    Buyer = selectedKanban.ProductionOrderBuyerName,
                                    DOCreatedUtcMonth = model.CreatedUtc.Month,
                                    DOCreatedUtcYear = model.CreatedUtc.Year,
                                    KanbanId = model.KanbanId,
                                    Konstruksi = konstruksi,
                                    Qty = selectedKanban.SelectedProductionOrderDetailQuantity,
                                    SPPNo = selectedKanban.ProductionOrderOrderNo,
                                    FinishingDay = Math.Abs(model.CreatedUtc.Day - inputData.FinishingInputDate) + 1,
                                    FinishingInputQty = inputData.FinishingInputQty,
                                    FinishingKonstruksi = konstruksi,
                                    FinishingBadOutputQty = model.BadOutput,
                                    FinishingOutputStepIndex = stepIndex,
                                    FinishingGoodOutputQty = model.GoodOutput,
                                    FinishingCartNumber = selectedKanban.CartCartNumber,
                                    FinishingOutputDate = model.CreatedUtc.Day

                                };
                                DbContext.KanbanSnapshots.Add(newModel);
                            }

                        }

                    }
                    else
                    {
                        if (outputData.FinishingOutputDate <= model.CreatedUtc.Day)
                        {
                            outputData.FinishingKonstruksi = konstruksi;
                            outputData.DOCreatedUtcMonth = model.CreatedUtc.Month;
                            outputData.DOCreatedUtcYear = model.CreatedUtc.Year;
                            outputData.FinishingOutputDate = model.CreatedUtc.Day;
                            outputData.FinishingBadOutputQty = model.BadOutput;
                            outputData.FinishingGoodOutputQty = model.GoodOutput;
                            outputData.FinishingCartNumber = selectedKanban.CartCartNumber;
                            outputData.FinishingOutputStepIndex = stepIndex;
                            outputData.FinishingDay = Math.Abs(model.CreatedUtc.Day - inputData.FinishingInputDate) + 1;
                        }
                    }
                }
            }
            else
            {
                if (model.Type.ToLower() == "input")
                {
                    var inputData = snapshotData.FirstOrDefault(s => s.QCInputDate == model.CreatedUtc.Day ||
                                                                    s.FinishingOutputDate == model.CreatedUtc.Day ||
                                                                    s.PrintingOutputDate == model.CreatedUtc.Day ||
                                                                    s.DyeingOutputDate == model.CreatedUtc.Day ||
                                                                    s.PreTreatmentOutputDate == model.CreatedUtc.Day);
                    if (inputData == null)
                    {
                        KanbanSnapshotModel newModel = new KanbanSnapshotModel()
                        {
                            Buyer = selectedKanban.ProductionOrderBuyerName,
                            DOCreatedUtcMonth = model.CreatedUtc.Month,
                            DOCreatedUtcYear = model.CreatedUtc.Year,
                            KanbanId = model.KanbanId,
                            Konstruksi = konstruksi,
                            Qty = selectedKanban.SelectedProductionOrderDetailQuantity,
                            SPPNo = selectedKanban.ProductionOrderOrderNo,
                            QCInputDate = model.CreatedUtc.Day,
                            QCInputQty = model.Input,
                            QCKonstruksi = konstruksi,
                            QCCartNumber = selectedKanban.CartCartNumber,
                            QCInputStepIndex = stepIndex,
                            QCDay = 1

                        };
                        DbContext.KanbanSnapshots.Add(newModel);
                    }
                    else
                    {
                        if (inputData.QCInputDate == 0 || model.CreatedUtc.Day < inputData.QCInputDate)
                        {
                            inputData.QCKonstruksi = konstruksi;
                            inputData.DOCreatedUtcMonth = model.CreatedUtc.Month;
                            inputData.DOCreatedUtcYear = model.CreatedUtc.Year;
                            inputData.QCInputDate = model.CreatedUtc.Day;
                            inputData.QCInputQty = model.Input;
                            inputData.QCInputStepIndex = stepIndex;
                            inputData.QCCartNumber = selectedKanban.CartCartNumber;
                            inputData.QCDay = 1;
                        }
                    }
                }
                else
                {
                    var outputData = snapshotData.FirstOrDefault(s => s.QCOutputDate == model.CreatedUtc.Day);
                    var stepIndexexQC = selectedKanban.Instruction.Steps.Where(s => s.ProcessArea.ToLower() == "area qc").Select(s => s.StepIndex);
                    var inputData = DbContext.KanbanSnapshots
                        .Where(s => s.KanbanId == model.KanbanId && s.DOCreatedUtcMonth == model.CreatedUtc.Month && s.DOCreatedUtcYear == model.CreatedUtc.Year && stepIndexexQC.Contains(s.QCInputStepIndex))
                        .OrderBy(s => s.QCInputStepIndex).FirstOrDefault();
                    if (outputData == null)
                    {
                        if (inputData != null)
                        {
                            if (model.CreatedUtc.Day == inputData.QCInputDate)
                            {
                                inputData.QCKonstruksi = konstruksi;
                                inputData.QCBadOutputQty = model.BadOutput;
                                inputData.QCGoodOutputQty = model.GoodOutput;
                                inputData.QCOutputDate = model.CreatedUtc.Day;
                                inputData.QCCartNumber = selectedKanban.CartCartNumber;
                                inputData.QCOutputStepIndex = stepIndex;
                                inputData.QCDay = Math.Abs(model.CreatedUtc.Day - inputData.QCInputDate) + 1;
                            }
                            else
                            {
                                KanbanSnapshotModel newModel = new KanbanSnapshotModel()
                                {
                                    Buyer = selectedKanban.ProductionOrderBuyerName,
                                    DOCreatedUtcMonth = model.CreatedUtc.Month,
                                    DOCreatedUtcYear = model.CreatedUtc.Year,
                                    KanbanId = model.KanbanId,
                                    Konstruksi = konstruksi,
                                    Qty = selectedKanban.SelectedProductionOrderDetailQuantity,
                                    SPPNo = selectedKanban.ProductionOrderOrderNo,
                                    QCDay = Math.Abs(model.CreatedUtc.Day - inputData.QCInputDate) + 1,
                                    QCInputQty = inputData.QCInputQty,
                                    QCKonstruksi = konstruksi,
                                    QCBadOutputQty = model.BadOutput,
                                    QCGoodOutputQty = model.GoodOutput,
                                    QCOutputStepIndex = stepIndex,
                                    QCCartNumber = selectedKanban.CartCartNumber,
                                    QCOutputDate = model.CreatedUtc.Day

                                };
                                DbContext.KanbanSnapshots.Add(newModel);
                            }

                        }

                    }
                    else
                    {
                        if (outputData.QCOutputDate <= model.CreatedUtc.Day)
                        {
                            outputData.QCKonstruksi = konstruksi;
                            outputData.DOCreatedUtcMonth = model.CreatedUtc.Month;
                            outputData.DOCreatedUtcYear = model.CreatedUtc.Year;
                            outputData.QCOutputDate = model.CreatedUtc.Day;
                            outputData.QCBadOutputQty = model.BadOutput;
                            outputData.QCGoodOutputQty = model.GoodOutput;
                            outputData.QCOutputStepIndex = stepIndex;
                            outputData.QCCartNumber = selectedKanban.CartCartNumber;
                            outputData.QCDay = Math.Abs(model.CreatedUtc.Day - inputData.QCInputDate) + 1;
                        }
                    }
                }
            }


        }

        public void EditSnapshot(DailyOperationModel model)
        {
            var selectedKanban = DbSetKanban.Include(s => s.Instruction).ThenInclude(s => s.Steps)
               .Where(kanban => kanban.Id == model.KanbanId).SingleOrDefault();
            var stepData = selectedKanban.Instruction.Steps.FirstOrDefault(s => s.Process == model.StepProcess);
            int stepIndex = stepData.StepIndex;
            var data = DbContext.KanbanSnapshots
                        .Where(s => s.KanbanId == model.KanbanId && s.DOCreatedUtcYear == model.CreatedUtc.Year && s.DOCreatedUtcMonth == model.CreatedUtc.Month);
            if (stepData.ProcessArea.ToLower() == "area pre treatment")
            {
                if (model.Type.ToLower() == "input")
                {
                    var inputData = data
                        .FirstOrDefault(s => s.PreTreatmentInputDate == model.CreatedUtc.Day);

                    if (inputData != null)
                    {
                        if (inputData.PreTreatmentInputStepIndex == stepIndex)
                        {
                            inputData.PreTreatmentInputQty = model.Input;
                        }
                    }
                }
                else
                {
                    var outputData = data.FirstOrDefault(s => s.PreTreatmentOutputDate == model.CreatedUtc.Day);

                    if (outputData != null)
                    {
                        if (outputData.PreTreatmentOutputStepIndex == stepIndex)
                        {

                            outputData.PreTreatmentGoodOutputQty = model.GoodOutput;
                            outputData.PreTreatmentBadOutputQty = model.BadOutput;
                        }
                    }
                }
            }
            else if (stepData.ProcessArea.ToLower() == "area dyeing")
            {
                if (model.Type.ToLower() == "input")
                {
                    var inputData = data
                        .FirstOrDefault(s => s.DyeingInputDate == model.CreatedUtc.Day);

                    if (inputData != null)
                    {
                        if (inputData.DyeingInputStepIndex == stepIndex)
                        {
                            inputData.DyeingInputQty = model.Input;
                        }
                    }
                }
                else
                {
                    var outputData = data.FirstOrDefault(s => s.DyeingOutputDate == model.CreatedUtc.Day);

                    if (outputData != null)
                    {
                        if (outputData.DyeingOutputStepIndex == stepIndex)
                        {

                            outputData.DyeingGoodOutputQty = model.GoodOutput;
                            outputData.DyeingBadOutputQty = model.BadOutput;
                        }
                    }
                }
            }
            else if (stepData.ProcessArea.ToLower() == "area printing")
            {
                if (model.Type.ToLower() == "input")
                {
                    var inputData = data
                        .FirstOrDefault(s => s.PrintingInputDate == model.CreatedUtc.Day);

                    if (inputData != null)
                    {
                        if (inputData.PrintingInputStepIndex == stepIndex)
                        {
                            inputData.PrintingInputQty = model.Input;
                        }
                    }
                }
                else
                {
                    var outputData = data.FirstOrDefault(s => s.PrintingOutputDate == model.CreatedUtc.Day);

                    if (outputData != null)
                    {
                        if (outputData.PrintingOutputStepIndex == stepIndex)
                        {

                            outputData.PrintingGoodOutputQty = model.GoodOutput;
                            outputData.PrintingBadOutputQty = model.BadOutput;
                        }
                    }
                }
            }
            else if (stepData.ProcessArea.ToLower() == "area finishing")
            {
                if (model.Type.ToLower() == "input")
                {
                    var inputData = data
                        .FirstOrDefault(s => s.FinishingInputDate == model.CreatedUtc.Day);

                    if (inputData != null)
                    {
                        if (inputData.FinishingInputStepIndex == stepIndex)
                        {
                            inputData.FinishingInputQty = model.Input;
                        }
                    }
                }
                else
                {
                    var outputData = data.FirstOrDefault(s => s.FinishingOutputDate == model.CreatedUtc.Day);

                    if (outputData != null)
                    {
                        if (outputData.FinishingOutputStepIndex == stepIndex)
                        {

                            outputData.FinishingGoodOutputQty = model.GoodOutput;
                            outputData.FinishingBadOutputQty = model.BadOutput;
                        }
                    }
                }
            }
            else
            {
                if (model.Type.ToLower() == "input")
                {
                    var inputData = data
                        .FirstOrDefault(s => s.QCInputDate == model.CreatedUtc.Day);

                    if (inputData != null)
                    {
                        if (inputData.QCInputStepIndex == stepIndex)
                        {
                            inputData.QCInputQty = model.Input;
                        }
                    }
                }
                else
                {
                    var outputData = data.FirstOrDefault(s => s.QCOutputDate == model.CreatedUtc.Day);

                    if (outputData != null)
                    {
                        if (outputData.QCOutputStepIndex == stepIndex)
                        {

                            outputData.QCGoodOutputQty = model.GoodOutput;
                            outputData.QCBadOutputQty = model.BadOutput;
                        }
                    }
                }
            }
        }


        public void DeleteSnapshot(DailyOperationModel model)
        {
            var selectedKanban = DbSetKanban.Include(s => s.Instruction).ThenInclude(s => s.Steps)
               .Where(kanban => kanban.Id == model.KanbanId).SingleOrDefault();
            var stepData = selectedKanban.Instruction.Steps.FirstOrDefault(s => s.Process == model.StepProcess);
            int stepIndex = stepData.StepIndex;
            var data = DbContext.KanbanSnapshots
                        .Where(s => s.KanbanId == model.KanbanId && s.DOCreatedUtcYear == model.CreatedUtc.Year && s.DOCreatedUtcMonth == model.CreatedUtc.Month);
            if (stepData.ProcessArea.ToLower() == "area pre treatment")
            {
                if (model.Type.ToLower() == "input")
                {
                    var inputData = data
                        .FirstOrDefault(s => s.PreTreatmentInputDate == model.CreatedUtc.Day);

                    if (inputData != null)
                    {
                        if (inputData.PreTreatmentInputStepIndex == stepIndex)
                        {
                            inputData.PreTreatmentCartNumber = null;
                            inputData.PreTreatmentDay = 0;
                            inputData.PreTreatmentInputDate = 0;
                            inputData.PreTreatmentInputQty = null;
                            inputData.PreTreatmentInputStepIndex = 0;
                            inputData.PreTreatmentKonstruksi = null;

                            if (inputData.PreTreatmentCartNumber == null && inputData.DyeingCartNumber == null && inputData.PrintingCartNumber == null &&
                                inputData.FinishingCartNumber == null && inputData.QCCartNumber == null)
                            {
                                DbContext.KanbanSnapshots.Remove(inputData);
                            }
                        }

                    }
                }
                else
                {
                    var outputData = data.FirstOrDefault(s => s.PreTreatmentOutputDate == model.CreatedUtc.Day);

                    if (outputData != null)
                    {
                        if (outputData.PreTreatmentOutputStepIndex == stepIndex)
                        {
                            var stepIds = selectedKanban.Instruction.Steps.Where(s => s.ProcessArea.ToLower() == "area pre treatment").Select(s => s.Process);
                            var stepMachines = selectedKanban.Instruction.Steps.Where(s => s.ProcessArea.ToLower() == "area pre treatment").Select(s => s.MachineId);

                            var previousDO = DbSet.Where(s => s.Id != model.Id && s.Type.ToLower() == "output" && s.KanbanId == model.KanbanId && stepIds.Contains(s.StepProcess)
                                                            && stepMachines.Contains(s.MachineId))
                                .OrderBy(s => s.CreatedUtc).LastOrDefault();
                           
                            if (previousDO != null)
                            {
                                var stepIndexexPreTreatment = selectedKanban.Instruction.Steps.Where(s => s.ProcessArea.ToLower() == "area pre treatment").Select(s => s.StepIndex);

                                var inputData = DbContext.KanbanSnapshots
                                       .Where(s => s.KanbanId == model.KanbanId && s.DOCreatedUtcMonth == model.CreatedUtc.Month && s.DOCreatedUtcYear == model.CreatedUtc.Year && stepIndexexPreTreatment.Contains(s.PreTreatmentInputStepIndex))
                                       .OrderBy(s => s.PreTreatmentInputStepIndex).FirstOrDefault();

                                outputData.PreTreatmentGoodOutputQty = previousDO.GoodOutput;
                                outputData.PreTreatmentBadOutputQty = previousDO.BadOutput;
                                outputData.PreTreatmentOutputDate = previousDO.CreatedUtc.Day;
                                var previousStepData = selectedKanban.Instruction.Steps.FirstOrDefault(s => s.Process == previousDO.StepProcess);
                                int previousStepIndex = stepData.StepIndex;
                                outputData.PreTreatmentOutputStepIndex = previousStepIndex;
                                outputData.PreTreatmentDay = Math.Abs(previousDO.CreatedUtc.Day - inputData.PreTreatmentInputDate) + 1;
                            }
                            else
                            {
                                outputData.PreTreatmentGoodOutputQty = null;
                                outputData.PreTreatmentBadOutputQty = null;
                                outputData.PreTreatmentOutputDate = 0;
                                outputData.PreTreatmentOutputStepIndex = 0;
                                outputData.PreTreatmentDay = 1;
                            }
                        }
                    }
                }
            }
            else if (stepData.ProcessArea.ToLower() == "area dyeing")
            {
                if (model.Type.ToLower() == "input")
                {
                    var inputData = data
                        .FirstOrDefault(s => s.DyeingInputDate == model.CreatedUtc.Day);

                    if (inputData != null)
                    {
                        if (inputData.DyeingInputStepIndex == stepIndex)
                        {
                            inputData.DyeingCartNumber = null;
                            inputData.DyeingDay = 0;
                            inputData.DyeingInputDate = 0;
                            inputData.DyeingInputQty = null;
                            inputData.DyeingInputStepIndex = 0;
                            inputData.DyeingKonstruksi = null;

                            if (inputData.PreTreatmentCartNumber == null && inputData.DyeingCartNumber == null && inputData.PrintingCartNumber == null &&
                                inputData.FinishingCartNumber == null && inputData.QCCartNumber == null)
                            {
                                DbContext.KanbanSnapshots.Remove(inputData);
                            }
                        }

                    }
                }
                else
                {
                    var outputData = data.FirstOrDefault(s => s.DyeingOutputDate == model.CreatedUtc.Day);

                    if (outputData != null)
                    {
                        if (outputData.DyeingOutputStepIndex == stepIndex)
                        {
                            var stepIds = selectedKanban.Instruction.Steps.Where(s => s.ProcessArea.ToLower() == "area dyeing").Select(s => s.Process);
                            var stepMachines = selectedKanban.Instruction.Steps.Where(s => s.ProcessArea.ToLower() == "area dyeing").Select(s => s.MachineId);

                            var previousDO = DbSet.Where(s => s.Id != model.Id && s.Type.ToLower() == "output" && s.KanbanId == model.KanbanId && stepIds.Contains(s.StepProcess)
                                                            && stepMachines.Contains(s.MachineId))
                                .OrderBy(s => s.CreatedUtc).LastOrDefault();

                            if (previousDO != null)
                            {
                                var stepIndexexDyeing = selectedKanban.Instruction.Steps.Where(s => s.ProcessArea.ToLower() == "area dyeing").Select(s => s.StepIndex);

                                var inputData = DbContext.KanbanSnapshots
                                       .Where(s => s.KanbanId == model.KanbanId && s.DOCreatedUtcMonth == model.CreatedUtc.Month && s.DOCreatedUtcYear == model.CreatedUtc.Year && stepIndexexDyeing.Contains(s.DyeingInputStepIndex))
                                       .OrderBy(s => s.DyeingInputStepIndex).FirstOrDefault();

                                outputData.DyeingGoodOutputQty = previousDO.GoodOutput;
                                outputData.DyeingBadOutputQty = previousDO.BadOutput;
                                outputData.DyeingOutputDate = previousDO.CreatedUtc.Day;
                                var previousStepData = selectedKanban.Instruction.Steps.FirstOrDefault(s => s.Process == previousDO.StepProcess);
                                int previousStepIndex = stepData.StepIndex;
                                outputData.DyeingOutputStepIndex = previousStepIndex;
                                outputData.DyeingDay = Math.Abs(previousDO.CreatedUtc.Day - inputData.DyeingInputDate) + 1;
                            }
                            else
                            {
                                outputData.DyeingGoodOutputQty = null;
                                outputData.DyeingBadOutputQty = null;
                                outputData.DyeingOutputDate = 0;
                                outputData.DyeingOutputStepIndex = 0;
                                outputData.DyeingDay = 1;
                            }
                        }
                    }
                }
            }
            else if (stepData.ProcessArea.ToLower() == "area printing")
            {
                if (model.Type.ToLower() == "input")
                {
                    var inputData = data
                        .FirstOrDefault(s => s.PrintingInputDate == model.CreatedUtc.Day);

                    if (inputData != null)
                    {
                        if (inputData.PrintingInputStepIndex == stepIndex)
                        {
                            inputData.PrintingCartNumber = null;
                            inputData.PrintingDay = 0;
                            inputData.PrintingInputDate = 0;
                            inputData.PrintingInputQty = null;
                            inputData.PrintingInputStepIndex = 0;
                            inputData.PrintingKonstruksi = null;

                            if (inputData.PreTreatmentCartNumber == null && inputData.DyeingCartNumber == null && inputData.PrintingCartNumber == null &&
                                inputData.FinishingCartNumber == null && inputData.QCCartNumber == null)
                            {
                                DbContext.KanbanSnapshots.Remove(inputData);
                            }
                        }

                    }
                }
                else
                {
                    var outputData = data.FirstOrDefault(s => s.PrintingOutputDate == model.CreatedUtc.Day);

                    if (outputData != null)
                    {
                        if (outputData.DyeingOutputStepIndex == stepIndex)
                        {
                            var stepIds = selectedKanban.Instruction.Steps.Where(s => s.ProcessArea.ToLower() == "area printing").Select(s => s.Process);
                            var stepMachines = selectedKanban.Instruction.Steps.Where(s => s.ProcessArea.ToLower() == "area printing").Select(s => s.MachineId);

                            var previousDO = DbSet.Where(s => s.Id != model.Id && s.Type.ToLower() == "output" && s.KanbanId == model.KanbanId && stepIds.Contains(s.StepProcess)
                                                            && stepMachines.Contains(s.MachineId))
                                .OrderBy(s => s.CreatedUtc).LastOrDefault();

                            if (previousDO != null)
                            {
                                var stepIndexexPrinting = selectedKanban.Instruction.Steps.Where(s => s.ProcessArea.ToLower() == "area printing").Select(s => s.StepIndex);

                                var inputData = DbContext.KanbanSnapshots
                                       .Where(s => s.KanbanId == model.KanbanId && s.DOCreatedUtcMonth == model.CreatedUtc.Month && s.DOCreatedUtcYear == model.CreatedUtc.Year && stepIndexexPrinting.Contains(s.PrintingInputStepIndex))
                                       .OrderBy(s => s.PrintingInputStepIndex).FirstOrDefault();

                                outputData.PrintingGoodOutputQty = previousDO.GoodOutput;
                                outputData.PrintingBadOutputQty = previousDO.BadOutput;
                                outputData.PrintingOutputDate = previousDO.CreatedUtc.Day;
                                var previousStepData = selectedKanban.Instruction.Steps.FirstOrDefault(s => s.Process == previousDO.StepProcess);
                                int previousStepIndex = stepData.StepIndex;
                                outputData.PrintingOutputStepIndex = previousStepIndex;
                                outputData.PrintingDay = Math.Abs(previousDO.CreatedUtc.Day - inputData.PrintingInputDate) + 1;
                            }
                            else
                            {
                                outputData.PrintingGoodOutputQty = null;
                                outputData.PrintingBadOutputQty = null;
                                outputData.PrintingOutputDate = 0;
                                outputData.PrintingOutputStepIndex = 0;
                                outputData.PrintingDay = 1;
                                
                            }
                        }
                    }
                }
            }
            else if (stepData.ProcessArea.ToLower() == "area finishing")
            {
                if (model.Type.ToLower() == "input")
                {
                    var inputData = data
                        .FirstOrDefault(s => s.FinishingInputDate == model.CreatedUtc.Day);

                    if (inputData != null)
                    {
                        if (inputData.FinishingInputStepIndex == stepIndex)
                        {
                            inputData.FinishingCartNumber = null;
                            inputData.FinishingDay = 0;
                            inputData.FinishingInputDate = 0;
                            inputData.FinishingInputQty = null;
                            inputData.FinishingInputStepIndex = 0;
                            inputData.FinishingKonstruksi = null;

                            if (inputData.PreTreatmentCartNumber == null && inputData.DyeingCartNumber == null && inputData.PrintingCartNumber == null &&
                                inputData.FinishingCartNumber == null && inputData.QCCartNumber == null)
                            {
                                DbContext.KanbanSnapshots.Remove(inputData);
                            }
                        }

                    }
                }
                else
                {
                    var outputData = data.FirstOrDefault(s => s.FinishingOutputDate == model.CreatedUtc.Day);

                    if (outputData != null)
                    {
                        if (outputData.FinishingOutputStepIndex == stepIndex)
                        {
                            var stepIds = selectedKanban.Instruction.Steps.Where(s => s.ProcessArea.ToLower() == "area finishing").Select(s => s.Process);
                            var stepMachines = selectedKanban.Instruction.Steps.Where(s => s.ProcessArea.ToLower() == "area finishing").Select(s => s.MachineId);

                            var previousDO = DbSet.Where(s => s.Id != model.Id && s.Type.ToLower() == "output" && s.KanbanId == model.KanbanId && stepIds.Contains(s.StepProcess)
                                                            && stepMachines.Contains(s.MachineId))
                                .OrderBy(s => s.CreatedUtc).LastOrDefault();

                            if (previousDO != null)
                            {
                                var stepIndexexFinishing = selectedKanban.Instruction.Steps.Where(s => s.ProcessArea.ToLower() == "area finishing").Select(s => s.StepIndex);

                                var inputData = DbContext.KanbanSnapshots
                                       .Where(s => s.KanbanId == model.KanbanId && s.DOCreatedUtcMonth == model.CreatedUtc.Month && s.DOCreatedUtcYear == model.CreatedUtc.Year && stepIndexexFinishing.Contains(s.FinishingInputStepIndex))
                                       .OrderBy(s => s.FinishingInputStepIndex).FirstOrDefault();

                                outputData.FinishingGoodOutputQty = previousDO.GoodOutput;
                                outputData.FinishingBadOutputQty = previousDO.BadOutput;
                                outputData.FinishingOutputDate = previousDO.CreatedUtc.Day;
                                var previousStepData = selectedKanban.Instruction.Steps.FirstOrDefault(s => s.Process == previousDO.StepProcess);
                                int previousStepIndex = stepData.StepIndex;
                                outputData.FinishingOutputStepIndex = previousStepIndex;
                                outputData.FinishingDay = Math.Abs(previousDO.CreatedUtc.Day - inputData.FinishingInputDate) + 1;
                            }
                            else
                            {
                                outputData.FinishingGoodOutputQty = null;
                                outputData.FinishingBadOutputQty = null;
                                outputData.FinishingOutputDate = 0;
                                outputData.FinishingOutputStepIndex = 0;
                                outputData.FinishingDay = 1;

                            }
                        }
                    }
                }
            }
            else
            {
                if (model.Type.ToLower() == "input")
                {
                    var inputData = data
                        .FirstOrDefault(s => s.QCInputDate == model.CreatedUtc.Day);

                    if (inputData != null)
                    {
                        if (inputData.QCInputStepIndex == stepIndex)
                        {
                            inputData.QCCartNumber = null;
                            inputData.QCDay = 0;
                            inputData.QCInputDate = 0;
                            inputData.QCInputQty = null;
                            inputData.QCInputStepIndex = 0;
                            inputData.QCKonstruksi = null;

                            if (inputData.PreTreatmentCartNumber == null && inputData.DyeingCartNumber == null && inputData.PrintingCartNumber == null &&
                                inputData.FinishingCartNumber == null && inputData.QCCartNumber == null)
                            {
                                DbContext.KanbanSnapshots.Remove(inputData);
                            }
                        }

                    }
                }
                else
                {
                    var outputData = data.FirstOrDefault(s => s.QCOutputDate == model.CreatedUtc.Day);

                    if (outputData != null)
                    {
                        if (outputData.QCOutputStepIndex == stepIndex)
                        {
                            var stepIds = selectedKanban.Instruction.Steps.Where(s => s.ProcessArea.ToLower() == "area qc").Select(s => s.Process);
                            var stepMachines = selectedKanban.Instruction.Steps.Where(s => s.ProcessArea.ToLower() == "area qc").Select(s => s.MachineId);

                            var previousDO = DbSet.Where(s => s.Id != model.Id && s.Type.ToLower() == "output" && s.KanbanId == model.KanbanId && stepIds.Contains(s.StepProcess)
                                                            && stepMachines.Contains(s.MachineId))
                                .OrderBy(s => s.CreatedUtc).LastOrDefault();

                            if (previousDO != null)
                            {
                                var stepIndexexQC = selectedKanban.Instruction.Steps.Where(s => s.ProcessArea.ToLower() == "area qc").Select(s => s.StepIndex);

                                var inputData = DbContext.KanbanSnapshots
                                       .Where(s => s.KanbanId == model.KanbanId && s.DOCreatedUtcMonth == model.CreatedUtc.Month && s.DOCreatedUtcYear == model.CreatedUtc.Year && stepIndexexQC.Contains(s.QCInputStepIndex))
                                       .OrderBy(s => s.QCInputStepIndex).FirstOrDefault();

                                outputData.QCGoodOutputQty = previousDO.GoodOutput;
                                outputData.QCBadOutputQty = previousDO.BadOutput;
                                outputData.QCOutputDate = previousDO.CreatedUtc.Day;
                                var previousStepData = selectedKanban.Instruction.Steps.FirstOrDefault(s => s.Process == previousDO.StepProcess);
                                int previousStepIndex = stepData.StepIndex;
                                outputData.QCOutputStepIndex = previousStepIndex;
                                outputData.QCDay = Math.Abs(previousDO.CreatedUtc.Day - inputData.QCInputDate) + 1;
                            }
                            else
                            {
                                outputData.QCGoodOutputQty = null;
                                outputData.QCBadOutputQty = null;
                                outputData.QCOutputDate = 0;
                                outputData.QCOutputStepIndex = 0;
                                outputData.QCDay = 1;

                            }
                        }
                    }
                }
            }
        }
        //public async Task<int> ETLKanbanStepIndex(int page)
        //{

        //	var groupedData = DbSet
        //		.Select(x => new DailyOperationModel()
        //		{
        //			Id = x.Id,
        //			KanbanId = x.KanbanId,
        //			StepProcess = x.StepProcess,
        //			DateInput = x.DateInput,
        //			TimeInput = x.TimeInput,
        //			DateOutput = x.DateOutput,
        //			TimeOutput = x.TimeOutput,
        //			Type = x.Type,
        //			KanbanStepIndex = x.KanbanStepIndex
        //		}).GroupBy(s => new { s.KanbanId, s.StepProcess }).Where(x => x.Count() > 2).OrderBy(x => x.Key.KanbanId);


        //	int dd = groupedData.Count();
        //	var kanbanStepData = DbContext.KanbanSteps.Include(x => x.Instruction)
        //		.Select(x => new KanbanStepModel()
        //		{
        //			Id = x.Id,
        //			Instruction = new KanbanInstructionModel()
        //			{
        //				Id = x.Instruction.Id,
        //				KanbanId = x.Instruction.KanbanId
        //			},
        //			InstructionId = x.InstructionId,
        //			StepIndex = x.StepIndex,
        //			Process = x.Process
        //		});
        //	int index = 0;
        //	int result = 0;
        //	using (var transaction = DbContext.Database.BeginTransaction())
        //	{
        //		foreach (var item in groupedData)
        //		//foreach (var item in groupedData.GroupBy(x => x.KanbanId).OrderBy(x => x.Key).Skip((page - 1) * 5000).Take(5000))
        //		{
        //			var dataInput = item.Where(x => x.Type.ToLower() == "input").OrderBy(x => x.DateInput).ThenBy(x => x.TimeInput);
        //			var dataOutput = item.Where(x => x.Type.ToLower() == "output").OrderBy(x => x.DateOutput).ThenBy(x => x.TimeOutput);
        //			//var data = item.OrderBy(x => x.CreatedUtc).ThenBy(x => x.Id);
        //			var steps = kanbanStepData.Where(x => x.Instruction.KanbanId == item.Key.KanbanId && x.Process == item.Key.StepProcess).OrderBy(x => x.StepIndex);

        //			foreach(var daily in dataInput)
        //			{
        //				int idx = dataInput.ToList().FindIndex(x => x.Id == daily.Id);
        //				var kanbanStep = steps.ToList().ElementAtOrDefault(idx);
        //				var model = await DbSet.FirstOrDefaultAsync(x => x.Id == daily.Id);
        //				if (kanbanStep != null)
        //				{

        //					model.KanbanStepIndex = kanbanStep.StepIndex;

        //				}
        //				else
        //				{
        //					model.KanbanStepIndex = 0;

        //				}
        //				index++;
        //				Debug.WriteLine(index);

        //				if (index % 10000 == 0)
        //				{
        //					result += await DbContext.SaveChangesAsync();
        //				}
        //			}

        //			foreach (var daily in dataOutput)
        //			{
        //				int idx = dataOutput.ToList().FindIndex(x => x.Id == daily.Id);
        //				var kanbanStep = steps.ToList().ElementAtOrDefault(idx);
        //				var model = await DbSet.FirstOrDefaultAsync(x => x.Id == daily.Id);
        //				if (kanbanStep != null)
        //				{

        //					model.KanbanStepIndex = kanbanStep.StepIndex;

        //				}
        //				else
        //				{
        //					model.KanbanStepIndex = 0;
        //				}
        //				index++;
        //				Debug.WriteLine(index);

        //				if (index % 10000 == 0)
        //				{
        //					result += await DbContext.SaveChangesAsync();
        //				}
        //			}
        //			//foreach (var daily in data)
        //			//{
        //			//	int idx = data.Where(x => x.StepProcess == daily.StepProcess).ToList().FindIndex(x => x.StepProcess == daily.StepProcess);
        //			//	var kanbanStep = steps.Where(x => x.Process == daily.StepProcess).ToList().ElementAtOrDefault(idx);
        //			//	if (kanbanStep != null)
        //			//	{
        //			//		var model = await DbSet.FirstOrDefaultAsync(x => x.Id == daily.Id);
        //			//		model.KanbanStepIndex = kanbanStep.StepIndex;
        //			//		index++;
        //			//		Debug.WriteLine(index);

        //			//		if (index % 10000 == 0)
        //			//		{
        //			//			result += await DbContext.SaveChangesAsync();
        //			//		}
        //			//	}

        //			//}
        //		}
        //		result += await DbContext.SaveChangesAsync();
        //		transaction.Commit();
        //	}

        //	return result;
        //}
    }
}