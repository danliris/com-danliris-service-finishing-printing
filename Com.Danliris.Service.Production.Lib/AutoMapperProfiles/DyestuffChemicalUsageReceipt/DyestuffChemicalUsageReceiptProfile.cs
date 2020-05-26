using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.DyestuffChemicalUsageReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DyestuffChemicalUsageReceipt;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.DyestuffChemicalUsageReceipt
{
    public class DyestuffChemicalUsageReceiptProfile : Profile
    {
        public DyestuffChemicalUsageReceiptProfile()
        {
            CreateMap<DyestuffChemicalUsageReceiptModel, DyestuffChemicalUsageReceiptViewModel>()
                .ForPath(dest => dest.ProductionOrder.Id, opt => opt.MapFrom(src => src.ProductionOrderId))
                .ForPath(dest => dest.ProductionOrder.OrderNo, opt => opt.MapFrom(src => src.ProductionOrderOrderNo))
                .ForPath(dest => dest.ProductionOrder.OrderQuantity, opt => opt.MapFrom(src => src.ProductionOrderOrderQuantity))
                .ForPath(dest => dest.ProductionOrder.Material.Id, opt => opt.MapFrom(src => src.ProductionOrderMaterialId))
                .ForPath(dest => dest.ProductionOrder.Material.Name, opt => opt.MapFrom(src => src.ProductionOrderMaterialName))
                .ForPath(dest => dest.ProductionOrder.MaterialConstruction.Id, opt => opt.MapFrom(src => src.ProductionOrderMaterialConstructionId))
                .ForPath(dest => dest.ProductionOrder.MaterialConstruction.Name, opt => opt.MapFrom(src => src.ProductionOrderMaterialConstructionName))
                .ForPath(dest => dest.ProductionOrder.MaterialWidth, opt => opt.MapFrom(src => src.ProductionOrderMaterialWidth))
                .ForPath(dest => dest.StrikeOff.Id, opt => opt.MapFrom(src => src.StrikeOffId))
                .ForPath(dest => dest.StrikeOff.Code, opt => opt.MapFrom(src => src.StrikeOffCode))
                .ForPath(dest => dest.StrikeOff.Type, opt => opt.MapFrom(src => src.StrikeOffType))
                .ForPath(dest => dest.UsageReceiptItems, opt => opt.MapFrom(src => src.DyestuffChemicalUsageReceiptItems))
                .ReverseMap();

            CreateMap<DyestuffChemicalUsageReceiptItemModel, DyestuffChemicalUsageReceiptItemViewModel>()
                .ForPath(dest => dest.UsageReceiptDetails, opt => opt.MapFrom(src => src.DyestuffChemicalUsageReceiptItemDetails))
                .ReverseMap();

            CreateMap<DyestuffChemicalUsageReceiptItemDetailModel, DyestuffChemicalUsageReceiptItemDetailViewModel>()
               .ReverseMap();
        }
    }
}
