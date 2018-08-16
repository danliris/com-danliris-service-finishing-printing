using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Monitoring_Specification_Machine;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Monitoring_Specification_Machine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.MonitoringSpecificationMachine
{
    public class MonitoringSpecificationMachineProfile : Profile
    {
        public MonitoringSpecificationMachineProfile()
        {
            CreateMap<MonitoringSpecificationMachineModel, MonitoringSpecificationMachineViewModel>()
            .ForPath(d => d.Machine.Id, opt => opt.MapFrom(s => s.MachineId))
            .ForPath(d => d.Machine.Name, opt => opt.MapFrom(s => s.MachineName))
            .ForPath(d => d.ProductionOrder.Id, opt => opt.MapFrom(s => s.ProductionOrderId))
            .ForPath(d => d.ProductionOrder.OrderNo, opt => opt.MapFrom(s => s.ProductionOrderNo))
            .ReverseMap();
            CreateMap<MonitoringSpecificationMachineDetailsModel, MonitoringSpecificationMachineDetailsViewModel>().ReverseMap();
        }
    }
}