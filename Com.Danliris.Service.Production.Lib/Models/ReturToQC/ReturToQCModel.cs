using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.ReturToQC
{
    public class ReturToQCModel : StandardEntity, IValidatableObject
    {
        public DateTimeOffset Date { get; set; }

        public string DeliveryOrderNo { get; set; }

        public string Destination { get; set; }

        public string FinishedGoodCode { get; set; }

        public bool IsVoid { get; set; }

        public int MaterialId { get; set; }

        [MaxLength(250)]
        public string MaterialName { get; set; }

        [MaxLength(25)]
        public string MaterialCode { get; set; }
        
        [MaxLength(25)]
        public string MaterialWidthFinish { get; set; }

        [MaxLength(500)]
        public string Remark { get; set; }

        [MaxLength(25)]
        public string ReturNo { get; set; }

        public int MaterialConstructionId { get; set; }

        [MaxLength(250)]
        public string MaterialConstructionName { get; set; }

        [MaxLength(25)]
        public string MaterialConstructionCode { get; set; }
        
        //public ICollection<ReturToQCItemViewModel> Items { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
