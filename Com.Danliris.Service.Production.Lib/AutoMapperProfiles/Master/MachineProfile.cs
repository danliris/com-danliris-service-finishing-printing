using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.Machine;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.Machine;
using Com.Danliris.Service.Production.Lib.Models.Master.Step;
using Com.Danliris.Service.Production.Lib.ViewModels.Master.Step;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.Master
{
    public class MachineProfile : Profile
    {
        public MachineProfile()
        {
            CreateMap<MachineModel, MachineViewModel>()
            .ForPath(d => d.Unit.Id, opt => opt.MapFrom(s => s.UnitId))
            .ForPath(d => d.Unit.Name, opt => opt.MapFrom(s => s.UnitName))
            .ForPath(d => d.Unit.Code, opt => opt.MapFrom(s => s.UnitCode))

            .ForPath(d => d.MachineType.Id, opt => opt.MapFrom(s => s.MachineTypeId))
            .ForPath(d => d.MachineType.Name, opt => opt.MapFrom(s => s.MachineTypeName))
            .ForPath(d => d.MachineType.Code, opt => opt.MapFrom(s => s.MachineTypeCode))

            .ReverseMap();

            CreateMap<MachineEventsModel, MachineEventViewModel>().ReverseMap();
            CreateMap<StepModel, StepViewModel>().ReverseMap();
        }
    }
}
