using Com.Danliris.Service.Finishing.Printing.Lib.Models.DailyMonitoringEvent;
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

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.DailyMonitoringEvent
{
    public class DailyMonitoringEventLogic : BaseLogic<DailyMonitoringEventModel>
    {
        public DailyMonitoringEventLogic(IIdentityService identityService, ProductionDbContext dbContext) : base(identityService, dbContext)
        {
        }

        public override void CreateModel(DailyMonitoringEventModel model)
        {
            foreach (var item in model.DailyMonitoringEventLossEventItems)
            {
                item.FlagForCreate(IdentityService.Username, UserAgent);
            }
            foreach (var item in model.DailyMonitoringEventProductionOrderItems)
            {
                item.FlagForCreate(IdentityService.Username, UserAgent);
            }
            base.CreateModel(model);
        }

        public override async Task DeleteModel(int id)
        {
            var model = await ReadModelById(id);
            model.FlagForDelete(IdentityService.Username, UserAgent);
            foreach (var item in model.DailyMonitoringEventLossEventItems)
            {
                item.FlagForDelete(IdentityService.Username, UserAgent);
            }
            foreach (var item in model.DailyMonitoringEventProductionOrderItems)
            {
                item.FlagForDelete(IdentityService.Username, UserAgent);
            }
            DbSet.Update(model);
        }

        public override Task<DailyMonitoringEventModel> ReadModelById(int id)
        {
            return DbSet.Include(s => s.DailyMonitoringEventLossEventItems).Include(s => s.DailyMonitoringEventProductionOrderItems).FirstOrDefaultAsync(s => s.Id == id);
        }

        public override async Task UpdateModelAsync(int id, DailyMonitoringEventModel model)
        {
            var dbModel = await ReadModelById(id);
            dbModel.Date = model.Date;
            dbModel.ElectricMechanic = model.ElectricMechanic;
            dbModel.Group = model.Group;
            dbModel.Kasie = model.Kasie;
            dbModel.Kasubsie = model.Kasubsie;
            dbModel.MachineCode = model.MachineCode;
            dbModel.MachineId = model.MachineId;
            dbModel.MachineName = model.MachineName;
            dbModel.Notes = model.Notes;
            dbModel.OrderTypeCode = model.OrderTypeCode;
            dbModel.OrderTypeId = model.OrderTypeId;
            dbModel.OrderTypeName = model.OrderTypeName;
            dbModel.ProcessArea = model.ProcessArea;
            dbModel.ProcessTypeCode = model.ProcessTypeCode;
            dbModel.ProcessTypeId = model.ProcessTypeId;
            dbModel.ProcessTypeName = model.ProcessTypeName;
            dbModel.Shift = model.Shift;

            dbModel.FlagForUpdate(IdentityService.Username, UserAgent);

            var addedLossItems = model.DailyMonitoringEventLossEventItems.Where(x => !dbModel.DailyMonitoringEventLossEventItems.Any(y => y.Id == x.Id)).ToList();
            var updatedLossItems = model.DailyMonitoringEventLossEventItems.Where(x => dbModel.DailyMonitoringEventLossEventItems.Any(y => y.Id == x.Id)).ToList();
            var deletedLossItems = dbModel.DailyMonitoringEventLossEventItems.Where(x => !model.DailyMonitoringEventLossEventItems.Any(y => y.Id == x.Id)).ToList();

            foreach (var item in updatedLossItems)
            {
                var dbItem = dbModel.DailyMonitoringEventLossEventItems.FirstOrDefault(x => x.Id == item.Id);

                dbItem.LossEventLosses = item.LossEventLosses;
                dbItem.LossEventLossesCategory = item.LossEventLossesCategory;
                dbItem.LossEventProductionLossCode = item.LossEventProductionLossCode;
                dbItem.LossEventRemark = item.LossEventRemark;
                dbItem.LossEventRemarkCode = item.LossEventRemarkCode;
                dbItem.LossEventRemarkId = item.LossEventRemarkId;
                dbItem.Time = item.Time;

                dbItem.FlagForUpdate(IdentityService.Username, UserAgent);

                
            }


            foreach (var item in deletedLossItems)
            {
                item.FlagForDelete(IdentityService.Username, UserAgent);
            }

            foreach (var item in addedLossItems)
            {
                item.DailyMonitoringEventId = id;
                item.FlagForCreate(IdentityService.Username, UserAgent);
                
                dbModel.DailyMonitoringEventLossEventItems.Add(item);
            }

            var addedProductionOrderItems = model.DailyMonitoringEventProductionOrderItems.Where(x => !dbModel.DailyMonitoringEventProductionOrderItems.Any(y => y.Id == x.Id)).ToList();
            var updatedProductionOrderItems = model.DailyMonitoringEventProductionOrderItems.Where(x => dbModel.DailyMonitoringEventProductionOrderItems.Any(y => y.Id == x.Id)).ToList();
            var deletedProductionOrderItems = dbModel.DailyMonitoringEventProductionOrderItems.Where(x => !model.DailyMonitoringEventProductionOrderItems.Any(y => y.Id == x.Id)).ToList();

            foreach (var item in updatedProductionOrderItems)
            {
                var dbItem = dbModel.DailyMonitoringEventProductionOrderItems.FirstOrDefault(x => x.Id == item.Id);

                dbItem.Input_BQ = item.Input_BQ;
                dbItem.Output_BS = item.Output_BS;
                dbItem.ProductionOrderCode = item.ProductionOrderCode;
                dbItem.ProductionOrderId = item.ProductionOrderId;
                dbItem.ProductionOrderNo = item.ProductionOrderNo;
                dbItem.KanbanCartCode = item.KanbanCartCode;
                dbItem.KanbanCartNumber = item.KanbanCartNumber;
                dbItem.KanbanCode = item.KanbanCode;
                dbItem.KanbanId = item.KanbanId;
                dbItem.Speed = item.Speed;

                dbItem.FlagForUpdate(IdentityService.Username, UserAgent);


            }


            foreach (var item in deletedProductionOrderItems)
            {
                item.FlagForDelete(IdentityService.Username, UserAgent);
            }

            foreach (var item in addedProductionOrderItems)
            {
                item.DailyMonitoringEventId = id;
                item.FlagForCreate(IdentityService.Username, UserAgent);

                dbModel.DailyMonitoringEventProductionOrderItems.Add(item);
            }
        }
    }
}
