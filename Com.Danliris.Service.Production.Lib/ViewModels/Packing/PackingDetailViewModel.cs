using Com.Danliris.Service.Finishing.Printing.Lib.Models.Packing;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Packing
{
    public class PackingDetailViewModel : BaseViewModel, IValidatableObject
    {
        [MaxLength(250)]
        public string Lot { get; set; }
        [MaxLength(100)]
        public string Grade { get; set; }
        public int Weight { get; set; }
        public int Length { get; set; }
        public int Quantity { get; set; }
        [MaxLength(500)]
        public string Remark { get; set; }

        public int PackingId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Lot))
                yield return new ValidationResult("Lot harus diisi", new List<string> { "Lot" });
        }
    }
}
