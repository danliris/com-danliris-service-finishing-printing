using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.ShipmentDocument;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.ShipmentDocument
{
    public class ShipmentDocumentPackingReceiptItemViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            ShipmentDocumentPackingReceiptItemViewModel viewModel = new ShipmentDocumentPackingReceiptItemViewModel()
            {
                ColorType = "ColorType",
                DesignCode = "DesignCode",
                Length = 1,
                ProductCode = "ProductCode",
                ProductName = "ProductName",
                Quantity = 1,
                Remark = "Remark",
                UOMId =1,
                UOMUnit = "UOMUnit",
                Weight =1,
                ProductId =1,
                DesignNumber = "DesignNumber"
            };

            Assert.Equal("DesignNumber", viewModel.DesignNumber);
            Assert.Equal("ColorType", viewModel.ColorType);
            Assert.Equal("DesignCode", viewModel.DesignCode);
            Assert.Equal(1, viewModel.Length);
            Assert.Equal("ColorType", viewModel.ColorType);
            Assert.Equal("ProductCode", viewModel.ProductCode);
            Assert.Equal(1, viewModel.ProductId);
            Assert.Equal("ProductName", viewModel.ProductName);
            Assert.Equal(1, viewModel.Quantity);
            Assert.Equal("Remark", viewModel.Remark);
            Assert.Equal(1, viewModel.UOMId);
            Assert.Equal("UOMUnit", viewModel.UOMUnit);
            Assert.Equal(1, viewModel.Weight);
        }
        }
}
