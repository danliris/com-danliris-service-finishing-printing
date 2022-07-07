using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DyestuffChemicalUsageReceipt
{
    public class DyestuffChemicalUsageReceiptReportViewModel : BaseViewModel
    {
        public string createdBy { get; set; }
        public string productionOrderNo { get; set; }
        public string strikeOff { get; set; }
        public string strikeOffType { get; set; }
        public string color { get; set; }
        public DateTimeOffset date { get; set; }
        public int? quantity { get; set; }
        public string materialName { get; set; }
        public string materialConstructionName { get; set; }
        public string materialWidth { get; set; }
        public string name { get; set; }
        public double receiptQty { get; set; }
        public double adj1Qty { get; set; }
        public double adj2Qty { get; set; }
        public double adj3Qty { get; set; }
        public double adj4Qty { get; set; }
        public decimal totalRealization { get; set; }
        public decimal totalScreen { get; set; }
    }
}
