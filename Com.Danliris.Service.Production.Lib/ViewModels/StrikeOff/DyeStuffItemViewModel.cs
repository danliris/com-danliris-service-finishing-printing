using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.StrikeOff
{
    public class DyeStuffItemViewModel : BaseViewModel
    {
        public ProductIntegrationViewModel Product { get; set; }
        public double Quantity { get; set; }
        public string SubType { get; set; }
    }
}
