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
                .ForPath(p => p.Kanban.ProductionOrder.Id, opt => opt.MapFrom(m => m.ProductionOrderId))
                .ForPath(p => p.Kanban.ProductionOrder.Code, opt => opt.MapFrom(m => m.ProductionOrderCode))
                .ForPath(p => p.Kanban.ProductionOrder.OrderNo, opt => opt.MapFrom(m => m.ProductionOrderNo))
                .ForPath(p => p.Kanban.Id, opt => opt.MapFrom(m => m.KanbanId))
                .ForPath(p => p.Kanban.Code, opt => opt.MapFrom(m => m.KanbanCode))
                .ForPath(p => p.Kanban.Cart.Code, opt => opt.MapFrom(m => m.KanbanCartCode))
                .ForPath(p => p.Kanban.Cart.CartNumber, opt => opt.MapFrom(m => m.KanbanCartNumber))
                .ReverseMap();

            CreateMap<DailyMonitoringEventModel, DailyMonitoringEventViewModel>()
                .ForPath(p => p.ProcessType.Id, opt => opt.MapFrom(m => m.ProcessTypeId))
                .ForPath(p => p.ProcessType.Code, opt => opt.MapFrom(m => m.ProcessTypeCode))
                .ForPath(p => p.ProcessType.Name, opt => opt.MapFrom(m => m.ProcessTypeName))
                .ForPath(p => p.Machine.Id, opt => opt.MapFrom(m => m.MachineId))
                .ForPath(p => p.Machine.Code, opt => opt.MapFrom(m => m.MachineCode))
                .ForPath(p => p.EventOrganizer.Id, opt => opt.MapFrom(m => m.EventOrganizerId))
                .ForPath(p=>p.EventOrganizer.ProcessArea,opt=>opt.MapFrom(m=>m.ProcessArea))
                .ForPath(p => p.EventOrganizer.Kasie, opt => opt.MapFrom(m => m.Kasie))
                .ForPath(p => p.EventOrganizer.Kasubsie, opt => opt.MapFrom(m => m.Kasubsie))
                 .ForPath(p => p.EventOrganizer.Group, opt => opt.MapFrom(m => m.Group))
                .ForPath(p => p.Machine.Name, opt => opt.MapFrom(m => m.MachineName))
                .ForPath(p => p.Machine.UseBQBS, opt => opt.MapFrom(m => m.MachineUseBQBS))
                .ForPath(p => p.ProcessType.OrderType.Id, opt => opt.MapFrom(m => m.OrderTypeId))
                .ForPath(p => p.ProcessType.OrderType.Code, opt => opt.MapFrom(m => m.OrderTypeCode))
                .ForPath(p => p.ProcessType.OrderType.Name, opt => opt.MapFrom(m => m.OrderTypeName))
                .ReverseMap();

        }
    }
}
