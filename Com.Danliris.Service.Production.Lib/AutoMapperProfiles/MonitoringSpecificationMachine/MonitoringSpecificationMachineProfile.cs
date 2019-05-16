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
                .ForPath(dest => dest.DateTimeInput, opt => opt.MapFrom(s => s.DateTimeInput))
                .ForPath(dest => dest.Machine.Id, opt => opt.MapFrom(s => s.MachineId))
                .ForPath(dest => dest.Machine.Name, opt => opt.MapFrom(s => s.MachineName))
                .ForPath(dest => dest.ProductionOrder.Id, opt => opt.MapFrom(s => s.ProductionOrderId))
                .ForPath(dest => dest.ProductionOrder.OrderNo, opt => opt.MapFrom(s => s.ProductionOrderNo))
                .ReverseMap();

            CreateMap<MonitoringSpecificationMachineDetailsModel, MonitoringSpecificationMachineDetailsViewModel>()
                .ForPath(dest => dest.MonitoringSpecificationMachineId, opt => opt.MapFrom(s => s.MonitoringSpecificationMachineId))
                .ReverseMap();
        }
    }
}
