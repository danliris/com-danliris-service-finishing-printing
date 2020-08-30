using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.LossEvent;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.LossEvent
{
    public class LossEventLogic : BaseLogic<LossEventModel>
    {
        public LossEventLogic(IIdentityService identityService, ProductionDbContext dbContext) : base(identityService, dbContext)
        {

        }
    }
}
