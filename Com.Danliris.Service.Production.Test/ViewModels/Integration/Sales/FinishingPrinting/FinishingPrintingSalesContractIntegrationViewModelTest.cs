using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Master;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Sales.FinishingPrinting;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Integration.Sales.FinishingPrinting
{
    public class FinishingPrintingSalesContractIntegrationViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            int Id = 1;
            AccountBankIntegrationViewModel abivm = new AccountBankIntegrationViewModel();
            BuyerIntegrationViewModel bivm = new BuyerIntegrationViewModel();
            double Amount = 100; 
            int AutoIncrementNumber = 1;
            string Code = "Code test";
            string Commission = "Commission test";
            CommodityIntegrationViewModel civm = new CommodityIntegrationViewModel();
            string CommodityDescription = "CommodityDescription test";
            string Condition = "Condition test";
            string DeliveredTo = "DeliveredTo test";
            DateTimeOffset DeliverySchedule = DateTime.Now;
            OrderTypeIntegrationViewModel otivm = new OrderTypeIntegrationViewModel();
            string DispositionNumber = "DispositionNumber test";
            bool FromStock = true;
            ProductIntegrationViewModel pivm = new ProductIntegrationViewModel();
            MaterialConstructionIntegrationViewModel mcivm = new MaterialConstructionIntegrationViewModel();
            string MaterialWidth = "MaterialWidth test";
            double OrderQuantity = 100;
            string Packing = "Packing test";
            string PieceLength = "PieceLength test";
            double PointLimit = 100;
            int PointSystem = 1;
            QualityIntegrationViewModel qivm = new QualityIntegrationViewModel();
            double RemainingQuantity = 100;
            string SalesContractNo = "SalesContractNo test";
            string ShipmentDescription = "ShipmentDescription test";
            double ShippingQuantityTolerance = 100;
            TermOfPaymentIntegrationViewModel topivm = new TermOfPaymentIntegrationViewModel(); 
            string TermOfShipment = "TermOfShipment test";
            string TransportFee = "TransportFee test";
            bool UseIncomeTax = true;
            UOMIntegrationViewModel uivm = new UOMIntegrationViewModel();
            YarnMaterialIntegrationViewModel ymivm = new YarnMaterialIntegrationViewModel();


            FinishingPrintingSalesContractIntegrationViewModel fpscivm = new FinishingPrintingSalesContractIntegrationViewModel();
            
            
            fpscivm.Id = 1;
            fpscivm.AccountBank = abivm;
            fpscivm.Agent = bivm;
            fpscivm.Amount = 100;
            fpscivm.AutoIncrementNumber = 1;
            fpscivm.Buyer = bivm;
            fpscivm.Code = Code;
            fpscivm.Commission = Commission;
            fpscivm.Commodity = civm;
            fpscivm.CommodityDescription = CommodityDescription;
            fpscivm.Condition = Condition;
            fpscivm.DeliveredTo = DeliveredTo;
            fpscivm.DeliverySchedule = DeliverySchedule;
            fpscivm.DesignMotive = otivm;
            fpscivm.DispositionNumber = DispositionNumber;
            fpscivm.FromStock = true;
            fpscivm.Material = pivm;
            fpscivm.MaterialConstruction = mcivm;
            fpscivm.MaterialWidth = MaterialWidth;
            fpscivm.OrderQuantity = 100;
            fpscivm.OrderType = otivm;
            fpscivm.Packing = Packing;
            fpscivm.PieceLength = PieceLength;
            fpscivm.PointLimit = 100;
            fpscivm.PointSystem = 1;
            fpscivm.Quality = qivm;
            fpscivm.RemainingQuantity = 100;
            fpscivm.SalesContractNo = SalesContractNo;
            fpscivm.ShipmentDescription = ShipmentDescription;
            fpscivm.ShippingQuantityTolerance = 100;
            fpscivm.TermOfPayment = topivm;
            fpscivm.TermOfShipment = TermOfShipment;
            fpscivm.TransportFee = TransportFee;
            fpscivm.UseIncomeTax = true;
            fpscivm.UOM = uivm;
            fpscivm.YarnMaterial = ymivm;


            Assert.Equal(Id, fpscivm.Id);
            Assert.Equal(Code, fpscivm.Code);
            Assert.Equal(abivm, fpscivm.AccountBank);
            Assert.Equal(bivm, fpscivm.Agent);
            Assert.Equal(Amount, fpscivm.Amount);
            Assert.Equal(AutoIncrementNumber, fpscivm.AutoIncrementNumber);
            Assert.Equal(bivm, fpscivm.Buyer);
            Assert.Equal(Code, fpscivm.Code);
            Assert.Equal(Commission, fpscivm.Commission);
            Assert.Equal(civm, fpscivm.Commodity);
            Assert.Equal(CommodityDescription, fpscivm.CommodityDescription);
            Assert.Equal(Condition, fpscivm.Condition);
            Assert.Equal(DeliveredTo, fpscivm.DeliveredTo);
            Assert.Equal(DeliverySchedule, fpscivm.DeliverySchedule);
            Assert.Equal(otivm, fpscivm.DesignMotive);
            Assert.Equal(DispositionNumber, fpscivm.DispositionNumber);
            Assert.Equal(FromStock, fpscivm.FromStock);
            Assert.Equal(pivm, fpscivm.Material);
            Assert.Equal(mcivm, fpscivm.MaterialConstruction);
            Assert.Equal(MaterialWidth, fpscivm.MaterialWidth);
            Assert.Equal(OrderQuantity, fpscivm.OrderQuantity);
            Assert.Equal(otivm, fpscivm.OrderType);
            Assert.Equal(Packing, fpscivm.Packing);
            Assert.Equal(PieceLength, fpscivm.PieceLength);
            Assert.Equal(PointLimit, fpscivm.PointLimit);
            Assert.Equal(PointSystem, fpscivm.PointSystem);
            Assert.Equal(qivm, fpscivm.Quality);
            Assert.Equal(RemainingQuantity, fpscivm.RemainingQuantity);
            Assert.Equal(SalesContractNo, fpscivm.SalesContractNo);
            Assert.Equal(ShipmentDescription, fpscivm.ShipmentDescription);
            Assert.Equal(ShippingQuantityTolerance, fpscivm.ShippingQuantityTolerance);
            Assert.Equal(topivm, fpscivm.TermOfPayment);
            Assert.Equal(TermOfShipment, fpscivm.TermOfShipment);
            Assert.Equal(TransportFee, fpscivm.TransportFee);
            Assert.Equal(UseIncomeTax, fpscivm.UseIncomeTax);
            Assert.Equal(uivm, fpscivm.UOM);
            Assert.Equal(ymivm, fpscivm.YarnMaterial);
        }
    }
}
