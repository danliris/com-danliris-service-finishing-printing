using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.ColorReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.ColorReceipt;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.ColorReceipt
{
    public class ColorReceiptProfile : Profile
    {
        public ColorReceiptProfile()
        {
            CreateMap<ColorReceiptModel, ColorReceiptViewModel>()
                .ForPath(dest => dest.Technician.Id, opt => opt.MapFrom(src => src.TechnicianId))
                .ForPath(dest => dest.Technician.Name, opt => opt.MapFrom(src => src.TechnicianName))
                .ReverseMap();
            CreateMap<ColorReceiptItemModel, ColorReceiptItemViewModel>()
                .ForPath(dest => dest.Product.Id, opt => opt.MapFrom(src => src.ProductId))
                .ForPath(dest => dest.Product.Code, opt => opt.MapFrom(src => src.ProductCode))
                .ForPath(dest => dest.Product.Name, opt => opt.MapFrom(src => src.ProductName))
                .ReverseMap();
        }
    }
}
