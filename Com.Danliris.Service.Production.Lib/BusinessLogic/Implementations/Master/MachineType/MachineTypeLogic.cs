using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.MachineType;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.MachineType
{
    public class MachineTypeLogic : BaseLogic<MachineTypeModel>
    {
        private const string UserAgent = "production-service";
        private MachineTypeIndicatorsLogic MachineTypeIndicatorsLogic;

        public MachineTypeLogic(MachineTypeIndicatorsLogic machineTypeIndicatorsLogic, IIdentityService identityService, ProductionDbContext dbContext) : base(identityService, dbContext)
        {
            this.MachineTypeIndicatorsLogic = machineTypeIndicatorsLogic;
        }

        public override void CreateModel(MachineTypeModel model)
        {
            foreach (MachineTypeIndicatorsModel item in model.Indicators)
            {
                EntityExtension.FlagForCreate(item, IdentityService.Username, UserAgent);
            }
            base.CreateModel(model);
        }

        public override async Task DeleteModel(int id)
        {
            var model = await ReadModelById(id);

            foreach (var item in model.Indicators)
            {
                EntityExtension.FlagForDelete(item, IdentityService.Username, UserAgent);
            }

            EntityExtension.FlagForDelete(model, IdentityService.Username, UserAgent, true);
            DbSet.Update(model);
        }

        public override async Task UpdateModelAsync(int id, MachineTypeModel model)
        {
            if (model.Indicators != null)
            {
                HashSet<int> indicatorsId = MachineTypeIndicatorsLogic.IndicatorIds(id);
                foreach (var itemId in indicatorsId)
                {
                    MachineTypeIndicatorsModel data = model.Indicators.FirstOrDefault(prop => prop.Id.Equals(itemId));
                    if (data == null)
                        await MachineTypeIndicatorsLogic.DeleteModel(itemId);
                    else
                    {
                       await MachineTypeIndicatorsLogic.UpdateModelAsync(itemId, data);
                    }

                    foreach (MachineTypeIndicatorsModel item in model.Indicators)
                    {
                        if (item.Id == 0)
                            MachineTypeIndicatorsLogic.CreateModel(item);
                    }
                }
            }

            EntityExtension.FlagForUpdate(model, IdentityService.Username, UserAgent);
            DbSet.Update(model);
        }

        public override Task<MachineTypeModel> ReadModelById(int id)
        {
            return DbSet.Include(d => d.Indicators).FirstOrDefaultAsync(d => d.Id.Equals(id) && d.IsDeleted.Equals(false));
        }
    }
}
