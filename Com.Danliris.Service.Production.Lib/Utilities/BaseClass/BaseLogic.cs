using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Com.Danliris.Service.Production.Lib.Utilities.BaseInterface;
using System.ComponentModel.DataAnnotations;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;

namespace Com.Danliris.Service.Production.Lib.Utilities.BaseClass
{
    public abstract class BaseLogic<TModel> : IBaseLogic<TModel>
        where TModel : StandardEntity, IValidatableObject
    {
        private const string UserAgent = "production-service";
        protected DbSet<TModel> DbSet;
        protected IIdentityService IdentityService;

        public BaseLogic(IIdentityService identityService, ProductionDbContext dbContext)
        {
            this.DbSet = dbContext.Set<TModel>();
            this.IdentityService = identityService;
        }

        public virtual void CreateModel(TModel model)
        {
            EntityExtension.FlagForCreate(model, IdentityService.Username, UserAgent);
            DbSet.Add(model);
        }

        public virtual Task<TModel> ReadModelById(int id)
        {
            return DbSet.FirstOrDefaultAsync(d => d.Id.Equals(id) && d.IsDeleted.Equals(false));
        }

        public virtual async Task UpdateModelAsync(int id, TModel model)
        {
            TModel dbModel = await ReadModelById(id);
            EntityExtension.FlagForUpdate(model, IdentityService.Username, UserAgent);
            DbSet.Update(model);
        }

        public virtual async Task DeleteModel(int id)
        {
            TModel model = await ReadModelById(id);
            EntityExtension.FlagForDelete(model, IdentityService.Username, UserAgent, true);
            DbSet.Update(model);
        }
    }
}
