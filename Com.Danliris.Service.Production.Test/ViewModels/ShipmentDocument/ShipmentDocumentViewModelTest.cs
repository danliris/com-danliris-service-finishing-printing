using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.ShipmentDocument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.ShipmentDocument
{
    public class ShipmentDocumentViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            var storage = new StorageIntegrationViewModel();
            ShipmentDocumentViewModel viewModel = new ShipmentDocumentViewModel()
            {
                UId = "UId",
                Code = "Code",
                DeliveryReference = "DeliveryReference",
                IsVoid =true,
                Storage = storage

            };
        }

        [Fact]
        public void validate_default()
        {

            ShipmentDocumentViewModel viewModel = new ShipmentDocumentViewModel()
            {
                UId = "UId",
                Code = "Code",
                DeliveryReference = "DeliveryReference",
                IsVoid = true,
                DeliveryDate =DateTimeOffset.Now.AddDays(1)

            };
            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }
    }
}
