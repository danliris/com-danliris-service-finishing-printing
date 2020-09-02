using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.LossEventRemark;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.LossEventRemark;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.Master
{
    public class LossEventRemarkProfile : Profile
    {
        public LossEventRemarkProfile()
        {
            CreateMap<LossEventRemarkModel, LossEventRemarkViewModel>()
               .ForPath(p => p.LossEventCategory.LossEvent.ProcessArea, opt => opt.MapFrom(m => m.LossEventProcessArea))
               .ForPath(p => p.LossEventCategory.Id, opt => opt.MapFrom(m => m.LossEventCategoryId))
               .ForPath(p => p.LossEventCategory.Code, opt => opt.MapFrom(m => m.LossEventCategoryCode))
               .ForPath(p => p.LossEventCategory.LossesCategory, opt => opt.MapFrom(m => m.LossEventCategoryLossesCategory))
               .ForPath(p => p.LossEventCategory.LossEvent.Id, opt => opt.MapFrom(m => m.LossEventId))
               .ForPath(p => p.LossEventCategory.LossEvent.Code, opt => opt.MapFrom(m => m.LossEventCode))
               .ForPath(p => p.LossEventCategory.LossEvent.Losses, opt => opt.MapFrom(m => m.LossEventLosses))
               .ForPath(p => p.LossEventCategory.LossEvent.ProcessType.Id, opt => opt.MapFrom(m => m.LossEventProcessTypeId))
               .ForPath(p => p.LossEventCategory.LossEvent.ProcessType.Code, opt => opt.MapFrom(m => m.LossEventProcessTypeCode))
               .ForPath(p => p.LossEventCategory.LossEvent.ProcessType.Name, opt => opt.MapFrom(m => m.LossEventProcessTypeName))
               .ForPath(p => p.LossEventCategory.LossEvent.ProcessType.OrderType.Id, opt => opt.MapFrom(m => m.LossEventOrderTypeId))
               .ForPath(p => p.LossEventCategory.LossEvent.ProcessType.OrderType.Code, opt => opt.MapFrom(m => m.LossEventOrderTypeCode))
               .ForPath(p => p.LossEventCategory.LossEvent.ProcessType.OrderType.Name, opt => opt.MapFrom(m => m.LossEventOrderTypeName))
               .ReverseMap();
        }
    }
}
