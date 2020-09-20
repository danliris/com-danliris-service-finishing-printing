using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.LossEvent;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.LossEvent;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.Master
{
    public class LossEventProfile : Profile
    {
        public LossEventProfile()
        {
            CreateMap<LossEventModel, LossEventViewModel>()
                .ForPath(p => p.ProcessType.Id, opt => opt.MapFrom(m => m.ProcessTypeId))
                .ForPath(p => p.ProcessType.Code, opt => opt.MapFrom(m => m.ProcessTypeCode))
                .ForPath(p => p.ProcessType.Name, opt => opt.MapFrom(m => m.ProcessTypeName))
                .ForPath(p => p.ProcessType.OrderType.Id, opt => opt.MapFrom(m => m.OrderTypeId))
                .ForPath(p => p.ProcessType.OrderType.Code, opt => opt.MapFrom(m => m.OrderTypeCode))
                .ForPath(p => p.ProcessType.OrderType.Name, opt => opt.MapFrom(m => m.OrderTypeName))
                .ReverseMap();
        }
    }
}
