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
        /* Unit */
        [MaxLength(255)]
        public string UnitCode { get; set; }
        [MaxLength(255)]
        public string UnitName { get; set; }

        /* Quantity */
        public double PackingQuantity { get; set; }
        public double ImperialQuantity { get; set; }
        public double MetricQuantity { get; set; }

        public int DOSalesId { get; set; }

        [ForeignKey("DOSalesId")]
        public virtual DOSalesModel DOSales { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
