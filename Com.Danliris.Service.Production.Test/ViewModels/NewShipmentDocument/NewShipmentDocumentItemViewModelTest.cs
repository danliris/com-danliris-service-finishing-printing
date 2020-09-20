using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.NewShipmentDocument;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.NewShipmentDocument
{
    public class NewShipmentDocumentItemViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            var packingReceiptItems = new List<NewShipmentDocumentPackingReceiptItemViewModel>(){
                new NewShipmentDocumentPackingReceiptItemViewModel()
            };
            NewShipmentDocumentItemViewModel viewModel = new NewShipmentDocumentItemViewModel()
            {
                PackingReceiptCode = "PackingReceiptCode",
                PackingReceiptId = 1,
                PackingReceiptItems = packingReceiptItems,
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
            NewShipmentDocumentItemViewModel viewModel = new NewShipmentDocumentItemViewModel();

            Assert.Throws<NotImplementedException>(() => viewModel.Validate(null));
        }
    }
}
