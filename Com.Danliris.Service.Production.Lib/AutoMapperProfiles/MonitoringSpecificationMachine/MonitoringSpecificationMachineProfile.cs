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
            CreateMap<MonitoringSpecificationMachineModel, MonitoringSpecificationMachineViewModel>().ReverseMap();
            CreateMap<MonitoringSpecificationMachineDetailsModel, MonitoringSpecificationMachineDetailsViewModel>().ReverseMap();
        }
    }
}