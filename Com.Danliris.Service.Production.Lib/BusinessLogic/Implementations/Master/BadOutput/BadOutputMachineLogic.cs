using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.BadOutput;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.BadOutput
{
    public class BadOutputMachineLogic : BaseLogic<BadOutputMachineModel>
    {
        private const string UserAgent = "production-service";
        public BadOutputMachineLogic(IIdentityService identityService, ProductionDbContext dbContext) : base(identityService, dbContext)
        {
        }

        public HashSet<int> DetailsId(int id)
        {
            return new HashSet<int>(DbSet.Where(d => d.BadOutputId == id).Select(d => d.Id));
        }
    }
}
