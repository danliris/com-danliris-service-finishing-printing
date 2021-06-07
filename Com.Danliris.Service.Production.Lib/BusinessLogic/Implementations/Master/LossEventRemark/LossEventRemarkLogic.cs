using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.LossEventRemark;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.LossEventRemark
{
    public class LossEventRemarkLogic : BaseLogic<LossEventRemarkModel>
    {
        public LossEventRemarkLogic(IIdentityService identityService, ProductionDbContext dbContext) : base(identityService, dbContext)
        {
        }
    }
}
