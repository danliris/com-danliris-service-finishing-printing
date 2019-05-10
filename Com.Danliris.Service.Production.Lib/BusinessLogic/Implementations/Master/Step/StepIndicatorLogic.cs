using System.Collections.Generic;
using System.Linq;
using Com.Danliris.Service.Production.Lib.Models.Master.Step;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;

namespace Com.Danliris.Service.Production.Lib.BusinessLogic.Implementations.Master.Step
{
    public class StepIndicatorLogic : BaseLogic<StepIndicatorModel>
    {
        public StepIndicatorLogic(IIdentityService identityService, ProductionDbContext dbContext) : base(identityService, dbContext)
        {
        }

        public HashSet<int> GetStepIndicatorIds(int id)
        {
            return new HashSet<int>(DbSet.Where(d => d.StepId == id).Select(d => d.Id));
        }
    }
}