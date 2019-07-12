using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.DirectLaborCost
{
    public class DirectLaborCostModel : StandardEntity, IValidatableObject
    {
        [MaxLength(25)]
        public string Code { get; set; }
        public int Month { get; set; }

        public int Year { get; set; }

        public double LaborTotal { get; set; }

        public decimal WageTotal { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
