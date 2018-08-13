using AutoMapper;
using Com.Danliris.Service.Production.Lib.Models.Master.Step;
using Com.Danliris.Service.Production.Lib.ViewModels.Master.Step;

namespace Com.Danliris.Service.Production.Lib.AutoMapperProfiles.Master
{
    public class StepProfile : Profile
    {
        public StepProfile()
        {
            CreateMap<StepIndicatorModel, StepIndicatorViewModel>().ReverseMap();
            CreateMap<StepModel, StepViewModel>().ReverseMap();
        }
    }
}
