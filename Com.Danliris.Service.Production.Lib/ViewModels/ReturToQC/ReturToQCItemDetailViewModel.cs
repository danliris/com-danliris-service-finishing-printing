using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Master;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.ReturToQC
{
    public class ReturToQCItemDetailViewModel : BaseViewModel, IValidatableObject
    {
        public ReturToQCItemDetailViewModel()
        {

        }

        public string ColorWay { get; set; }

        public string DesignCode { get; set; }

        public string DesignNumber { get; set; }
        
        public double Length { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductCode { get; set; }

        public double QuantityBefore { get; set; }

        [StringLength(500)]
        public string Remark { get; set; }

        public double ReturQuantity { get; set; }

        public int StorageId { get; set; }

        public string StorageCode { get; set; }

        public string StorageName { get; set; }

        public string UOMUnit { get; set; }

        public int UOMId { get; set; }
        
        public double Weight
        {
            get; set;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (QuantityBefore < ReturQuantity)
                yield return new ValidationResult("ReturQuantity tidak boleh lebih dari stockQuantity", new List<string> { "ReturQuantity" });

            if (string.IsNullOrWhiteSpace(Remark))
                yield return new ValidationResult("Remark tidak boleh kosong", new List<string> { "Remark" });

            if (Length <= 0)
                yield return new ValidationResult("Length tidak boleh kosong", new List<string> { "Length" });


        }
    }
}
