using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Monitoring_Specification_Machine
{
    public class MonitoringSpecificationMachineReportViewModel : BaseViewModel
    {
        public string machine { get; set; }
        public DateTimeOffset DateTimeInput { get; set; }
        public string orderNo { get; set; }
        public string cartNumber { get; set; }
        public List<ReportItem> items { get; set; }

    }

    public class ReportItem
    {
        public string indicator { get; set; }
        public string uom { get; set; }
        public string value { get; set; }
    }
}
