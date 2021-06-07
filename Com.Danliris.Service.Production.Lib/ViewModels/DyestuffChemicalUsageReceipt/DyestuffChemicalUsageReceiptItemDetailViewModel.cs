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

        public double Adjs1Quantity { get; set; }

        public double Adjs2Quantity { get; set; }

        public double Adjs3Quantity { get; set; }

        public double Adjs4Quantity { get; set; }

    }
}
