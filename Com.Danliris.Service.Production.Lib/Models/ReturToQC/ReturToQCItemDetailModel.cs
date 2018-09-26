using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.ReturToQC
{
    public class ReturToQCItemDetailModel : StandardEntity, IValidatableObject
    {
        public int ReturToQCItemId { get; set; }

        [ForeignKey("ReturToQCItemId")]
        public ReturToQCItemModel ReturToQCItem { get; set; }

        public string ColorWay { get; set; }

        public string DesignCode { get; set; }

        public int DesignNumber { get; set; }

        public double Length { get; set; }

        public int ProductId { get; set; }

        public int ProductName { get; set; }

        public int QuantityBefore { get; set; }

        [MaxLength(500)]
        public string Remark { get; set; }

        public int ReturQuantity { get; set; }

        public int StorageId { get; set; }

        public string UOM { get; set; }

        public int UOMId { get; set; }

        public double Weight
        {
            get; set;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
