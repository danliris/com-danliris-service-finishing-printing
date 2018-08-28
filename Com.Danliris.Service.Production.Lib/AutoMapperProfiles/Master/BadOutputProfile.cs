using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.BadOutput;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.BadOutput;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.Master
{
    public class BadOutputProfile : Profile
    {
        public BadOutputProfile()
        {
            CreateMap<BadOutputModel, BadOutputViewModel>().ReverseMap();
            CreateMap<BadOutputMachineModel, BadOutputMachineViewModel>()
                .ForPath(d => d.Name, opt => opt.MapFrom(s => s.MachineName))
                .ForPath(d => d.Code, opt => opt.MapFrom(s => s.MachineCode))
                .ReverseMap();
        }
    }
}
