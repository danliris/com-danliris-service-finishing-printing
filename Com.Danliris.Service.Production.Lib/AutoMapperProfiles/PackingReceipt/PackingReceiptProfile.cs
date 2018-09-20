using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.PackingReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.PackingReceipt;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.PackingReceipt
{
    public class PackingReceiptProfile : Profile
    {
        public PackingReceiptProfile()
        {
            CreateMap<PackingReceiptModel, PackingReceiptViewModel>().ReverseMap();
        }
    }
}
