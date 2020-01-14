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
        [MaxLength(255)]
        public string UnitRemark { get; set; }

        /* Quantity */
        public double TotalPacking { get; set; }
        public double TotalLength { get; set; }
        public double TotalLengthConversion { get; set; }

        public int? DOSalesId { get; set; }

    }
}
