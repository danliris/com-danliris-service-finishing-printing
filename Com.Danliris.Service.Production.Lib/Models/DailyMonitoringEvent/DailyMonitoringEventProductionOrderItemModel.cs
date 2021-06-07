using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.DailyMonitoringEvent
{
    public class DailyMonitoringEventProductionOrderItemModel : StandardEntity, IValidatableObject
    {
        public long ProductionOrderId { get; set; }
        [MaxLength(256)]
        public string ProductionOrderCode { get; set; }
        [MaxLength(256)]
        public string ProductionOrderNo { get; set; }

        public int KanbanId { get; set; }
        [MaxLength(4096)]
        public string KanbanCode { get; set; }
        [MaxLength(4096)]
        public string KanbanCartCode { get; set; }
        [MaxLength(4096)]
        public string KanbanCartNumber { get; set; }

        public double Speed { get; set; }

        public double Input_BQ { get; set; }

        public double Output_BS { get; set; }

        public int DailyMonitoringEventId { get; set; }
        public virtual DailyMonitoringEventModel DailyMonitoringEvent { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
