using Com.Moonlay.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.DOSales
{
    [JsonObject(IsReference = true)]
    public class DOSalesDetailModel : StandardEntity, IValidatableObject
    {
        [MaxLength(250)]
        public string UnitName { get; set; }
        public int Quantity { get; set; }
        public double Weight { get; set; }
        public double Length { get; set; }
        [MaxLength(500)]
        public string Remark { get; set; }

        public int DOSalesId { get; set; }

        [ForeignKey("DOSalesId")]
        public virtual DOSalesModel DOSales { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
