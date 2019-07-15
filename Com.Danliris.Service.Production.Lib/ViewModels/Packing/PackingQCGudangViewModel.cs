using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Packing
{
    public class PackingQCGudangViewModel
    {
        public DateTimeOffset? Date { get; set; }
        public double Dyeing { get; set; }
        public double Jumlah { get; set; }
        public double Printing { get; set; }
        public double UlanganPrinting { get; set; }
        public double UlanganSolid { get; set; }
        public double White { get; set; }
    }
}
