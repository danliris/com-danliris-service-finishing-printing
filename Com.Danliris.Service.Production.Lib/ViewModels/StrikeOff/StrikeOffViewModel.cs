
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.StrikeOff;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.StrikeOff
{
    public class StrikeOffViewModel : BaseViewModel, IValidatableObject
    {
        public StrikeOffViewModel()
        {
            StrikeOffItems = new HashSet<StrikeOffItemViewModel>();
        }

        public string Code { get; set; }
        public string Remark { get; set; }

        public ICollection<StrikeOffItemViewModel> StrikeOffItems { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            StrikeOffLogic service = (StrikeOffLogic)validationContext.GetService(typeof(StrikeOffLogic));

            if (string.IsNullOrEmpty(Code))
            {
                yield return new ValidationResult("Kode harus diisi", new List<string> { "Code" });
            }
            else
            {
                if (service.CheckDuplicateCode(Code, Id))
                {
                    yield return new ValidationResult("Kode Tidak boleh Duplikat", new List<string> { "Code" });
                }
            }

            int Count = 0;
            string DetailErrors = "[";

            if (StrikeOffItems != null && StrikeOffItems.Count > 0)
            {
                foreach (var item in StrikeOffItems)
                {
                    DetailErrors += "{";

                    if (item.ColorReceipt == null)
                    {
                        Count++;
                        DetailErrors += "ColorReceipt: 'Kode Warna Harus Diisi',";
                    }

                    DetailErrors += "}, ";
                }

            }
            else
            {
                yield return new ValidationResult("Detail harus diisi", new List<string> { "StrikeOffItems" });
            }

            DetailErrors += "]";

            if (Count > 0)
                yield return new ValidationResult(DetailErrors, new List<string> { "StrikeOffItems" });
        }
    }
}
