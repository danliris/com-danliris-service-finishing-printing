using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.MachineType;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.MachineType
{
    public class MachineTypeIndicatorsLogic : BaseLogic<MachineTypeIndicatorsModel>
    {
        private const string UserAgent = "production-service";
        public MachineTypeIndicatorsLogic(IIdentityService identityService, ProductionDbContext dbContext) : base(identityService, dbContext)
        {
        }

        public HashSet<int> IndicatorIds(int id)
        {
            return new HashSet<int>(DbSet.Where(d => d.MachineTypeId == id).Select(d => d.Id));
        }
    }
}
