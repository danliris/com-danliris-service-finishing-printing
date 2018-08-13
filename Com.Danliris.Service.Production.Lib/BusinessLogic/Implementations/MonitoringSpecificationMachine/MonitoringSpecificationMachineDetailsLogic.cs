using Com.Danliris.Service.Finishing.Printing.Lib.Models.Monitoring_Specification_Machine;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.MonitoringSpecificationMachine
{
    public class MonitoringSpecificationMachineDetailsLogic : BaseLogic<MonitoringSpecificationMachineDetailsModel>
    {
        public MonitoringSpecificationMachineDetailsLogic(IIdentityService identityService, ProductionDbContext dbContext) : base(identityService, dbContext)
        {
        }

        public HashSet<int> DataId(int id)
        {
            return new HashSet<int>(DbSet.Where(d => d.MonitoringSpecificationMachineId == id).Select(d => d.Id));
        }
    }
}
