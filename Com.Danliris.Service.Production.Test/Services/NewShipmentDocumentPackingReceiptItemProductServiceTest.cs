using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.NewShipmentDocument;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Services
{
    public class NewShipmentDocumentPackingReceiptItemProductServiceTest
    {
        [Fact]
        public void Should_Success_Instanciate_NewShipmentDocumentPackingReceiptItemProduct_ViewModel()
        {

            var viewModel = new NewShipmentDocumentPackingReceiptItemProductViewModel()
            {
                ProductName = "ProductName",
                Quantity = 1,
                QuantityUOM = "QuantityUOM",
                Total = 100,
                UOMId = 1,
                UOMUnit = "UOMUnit",
            };

            Assert.NotNull(viewModel.ProductName);
            Assert.NotNull(viewModel.Quantity);
            Assert.NotNull(viewModel.QuantityUOM);
            Assert.NotNull(viewModel.Total);
            Assert.NotNull(viewModel.UOMId);
            Assert.NotNull(viewModel.UOMUnit);
        }
    }
}
