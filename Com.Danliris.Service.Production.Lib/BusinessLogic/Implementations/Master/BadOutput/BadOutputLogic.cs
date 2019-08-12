using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.BadOutput;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override Task<BadOutputModel> ReadModelById(int id)
        {
            return DbSet.Include(d => d.MachineDetails).FirstOrDefaultAsync(d => d.Id.Equals(id) && d.IsDeleted.Equals(false));
        }

        public override void CreateModel(BadOutputModel model)
        {
            foreach (BadOutputMachineModel item in model.MachineDetails)
            {
                EntityExtension.FlagForCreate(item, IdentityService.Username, UserAgent);
            }
            base.CreateModel(model);
        }

        public override async Task UpdateModelAsync(int id, BadOutputModel model)
        {
            if (model.MachineDetails != null)
            {
                HashSet<int> indicatorsId = BadOutputMachineLogic.DetailsId(id);
                foreach (var itemId in indicatorsId)
                {
                    BadOutputMachineModel data = model.MachineDetails.FirstOrDefault(prop => prop.Id.Equals(itemId));
                    if (data == null)
                        await BadOutputMachineLogic.DeleteModel(itemId);
                    else
                    {
                        await BadOutputMachineLogic.UpdateModelAsync(itemId, data);
                    }

                    foreach (BadOutputMachineModel item in model.MachineDetails)
                    {
                        if (item.Id == 0)
                            BadOutputMachineLogic.CreateModel(item);
                    }
                }
            }

            EntityExtension.FlagForUpdate(model, IdentityService.Username, UserAgent);
            DbSet.Update(model);
        }

        public override async Task DeleteModel(int id)
        {
            var model = await ReadModelById(id);

            foreach (var item in model.MachineDetails)
            {
                EntityExtension.FlagForDelete(item, IdentityService.Username, UserAgent);
            }

            EntityExtension.FlagForDelete(model, IdentityService.Username, UserAgent, true);
            DbSet.Update(model);
        }

    }
}
