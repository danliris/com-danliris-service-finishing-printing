using Com.Danliris.Service.Finishing.Printing.Lib.Models.ReturToQC;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.ReturToQC
{
    public class ReturToQCLogic : BaseLogic<ReturToQCModel>
    {
        private const string UserAgent = "production-service";
        private readonly ProductionDbContext dbContext;
        private readonly DbSet<ReturToQCModel> dbSet;
        private readonly DbSet<ReturToQCItemModel> dbSetItem;
        private readonly DbSet<ReturToQCItemDetailModel> dbSetItemDetail;

        public ReturToQCLogic(IIdentityService identityService, ProductionDbContext dbContext) : base(identityService, dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<ReturToQCModel>();
            this.dbSetItem = dbContext.Set<ReturToQCItemModel>();
            this.dbSetItemDetail = dbContext.Set<ReturToQCItemDetailModel>();
        }

        public override void CreateModel(ReturToQCModel model)
        {
            foreach (var item in model.ReturToQCItems)
            {
                EntityExtension.FlagForCreate(item, IdentityService.Username, UserAgent);
                foreach (var detail in item.ReturToQCItemDetails)
                {
                    EntityExtension.FlagForCreate(detail, IdentityService.Username, UserAgent);
                }
            }
            base.CreateModel(model);
        }

        public override async Task DeleteModel(int id)
        {
            var model = await ReadModelById(id);
            EntityExtension.FlagForDelete(model, IdentityService.Username, UserAgent, true);
            foreach (var item in model.ReturToQCItems)
            {
                EntityExtension.FlagForDelete(item, IdentityService.Username, UserAgent);
                foreach (var detail in item.ReturToQCItemDetails)
                {
                    EntityExtension.FlagForDelete(detail, IdentityService.Username, UserAgent);
                }
            }
            DbSet.Update(model);
        }

        public override Task<ReturToQCModel> ReadModelById(int id)
        {
            return base.ReadModelById(id);
            //return dbContext.ReturToQCs.Include(x => x.ReturToQCItems.Select(y => y.ReturToQCItemDetails)).FirstOrDefaultAsync(d => d.Id.Equals(id)
            //       && d.IsDeleted.Equals(false)
            //       && d.ReturToQCItems.Any(e => e.IsDeleted.Equals(false) && e.ReturToQCItemDetails.Any(f => f.IsDeleted.Equals(false))));
        }

        public override void UpdateModelAsync(int id, ReturToQCModel model)
        {
            EntityExtension.FlagForUpdate(model, IdentityService.Username, UserAgent);
            foreach (var item in model.ReturToQCItems)
            {
                if (item.Id == 0)
                {
                    EntityExtension.FlagForCreate(item, IdentityService.Username, UserAgent);
                    dbSetItem.Add(item);
                }
                else
                {
                    EntityExtension.FlagForUpdate(item, IdentityService.Username, UserAgent);

                }
                foreach (var detail in item.ReturToQCItemDetails)
                {
                    if (detail.Id == 0)
                    {
                        EntityExtension.FlagForCreate(detail, IdentityService.Username, UserAgent);
                        dbSetItemDetail.Add(detail);
                    }
                    else
                    {
                        EntityExtension.FlagForUpdate(detail, IdentityService.Username, UserAgent);

                    }
                }
            }
            DbSet.Update(model);
        }
    }
}
