using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.DailyMonitoringEvent;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DailyMonitoringEvent;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.DailyMonitoringEvent
{
    public class DailyMonitoringEventProfile : Profile
    {
        public DailyMonitoringEventProfile()
        {
            CreateMap<DailyMonitoringEventLossEventItemModel, DailyMonitoringEventLossEventItemViewModel>()
               .ForPath(p => p.LossEventRemark.Id, opt => opt.MapFrom(m => m.LossEventRemarkId))
               .ForPath(p => p.LossEventRemark.Code, opt => opt.MapFrom(m => m.LossEventRemarkCode))
               .ForPath(p => p.LossEventRemark.Remark, opt => opt.MapFrom(m => m.LossEventRemark))
               .ForPath(p => p.LossEventRemark.ProductionLossCode, opt => opt.MapFrom(m => m.LossEventProductionLossCode))
               .ForPath(p => p.LossEventRemark.LossEventCategory.LossesCategory, opt => opt.MapFrom(m => m.LossEventLossesCategory))
               .ForPath(p => p.LossEventRemark.LossEventCategory.LossEvent.Losses, opt => opt.MapFrom(m => m.LossEventLosses))
               .ReverseMap();

            CreateMap<DailyMonitoringEventProductionOrderItemModel, DailyMonitoringEventProductionOrderItemViewModel>()
                .ForPath(p => p.ProductionOrder.Id, opt => opt.MapFrom(m => m.ProductionOrderId))
                .ForPath(p => p.ProductionOrder.Code, opt => opt.MapFrom(m => m.ProductionOrderCode))
                .ForPath(p => p.ProductionOrder.OrderNo, opt => opt.MapFrom(m => m.ProductionOrderNo))
                .ReverseMap();

            CreateMap<DailyMonitoringEventModel, DailyMonitoringEventViewModel>()
                .ForPath(p => p.ProcessType.Id, opt => opt.MapFrom(m => m.ProcessTypeId))
                .ForPath(p => p.ProcessType.Code, opt => opt.MapFrom(m => m.ProcessTypeCode))
                .ForPath(p => p.ProcessType.Name, opt => opt.MapFrom(m => m.ProcessTypeName))
                .ForPath(p => p.Machine.Id, opt => opt.MapFrom(m => m.MachineId))
                .ForPath(p => p.Machine.Code, opt => opt.MapFrom(m => m.MachineCode))
                .ForPath(p => p.Machine.Name, opt => opt.MapFrom(m => m.MachineName))
                .ForPath(p => p.ProcessType.OrderType.Id, opt => opt.MapFrom(m => m.OrderTypeId))
                .ForPath(p => p.ProcessType.OrderType.Code, opt => opt.MapFrom(m => m.OrderTypeCode))
                .ForPath(p => p.ProcessType.OrderType.Name, opt => opt.MapFrom(m => m.OrderTypeName))
                .ReverseMap();

        }
    }
}
