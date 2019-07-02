using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.OperationalCost;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.OperationalCost;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.Master
{
    public class OperationalCostProfile : Profile
    {
        public OperationalCostProfile()
        {
            CreateMap<OperationalCostModel, OperationalCostViewModel>().ReverseMap();
        }
    }
}
