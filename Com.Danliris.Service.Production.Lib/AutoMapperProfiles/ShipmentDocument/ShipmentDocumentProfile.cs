using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.ShipmentDocument;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.ShipmentDocument;

namespace Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.ShipmentDocument
{
    public class ShipmentDocumentProfile : Profile
    {
        public ShipmentDocumentProfile()
        {
            CreateMap<ShipmentDocumentModel, ShipmentDocumentViewModel>()
                .ForPath(dest => dest.Buyer.Address, opt => opt.MapFrom(src => src.BuyerAddress))
                .ForPath(dest => dest.Buyer.City, opt => opt.MapFrom(src => src.BuyerCity))
                .ForPath(dest => dest.Buyer.Code, opt => opt.MapFrom(src => src.BuyerCode))
                .ForPath(dest => dest.Buyer.Contact, opt => opt.MapFrom(src => src.BuyerContact))
                .ForPath(dest => dest.Buyer.Country, opt => opt.MapFrom(src => src.BuyerCountry))
                .ForPath(dest => dest.Buyer.Id, opt => opt.MapFrom(src => src.BuyerId))
                .ForPath(dest => dest.Buyer.Name, opt => opt.MapFrom(src => src.BuyerName))
                .ForPath(dest => dest.Buyer.NPWP, opt => opt.MapFrom(src => src.BuyerNPWP))
                .ForPath(dest => dest.Buyer.Tempo, opt => opt.MapFrom(src => src.BuyerTempo))
                .ForPath(dest => dest.Buyer.Type, opt => opt.MapFrom(src => src.BuyerType))
                .ForPath(dest => dest.Details, opt => opt.MapFrom(src => src.Details))
                .ForPath(dest => dest.Storage.code, opt => opt.MapFrom(src => src.StorageCode))
                .ForPath(dest => dest.Storage.description, opt => opt.MapFrom(src => src.StorageDescription))
                .ForPath(dest => dest.Storage.name, opt => opt.MapFrom(src => src.StorageName))
                .ForPath(dest => dest.Storage.unit.code, opt => opt.MapFrom(src => src.StorageUnitCode))
                .ForPath(dest => dest.Storage.unit.name, opt => opt.MapFrom(src => src.StorageUnitName))
                .ForPath(dest => dest.Storage._id, opt => opt.MapFrom(src => src.StorageId))
                .ReverseMap();

            CreateMap<ShipmentDocumentDetailModel, ShipmentDocumentDetailViewModel>()
                .ForPath(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
                .ForPath(dest => dest.ProductionOrder.OrderNo, opt => opt.MapFrom(src => src.ProductionOrderNo))
                .ForPath(dest => dest.ProductionOrder.Id, opt => opt.MapFrom(src => src.ProductionOrderId))
                .ForPath(dest => dest.ProductionOrder.OrderType.Name, opt => opt.MapFrom(src => src.ProductionOrderType))
                .ForPath(dest => dest.ProductionOrder.DesignCode, opt => opt.MapFrom(src => src.ProductionOrderDesignCode))
                .ForPath(dest => dest.ProductionOrder.DesignNumber, opt => opt.MapFrom(src => src.ProductionOrderDesignNumber))
                .ReverseMap();

            CreateMap<ShipmentDocumentItemModel, ShipmentDocumentItemViewModel>()
                .ForPath(dest => dest.PackingReceiptItems, opt => opt.MapFrom(src => src.PackingReceiptItems))
                .ReverseMap();

            CreateMap<ShipmentDocumentPackingReceiptItemModel, ShipmentDocumentPackingReceiptItemViewModel>()
                .ReverseMap();
        }
    }
}
