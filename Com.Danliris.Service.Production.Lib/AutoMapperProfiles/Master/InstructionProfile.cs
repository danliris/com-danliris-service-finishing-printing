using AutoMapper;
using Com.Danliris.Service.Production.Lib.Models.Master.Instruction;
using Com.Danliris.Service.Production.Lib.ViewModels.Master.Instruction;
using Com.Danliris.Service.Production.Lib.ViewModels.Master.Step;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Production.Lib.AutoMapperProfiles.Master
{
    public class InstructionProfile : Profile
    {
        public InstructionProfile()
        {
            CreateMap<InstructionStepIndicatorModel, StepIndicatorViewModel>().ReverseMap();
            CreateMap<InstructionStepModel, StepViewModel>().ReverseMap();
            CreateMap<InstructionModel, InstructionViewModel>().ReverseMap();
        }
    }
}
