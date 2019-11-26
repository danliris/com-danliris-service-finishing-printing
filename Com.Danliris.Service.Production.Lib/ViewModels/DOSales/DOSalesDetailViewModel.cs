using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DOSales
{
    public class DOSalesDetailViewModel : BaseViewModel, IValidatableObject
    {
        [MaxLength(250)]
        public string UnitName { get; set; }
        [MaxLength(100)]
        public string UnitCode { get; set; }
        public int? Quantity { get; set; }
        public double? Weight { get; set; }
        public double? Length { get; set; }
        [MaxLength(500)]
        public string Remark { get; set; }

        public int? DOSalesId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
