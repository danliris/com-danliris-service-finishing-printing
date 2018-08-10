using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.Machine;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.Machine
{
    public class MachineEventLogic : BaseLogic<MachineEventsModel>
    {
        private const string UserAgent = "production-service";

        public MachineEventLogic(IIdentityService identityService, ProductionDbContext dbContext) : base(identityService, dbContext)
        {
        }

        public HashSet<int> MachineEventIds(int id)
        {
            return new HashSet<int>(DbSet.Where(d => d.MachineId == id).Select(d => d.Id));
        }
    }
}
