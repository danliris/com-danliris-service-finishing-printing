using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.FabricQualityControl;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.FabricQualityControl;

namespace Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.FabricQualityControl
{
    public class FabricQualityControlProfile : Profile
    {
        public FabricQualityControlProfile()
        {
            CreateMap<FabricQualityControlModel, FabricQualityControlViewModel>().ReverseMap();
            CreateMap<FabricGradeTestModel, FabricGradeTestViewModel>().ReverseMap();
            CreateMap<CriteriaModel, CriteriaViewModel>()
                .ForPath(p => p.Score.A, opt => opt.MapFrom(m => m.ScoreA))
                .ForPath(p => p.Score.B, opt => opt.MapFrom(m => m.ScoreB))
                .ForPath(p => p.Score.B, opt => opt.MapFrom(m => m.ScoreC))
                .ForPath(p => p.Score.D, opt => opt.MapFrom(m => m.ScoreD))
                .ReverseMap();
        }
    }
}
