using AutoMapper;
using Com.Danliris.Service.Production.Lib.Models.Master.DurationEstimation;
using Com.Danliris.Service.Production.Lib.ViewModels.Master.DurationEstimation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Production.Lib.AutoMapperProfiles.Master
{
    public class DurationEstimationProfile : Profile
    {
        public DurationEstimationProfile()
        {
            CreateMap<DurationEstimationAreaModel, DurationEstimationAreaViewModel>()
                .ReverseMap();
            CreateMap<DurationEstimationModel, DurationEstimationViewModel>()
                //ProcessType
                .ForPath(p => p.ProcessType.Id, opt => opt.MapFrom(m => m.ProcessTypeId))
                .ForPath(p => p.ProcessType.Code, opt => opt.MapFrom(m => m.ProcessTypeCode))
                .ForPath(p => p.ProcessType.Name, opt => opt.MapFrom(m => m.ProcessTypeName))

                //OrderType
                .ForPath(p => p.ProcessType.OrderType.Id, opt => opt.MapFrom(m => m.OrderTypeId))
                .ForPath(p => p.ProcessType.OrderType.Code, opt => opt.MapFrom(m => m.OrderTypeCode))
                .ForPath(p => p.ProcessType.OrderType.Name, opt => opt.MapFrom(m => m.OrderTypeName))
                .ReverseMap();
        }
    }
}
