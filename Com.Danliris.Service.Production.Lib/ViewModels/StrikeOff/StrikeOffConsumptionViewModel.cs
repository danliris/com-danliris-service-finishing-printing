using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.StrikeOff
{
    public class StrikeOffConsumptionViewModel : BaseViewModel
    {
        public StrikeOffConsumptionViewModel()
        {
            StrikeOffItems = new HashSet<StrikeOffConsumptionItemViewModel>();
        }
        public string Code { get; set; }
        public string Remark { get; set; }
        public string Type { get; set; }
        public string Cloth { get; set; }

        public ICollection<StrikeOffConsumptionItemViewModel> StrikeOffItems { get; set; }
    }

    public class StrikeOffConsumptionItemViewModel : BaseViewModel
    {
        public StrikeOffConsumptionItemViewModel()
        {
            StrikeOffItemDetails = new HashSet<StrikeOffConsumptionDetailViewModel>();
        }

        public string ColorCode { get; set; }
        public ICollection<StrikeOffConsumptionDetailViewModel> StrikeOffItemDetails { get; set; }
    }

    public class StrikeOffConsumptionDetailViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
    }
}
