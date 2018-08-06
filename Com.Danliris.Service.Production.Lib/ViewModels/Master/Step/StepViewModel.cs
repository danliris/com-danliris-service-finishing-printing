using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Production.Lib.ViewModels.Master.Step
{
    public class StepViewModel : BaseViewModel, IValidatableObject
    {
        public string Alias { get; set; }
        public string Code { get; set; }
        public string Process { get; set; }
        public string ProcessArea { get; set; }
        public List<StepIndicatorViewModel> StepIndicators { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Alias))
                yield return new ValidationResult("Alias harus diisi", new List<string> { "Alias" });

            if (string.IsNullOrWhiteSpace(Process))
                yield return new ValidationResult("Process harus diisi", new List<string> { "Process" });

            int Count = 0;
            string StepIndicatorErrors = "[";

            if (StepIndicators == null || StepIndicators.Count <= 0)
                yield return new ValidationResult("Tabel Indikator harus diisi", new List<string> { "StepIndicator" });
            else
            {
                foreach (StepIndicatorViewModel StepIndicator in StepIndicators)
                {
                    StepIndicatorErrors += "{";
                    if (string.IsNullOrWhiteSpace(StepIndicator.Name))
                    {
                        Count++;
                        StepIndicatorErrors += "Name : 'Indikator harus diisi'";
                    }
                    StepIndicatorErrors += "}, ";
                }
            }
            StepIndicatorErrors += "]";

            if (Count > 0)
                yield return new ValidationResult(StepIndicatorErrors, new List<string> { "StepIndicators" });

        }
    }
}
