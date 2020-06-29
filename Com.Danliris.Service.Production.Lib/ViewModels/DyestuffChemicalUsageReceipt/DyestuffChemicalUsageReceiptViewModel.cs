using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.StrikeOff;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Sales.FinishingPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DyestuffChemicalUsageReceipt
{
    public class DyestuffChemicalUsageReceiptViewModel : BaseViewModel, IValidatableObject
    {
        public DyestuffChemicalUsageReceiptViewModel()
        {
            UsageReceiptItems = new HashSet<DyestuffChemicalUsageReceiptItemViewModel>();
        }

        public ProductionOrderIntegrationViewModel ProductionOrder { get; set; }

        public StrikeOffViewModel StrikeOff { get; set; }

        public DateTimeOffset Date { get; set; }

        public ICollection<DyestuffChemicalUsageReceiptItemViewModel> UsageReceiptItems { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Date == default(DateTimeOffset))
            {
                yield return new ValidationResult("Tanggal Harus Diisi", new List<string> { "Date" });
            }

            if (ProductionOrder == null || ProductionOrder.Id.GetValueOrDefault() == 0)
            {
                yield return new ValidationResult("SPP Harus Diisi", new List<string> { "ProductionOrder" });
            }

            if(StrikeOff == null || StrikeOff.Id == 0)
            {
                yield return new ValidationResult("Motif Harus Diisi", new List<string> { "StrikeOff" });
            }

            
        }
    }
}
