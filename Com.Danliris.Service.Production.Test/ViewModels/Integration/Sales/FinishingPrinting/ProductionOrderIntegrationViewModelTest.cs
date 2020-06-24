using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Master;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Sales.FinishingPrinting;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Integration.Sales.FinishingPrinting
{
    public class ProductionOrderIntegrationViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            var account = new AccountIntegrationViewModel();
            var buyer = new BuyerIntegrationViewModel();
            var time = DateTimeOffset.Now;
            var designMotive = new DesignMotiveIntegrationViewModel();
            var details = new List<ProductionOrderDetailIntegrationViewModel>()
            {
                    new ProductionOrderDetailIntegrationViewModel()
            };
            var finishType = new FinishTypeIntegrationViewModel();
            var lampStandards = new List<ProductionLampStandardIntegrationViewModel>()
            {
                new ProductionLampStandardIntegrationViewModel()
            };

            var material = new MaterialIntegrationViewModel();
            var materialConstruction = new MaterialConstructionIntegrationViewModel();
            var orderType = new OrderTypeIntegrationViewModel();
            var processType = new ProcessTypeIntegrationViewModel();
            var runWidths = new List<ProductionRunWidthIntegrationViewModel>()
            {
                new ProductionRunWidthIntegrationViewModel()
            };
            var standardTests = new StandardTestIntegrationViewModel();
            var uom = new UOMIntegrationViewModel();
            var yarnMaterial = new YarnMaterialIntegrationViewModel();
            ProductionOrderIntegrationViewModel viewModel = new ProductionOrderIntegrationViewModel()
            {
                Account = account,
                ArticleFabricEdge = "ArticleFabricEdge",
                Buyer = buyer,
                Code = "Code",
                DeliveryDate = time,
                DesignCode = "DesignCode",
                DesignMotive = designMotive,
                DesignNumber = "DesignNumber",
                Details = details,
                DistributedQuantity = 1,
                FinishType = finishType,
                IsClosed = true,
                IsCompleted = true,
                IsRequested = true,
                IsUsed = true,
                LampStandards = lampStandards,
                Material = material,
                MaterialConstruction = materialConstruction,
                MaterialOrigin = "MaterialOrigin",
                MaterialWidth = "MaterialWidth",
                OrderNo = "OrderNo",
                OrderQuantity = 1,
                OrderType = orderType,
                PackingInstruction = "PackingInstruction",
                ProcessType = processType,
                Remark = "Remark",
                Run = "Run",
                RunWidths = runWidths,
                SalesContractNo = "SalesContractNo",
                ShippingQuantityTolerance =1,
                Sample = "Sample",
                ShrinkageStandard = "ShrinkageStandard",
                StandardTests =standardTests,
                Uom = uom,
                YarnMaterial = yarnMaterial
            };
            Assert.Equal(account, viewModel.Account);
            Assert.Equal("ArticleFabricEdge", viewModel.ArticleFabricEdge);
            Assert.Equal(buyer, viewModel.Buyer);
            Assert.Equal("Code", viewModel.Code);
            Assert.Equal(time, viewModel.DeliveryDate);
            Assert.Equal("DesignCode", viewModel.DesignCode);
            Assert.Equal(designMotive, viewModel.DesignMotive);
            Assert.Equal("DesignNumber", viewModel.DesignNumber);
            Assert.Equal(details, viewModel.Details);
            Assert.Equal(1, viewModel.DistributedQuantity);
            Assert.Equal(finishType, viewModel.FinishType);
            Assert.True(viewModel.IsClosed);
            Assert.True(viewModel.IsCompleted);
            Assert.True(viewModel.IsRequested);
            Assert.True(viewModel.IsUsed);
            Assert.Equal(lampStandards, viewModel.LampStandards);
            Assert.Equal(materialConstruction, viewModel.MaterialConstruction);
            Assert.Equal("MaterialOrigin", viewModel.MaterialOrigin);
            Assert.Equal("MaterialWidth", viewModel.MaterialWidth);
            Assert.Equal(1, viewModel.OrderQuantity);
            Assert.Equal("OrderNo", viewModel.OrderNo);
            Assert.Equal("PackingInstruction", viewModel.PackingInstruction);
            Assert.Equal(orderType, viewModel.OrderType);
            Assert.Equal(processType, viewModel.ProcessType);
            Assert.Equal("Remark", viewModel.Remark);
            Assert.Equal("Run", viewModel.Run);
            Assert.Equal(runWidths, viewModel.RunWidths);
            Assert.Equal("SalesContractNo", viewModel.SalesContractNo);
            Assert.Equal("Sample", viewModel.Sample);
            Assert.Equal(1, viewModel.ShippingQuantityTolerance);
            Assert.Equal("ShrinkageStandard", viewModel.ShrinkageStandard);
            Assert.Equal(standardTests, viewModel.StandardTests);
            Assert.Equal(uom, viewModel.Uom);
            Assert.Equal(yarnMaterial, viewModel.YarnMaterial);
        }
        }
}
