using Com.Danliris.Service.Finishing.Printing.Lib.Models.Monitoring_Specification_Machine;
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

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.MonitoringSpecificationMachine
{
    public class MonitoringSpecificationMachineLogic : BaseLogic<MonitoringSpecificationMachineModel>
    {
        private const string UserAgent = "production-service";
        private MonitoringSpecificationMachineDetailsLogic MonitoringSpecificationMachineDetailsLogic;

        public MonitoringSpecificationMachineLogic(MonitoringSpecificationMachineDetailsLogic monitoringSpecificationMachineDetailsLogic,IIdentityService identityService, ProductionDbContext dbContext) : base(identityService, dbContext)
        {
            this.MonitoringSpecificationMachineDetailsLogic = monitoringSpecificationMachineDetailsLogic;
        }

        public override void CreateModel(MonitoringSpecificationMachineModel model)
        {
            foreach (MonitoringSpecificationMachineDetailsModel item in model.Details)
            {
                EntityExtension.FlagForCreate(item, IdentityService.Username, UserAgent);
            }
            base.CreateModel(model);
        }

        public override async Task DeleteModel(int id)
        {
            var model = await ReadModelById(id);

            foreach (var item in model.Details)
            {
                EntityExtension.FlagForDelete(item, IdentityService.Username, UserAgent);
            }

            EntityExtension.FlagForDelete(model, IdentityService.Username, UserAgent, true);
            DbSet.Update(model);
        }

        public override async void UpdateModelAsync(int id, MonitoringSpecificationMachineModel model)
        {
            if (model.Details != null)
            {
                HashSet<int> detailId = MonitoringSpecificationMachineDetailsLogic.DataId(id);
                foreach (var itemId in detailId)
                {
                    MonitoringSpecificationMachineDetailsModel data = model.Details.FirstOrDefault(prop => prop.Id.Equals(itemId));
                    if (data == null)
                        await MonitoringSpecificationMachineDetailsLogic.DeleteModel(itemId);
                    else
                    {
                        MonitoringSpecificationMachineDetailsLogic.UpdateModelAsync(itemId, data);
                    }

                    foreach (MonitoringSpecificationMachineDetailsModel item in model.Details)
                    {
                        if (item.Id == 0)
                            MonitoringSpecificationMachineDetailsLogic.CreateModel(item);
                    }
                }
            }

            EntityExtension.FlagForUpdate(model, IdentityService.Username, UserAgent);
            DbSet.Update(model);
        }

        public override Task<MonitoringSpecificationMachineModel> ReadModelById(int id)
        {
            return DbSet.Include(d => d.Details).FirstOrDefaultAsync(d => d.Id.Equals(id) && d.IsDeleted.Equals(false));
        }
    }
}