using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.LossEventCategory;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.LossEventCategory
{
    public class LossEventCategoryLogic : BaseLogic<LossEventCategoryModel>
    {
        public LossEventCategoryLogic(IIdentityService identityService, ProductionDbContext dbContext) : base(identityService, dbContext)
        {
        }
    }
}
