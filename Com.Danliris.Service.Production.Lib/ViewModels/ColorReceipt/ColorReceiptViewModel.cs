using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.ColorReceipt
{
    public class ColorReceiptViewModel : BaseViewModel, IValidatableObject
    {
        public ColorReceiptViewModel()
        {
            ColorReceiptItems = new HashSet<ColorReceiptItemViewModel>();
        }

        public string ColorName { get; set; }
        public string ColorCode { get; set; }
        public TechnicianViewModel Technician { get; set; }
        public bool ChangeTechnician { get; set; }
        public string Remark { get; set; }

        public ICollection<ColorReceiptItemViewModel> ColorReceiptItems { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(string.IsNullOrEmpty(ColorName))
                yield return new ValidationResult("Nama Warna harus diisi", new List<string> { "ColorName" });

            if(Technician == null)
                yield return new ValidationResult("Teknisi harus diisi", new List<string> { "Technician" });

            int Count = 0;
            string DetailErrors = "[";

            if (ColorReceiptItems != null && ColorReceiptItems.Count > 0)
            {
                foreach (var item in ColorReceiptItems)
                {
                    DetailErrors += "{";

                    if(item.Product == null)
                    {
                        Count++;
                        DetailErrors += "Product: 'Dye Stuff Harus Diisi',";
                    }

                    DetailErrors += "}, ";
                }
            }
            else
            {
                yield return new ValidationResult("Detail harus diisi", new List<string> { "ColorReceiptItems" });
            }

            DetailErrors += "]";

            if (Count > 0)
                yield return new ValidationResult(DetailErrors, new List<string> { "ColorReceiptItems" });
        }
    }
}
