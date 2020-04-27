using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.ColorReceipt
{
    public class ColorReceiptItemViewModel : BaseViewModel
    {
        public ProductIntegrationViewModel Product { get; set; }
        public double Quantity { get; set; }
    }
}
