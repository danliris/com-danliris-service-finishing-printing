using Com.Danliris.Service.Finishing.Printing.Lib.Models.Daily_Operation;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System.Collections.Generic;
using System.Linq;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.DailyOperation
{
    public class DailyOperationBadOutputReasonsLogic : BaseLogic<DailyOperationBadOutputReasonsModel>
    {
        private const string UserAgent = "production-service";
        public DailyOperationBadOutputReasonsLogic(IIdentityService identityService, ProductionDbContext dbContext) : base(identityService, dbContext)
        {
        }
        public HashSet<int> DataId(int id)
        {
            return new HashSet<int>(DbSet.Where(d => d.DailyOperationId == id).Select(d => d.Id));
        }
    }
}
