using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.Monitoring_Specification_Machine
{
    public class MonitoringSpecificationMachineModel : StandardEntity, IValidatableObject
    {
        public string Code { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public string CartNumber { get; set; }

        //Machine
        public int MachineId { get; set; }
        public string MachineName { get; set; }

        //ProductionOrder
        public int ProductionOrderId { get; set; }
        public string ProductionOrderNo { get; set; }

        public ICollection<MonitoringSpecificationMachineDetailsModel> Details { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
