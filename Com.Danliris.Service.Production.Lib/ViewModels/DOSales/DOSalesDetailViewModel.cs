using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DOSales
{
    public class DOSalesDetailViewModel : BaseViewModel
    {
        /* Unit */
        [MaxLength(255)]
        public string UnitCode { get; set; }
        [MaxLength(255)]
        public string UnitName { get; set; }

        /* Quantity */
        public double PackingQuantity { get; set; }
        public double ImperialQuantity { get; set; }
        public double MetricQuantity { get; set; }

        public int? DOSalesId { get; set; }

    }
}
