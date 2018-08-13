using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.MachineType;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.MachineType;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.Master
{
    public class MachineTypeProfile : Profile
    {
        public MachineTypeProfile()
        {
            CreateMap<MachineTypeModel, MachineTypeViewModel>().ReverseMap();
            CreateMap<MachineTypeIndicatorsModel, MachineTypeIndicatorsViewModel>().ReverseMap();
        }

    }
}
