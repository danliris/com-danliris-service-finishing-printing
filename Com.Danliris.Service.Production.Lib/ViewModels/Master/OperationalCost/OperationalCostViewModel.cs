using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.OperationalCost
{
    public class OperationalCostViewModel : BaseViewModel, IValidatableObject
    {
        public string Code { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Month < 1)
                yield return new ValidationResult("Bulan harus diisi", new List<string> { "Month" });

            if (Year < 1)
                yield return new ValidationResult("Tahun harus diisi", new List<string> { "Year" });
        }
    }
}
