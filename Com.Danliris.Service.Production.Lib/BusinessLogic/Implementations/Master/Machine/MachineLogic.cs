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
        private StepLogic StepLogic;

        public MachineLogic(MachineEventLogic machineEventLogic, StepLogic stepLogic, IIdentityService identityService, ProductionDbContext dbContext) : base(identityService, dbContext)
        {
            this.MachineEventLogic = machineEventLogic;
            this.StepLogic = stepLogic;
        }

        public override void CreateModel(MachineModel model)
        {
            foreach (MachineEventsModel item in model.MachineEvents)
            {
                EntityExtension.FlagForCreate(item, IdentityService.Username, UserAgent);
            }

            foreach (StepModel step in model.Steps)
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

            foreach (var step in model.Steps)
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


                HashSet<int> StepId = MachineEventLogic.MachineEventIds(id);
                foreach (var itemId in StepId)
                {
                    StepModel data = model.Steps.FirstOrDefault(prop => prop.Id.Equals(itemId));
                    if (data == null)
                        await StepLogic.DeleteModel(itemId);
                    else
                    {
                        StepLogic.UpdateModelAsync(itemId, data);
                    }

                    foreach (StepModel item in model.Steps)
                    {
                        if (item.Id == 0)
                            StepLogic.CreateModel(item);
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
