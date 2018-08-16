using Com.Danliris.Service.Finishing.Printing.Lib.Models.Kanban;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Kanban
{
    public class KanbanLogic : BaseLogic<KanbanModel>
    {
        private const string UserAgent = "production-service";
        private readonly ProductionDbContext DbContext;
        public KanbanLogic(IIdentityService identityService, ProductionDbContext dbContext) : base(identityService, dbContext)
        {
            DbContext = dbContext;
        }

        public override void CreateModel(KanbanModel model)
        {
            EntityExtension.FlagForCreate(model.Instruction, IdentityService.Username, UserAgent);
            foreach (var step in model.Instruction.Steps)
            {
                EntityExtension.FlagForCreate(step, IdentityService.Username, UserAgent);
            }
            base.CreateModel(model);
        }

        public override async Task DeleteModel(int id)
        {
            var model = await ReadModelById(id);
            EntityExtension.FlagForDelete(model, IdentityService.Username, UserAgent);
            EntityExtension.FlagForDelete(model.Instruction, IdentityService.Username, UserAgent);
            foreach (var step in model.Instruction.Steps)
            {
                EntityExtension.FlagForDelete(step, IdentityService.Username, UserAgent);
            }
            DbSet.Update(model);
        }

        public override Task<KanbanModel> ReadModelById(int id)
        {
            return DbSet
                .Include(b => b.Instruction)
                .ThenInclude(c => c.Steps.Select(s => s.StepIndicators))
                .FirstOrDefaultAsync(d => d.Id.Equals(id) && d.IsDeleted.Equals(false));
        }

        public override void UpdateModelAsync(int id, KanbanModel model)
        {
            base.UpdateModelAsync(id, model);
        }
    }
}
