using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.UploadExcel.Excel_Pretreatment
{
    public class Excel_AreaPretreatmentDiaryMachineModel
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string Shift { get; set; }
        public string Group { get; set; }
        public string MachineName { get; set; }
        public string OrderNo { get; set; }
        public string Material { get; set; }
        public string Color { get; set; }
        public double? QtyIn { get; set; }
        public double? QtyOutBQ { get; set; }
        public double? QtyOutBS { get; set; }
        public string CartNo { get; set; }
        public double? WidthGreige { get; set; }
        public string ProcessType { get; set; }
        public double? Speed { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? FinishTime { get; set; }
        public string MotifCode { get; set; }
        public string Screen { get; set; }
        public double? Ph { get; set; }
        public double? PresureCD { get; set; }
        public double? StenterTemp1 { get; set; }
        public double? StenterTemp2 { get; set; }
        public double? StenterTemp3 { get; set; }
        public double? StenterTemp4 { get; set; }
        public double? StenterTemp5 { get; set; }
        public double? StenterTemp6 { get; set; }
        public double? StenterTemp7 { get; set; }
        public double? StenterTemp8 { get; set; }
        public double? StenterTemp9 { get; set; }
        public double? StenterTemp10 { get; set; }
        public double? WasherTemp1 { get; set; }
        public double? WasherTemp2 { get; set; }
        public double? WasherTemp3 { get; set; }
        public double? WasherTemp4 { get; set; }
        public double? SaturatorTemp4 { get; set; }
        public double? BurnerProcess { get; set; }
        public double? SaturatorPress { get; set; }
        public string ResultBB { get; set; }
        public string FirePoint { get; set; }
        public double? LoseMTR { get; set; }
        public string Note { get; set; }
        public string Remark { get; set; }
    }
}
