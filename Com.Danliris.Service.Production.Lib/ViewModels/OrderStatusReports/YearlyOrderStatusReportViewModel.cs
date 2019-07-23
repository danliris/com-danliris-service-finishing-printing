using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.OrderStatusReports
{
    public class YearlyOrderStatusReportViewModel
    {
        public double afterProductionQuantity { get; set; }
        public double diffOrderKanbanQuantity { get; set; }
        public double diffOrderShipmentQuantity { get; set; }
        public double inspectingQuantity { get; set; }
        public string name { get; set; }
        public double onProductionQuantity { get; set; }
        public double orderQuantity { get; set; }
        public double preProductionQuantity { get; set; }
        public double shipmentQuantity { get; set; }
        public double storageQuantity { get; set; }
    }

    public class MonthlyOrderStatusReportViewModel : MonthlyOrderQuantity
    {

    }

    public class ProductionOrderStatusReportViewModel
    {
        public string cartNumber { get; set; }
        public string processArea { get; set; }
        public string process { get; set; }
        public double quantity { get; set; }
        public string status { get; set; }
    }

    public class MonthlyOrderQuantity
    {
        public int orderId { get; set; }
        public string accountName { get; set; }
        public double afterProductionQuantity { get; set; }
        public string buyerName { get; set; }
        public string colorRequest { get; set; }
        public string constructionComposite { get; set; }
        public DateTimeOffset deliveryDate { get; set; }
        public string designCode { get; set; }
        public double diffOrderShipmentQuantity { get; set; }
        public double inspectingQuantity { get; set; }
        public double notInKanbanQuantity { get; set; }
        public double onProductionQuantity { get; set; }
        public string orderNo { get; set; }
        public double orderQuantity { get; set; }
        public string processType { get; set; }
        public double shipmentQuantity { get; set; }
        public double storageQuantity { get; set; }
        public DateTimeOffset _createdDate { get; set; }
    }
}
