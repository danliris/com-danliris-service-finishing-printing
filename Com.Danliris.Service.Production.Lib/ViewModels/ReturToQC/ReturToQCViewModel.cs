using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.ReturToQC
{
    public class ReturToQCViewModel : BaseViewModel, IValidatableObject
    {
        public ReturToQCViewModel()
        {
        }

        public string UId { get; set; }

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

            foreach(var item in Items)
            {
                if (item.ProductionOrder == null)
                    yield return new ValidationResult("ProductionOrderNo tidak boleh kosong", new List<string> { "ProductionOrder" });
                else
                {
                    if (item.ProductionOrder.Id == null || item.ProductionOrder.Id == 0)
                        yield return new ValidationResult("ProductionOrderNo tidak boleh kosong", new List<string> { "ProductionOrder" });
                }

                if (item.Details == null || item.Details.Count <= 0)
                    yield return new ValidationResult("Items tidak boleh kosong", new List<string> { "Details" });

                bool flag = item.Details.All(x => x.ReturQuantity <= 0);

                if (flag)
                {
                    foreach (var detail in item.Details)
                    {
                        if (detail.QuantityBefore > 0)
                            yield return new ValidationResult("ReturQuantity tidak boleh lebih dari stockQuantity", new List<string> { "Details" });
                    }
                }
                

                foreach (var detail in item.Details)
                {
                    if (detail.QuantityBefore < detail.ReturQuantity)
                        yield return new ValidationResult("ReturQuantity tidak boleh lebih dari stockQuantity", new List<string> { "ReturQuantity" });

                    if (string.IsNullOrWhiteSpace(detail.Remark))
                        yield return new ValidationResult("Remark tidak boleh kosong", new List<string> { "Remark" });

                    if (detail.Length <= 0)
                        yield return new ValidationResult("Length tidak boleh kosong", new List<string> { "Length" });
                }
            }
        }
    }
}
