using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.DOSales;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DOSales;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.DOSales
{
    public class DOSalesProfile : Profile
    {
        public DOSalesProfile()
        {
            CreateMap<DOSalesModel, DOSalesViewModel>().ReverseMap();
            CreateMap<DOSalesDetailModel, DOSalesDetailViewModel>().ReverseMap();
        }
    }
}
