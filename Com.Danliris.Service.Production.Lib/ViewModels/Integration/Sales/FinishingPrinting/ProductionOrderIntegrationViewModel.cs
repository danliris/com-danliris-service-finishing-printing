
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Master;
using System.Collections.Generic;

namespace Com.Danliris.Service.Production.Lib.ViewModels.Integration.Sales.FinishingPrinting
{
    public class ProductionOrderIntegrationViewModel : BaseViewModel
    {
        public BuyerIntegrationViewModel Buyer { get; set; }
        public List<ProductionOrderDetailIntegrationViewModel> Details { get; set; }
        public string OrderNo { get; set; }
        public double OrderQuantity { get; set; }
    }
}
