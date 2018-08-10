using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.Monitoring_Specification_Machine
{
    public class MonitoringSpecificationMachineModel : StandardEntity
    {
        public DateTimeOffset DateTime { get; set; }
        public string CartNumber { get; set; }
    }
}
