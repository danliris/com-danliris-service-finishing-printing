using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.EventOrganizer;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.EventOrganizer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.Master
{
     public  class EventOrganizerProfil : Profile
    {
        public EventOrganizerProfil()
        {
            CreateMap<EventOrganizer, EventOrganizerViewModel>().ReverseMap();
            
        }
    }
}
