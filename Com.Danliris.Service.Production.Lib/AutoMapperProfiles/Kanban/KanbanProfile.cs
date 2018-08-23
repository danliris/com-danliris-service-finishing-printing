using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Kanban;
using Com.Danliris.Service.Production.Lib.ViewModels.Master.Step;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.Kanban
{
    public class KanbanProfile : Profile
    {
        public KanbanProfile()
        {
            CreateMap<KanbanStepIndicatorModel, StepIndicatorViewModel>().ReverseMap();
            CreateMap<KanbanStepModel, KanbanStepViewModel>().ReverseMap();
            CreateMap<KanbanInstructionModel, KanbanInstructionViewModel>().ReverseMap();
            CreateMap<KanbanModel, KanbanViewModel>()
                //Cart
                .ForPath(p => p.Cart.CartNumber, opt => opt.MapFrom(m => m.CartCartNumber))
                .ForPath(p => p.Cart.Code, opt => opt.MapFrom(m => m.CartCode))
                .ForPath(p => p.Cart.Pcs, opt => opt.MapFrom(m => m.CartPcs))
                .ForPath(p => p.Cart.Qty, opt => opt.MapFrom(m => m.CartQty))
                .ForPath(p => p.Cart.Uom.Unit, opt => opt.MapFrom(m => m.CartUomUnit))
                //Production Order
                .ForPath(p => p.ProductionOrder.Id, opt => opt.MapFrom(m => m.ProductionOrderId))
                .ForPath(p => p.ProductionOrder.DeliveryDate, opt => opt.MapFrom(m => m.ProductionOrderDeliveryDate))
                .ForPath(p => p.ProductionOrder.Buyer.Id, opt => opt.MapFrom(m => m.ProductionOrderBuyerId))
                .ForPath(p => p.ProductionOrder.Buyer.Code, opt => opt.MapFrom(m => m.ProductionOrderBuyerCode))
                .ForPath(p => p.ProductionOrder.Buyer.Name, opt => opt.MapFrom(m => m.ProductionOrderBuyerName))
                .ForPath(p => p.ProductionOrder.HandlingStandard, opt => opt.MapFrom(m => m.ProductionOrderHandlingStandard))
                .ForPath(p => p.ProductionOrder.Material.Id, opt => opt.MapFrom(m => m.ProductionOrderMaterialId))
                .ForPath(p => p.ProductionOrder.Material.Code, opt => opt.MapFrom(m => m.ProductionOrderMaterialCode))
                .ForPath(p => p.ProductionOrder.Material.Name, opt => opt.MapFrom(m => m.ProductionOrderMaterialName))
                .ForPath(p => p.ProductionOrder.MaterialConstruction.Id, opt => opt.MapFrom(m => m.ProductionOrderMaterialConstructionId))
                .ForPath(p => p.ProductionOrder.MaterialConstruction.Code, opt => opt.MapFrom(m => m.ProductionOrderMaterialConstructionCode))
                .ForPath(p => p.ProductionOrder.MaterialConstruction.Name, opt => opt.MapFrom(m => m.ProductionOrderMaterialConstructionName))
                .ForPath(p => p.ProductionOrder.OrderNo, opt => opt.MapFrom(m => m.ProductionOrderOrderNo))
                .ForPath(p => p.ProductionOrder.OrderType.Id, opt => opt.MapFrom(m => m.ProductionOrderOrderTypeId))
                .ForPath(p => p.ProductionOrder.OrderType.Code, opt => opt.MapFrom(m => m.ProductionOrderOrderTypeCode))
                .ForPath(p => p.ProductionOrder.OrderType.Name, opt => opt.MapFrom(m => m.ProductionOrderOrderTypeName))
                .ForPath(p => p.ProductionOrder.ProcessType.Id, opt => opt.MapFrom(m => m.ProductionOrderProcessTypeId))
                .ForPath(p => p.ProductionOrder.ProcessType.Code, opt => opt.MapFrom(m => m.ProductionOrderProcessTypeCode))
                .ForPath(p => p.ProductionOrder.ProcessType.Name, opt => opt.MapFrom(m => m.ProductionOrderProcessTypeName))
                .ForPath(p => p.ProductionOrder.YarnMaterial.Id, opt => opt.MapFrom(m => m.ProductionOrderYarnMaterialId))
                .ForPath(p => p.ProductionOrder.YarnMaterial.Code, opt => opt.MapFrom(m => m.ProductionOrderYarnMaterialCode))
                .ForPath(p => p.ProductionOrder.YarnMaterial.Name, opt => opt.MapFrom(m => m.ProductionOrderYarnMaterialName))
                .ForPath(p => p.SelectedProductionOrderDetail.Id, opt => opt.MapFrom(m => m.SelectedProductionOrderDetailId))
                .ForPath(p => p.SelectedProductionOrderDetail.ColorRequest, opt => opt.MapFrom(m => m.SelectedProductionOrderDetailColorRequest))
                .ForPath(p => p.SelectedProductionOrderDetail.ColorTemplate, opt => opt.MapFrom(m => m.SelectedProductionOrderDetailColorTemplate))
                .ForPath(p => p.SelectedProductionOrderDetail.ColorType.Code, opt => opt.MapFrom(m => m.SelectedProductionOrderDetailColorTypeCode))
                .ForPath(p => p.SelectedProductionOrderDetail.ColorType.Name, opt => opt.MapFrom(m => m.SelectedProductionOrderDetailColorTypeName))
                .ForPath(p => p.SelectedProductionOrderDetail.Quantity, opt => opt.MapFrom(m => m.SelectedProductionOrderDetailQuantity))
                .ForPath(p => p.SelectedProductionOrderDetail.Uom.Unit, opt => opt.MapFrom(m => m.SelectedProductionOrderDetailUomUnit))
                .ReverseMap();
        }
    }
}
