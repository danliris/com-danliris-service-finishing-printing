using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.DirectLaborCost;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.DirectLaborCost;

namespace Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.Master
{
    public class DirectLaborCostProfile : Profile
    {
        public DirectLaborCostProfile()
        {
            CreateMap<DirectLaborCostModel, DirectLaborCostViewModel>().ReverseMap();
        }
    }
}
