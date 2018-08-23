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
    public class BadOutputLogic : BaseLogic<BadOutputModel>
    {
        private const string UserAgent = "production-service";
        private BadOutputMachineLogic BadOutputMachineLogic;
        public BadOutputLogic(BadOutputMachineLogic badOutputMachineLogic, IIdentityService identityService, ProductionDbContext dbContext) : base(identityService, dbContext)
        {
            this.BadOutputMachineLogic = badOutputMachineLogic;
        }

    }
}
