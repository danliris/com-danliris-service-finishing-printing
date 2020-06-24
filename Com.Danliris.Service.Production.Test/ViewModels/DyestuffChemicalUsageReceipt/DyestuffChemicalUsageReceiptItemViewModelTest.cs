using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DyestuffChemicalUsageReceipt;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.DyestuffChemicalUsageReceipt
{
    public class DyestuffChemicalUsageReceiptItemViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            var time = DateTimeOffset.Now;
            DyestuffChemicalUsageReceiptItemViewModel viewModel = new DyestuffChemicalUsageReceiptItemViewModel()
            {
                ColorCode = "ColorCode",
                Adjs1Date = time,
                Adjs2Date =time,
                Adjs3Date =time,
                Adjs4Date = time,
                Adjs5Date = time,
            };

            Assert.Equal("ColorCode", viewModel.ColorCode);
            Assert.Equal(time, viewModel.Adjs1Date);
            Assert.Equal(time, viewModel.Adjs2Date);
            Assert.Equal(time, viewModel.Adjs3Date);
            Assert.Equal(time, viewModel.Adjs4Date);
            Assert.Equal(time, viewModel.Adjs5Date);
        }
        }
}
