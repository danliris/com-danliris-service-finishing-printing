using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DyestuffChemicalUsageReceipt;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.DyestuffChemicalUsageReceipt
{
    public class DyestuffChemicalUsageReceiptItemDetailViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            DyestuffChemicalUsageReceiptItemDetailViewModel viewModel = new DyestuffChemicalUsageReceiptItemDetailViewModel()
            {
                Index =1,
                Name = "Name",
                ReceiptQuantity =1,
                Adjs1Quantity =1,
                Adjs2Quantity =1,
                Adjs3Quantity =1,
                Adjs4Quantity =1, 
                Adjs5Quantity=1
            };
            Assert.Equal(1, viewModel.Index);
            Assert.Equal("Name", viewModel.Name);
            Assert.Equal(1, viewModel.ReceiptQuantity);
            Assert.Equal(1, viewModel.Adjs1Quantity);
            Assert.Equal(1, viewModel.Adjs2Quantity);
            Assert.Equal(1, viewModel.Adjs3Quantity);
            Assert.Equal(1, viewModel.Adjs4Quantity);
            Assert.Equal(1, viewModel.Adjs5Quantity = 1
);

        }
        }
}
