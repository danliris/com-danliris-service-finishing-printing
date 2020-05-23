using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DyestuffChemicalUsageReceipt
{
    public class DyestuffChemicalUsageReceiptItemDetailViewModel : BaseViewModel
    {
        public int Index { get; set; }
        public string Name { get; set; }

        public double ReceiptQuantity { get; set; }

        public double Prod1Quantity { get; set; }

        public double Prod2Quantity { get; set; }

        public double Prod3Quantity { get; set; }

        public double Prod4Quantity { get; set; }

        public double Prod5Quantity { get; set; }
    }
}
