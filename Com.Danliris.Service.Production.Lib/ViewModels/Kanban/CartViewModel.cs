using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Master;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Kanban
{
    public class CartViewModel : BaseViewModel
    {
        public string CartNumber { get; set; }
        public string Code { get; set; }
        public double Qty { get; set; }
        public double Pcs { get; set; }
        public UOMIntegrationViewModel Uom { get; set; }
    }
}
