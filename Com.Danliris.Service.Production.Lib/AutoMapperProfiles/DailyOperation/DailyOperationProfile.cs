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
            .ForPath(p => p.Step.StepId, opt => opt.MapFrom(m => m.StepId))
            .ForPath(p => p.Step.Process, opt => opt.MapFrom(m => m.StepProcess))
            .ForPath(p => p.Kanban.Id, opt => opt.MapFrom(m => m.KanbanId))
            .ForPath(p => p.Kanban.Code, opt => opt.MapFrom(m => m.KanbanCode))
            .ForPath(p => p.Kanban.ProductionOrder.OrderNo, opt => opt.MapFrom(m => m.Kanban.ProductionOrderOrderNo))
            .ForPath(p => p.Kanban.Cart.CartNumber, opt => opt.MapFrom(m => m.Kanban.CartCartNumber))
            .ForPath(p => p.Machine.Id, opt => opt.MapFrom(m => m.MachineId))
            .ForPath(p => p.Machine.Code, opt => opt.MapFrom(m => m.MachineCode))
            .ForPath(p => p.Machine, opt => opt.MapFrom(m => m.Machine))
            .ForPath(p => p.Kanban, opt => opt.MapFrom(m => m.Kanban))
            .ReverseMap();

            CreateMap<DailyOperationBadOutputReasonsModel, DailyOperationBadOutputReasonsViewModel>()
            .ForPath(d => d.Machine.Id, opt => opt.MapFrom(m => m.MachineId))
            .ForPath(d => d.Machine.Code, opt => opt.MapFrom(m => m.MachineCode))
            .ForPath(d => d.Machine.Name, opt => opt.MapFrom(m => m.MachineName))
            .ForPath(d => d.BadOutput.Id, opt => opt.MapFrom(m => m.BadOutputId))
            .ForPath(d => d.BadOutput.Code, opt => opt.MapFrom(m => m.BadOutputCode))
            .ForPath(d => d.BadOutput.Reason, opt => opt.MapFrom(m => m.BadOutputReason))
            .ReverseMap();
        }
    }
}
