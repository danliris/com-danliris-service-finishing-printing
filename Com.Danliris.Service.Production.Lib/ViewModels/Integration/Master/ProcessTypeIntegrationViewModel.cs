using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;

namespace Com.Danliris.Service.Production.Lib.ViewModels.Integration.Master
{
    public class ProcessTypeIntegrationViewModel
    {
        public int? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public OrderTypeIntegrationViewModel OrderType { get; set; }
    }
}
