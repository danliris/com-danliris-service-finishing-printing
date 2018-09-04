using Com.Danliris.Service.Finishing.Printing.Lib.Models.Daily_Operation;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Kanban;
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
                string flag = "create";
                foreach (DailyOperationBadOutputReasonsModel item in model.BadOutputReasons)
                {
                    EntityExtension.FlagForCreate(item, IdentityService.Username, UserAgent);
                }
                this.UpdateKanban(model, flag);
            }

            model.Kanban = null;
            model.Machine = null;

            base.CreateModel(model);
        }

        public override async Task DeleteModel(int id)
        {
            var model = await ReadModelById(id);

            if (model.Type == "output")
            {
                string flag = "delete";
                foreach (var item in model.BadOutputReasons)
                {
                    EntityExtension.FlagForDelete(item, IdentityService.Username, UserAgent);
                }
                this.UpdateKanban(model, flag);
            }
            model.Kanban = null;
            model.Machine = null;
            EntityExtension.FlagForDelete(model, IdentityService.Username, UserAgent, true);
            DbSet.Update(model);
        }

        public override async void UpdateModelAsync(int id, DailyOperationModel model)
        {
            if (model.Type == "output")
            {
                string flag = "update";
                HashSet<int> detailId = DailyOperationBadOutputReasonsLogic.DataId(id);
                foreach (var itemId in detailId)
                {
                    DailyOperationBadOutputReasonsModel data = model.BadOutputReasons.FirstOrDefault(prop => prop.Id.Equals(itemId));
                    if (data == null)
                        await DailyOperationBadOutputReasonsLogic.DeleteModel(itemId);
                    else
                    {
                        DailyOperationBadOutputReasonsLogic.UpdateModelAsync(itemId, data);
                    }

                    foreach (DailyOperationBadOutputReasonsModel item in model.BadOutputReasons)
                    {
                        if (item.Id == 0)
                            DailyOperationBadOutputReasonsLogic.CreateModel(item);
                    }
                }
                this.UpdateKanban(model, flag);
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
                .Include(d => d.BadOutputReasons)
                .FirstOrDefaultAsync(d => d.Id.Equals(id) && d.IsDeleted.Equals(false));
        }

        public void UpdateKanban(DailyOperationModel model, string flag)
        {

            KanbanModel kanban = this.DbSetKanban.Where(k => k.Id.Equals(model.KanbanId)).SingleOrDefault();

            int currentStepIndex = (flag == "create" ? kanban.CurrentStepIndex += 1 : flag == "update" ? kanban.CurrentStepIndex : kanban.CurrentStepIndex -= 1);

            kanban.CurrentQty = (double)model.GoodOutput;
            kanban.CurrentStepIndex = currentStepIndex;
            kanban.GoodOutput = (double)model.GoodOutput;
            kanban.BadOutput = (double)model.BadOutput;

            EntityExtension.FlagForUpdate(kanban, IdentityService.Username, UserAgent);
            DbSetKanban.Update(kanban);

        }
    }
}