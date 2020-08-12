using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.NewShipmentDocument;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.NewShipmentDocument
{
    public class NewShipmentDocumentViewModelTest
    {

        [Fact]
        public void Should_Success_Instantiate()
        {
          
            NewShipmentDocumentViewModel viewModel = new NewShipmentDocumentViewModel()
            {
                UId = "UId",
                Code = "Code",
                DeliveryReference = "DeliveryReference",
                IsVoid =false,
                Storage =new StorageIntegrationViewModel()
            };

            Assert.Equal("UId", viewModel.UId);
            Assert.Equal("Code", viewModel.Code);
            Assert.Equal("DeliveryReference", viewModel.DeliveryReference);

        }
    }
}
