using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.DirectLaborCost
{
    public class DirectLaborCostViewModel : BaseViewModel, IValidatableObject
    {
        public string Code { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public double LaborTotal { get; set; }

        public decimal WageTotal { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(LaborTotal < 0)
                yield return new ValidationResult("Jumlah Tenaga Kerja harus diisi", new List<string> { "LaborTotal" });

            if (WageTotal < 0)
                yield return new ValidationResult("Total Besar Upah harus diisi", new List<string> { "WageTotal" });

            if (Month < 1)
                yield return new ValidationResult("Bulan harus diisi", new List<string> { "Month" });
            
            if (Year < 1)
                yield return new ValidationResult("Tahun harus diisi", new List<string> { "Year" });
        }
    }
}
