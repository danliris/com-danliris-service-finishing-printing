using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.ReturToQC
{
    public class ReturToQCItemModel : StandardEntity, IValidatableObject
    {
        public int ReturToQCId { get; set; }

        [ForeignKey("ReturToQCId")]
        public ReturToQCModel ReturToQC { get; set; }

        public int ProductionOrderId { get; set; }

        public string ProductionOrderNo { get; set; }

        public string ProductionOrderCode { get; set; }

        public ICollection<ReturToQCItemDetailModel> ReturToQCItemDetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
