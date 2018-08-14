using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;

namespace Com.Danliris.Service.Production.Lib.ViewModels.Integration.Sales.FinishingPrinting
{
    public class ProductionLampStandardIntegrationViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public long LampStandardId { get; set; }
    }
}