using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Packing;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Packing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.Packing
{
    public class PackingProfile : Profile
    {
        public PackingProfile()
        {
            CreateMap<PackingModel, PackingViewModel>().ReverseMap();
            CreateMap<PackingDetailModel, PackingDetailViewModel>().ReverseMap();
        }
    }
}
