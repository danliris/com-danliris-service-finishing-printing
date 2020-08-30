using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.LossEvent
{
    public class LossEventViewModel : BaseViewModel, IValidatableObject
    {
        public string Code { get; set; }
        public ProcessTypeIntegrationViewModel ProcessType { get; set; }

        public string Losses { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(ProcessType == null || ProcessType.Id == 0)
                yield return new ValidationResult("Jenis Proses harus diisi", new List<string> { "ProcessType" });

            if(string.IsNullOrEmpty(Losses))
                yield return new ValidationResult("Losses harus diisi", new List<string> { "Losses" });
        }
    }
}
