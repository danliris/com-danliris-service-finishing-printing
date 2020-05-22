using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.StrikeOff
{
    public class ChemicalItemViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public int Index { get; set; }
    }
}
