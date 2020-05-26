using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DyestuffChemicalUsageReceipt
{
    public class DyestuffChemicalUsageReceiptItemViewModel : BaseViewModel
    {
        public DyestuffChemicalUsageReceiptItemViewModel()
        {
            UsageReceiptDetails = new HashSet<DyestuffChemicalUsageReceiptItemDetailViewModel>();
        }

        public string ColorCode { get; set; }

        public DateTimeOffset? Prod1Date { get; set; }

        public DateTimeOffset? Prod2Date { get; set; }

        public DateTimeOffset? Prod3Date { get; set; }

        public DateTimeOffset? Prod4Date { get; set; }

        public DateTimeOffset? Prod5Date { get; set; }

        public ICollection<DyestuffChemicalUsageReceiptItemDetailViewModel> UsageReceiptDetails { get; set; }
    }
}
