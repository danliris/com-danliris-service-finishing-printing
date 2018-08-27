using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.Daily_Operation
{
    public class DailyOperationBadOutputReasonsModel : StandardEntity
    {
        public double Length { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }

        //BadOutputReason
        public string BadOutputId { get; set; }
        public string BadOutputCode { get; set; }
        public string BadOutputReason { get; set; }

        //machine
        public string MachineId { get; set; }
        public string MachineCode { get; set; }
        public string MachineName { get; set; }

    }
}
