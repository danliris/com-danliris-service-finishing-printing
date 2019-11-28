using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DOSales
{
    public class DOSalesDetailViewModel : BaseViewModel
    {
        [MaxLength(250)]
        public string UnitName { get; set; }
        public int? Quantity { get; set; }
        public double? Weight { get; set; }
        public double? Length { get; set; }
        [MaxLength(500)]
        public string Remark { get; set; }

        public int? DOSalesId { get; set; }

    }
}
