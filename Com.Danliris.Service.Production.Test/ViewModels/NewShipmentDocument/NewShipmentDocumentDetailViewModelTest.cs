using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.NewShipmentDocument;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Sales.FinishingPrinting;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.NewShipmentDocument
{
    public class NewShipmentDocumentDetailViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            var productionOrder = new ProductionOrderIntegrationViewModel();
            var items = new List<NewShipmentDocumentItemViewModel>() {
                new NewShipmentDocumentItemViewModel()
            };
            NewShipmentDocumentDetailViewModel viewModel = new NewShipmentDocumentDetailViewModel()
            {
                ProductionOrderColorType = "ProductionOrderColorType",
                ProductionOrderDesignCode = "ProductionOrderDesignCode",
                ProductionOrderDesignNumber = "ProductionOrderDesignNumber",
                ProductionOrderId = 1,
                ProductionOrderNo = "ProductionOrderNo",
                ProductionOrderType = "ProductionOrderType",
                ProductionOrder = productionOrder,
                Items = items
            };
            Assert.Equal("ProductionOrderColorType", viewModel.ProductionOrderColorType);
            Assert.Equal("ProductionOrderDesignCode", viewModel.ProductionOrderDesignCode);
            Assert.Equal("ProductionOrderDesignNumber", viewModel.ProductionOrderDesignNumber);
            Assert.Equal(1, viewModel.ProductionOrderId);
            Assert.Equal("ProductionOrderNo", viewModel.ProductionOrderNo);
            Assert.Equal(productionOrder, viewModel.ProductionOrder);
            Assert.Equal(items, viewModel.Items);
          
        }
        }
}
