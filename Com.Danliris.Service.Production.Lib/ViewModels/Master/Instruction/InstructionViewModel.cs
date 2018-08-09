using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Danliris.Service.Production.Lib.ViewModels.Master.Step;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Production.Lib.ViewModels.Master.Instruction
{
    public class InstructionViewModel : BaseViewModel, IValidatableObject
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public List<StepViewModel> Steps { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Name))
                yield return new ValidationResult("Nama harus diisi", new List<string> { "Name" });

            int Count = 0;
            string StepErrors = "[";

            if (Steps == null || Steps.Count <= 0)
                yield return new ValidationResult("Tabel Indikator harus diisi", new List<string> { "Step" });
            else
            {
                foreach (StepViewModel Step in Steps)
                {
                    StepErrors += "{";
                    if (Step == null || string.IsNullOrWhiteSpace(Step.Process))
                    {
                        Count++;
                        StepErrors += "Process : 'Step harus diisi'";
                    }
                    StepErrors += "}, ";
                }
            }
            StepErrors += "]";

            if (Count > 0)
                yield return new ValidationResult(StepErrors, new List<string> { "StepIndicators" });

        }
    }
}
