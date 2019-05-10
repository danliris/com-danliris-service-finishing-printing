using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.Machine;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.MachineType;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.BusinessLogic.Implementations.Master.Step;
using Com.Danliris.Service.Production.Lib.Models.Master.Step;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.Machine
{
    public class MachineLogic : BaseLogic<MachineModel>
    {
        private const string UserAgent = "production-service";
        private MachineEventLogic MachineEventLogic;
        private MachineStepLogic MachineStepLogic;

        public MachineLogic(MachineEventLogic machineEventLogic, MachineStepLogic machineStepLogic, IIdentityService identityService, ProductionDbContext dbContext) : base(identityService, dbContext)
        {
            this.MachineEventLogic = machineEventLogic;
            this.MachineStepLogic = machineStepLogic;
        }

        public override void CreateModel(MachineModel model)
        {
            foreach (MachineEventsModel item in model.MachineEvents)
            {
                EntityExtension.FlagForCreate(item, IdentityService.Username, UserAgent);
            }

            foreach (MachineStepModel step in model.MachineSteps)
            {
                EntityExtension.FlagForCreate(step, IdentityService.Username, UserAgent);
            }

            EntityExtension.FlagForCreate(model, IdentityService.Username, UserAgent);
            base.CreateModel(model);
        }

        public override async Task DeleteModel(int id)
        {
            var model = await ReadModelById(id);

            foreach (var item in model.MachineEvents)
            {
                EntityExtension.FlagForDelete(item, IdentityService.Username, UserAgent);
            }

            foreach (var step in model.MachineSteps)
            {
                EntityExtension.FlagForDelete(step, IdentityService.Username, UserAgent);
            }


            EntityExtension.FlagForDelete(model, IdentityService.Username, UserAgent, true);
            DbSet.Update(model);
        }

        public override async void UpdateModelAsync(int id, MachineModel model)
        {
            if (model.MachineEvents != null)
            {
                HashSet<int> indicatorsId = MachineEventLogic.MachineEventIds(id);
                foreach (var itemId in indicatorsId)
                {
                    MachineEventsModel data = model.MachineEvents.FirstOrDefault(prop => prop.Id.Equals(itemId));
                    if (data == null)
                        await MachineEventLogic.DeleteModel(itemId);
                    else
                    {
                        MachineEventLogic.UpdateModelAsync(itemId, data);
                    }

                    foreach (MachineEventsModel item in model.MachineEvents)
                    {
                        if (item.Id == 0)
                            MachineEventLogic.CreateModel(item);
                    }
                }


                HashSet<int> MachineStepId = MachineStepLogic.MachineStepIds(id);
                foreach (var itemId in MachineStepId)
                {
                    MachineStepModel data = model.MachineSteps.FirstOrDefault(prop => prop.Id.Equals(itemId));
                    if (data == null)
                        await MachineStepLogic.DeleteModel(itemId);
                    else
                    {
                        MachineStepLogic.UpdateModelAsync(itemId, data);
                    }

                    foreach (MachineStepModel item in model.MachineSteps)
                    {
                        if (item.Id == 0)
                            MachineStepLogic.CreateModel(item);
                    }
                }
            }

            EntityExtension.FlagForUpdate(model, IdentityService.Username, UserAgent);
            DbSet.Update(model);
        }

        public override Task<MachineModel> ReadModelById(int id)
        {
            return DbSet.Include(d => d.MachineEvents)
                .Include(d => d.MachineSteps)
                .FirstOrDefaultAsync(d => d.Id.Equals(id) && d.IsDeleted.Equals(false));

        }
    }
}
