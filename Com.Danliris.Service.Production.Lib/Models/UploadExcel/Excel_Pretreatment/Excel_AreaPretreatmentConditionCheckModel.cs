using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.UploadExcel.Excel_Pretreatment
{
    public class Excel_AreaPretreatmentConditionCheckModel
    {
        public int Id { get; set; }
        public string Shift { get; set; }
        public string Group { get; set; }
        public DateTime Date { get; set; }
        public string Machine { get; set; }
        public TimeSpan Time { get; set; }
        public string OrderNo { get; set; }
        public string CartNo { get; set; }
        public double PressSat1 { get; set; }
        public double PressSat2 { get; set; }
        public double SpeedLB1 { get; set; }
        public double SpeedLB2 { get; set; }
        public double WasherTemp1 { get; set; }
        public double WasherTemp2 { get; set; }
        public double WasherTemp3 { get; set; }
        public double WasherTemp4 { get; set; }
        public double WasherTemp5 { get; set; }
        public double WasherTemp6 { get; set; }
        public double PressFM1 { get; set; }
        public double PressFM2 { get; set; }
        public double ChamberTempWater1 { get; set; }
        public double ChamberTempWater2 { get; set; }
        public double ChamberTempSteam1 { get; set; }
        public double ChamberTempSteam2 { get; set; }
        public double ChamberTiming1 { get; set; }
        public double ChamberTiming2 { get; set; }




        public double PolyStreamTemp1 { get; set; }
        public double PolyStreamTemp2 { get; set; }
        public double PolyStreamTemp3 { get; set; }
        public double PolyStreamTemp4 { get; set; }
        public double TransferPump1 { get; set; }
        public double TransferPump2 { get; set; }
        public string Operator { get; set; }
        public string Kasubsie { get; set; }
        public string Kasie { get; set; }







    }
}
