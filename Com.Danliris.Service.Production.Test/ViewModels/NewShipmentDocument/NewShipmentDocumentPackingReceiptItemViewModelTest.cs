using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.NewShipmentDocument;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.NewShipmentDocument
{
    public class NewShipmentDocumentPackingReceiptItemViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            NewShipmentDocumentPackingReceiptItemViewModel viewModel = new NewShipmentDocumentPackingReceiptItemViewModel()
            {
                ColorType = "ColorType",
                DesignCode = "DesignCode",
                DesignNumber = "DesignNumber",
                ProductCode = "ProductCode",
                Length =1,
                ProductId =1,
                ProductName = "ProductName",
                Quantity =1,
                Remark = "Remark",
                UOMId =1,
                UOMUnit = "UOMUnit",
                Weight =1
            };
            Assert.Equal("ColorType", viewModel.ColorType);
            Assert.Equal("DesignCode", viewModel.DesignCode);
            Assert.Equal("DesignNumber", viewModel.DesignNumber);
            Assert.Equal(1, viewModel.Length);
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
