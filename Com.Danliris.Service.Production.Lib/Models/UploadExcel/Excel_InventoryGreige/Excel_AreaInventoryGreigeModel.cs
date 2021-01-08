using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.UploadExcel.Excel_InventoryGreige
{
    public class Excel_AreaInventoryGreigeModel
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string OrderNo { get; set; }
        public string Material { get; set; }
        public double? QtyOrder { get; set; }
        public double? QtyCart { get; set; }
        public string Shift { get; set; }
        public string CartSeries { get; set; }
        public double? Qty1 { get; set; }
        public string Grade1 { get; set; }
        public double? Qty2 { get; set; }
        public string Grade2 { get; set; }
        public double? Qty3 { get; set; }
        public string Grade3 { get; set; }
        public double? QtyPlan { get; set; }
        public double? QtyRealization { get; set; }
        public DateTime? ReturDate { get; set; }
        public double? QtyRetur { get; set; }
        public string Color { get; set; }
        public double? QtyperCart { get; set; }
        public string CartNo { get; set; }
        public string Remark { get; set; }

    }
}
