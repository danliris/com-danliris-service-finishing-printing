using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.EventOrganizer;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.EventOrganizer;
using Com.Danliris.Service.Production.Lib.Utilities.BaseInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.Master
{
    public interface IEventOrganizerFacade : IBaseFacade<EventOrganizer>
    {
        Task<EventOrganizer> ReadByGroupArea(string area, string group);
    };

}