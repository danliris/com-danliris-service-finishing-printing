using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.LossEventCategory;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.LossEventRemark
{
    public class LossEventRemarkViewModel : BaseViewModel, IValidatableObject
    {
        public string Code { get; set; }
        public LossEventCategoryViewModel LossEventCategory { get; set; }

        public string ProductionLossCode { get; set; }
        public string Remark { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (LossEventCategory == null || LossEventCategory.Id == 0)
                yield return new ValidationResult("Kategory Losses harus diisi", new List<string> { "LossEventCategory" });

            if (string.IsNullOrEmpty(ProductionLossCode))
                yield return new ValidationResult("Kode Loss Produksi harus diisi", new List<string> { "ProductionLossCode" });

            if (string.IsNullOrEmpty(Remark))
                yield return new ValidationResult("Keterangan harus diisi", new List<string> { "Remark" });
        }
    }
}
