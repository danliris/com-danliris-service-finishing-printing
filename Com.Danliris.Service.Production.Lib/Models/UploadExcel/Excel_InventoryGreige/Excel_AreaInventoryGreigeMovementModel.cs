using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.UploadExcel.Excel_InventoryGreige
{
    public class Excel_AreaInventoryGreigeMovementModel
    {
        public int Id { get; set; }
        public string ActivityIN { get; set; }
        public DateTime? DateIN { get; set; }
        public string ProcessType { get; set; }
        public string ReceiptNo { get; set; }
        public string Construction { get; set; }
        public string Grade { get; set; }
        public double? QtyPiece { get; set; }
        public double? PieceNumber { get; set; }
        public double? QtyMtr { get; set; }
        public double? QtyYard { get; set; }
        public double? ConvMtr { get; set; }
        public double? ConvYard { get; set; }
        public double? QtyTotalMtr { get; set; }
        public double? QtyTotalYard { get; set; }
        public string Suplier { get; set; }
        public string FONo { get; set; }
        public string SCNo { get; set; }
        public string Operator { get; set; }
        public string Grade2 { get; set; }
        public double? QtyMtr2 { get; set; }
        public double? Diff { get; set; }
        public string ActivityOUT { get; set; }
        public DateTime? DateOUT { get; set; }
        public string ReceiptNoteOut { get; set; }
        public double? QtyTotal { get; set; }
    }
}
