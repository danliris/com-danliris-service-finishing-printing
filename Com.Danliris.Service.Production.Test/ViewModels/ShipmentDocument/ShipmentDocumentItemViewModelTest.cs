using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.ShipmentDocument;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.ShipmentDocument
{
    public class ShipmentDocumentItemViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            var packingReceiptItems = new List<ShipmentDocumentPackingReceiptItemViewModel>()
                {
                    new ShipmentDocumentPackingReceiptItemViewModel()
                };
            ShipmentDocumentItemViewModel viewModel = new ShipmentDocumentItemViewModel()
            {
                PackingReceiptCode = "PackingReceiptCode",
                PackingReceiptId =1,
                PackingReceiptItems =packingReceiptItems,
                ReferenceNo = "ReferenceNo",
                ReferenceType = "ReferenceType"
            };

            Assert.Equal("PackingReceiptCode", viewModel.PackingReceiptCode);
            Assert.Equal(1, viewModel.PackingReceiptId);
            Assert.Equal(packingReceiptItems, viewModel.PackingReceiptItems);
            Assert.Equal("ReferenceNo", viewModel.ReferenceNo);
            Assert.Equal("ReferenceType", viewModel.ReferenceType);
        }

        [Fact]
        public void validate_Throws_NotImplementedException()
        {
            ShipmentDocumentItemViewModel viewModel = new ShipmentDocumentItemViewModel();

            Assert.Throws<NotImplementedException>(() => viewModel.Validate(null));
        }
    }
}
