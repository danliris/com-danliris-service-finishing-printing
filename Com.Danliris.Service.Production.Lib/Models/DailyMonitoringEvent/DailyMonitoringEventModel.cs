using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.DailyMonitoringEvent
{
    public class DailyMonitoringEventModel : StandardEntity, IValidatableObject
    {
        public DailyMonitoringEventModel()
        {
            DailyMonitoringEventProductionOrderItems = new HashSet<DailyMonitoringEventProductionOrderItemModel>();
            DailyMonitoringEventLossEventItems = new HashSet<DailyMonitoringEventLossEventItemModel>();
        }

        [MaxLength(16)]
        public string Code { get; set; }
        public int MachineId { get; set; }
        [MaxLength(64)]
        public string MachineCode { get; set; }
        [MaxLength(4096)]
        public string MachineName { get; set; }
        public bool MachineUseBQBS { get; set; }
        
        public DateTimeOffset Date { get; set; }
        [MaxLength(128)]
        public string Shift { get; set; }
        [MaxLength(8)]
        public string Group { get; set; }

        public int ProcessTypeId { get; set; }
        [MaxLength(512)]
        public string ProcessTypeCode { get; set; }
        [MaxLength(2048)]
        public string ProcessTypeName { get; set; }

        public int EventOrganizerId { get; set; }

        [MaxLength(4096)]
        public string ProcessArea { get; set; }

        public int OrderTypeId { get; set; }
        [MaxLength(512)]
        public string OrderTypeCode { get; set; }
        [MaxLength(2048)]
        public string OrderTypeName { get; set; }

        [MaxLength(4096)]
        public string Kasie { get; set; }
        [MaxLength(4096)]
        public string Kasubsie { get; set; }
        [MaxLength(4096)]
        public string ElectricMechanic { get; set; }
        [MaxLength(4096)]
        public string Notes { get; set; }

        public ICollection<DailyMonitoringEventProductionOrderItemModel> DailyMonitoringEventProductionOrderItems { get; set; }
        public ICollection<DailyMonitoringEventLossEventItemModel> DailyMonitoringEventLossEventItems { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
