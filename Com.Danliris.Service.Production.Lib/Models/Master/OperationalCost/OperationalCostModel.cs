using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.OperationalCost
{
    public class OperationalCostModel : StandardEntity, IValidatableObject
    {
        [MaxLength(25)]
        public string Code { get; set; }
        public int Month { get; set; }

        public int Year { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
