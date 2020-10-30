using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.EventOrganizer;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.EventOrganizer
{
  
    public class EventOrganizerLogic : BaseLogic<Models.Master.EventOrganizer.EventOrganizer>
    {
        public EventOrganizerLogic(IIdentityService identityService, ProductionDbContext dbContext) : base(identityService, dbContext)
        {
        }

        public async Task<Models.Master.EventOrganizer.EventOrganizer> ReadByGroupArea(string area,string group)
        {
             return await DbSet.FirstOrDefaultAsync(d => d.ProcessArea.Equals(area) && d.IsDeleted.Equals(false) && d.Group.Equals(group));
        }



    }
}
