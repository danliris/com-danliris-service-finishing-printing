using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Master;
using System;

namespace Com.Danliris.Service.Production.Lib.ViewModels.Integration.Sales.FinishingPrinting
{
    public class FinishingPrintingSalesContractIntegrationViewModel : BaseViewModel
    {
        public AccountBankIntegrationViewModel AccountBank { get; set; }
        public BuyerIntegrationViewModel Agent { get; set; }
        public double Amount { get; set; }
        public int AutoIncrementNumber { get; set; }
        public BuyerIntegrationViewModel Buyer { get; set; }
        public string Code { get; set; }
        public string Commission { get; set; }
        public CommodityIntegrationViewModel Commodity { get; set; }
        public string CommodityDescription { get; set; }
        public string Condition { get; set; }
        public string DeliveredTo { get; set; }
        public DateTimeOffset DeliverySchedule { get; set; }
        public OrderTypeIntegrationViewModel DesignMotive { get; set; }
        public string DispositionNumber { get; set; }
        public bool FromStock { get; set; }
        public ProductIntegrationViewModel Material { get; set; }
        public MaterialConstructionIntegrationViewModel MaterialConstruction { get; set; }
        public string MaterialWidth { get; set; }
        public double OrderQuantity { get; set; }
        public OrderTypeIntegrationViewModel OrderType { get; set; }
        public string Packing { get; set; }
        public string PieceLength { get; set; }
        public double PointLimit { get; set; }
        public int PointSystem { get; set; }
        public QualityIntegrationViewModel Quality { get; set; }
        public double RemainingQuantity { get; set; }
        public string SalesContractNo { get; set; }
        public string ShipmentDescription { get; set; }
        public double ShippingQuantityTolerance { get; set; }
        public TermOfPaymentIntegrationViewModel TermOfPayment { get; set; }
        public string TermOfShipment { get; set; }
        public string TransportFee { get; set; }
        public bool UseIncomeTax { get; set; }
        public UOMIntegrationViewModel UOM { get; set; }
        public YarnMaterialIntegrationViewModel YarnMaterial { get; set; }
    }
}