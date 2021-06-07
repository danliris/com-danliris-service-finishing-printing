using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.StrikeOff
{
    public class StrikeOffItemChemicalItemModel : StandardEntity, IValidatableObject
    {
        [MaxLength(2048)]
        public string Name { get; set; }
        public double Quantity { get; set; }
        public int Index { get; set; }

        public int StrikeOffItemId { get; set; }

        [ForeignKey("StrikeOffItemId")]
        public StrikeOffItemModel StrikeOffItem { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
