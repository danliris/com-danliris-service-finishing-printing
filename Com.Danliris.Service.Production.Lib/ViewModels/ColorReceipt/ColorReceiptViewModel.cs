using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.ColorReceipt
{
    public class ColorReceiptViewModel : BaseViewModel, IValidatableObject
    {
        public ColorReceiptViewModel()
        {
            ColorReceiptItems = new HashSet<ColorReceiptItemViewModel>();
            DyeStuffReactives = new HashSet<ColorReceiptDyeStuffReactiveViewModel>();
        }

        public string ColorName { get; set; }
        public string ColorCode { get; set; }
        public TechnicianViewModel Technician { get; set; }
        public string NewTechnician { get; set; }
        public bool ChangeTechnician { get; set; }
        public string Remark { get; set; }
        public string Cloth { get; set; }
        public string Type { get; set; }

        public ICollection<ColorReceiptItemViewModel> ColorReceiptItems { get; set; }
        public ICollection<ColorReceiptDyeStuffReactiveViewModel> DyeStuffReactives { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(string.IsNullOrEmpty(ColorName))
                yield return new ValidationResult("Nama Warna harus diisi", new List<string> { "ColorName" });

            if(Technician == null && string.IsNullOrEmpty(NewTechnician))
                yield return new ValidationResult("Teknisi harus diisi", new List<string> { "Technician" });

            if(ChangeTechnician && string.IsNullOrEmpty(NewTechnician))
                yield return new ValidationResult("Teknisi Baru harus diisi", new List<string> { "NewTechnician" });

            if(string.IsNullOrEmpty(Type))
                yield return new ValidationResult("Jenis harus diisi", new List<string> { "Type" });

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

                if(ColorReceiptItems.Sum(s => s.Quantity) > 1000)
                    yield return new ValidationResult("Total Quantity tidak boleh melebihi 1000", new List<string> { "ColorReceiptItems" });
            }
            else
            {
                yield return new ValidationResult("Detail harus diisi", new List<string> { "ColorReceiptItems" });
            }

            DetailErrors += "]";

            if (Count > 0)
                yield return new ValidationResult(DetailErrors, new List<string> { "ColorReceiptItems" });

            int DyeCount = 0;
            string DyeDetailErrors = "[";

            if (DyeStuffReactives != null && DyeStuffReactives.Count > 0)
            {
                foreach (var item in DyeStuffReactives)
                {
                    DyeDetailErrors += "{";

                    if (item.Name == null)
                    {
                        DyeCount++;
                        DyeDetailErrors += "Name: 'Nama Harus Diisi',";
                    }

                    DyeDetailErrors += "}, ";
                }

                if(DyeStuffReactives.FirstOrDefault(s => s.Name != null && s.Name.ToLower() == "air").Quantity < 0)
                    yield return new ValidationResult("Quantity Air tidak boleh kurang dari 0", new List<string> { "DyeStuffItems" });
            }
            else
            {
                yield return new ValidationResult("Dye Stuff Reaktif harus diisi", new List<string> { "DyeStuffItems" });
            }

            DyeDetailErrors += "]";

            if (DyeCount > 0)
                yield return new ValidationResult(DyeDetailErrors, new List<string> { "DyeStuffItems" });

            if(DyeStuffReactives != null && DyeStuffReactives.Count > 0 && ColorReceiptItems != null && ColorReceiptItems.Count > 0)
            {
                if(DyeStuffReactives.Sum(s => s.Quantity) + ColorReceiptItems.Sum(s => s.Quantity) != 1000)
                    yield return new ValidationResult("Total Quantity dari Dye Stuff dan Dye Stuff Reaktif harus sama dengan 1000", new List<string> { "ColorName" });
            }
        }
    }
}
