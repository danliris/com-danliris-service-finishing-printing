using Com.Danliris.Service.Production.Lib.Models.Master.Step;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Production.Lib.BusinessLogic.Implementations.Master.Step
{
    public class StepLogic : BaseLogic<StepModel>
    {
        private const string UserAgent = "production-service";

        private StepIndicatorLogic StepIndicatorLogic;

        public StepLogic(StepIndicatorLogic stepIndicatorLogic, IIdentityService identityService, ProductionDbContext dbContext) : base(identityService, dbContext)
        {
            StepIndicatorLogic = stepIndicatorLogic;
        }

        public override void CreateModel(StepModel model)
        {
            foreach (var item in model.StepIndicators)
            {
                EntityExtension.FlagForCreate(item, IdentityService.Username, UserAgent);
            }
            base.CreateModel(model);
        }

        public override Task<StepModel> ReadModelById(int id)
        {
            return DbSet.Include(d => d.StepIndicators).FirstOrDefaultAsync(d => d.Id.Equals(id) && d.IsDeleted.Equals(false));
        }

        public async Task UpdateModelStep(int id, StepModel model)
        {
            if (model.StepIndicators != null)
            {
                HashSet<int> indicatorIds = StepIndicatorLogic.GetStepIndicatorIds(id);

                foreach (int indicatorId in indicatorIds)
                {
                    var indicator = model.StepIndicators.FirstOrDefault(prop => prop.Id.Equals(indicatorId));
                    if (indicator == null)
                        await StepIndicatorLogic.DeleteModel(indicatorId);
                    else
                        StepIndicatorLogic.UpdateModelAsync(indicatorId, indicator);
                }

                foreach (var indicator in model.StepIndicators)
                {
                    if (indicator.Id == 0)
                        StepIndicatorLogic.CreateModel(indicator);
                }
            }
            base.UpdateModelAsync(id, model);
        }

        public override async Task DeleteModel(int id)
        {
            var model = await ReadModelById(id);

            foreach (var item in model.StepIndicators)
            {
                EntityExtension.FlagForDelete(item, IdentityService.Username, UserAgent);
            }

            EntityExtension.FlagForDelete(model, IdentityService.Username, UserAgent, true);
            DbSet.Update(model);
        }
    }
}
