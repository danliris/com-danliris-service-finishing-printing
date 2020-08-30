using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.LossEventCategory;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.LossEventCategory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.Master
{
    public class LossEventCategoryProfile : Profile
    {
        public LossEventCategoryProfile()
        {
            CreateMap<LossEventCategoryModel, LossEventCategoryViewModel>()
                .ForPath(p => p.LossEvent.Id, opt => opt.MapFrom(m => m.LossEventId))
                .ForPath(p => p.LossEvent.Code, opt => opt.MapFrom(m => m.LossEventCode))
                .ForPath(p => p.LossEvent.Losses, opt => opt.MapFrom(m => m.LossEventLosses))
                .ForPath(p => p.LossEvent.ProcessType.Id, opt => opt.MapFrom(m => m.LossEventProcessTypeId))
                .ForPath(p => p.LossEvent.ProcessType.Code, opt => opt.MapFrom(m => m.LossEventProcessTypeCode))
                .ForPath(p => p.LossEvent.ProcessType.Name, opt => opt.MapFrom(m => m.LossEventProcessTypeName))
                .ForPath(p => p.LossEvent.ProcessType.OrderType.Id, opt => opt.MapFrom(m => m.LossEventOrderTypeId))
                .ForPath(p => p.LossEvent.ProcessType.OrderType.Code, opt => opt.MapFrom(m => m.LossEventOrderTypeCode))
                .ForPath(p => p.LossEvent.ProcessType.OrderType.Name, opt => opt.MapFrom(m => m.LossEventOrderTypeName))
                .ReverseMap();
        }
    }
}
