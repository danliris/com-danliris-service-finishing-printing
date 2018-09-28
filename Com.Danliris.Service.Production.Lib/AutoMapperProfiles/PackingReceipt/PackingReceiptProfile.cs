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
            .ForPath(d => d.Storage._id, opt => opt.MapFrom(s => s.StorageId))
            .ForPath(d => d.Storage.name, opt => opt.MapFrom(s => s.StorageName))
            .ForPath(d => d.Storage.code, opt => opt.MapFrom(s => s.StorageCode))
            .ForPath(d => d.Storage.unit.name, opt => opt.MapFrom(s => s.StorageUnitName))
            .ForPath(d => d.Storage.unit.code, opt => opt.MapFrom(s => s.StorageUnitCode))
            .ForPath(d => d.Storage.unit.division.name, opt => opt.MapFrom(s => s.StorageDivisionName))
            .ForPath(d => d.Storage.unit.division.code, opt => opt.MapFrom(s => s.StorageDivisionCode))
            .ReverseMap();

            CreateMap<PackingReceiptItem, PackingReceiptItemViewModel>().ReverseMap();
        }
    }
}
