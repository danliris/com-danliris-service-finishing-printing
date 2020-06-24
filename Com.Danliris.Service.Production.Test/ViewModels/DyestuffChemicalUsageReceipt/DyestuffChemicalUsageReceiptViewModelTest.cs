using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DyestuffChemicalUsageReceipt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.DyestuffChemicalUsageReceipt
{
 public   class DyestuffChemicalUsageReceiptViewModelTest
    {
        [Fact]
        public void validate_default()
        {

            DyestuffChemicalUsageReceiptViewModel viewModel = new DyestuffChemicalUsageReceiptViewModel();
            
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }
    }
}
