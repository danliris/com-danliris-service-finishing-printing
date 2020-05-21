using Com.Danliris.Service.Finishing.Printing.Lib.Models.ColorReceipt;
using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.StrikeOff
{
    public class StrikeOffItemModel : StandardEntity, IValidatableObject
    {
        public StrikeOffItemModel()
        {
            DyeStuffItems = new HashSet<StrikeOffItemDyeStuffItemModel>();
            ChemicalItems = new HashSet<StrikeOffItemChemicalItemModel>();
        }

        [MaxLength(2048)]
        public string ColorCode { get; set; }

        public int StrikeOffId { get; set; }

        [ForeignKey("StrikeOffId")]
        public StrikeOffModel StrikeOff { get; set; }

        public ICollection<StrikeOffItemDyeStuffItemModel> DyeStuffItems { get; set; }
        public ICollection<StrikeOffItemChemicalItemModel> ChemicalItems { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
