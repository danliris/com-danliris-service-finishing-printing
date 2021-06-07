using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.ShipmentDocument;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Sales.FinishingPrinting;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.ShipmentDocument
{
    public class ShipmentDocumentDetailViewModelTest
    {

        [Fact]
        public void Should_Success_Instantiate()
        {
            var productionOrder = new ProductionOrderIntegrationViewModel();
            var item = new List<ShipmentDocumentItemViewModel>()
            {
                new ShipmentDocumentItemViewModel()
            };
            
            ShipmentDocumentDetailViewModel viewModel = new ShipmentDocumentDetailViewModel()
            {
              ProductionOrderColorType = "ProductionOrderColorType",
              ProductionOrderDesignCode = "ProductionOrderDesignCode",
                ProductionOrderDesignNumber = "ProductionOrderDesignNumber",
                ProductionOrderId =1,
                ProductionOrderType = "ProductionOrderType",
                ProductionOrderNo = "ProductionOrderNo",
                ProductionOrder = productionOrder,
                Items = item
            };

            Assert.Equal("ProductionOrderColorType", viewModel.ProductionOrderColorType);
            Assert.Equal("ProductionOrderDesignCode", viewModel.ProductionOrderDesignCode);
            Assert.Equal("ProductionOrderDesignNumber", viewModel.ProductionOrderDesignNumber);
            Assert.Equal(1, viewModel.ProductionOrderId);
            Assert.Equal("ProductionOrderNo", viewModel.ProductionOrderNo);
            Assert.Equal(productionOrder, viewModel.ProductionOrder);
            Assert.Equal("ProductionOrderType", viewModel.ProductionOrderType);
            Assert.Equal(item, viewModel.Items);
        }

        [Fact]
        public void validate_Throws_NotImplementedException()
        {
            ShipmentDocumentDetailViewModel viewModel = new ShipmentDocumentDetailViewModel();

            Assert.Throws<NotImplementedException>(() => viewModel.Validate(null));
        }
    }
}
