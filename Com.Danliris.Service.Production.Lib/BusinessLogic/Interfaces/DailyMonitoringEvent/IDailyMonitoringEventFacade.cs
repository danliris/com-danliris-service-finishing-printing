using Com.Danliris.Service.Finishing.Printing.Lib.Models.DailyMonitoringEvent;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DailyMonitoringEvent;
using Com.Danliris.Service.Production.Lib.Utilities.BaseInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.DailyMonitoringEvent
{
    public interface IDailyMonitoringEventFacade : IBaseFacade<DailyMonitoringEventModel>
    {
        List<DailyMonitoringEventReportViewModel> GetReport(DateTime? dateFrom, DateTime? dateTo, string area, int machineId, int offSet);

        MemoryStream GenerateExcel(DateTime? dateFrom, DateTime? dateTo, string area, int machineId, int offSet);
    }
}
