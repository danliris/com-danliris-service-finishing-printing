using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.ReturToQC;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.ReturToQC;

namespace Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.ReturToQC
{
    public class ReturToQCProfile : Profile
    {
        public ReturToQCProfile()
        {
            CreateMap<ReturToQCModel, ReturToQCViewModel>()
                .ForPath(dest => dest.Material.Id, opt => opt.MapFrom(src => src.MaterialId))
                .ForPath(dest => dest.Material.Code, opt => opt.MapFrom(src => src.MaterialCode))
                .ForPath(dest => dest.Material.Name, opt => opt.MapFrom(src => src.MaterialName))
                .ForPath(dest => dest.MaterialConstruction.Id, opt => opt.MapFrom(src => src.MaterialConstructionId))
                .ForPath(dest => dest.MaterialConstruction.Name, opt => opt.MapFrom(src => src.MaterialConstructionName))
                .ForPath(dest => dest.MaterialConstruction.Code, opt => opt.MapFrom(src => src.MaterialConstructionCode))
                .ForPath(dest => dest.Items, opt => opt.MapFrom(src => src.ReturToQCItems))
                .ReverseMap();

            CreateMap<ReturToQCItemModel, ReturToQCItemViewModel>()
                .ForPath(dest => dest.ProductionOrder.Id, opt => opt.MapFrom(src => src.ProductionOrderId))
                .ForPath(dest => dest.ProductionOrder.Code, opt => opt.MapFrom(src => src.ProductionOrderCode))
                .ForPath(dest => dest.ProductionOrder.OrderNo, opt => opt.MapFrom(src => src.ProductionOrderNo))
                .ForPath(dest => dest.Details, opt => opt.MapFrom(src => src.ReturToQCItemDetails))
                .ReverseMap();

            CreateMap<ReturToQCItemDetailModel, ReturToQCItemDetailViewModel>()
                .ReverseMap();
        }
    }
}
