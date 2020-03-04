using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Kanban
{
    public class KanbanSnapshotViewModel
    {
        public string BuyerName { get; set; }

        public int BuyerId { get; set; }

        public int ProductionOrderId { get; set; }

        public string ProductionOrderNo { get; set; }

        public double Quantity { get; set; }

        public double? PreTreatmentQty { get; set; }

        public double? DyeingQty { get; set; }

        public double? PrintingQty { get; set; }

        public double? FinishingQty { get; set; }

        public double? QCQty { get; set; }
    }
}
