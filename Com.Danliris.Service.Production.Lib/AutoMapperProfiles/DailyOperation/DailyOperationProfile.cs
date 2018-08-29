using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Daily_Operation;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Daily_Operation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.DailyOperation
{
    public class DailyOperationProfile : Profile
    {
        public DailyOperationProfile()
        {
            CreateMap<DailyOperationModel, DailyOperationViewModel>()
            .ForPath(p => p.Step.Id, opt => opt.MapFrom(m => m.StepId))
            .ForPath(p => p.Step.Process, opt => opt.MapFrom(m => m.StepProcess))
            .ForPath(p => p.Kanban.Id, opt => opt.MapFrom(m => m.KanbanId))
            .ForPath(p => p.Kanban.Code, opt => opt.MapFrom(m => m.KanbanCode))
            .ForPath(p => p.Machine.Id, opt => opt.MapFrom(m => m.MachineId))
            .ForPath(p => p.Machine.Code, opt => opt.MapFrom(m => m.MachineCode))
            .ReverseMap();

            CreateMap<DailyOperationBadOutputReasonsModel, DailyOperationBadOutputReasonsViewModel>().ReverseMap();
        }
    }
}
