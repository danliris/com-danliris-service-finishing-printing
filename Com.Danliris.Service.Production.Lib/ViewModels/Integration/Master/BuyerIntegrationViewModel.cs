using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;

namespace Com.Danliris.Service.Production.Lib.ViewModels.Integration.Master
{
    public class BuyerIntegrationViewModel : BaseViewModel
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string Contact { get; set; }
        public string Country { get; set; }
        public string Name { get; set; }
        public string NPWP { get; set; }
        public string Tempo { get; set; }
        public string Type { get; set; }
    }
}
