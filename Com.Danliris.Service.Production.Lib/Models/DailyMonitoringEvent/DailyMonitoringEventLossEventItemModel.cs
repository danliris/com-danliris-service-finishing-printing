using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.DailyMonitoringEvent
{
    public class DailyMonitoringEventLossEventItemModel : StandardEntity, IValidatableObject
    {
        public int LossEventRemarkId { get; set; }
        [MaxLength(16)]
        public string LossEventRemarkCode { get; set; }
        [MaxLength(128)]
        public string LossEventProductionLossCode { get; set; }
        [MaxLength(4096)]
        public string LossEventLossesCategory { get; set; }
        [MaxLength(4096)]
        public string LossEventLosses { get; set; }
        [MaxLength(4096)]
        public string LossEventRemark { get; set; }
        public double Time { get; set; }

        public int DailyMonitoringEventId { get; set; }
        public virtual DailyMonitoringEventModel DailyMonitoringEvent { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
