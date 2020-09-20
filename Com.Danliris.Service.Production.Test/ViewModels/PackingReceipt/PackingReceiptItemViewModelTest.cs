using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.PackingReceipt;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.PackingReceipt
{
    public class PackingReceiptItemViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            PackingReceiptItemViewModel viewModel = new PackingReceiptItemViewModel()
            {
                Product = "Product",
                ProductCode = "ProductCode",
                ProductId =1,
                Quantity =1,
                Length =1,
                Weight =1,
                Remark = "Remark",
                Notes = "Notes",
                UomId =1,
                Uom = "Uom",
                IsDelivered =true,
                AvailableQuantity =1
            };

            Assert.Equal("Product", viewModel.Product);
            Assert.Equal("ProductCode", viewModel.ProductCode);
            Assert.Equal(1, viewModel.ProductId);
            Assert.Equal(1, viewModel.Quantity);
            Assert.Equal(1, viewModel.Length);
            Assert.Equal(1, viewModel.Weight);
            Assert.Equal("Remark", viewModel.Remark);
            Assert.Equal("Notes", viewModel.Notes);
            Assert.Equal(1, viewModel.UomId);
            Assert.Equal("Uom", viewModel.Uom);
            Assert.Equal(1, viewModel.AvailableQuantity);
            Assert.True(viewModel.IsDelivered);
        }

        [Fact]
        public void validate_Throws_NotImplementedException()
        {
            PackingReceiptItemViewModel viewModel = new PackingReceiptItemViewModel();

            Assert.Throws<NotImplementedException>(() => viewModel.Validate(null));
        }
    }
}
