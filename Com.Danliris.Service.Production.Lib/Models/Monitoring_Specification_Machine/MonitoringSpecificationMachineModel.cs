using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.Monitoring_Specification_Machine
{
    public class MonitoringSpecificationMachineModel : StandardEntity
    {
        public string Code { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public string CartNumber { get; set; }

        //unit
        public string UnitId { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }
        public string DivisionId { get; set; }
        public string DivisionName { get; set; }
        public string DivisionCode { get; set; }


    }
}
