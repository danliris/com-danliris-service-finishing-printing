using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Sales.FinishingPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.ReturToQC
{
    public class ReturToQCItemViewModel : BaseViewModel, IValidatableObject
    {
        public ReturToQCItemViewModel()
        {

        }
        
        public ProductionOrderIntegrationViewModel ProductionOrder { get; set; }
        

        public ICollection<ReturToQCItemDetailViewModel> Details { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ProductionOrder == null)
                yield return new ValidationResult("ProductionOrderNo tidak boleh kosong", new List<string> { "ProductionOrder" });
            else
            {
                if (ProductionOrder.Id == null || ProductionOrder.Id == 0)
                    yield return new ValidationResult("ProductionOrderNo tidak boleh kosong", new List<string> { "ProductionOrder" });
            }

            if(Details == null || Details.Count <= 0)
                yield return new ValidationResult("Items tidak boleh kosong", new List<string> { "Details" });

            List<ReturToQCItemDetailViewModel> tempDetail = Details.Where(x => x.ReturQuantity <= 0).ToList();

            foreach(var item in tempDetail)
            {
                if(item.QuantityBefore > 0)
                    yield return new ValidationResult("ReturQuantity tidak boleh lebih dari stockQuantity", new List<string> { "Details" });
            }

            foreach(var item in Details)
            {
                item.Validate(validationContext);
            }
        }
    }
}
