using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.DailyMonitoringEvent;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.DailyMonitoringEvent;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Test.DataUtils
{
    public class DailyMonitoringEventDataUtil : BaseDataUtil<DailyMonitoringEventFacade, DailyMonitoringEventModel>
    {
        public DailyMonitoringEventDataUtil(DailyMonitoringEventFacade facade) : base(facade)
        {
        }

        public override DailyMonitoringEventModel GetNewData()
        {
            return new DailyMonitoringEventModel()
            {
                Code = "c",
                DailyMonitoringEventLossEventItems = new List<DailyMonitoringEventLossEventItemModel>()
                {
                    new DailyMonitoringEventLossEventItemModel()
                    {
                        LossEventLosses = "l",
                        LossEventLossesCategory = "c",
                        LossEventProductionLossCode = "c",
                        Time = 1,
                        LossEventRemarkId = 1,
                        LossEventRemarkCode = "c",
                        LossEventRemark = "r"
                    }
                },
                DailyMonitoringEventProductionOrderItems = new List<DailyMonitoringEventProductionOrderItemModel>()
                {
                    new DailyMonitoringEventProductionOrderItemModel()
                    {
                        Input_BQ = 1,
                        KanbanCartCode = "c",
                        KanbanId = 1,
                        KanbanCode = "c",
                        KanbanCartNumber = "n",
                        Speed = 1,
                        ProductionOrderNo = "a",
                        ProductionOrderId = 1,
                        ProductionOrderCode  = "c",
                        Output_BS = 1
                    }
                },
                Date = DateTimeOffset.UtcNow,
                ElectricMechanic = "e",
                Group = "a",
                Kasie = "l",
                Kasubsie = "s",
                MachineCode = "c",
                MachineName = "s",
                OrderTypeName = "s",
                ProcessTypeCode = "c",
                MachineUseBQBS = true,
                Shift = "s",
                ProcessTypeName = "a",
                ProcessTypeId = 1,
                ProcessArea = "s",
                OrderTypeId = 1,
                OrderTypeCode = "c",
                Notes = "s",
                MachineId = 1
            };
        }
    }
}
