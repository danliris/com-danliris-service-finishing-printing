using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Daily_Operation
{
    public class DailyOperationKanbanViewModel
    {
        public string OrderNo { get; set; }
        public double OrderQuantity { get; set; }
        public string Color { get; set; }
        public string Area { get; set; }
        public string Machine { get; set; }
        public string Step { get; set; }
    }
}
