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

        public DateTimeOffset? ReceiptDate { get; set; }

        public DateTimeOffset? Adjs1Date { get; set; }

        public DateTimeOffset? Adjs2Date { get; set; }

        public DateTimeOffset? Adjs3Date { get; set; }

        public DateTimeOffset? Adjs4Date { get; set; }

        public decimal TotalRealizationQty { get; set; }
        public decimal Wide { get; set; }

        public ICollection<DyestuffChemicalUsageReceiptItemDetailViewModel> UsageReceiptDetails { get; set; }
    }
}
