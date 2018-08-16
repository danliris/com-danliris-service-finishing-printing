
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Master;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Production.Lib.ViewModels.Integration.Sales.FinishingPrinting
{
    public class ProductionOrderIntegrationViewModel
    {
        public int? Id { get; set; }
        public AccountIntegrationViewModel Account { get; set; }
        public string ArticleFabricEdge { get; set; }
        public BuyerIntegrationViewModel Buyer { get; set; }
        public string Code { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }
        public string DesignCode { get; set; }
        public DesignMotiveIntegrationViewModel DesignMotive { get; set; }
        public string DesignNumber { get; set; }
        public List<ProductionOrderDetailIntegrationViewModel> Details { get; set; }
        public double? DistributedQuantity { get; set; }
        public FinishTypeIntegrationViewModel FinishType { get; set; }
        public string FinishWidth { get; set; }
        public string HandlingStandard { get; set; }
        public bool? IsClosed { get; set; }
        public bool? IsCompleted { get; set; }
        public bool? IsRequested { get; set; }
        public bool? IsUsed { get; set; }
        public List<ProductionLampStandardIntegrationViewModel> LampStandards { get; set; }
        public MaterialIntegrationViewModel Material { get; set; }
        public MaterialConstructionIntegrationViewModel MaterialConstruction { get; set; }
        public string MaterialOrigin { get; set; }
        public string MaterialWidth { get; set; }
        public string OrderNo { get; set; }
        public double? OrderQuantity { get; set; }
        public OrderTypeIntegrationViewModel OrderType { get; set; }
        public string PackingInstruction { get; set; }
        public ProcessTypeIntegrationViewModel ProcessType { get; set; }
        public string Remark { get; set; }
        public string Run { get; set; }
        public List<ProductionRunWidthIntegrationViewModel> RunWidths { get; set; }
        public string Sample { get; set; }
        public double? ShippingQuantityTolerance { get; set; }
        public string ShrinkageStandard { get; set; }
        public StandardTestIntegrationViewModel StandardTests { get; set; }
        public UOMIntegrationViewModel Uom { get; set; }
        public YarnMaterialIntegrationViewModel YarnMaterial { get; set; }
    }
}
