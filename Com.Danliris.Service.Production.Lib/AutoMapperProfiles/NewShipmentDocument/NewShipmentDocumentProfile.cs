﻿using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.NewShipmentDocument;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.NewShipmentDocument;

namespace Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.NewShipmentDocument
{
    public class NewShipmentDocumentProfile : Profile
    {
        public NewShipmentDocumentProfile()
        {
            CreateMap<NewShipmentDocumentModel, NewShipmentDocumentViewModel>()
                .ForPath(dest => dest.DOSales.DOSalesNo, opt => opt.MapFrom(src => src.DOSalesNo))
                .ForPath(dest => dest.DOSales.Id, opt => opt.MapFrom(src => src.DOSalesId))
                .ForPath(dest => dest.DOSales.Buyer.Address, opt => opt.MapFrom(src => src.BuyerAddress))
                .ForPath(dest => dest.DOSales.Buyer.City, opt => opt.MapFrom(src => src.BuyerCity))
                .ForPath(dest => dest.DOSales.Buyer.Code, opt => opt.MapFrom(src => src.BuyerCode))
                .ForPath(dest => dest.DOSales.Buyer.Contact, opt => opt.MapFrom(src => src.BuyerContact))
                .ForPath(dest => dest.DOSales.Buyer.Country, opt => opt.MapFrom(src => src.BuyerCountry))
                .ForPath(dest => dest.DOSales.Buyer.Id, opt => opt.MapFrom(src => src.BuyerId))
                .ForPath(dest => dest.DOSales.Buyer.Name, opt => opt.MapFrom(src => src.BuyerName))
                .ForPath(dest => dest.DOSales.Buyer.NPWP, opt => opt.MapFrom(src => src.BuyerNPWP))
                .ForPath(dest => dest.DOSales.Buyer.Tempo, opt => opt.MapFrom(src => src.BuyerTempo))
                .ForPath(dest => dest.DOSales.Buyer.Type, opt => opt.MapFrom(src => src.BuyerType))
                .ForPath(dest => dest.Details, opt => opt.MapFrom(src => src.Details))
                .ForPath(dest => dest.Storage.code, opt => opt.MapFrom(src => src.StorageCode))
                .ForPath(dest => dest.Storage.description, opt => opt.MapFrom(src => src.StorageDescription))
                .ForPath(dest => dest.Storage.name, opt => opt.MapFrom(src => src.StorageName))
                .ForPath(dest => dest.Storage.unit.code, opt => opt.MapFrom(src => src.StorageUnitCode))
                .ForPath(dest => dest.Storage.unit.name, opt => opt.MapFrom(src => src.StorageUnitName))
                .ForPath(dest => dest.Storage._id, opt => opt.MapFrom(src => src.StorageId))
                .ReverseMap();

            CreateMap<NewShipmentDocumentDetailModel, NewShipmentDocumentDetailViewModel>()
                .ForPath(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                .ForPath(dest => dest.ProductionOrder.OrderNo, opt => opt.MapFrom(src => src.ProductionOrderNo))
                .ForPath(dest => dest.ProductionOrder.Id, opt => opt.MapFrom(src => src.ProductionOrderId))
                .ForPath(dest => dest.ProductionOrder.OrderType.Name, opt => opt.MapFrom(src => src.ProductionOrderType))
                .ForPath(dest => dest.ProductionOrder.DesignCode, opt => opt.MapFrom(src => src.ProductionOrderDesignCode))
                .ForPath(dest => dest.ProductionOrder.DesignNumber, opt => opt.MapFrom(src => src.ProductionOrderDesignNumber))
                .ReverseMap();

            CreateMap<NewShipmentDocumentItemModel, NewShipmentDocumentItemViewModel>()
                .ForPath(dest => dest.PackingReceiptItems, opt => opt.MapFrom(src => src.PackingReceiptItems))
                .ReverseMap();

            CreateMap<NewShipmentDocumentPackingReceiptItemModel, NewShipmentDocumentPackingReceiptItemViewModel>()
                .ReverseMap();
        }
    }
}