using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.PackingReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.PackingReceipt;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.PackingReceipt
{
    public class PackingReceiptProfile : Profile
    {
        public PackingReceiptProfile()
        {
            CreateMap<PackingReceiptModel, PackingReceiptViewModel>()
            .ForPath(d => d.Storage.Id, opt => opt.MapFrom(s => s.StorageId))
            .ForPath(d => d.Storage.Name, opt => opt.MapFrom(s => s.StorageName))
            .ForPath(d => d.Storage.Code, opt => opt.MapFrom(s => s.StorageCode))
            .ForPath(d => d.Storage.Unit.Name, opt => opt.MapFrom(s => s.StorageUnitName))
            .ForPath(d => d.Storage.Unit.Code, opt => opt.MapFrom(s => s.StorageUnitCode))
            .ForPath(d => d.Storage.Unit.Division.Name, opt => opt.MapFrom(s => s.StorageDivisionName))
            .ForPath(d => d.Storage.Unit.Division.Code, opt => opt.MapFrom(s => s.StorageDivisionCode))
            .ReverseMap();

            CreateMap<PackingReceiptItem, PackingReceiptItemViewModel>().ReverseMap();
        }
    }
}
