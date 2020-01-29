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

            if (model.Type == "input")
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
                this.SetKanbanDelete(model);
            }

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
                this.SetKanbanUpdate(model);
            }

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

        public void UpdateKanban(DailyOperationModel model, string flag)
        {
            KanbanModel kanban = this.DbSetKanban.Where(k => k.Id.Equals(model.KanbanId)).SingleOrDefault();

            int currentStepIndex = (flag == "create" ? kanban.CurrentStepIndex += 1 : flag == "update" ? kanban.CurrentStepIndex : kanban.CurrentStepIndex -= 1);

            kanban.CurrentQty = model.GoodOutput != null ? (double)model.GoodOutput : 0;
            kanban.CurrentStepIndex = currentStepIndex;
            kanban.GoodOutput = model.GoodOutput != null ? (double)model.GoodOutput : 0;
            kanban.BadOutput = model.BadOutput != null ? (double)model.GoodOutput : 0;

            EntityExtension.FlagForUpdate(kanban, IdentityService.Username, UserAgent);
            DbSetKanban.Update(kanban);
        }

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
                selectedKanban.CurrentQty = model.GoodOutput.GetValueOrDefault() + model.BadOutput.GetValueOrDefault();
                selectedKanban.GoodOutput = model.GoodOutput.GetValueOrDefault();
                selectedKanban.BadOutput = model.BadOutput.GetValueOrDefault();
            }

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
                    selectedKanban.CurrentQty = previousState.Input.GetValueOrDefault();
                }
                else if (previousState.Type.ToUpper() == "OUTPUT")
                {
                    selectedKanban.CurrentQty = previousState.GoodOutput.GetValueOrDefault() + previousState.BadOutput.GetValueOrDefault();
                    selectedKanban.GoodOutput = previousState.GoodOutput.GetValueOrDefault();
                    selectedKanban.BadOutput = previousState.BadOutput.GetValueOrDefault();
                }
            }

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

        public HashSet<int> hasInput(DailyOperationViewModel vm)
        {
            return new HashSet<int>(DbSet.Where(d => d.Kanban.Id == vm.Kanban.Id && d.Type == vm.Type && d.StepId == vm.Step.StepId).Select(d => d.Id));
        }
    }
}