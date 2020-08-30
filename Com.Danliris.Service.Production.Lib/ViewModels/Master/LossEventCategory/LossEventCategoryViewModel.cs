using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.LossEvent;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.LossEventCategory
{
    public class LossEventCategoryViewModel : BaseViewModel, IValidatableObject
    {
        public string Code { get; set; }
        public LossEventViewModel LossEvent { get; set; }

        public string LossesCategory { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (LossEvent == null || LossEvent.Id == 0)
                yield return new ValidationResult("Losses harus diisi", new List<string> { "LossEvent" });

            if (string.IsNullOrEmpty(LossesCategory))
                yield return new ValidationResult("Kategori Losses harus diisi", new List<string> { "LossesCategory" });
        }
    }
}
