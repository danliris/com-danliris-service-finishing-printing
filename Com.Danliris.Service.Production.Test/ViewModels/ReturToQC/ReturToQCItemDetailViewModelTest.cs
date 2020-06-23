using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.ReturToQC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.ReturToQC
{
    public class ReturToQCItemDetailViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            ReturToQCItemDetailViewModel viewModel = new ReturToQCItemDetailViewModel()
            {
                ColorWay = "ColorWay",
                DesignCode = "DesignCode",
                DesignNumber = "DesignNumber",
                ProductId =1,
                ProductCode = "ProductCode",
                StorageId =1,
                StorageCode = "StorageCode",
                StorageName = "StorageName",
                UOMId =1
            };
            Assert.Equal("ColorWay", viewModel.ColorWay);
            Assert.Equal(1, viewModel.StorageId);
            Assert.Equal("StorageCode", viewModel.StorageCode);
            Assert.Equal("StorageName", viewModel.StorageName);
            Assert.Equal(1, viewModel.UOMId);
            Assert.Equal("ProductCode", viewModel.ProductCode);
        }
        [Fact]
        public void validate_default()
        {
            ReturToQCItemDetailViewModel viewModel = new ReturToQCItemDetailViewModel()
            {
                QuantityBefore = 0,
                ReturQuantity = 1,

            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }
    }
}
