using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.Monitoring_Specification_Machine
{
    public class MonitoringSpecificationMachineDetailModel : StandardEntity
    {
        public string Indicator { get; set; }
        public string DataType { get; set; }
        public string DefaultValue { get; set; }
        public double Value { get; set; }
        public string Uom { get; set; }
    }
}
