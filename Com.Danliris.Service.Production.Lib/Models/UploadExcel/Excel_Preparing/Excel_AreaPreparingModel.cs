using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.UploadExcel.Excel_Preparing
{
    public class Excel_AreaPreparingModel
    {
        public int Id { get; set; }
        public string Activity { get; set; }
        public string OrderType { get; set; }
        public DateTime? DateIN { get; set; }
        public string Shift { get; set; }
        public string Group { get; set; }
        public string OrderNo { get; set; }
        public string Construction { get; set; }
        public string Motif { get; set; }
        public string Color { get; set; }
        public string CartNo { get; set; }
        public string PieceNumber { get; set; }
        public string Grade { get; set; }
        public double? QtyMtr { get; set; }
        public DateTime? DateSeal { get; set; }
        public DateTime? DateSewing { get; set; }
        public DateTime? DateRoll { get; set; }
        public TimeSpan? TimeRoll { get; set; }
        public string ActivityOut { get; set; }
        public DateTime? DateOut { get; set; }
        public string ReceiptIN { get; set; }
    }
}
