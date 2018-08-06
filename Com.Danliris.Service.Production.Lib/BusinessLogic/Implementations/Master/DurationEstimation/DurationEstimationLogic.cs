using Com.Danliris.Service.Production.Lib.Models.Master.DurationEstimation;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Production.Lib.BusinessLogic.Implementations.Master.DurationEstimation
{
    public class DurationEstimationLogic : BaseLogic<DurationEstimationModel>
    {
        private const string UserAgent = "production-service";
        private readonly ProductionDbContext DbContext;
        private readonly DbSet<DurationEstimationAreaModel> _DbSetDurationEstimationArea;

        //private StepIndicatorLogic StepIndicatorLogic;

        public DurationEstimationLogic(IIdentityService identityService, ProductionDbContext dbContext) : base(identityService, dbContext)
        {
            DbContext = dbContext;
            _DbSetDurationEstimationArea = DbContext.Set<DurationEstimationAreaModel>();
        }

        public override void CreateModel(DurationEstimationModel model)
        {
            foreach (var area in model.Areas)
            {
                EntityExtension.FlagForCreate(area, IdentityService.Username, UserAgent);
            }
            base.CreateModel(model);
        }

        public override Task<DurationEstimationModel> ReadModelById(int id)
        {
            return DbSet.Include(d => d.Areas).FirstOrDefaultAsync(d => d.Id.Equals(id) && d.IsDeleted.Equals(false));
        }

        public override void UpdateModel(int id, DurationEstimationModel model)
        {
            if (model.Areas != null)
            {
                HashSet<int> areaIds = _DbSetDurationEstimationArea.Where(d => d.DurationEstimationId == id).Select(d => d.Id).ToHashSet();

                foreach (int areaId in areaIds)
                {
                    var area = model.Areas.FirstOrDefault(prop => prop.Id.Equals(areaId));

                    if (area == null)
                    {
                        area = _DbSetDurationEstimationArea.Where(w => w.Id == areaId).FirstOrDefault();
                        EntityExtension.FlagForDelete(area, IdentityService.Username, UserAgent);
                        _DbSetDurationEstimationArea.Update(area);
                    }
                    else
                    {
                        EntityExtension.FlagForUpdate(area, IdentityService.Username, UserAgent);
                        _DbSetDurationEstimationArea.Update(area);
                    }
                }

                foreach (var area in model.Areas)
                {
                    if (area.Id == 0)
                    {
                        EntityExtension.FlagForCreate(area, IdentityService.Username, UserAgent);
                        _DbSetDurationEstimationArea.Add(area);
                    }
                }
            }
            base.UpdateModel(id, model);
        }

        public override async Task DeleteModel(int id)
        {
            var model = await ReadModelById(id);

            foreach (var area in model.Areas)
            {
                EntityExtension.FlagForDelete(area, IdentityService.Username, UserAgent);
            }

            EntityExtension.FlagForDelete(model, IdentityService.Username, UserAgent, true);
            DbSet.Update(model);
        }
    }
}
