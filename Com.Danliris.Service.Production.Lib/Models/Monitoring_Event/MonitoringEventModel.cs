using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.Monitoring_Event
{
    public class MonitoringEventModel : StandardEntity, IValidatableObject
    {
        public string Code { get; set; }
        public DateTimeOffset DateStart { get; set; }
        public DateTimeOffset DateEnd { get; set; }
        public double TimeInMilisStart { get; set; }
        public double TimeInMilisEnd { get; set; }
        public string CartNumber { get; set; }
        public string Remark { get; set; }
        //Machine
        public int MachineId { get; set; }
        public string MachineName { get; set; }
        //ProductionOrder
        public int ProductionOrderId { get; set; }
        public string ProductionOrderOrderNo { get; set; }
        public string ProductionOrderDeliveryDate { get; set; }
        //SelectedProductionOrderDetail
        public string ProductionOrderDetailCode { get; set; }
        public string ProductionOrderDetailColorRequest { get; set; }
        public string ProductionOrderDetailColorTemplate { get; set; }
        public string ProductionOrderDetailColorTypeId { get; set; }
        public string ProductionOrderDetailColorType { get; set; }
        public double ProductionOrderDetailQuantity { get; set; }
        //MachineEvent
        public int MachineEventId { get; set; }
        public string MachineEventCode { get; set; }
        public string MachineEventName { get; set; }

        public string MachineEventCategory { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
