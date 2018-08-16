using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Monitoring_Event;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Monitoring_Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.MonitoringEvent
{
    public class MonitoringEventProfile : Profile
    {
        public MonitoringEventProfile()
        {
            CreateMap<MonitoringEventModel, MonitoringEventViewModel>()
            .ForPath(d => d.Machine.Id, opt => opt.MapFrom(s => s.MachineId))
            .ForPath(d => d.Machine.Name, opt => opt.MapFrom(s => s.MachineName))

            .ForPath(d => d.ProductionOrder.Id, opt => opt.MapFrom(s => s.ProductionOrderId))
            .ForPath(d => d.ProductionOrder.OrderNo, opt => opt.MapFrom(s => s.ProductionOrderOrderNo))

            .ForPath(d => d.MachineEvent.Id, opt => opt.MapFrom(s => s.MachineEventId))
            .ForPath(d => d.MachineEvent.Code, opt => opt.MapFrom(s => s.MachineEventCode))
            .ForPath(d => d.MachineEvent.Name, opt => opt.MapFrom(s => s.MachineEventName))
            .ForPath(d => d.MachineEvent.Category, opt => opt.MapFrom(s => s.MachineEventCategory))

            .ForPath(d => d.ProductionOrderDetail.Code, opt => opt.MapFrom(s => s.ProductionOrderDetailCode))
            .ForPath(d => d.ProductionOrderDetail.ColorRequest, opt => opt.MapFrom(s => s.ProductionOrderDetailColorRequest))
            .ForPath(d => d.ProductionOrderDetail.ColorTemplate, opt => opt.MapFrom(s => s.ProductionOrderDetailColorTemplate))
            .ForPath(d => d.ProductionOrderDetail.ColorType.Id, opt => opt.MapFrom(s => s.ProductionOrderDetailColorTypeId))
            .ForPath(d => d.ProductionOrderDetail.ColorType.Name, opt => opt.MapFrom(s => s.ProductionOrderDetailColorType))
            .ForPath(d => d.ProductionOrderDetail.Quantity, opt => opt.MapFrom(s => s.ProductionOrderDetailQuantity))

            .ForPath(d => d.MachineEvent.Id, opt => opt.MapFrom(s => s.MachineEventId))
            .ForPath(d => d.MachineEvent.Code, opt => opt.MapFrom(s => s.MachineEventCode))
            .ForPath(d => d.MachineEvent.Name, opt => opt.MapFrom(s => s.MachineEventName))
            .ForPath(d => d.MachineEvent.Category, opt => opt.MapFrom(s => s.MachineEventCategory))
            .ReverseMap();


        //            public Master.Machine.MachineViewModel Machine { get; set; }
        //public ProductionOrderIntegrationViewModel ProductionOrder { get; set; }
        //public ProductionOrderDetailIntegrationViewModel ProductionOrderDetail { get; set; }
        //public MachineEventViewModel MachineEvent { get; set; }
    }
    }
}
