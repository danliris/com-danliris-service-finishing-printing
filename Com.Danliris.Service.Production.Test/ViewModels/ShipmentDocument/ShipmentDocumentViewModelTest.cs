using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.ShipmentDocument;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.ShipmentDocument
{
  public  class ShipmentDocumentViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            ShipmentDocumentViewModel viewModel = new ShipmentDocumentViewModel()
            {
                UId = "UId",
                Code = "Code",
                DeliveryReference = "DeliveryReference",
                IsVoid =true,
                Storage =new StorageIntegrationViewModel()
            };

            Assert.Equal("UId", viewModel.UId);
            Assert.Equal("Code", viewModel.Code);
            Assert.Equal("DeliveryReference", viewModel.DeliveryReference);
            Assert.True(viewModel.IsVoid);
            Assert.NotNull(viewModel.Storage);
        }
    }
}
