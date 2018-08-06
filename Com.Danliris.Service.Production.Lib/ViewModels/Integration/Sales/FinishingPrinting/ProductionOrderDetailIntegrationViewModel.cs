using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Master;

namespace Com.Danliris.Service.Production.Lib.ViewModels.Integration.Sales.FinishingPrinting
{
    public class ProductionOrderDetailIntegrationViewModel : BaseViewModel
    {
        public string ColorRequest { get; set; }
        public string ColorTemplate { get; set; }
        public ColorTypeIntegrationViewModel ColorType { get; set; }
        public double Quantity { get; set; }
        public UOMIntegrationViewModel Uom { get; set; } 
    }
}