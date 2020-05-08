using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.ColorReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.StrikeOff;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.ColorReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.StrikeOff;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.StrikeOff
{
    public class StrikeOffProfile : Profile
    {
        public StrikeOffProfile()
        {
            CreateMap<StrikeOffModel, StrikeOffViewModel>()
                .ReverseMap();

            CreateMap<StrikeOffItemModel, StrikeOffItemViewModel>()
                .ForPath(dest => dest.ColorReceipt.Id, opt => opt.MapFrom(src => src.ColorReceiptId))
                .ForPath(dest => dest.ColorReceipt.ColorCode, opt => opt.MapFrom(src => src.ColorReceiptColorCode))
                .ForPath(dest => dest.ColorReceipt.ColorReceiptItems, opt => opt.MapFrom(src => src.ColorReceiptItems))
                .ReverseMap();

            CreateMap<ColorReceiptItemModel, ColorReceiptItemViewModel>()
               .ForPath(dest => dest.Product.Id, opt => opt.MapFrom(src => src.ProductId))
               .ForPath(dest => dest.Product.Code, opt => opt.MapFrom(src => src.ProductCode))
               .ForPath(dest => dest.Product.Name, opt => opt.MapFrom(src => src.ProductName))
               .ReverseMap();
        }
    }
}
