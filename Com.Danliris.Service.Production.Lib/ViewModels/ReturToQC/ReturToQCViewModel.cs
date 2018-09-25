using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.ReturToQC
{
    public class ReturToQCViewModel : BaseViewModel, IValidatableObject
    {
        public ReturToQCViewModel()
        {
        }

        public DateTimeOffset Date { get; set; }

        public string DeliveryOrderNo { get; set; }

        public string Destination { get; set; }

        public string FinishedGoodCode { get; set; }

        public bool IsVoid { get; set; }

        public MaterialIntegrationViewModel Material { get; set; }

        [StringLength(25)]
        public string MaterialWidthFinish { get; set; }

        [StringLength(500)]
        public string Remark { get; set; }

        [StringLength(25)]
        public string ReturNo { get; set; }

        public MaterialConstructionIntegrationViewModel MaterialConstruction { get; set; }

        public ICollection<ReturToQCItemViewModel> Items { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Material == null)
                yield return new ValidationResult("Material harus diisi", new List<string> { "Material" });
            else
            {
                if (Material.Id == 0)
                    yield return new ValidationResult("Material harus diisi", new List<string> { "Material" });
            }

            if (MaterialConstruction == null)
                yield return new ValidationResult("Construction harus diisi", new List<string> { "MaterialConstruction" });
            else
            {
                if (MaterialConstruction.Id == 0)
                    yield return new ValidationResult("Construction harus diisi", new List<string> { "MaterialConstruction" });
            }

            if(string.IsNullOrWhiteSpace(Destination))
                yield return new ValidationResult("Destination harus diisi", new List<string> { "Destination" });

            if(Date == null || Date == DateTimeOffset.MinValue)
                yield return new ValidationResult("Date harus diisi", new List<string> { "Date" });

            if(string.IsNullOrWhiteSpace(MaterialWidthFinish))
                yield return new ValidationResult("MaterialWidthFinish harus diisi", new List<string> { "MaterialWidthFinish" });
        }
    }
}
