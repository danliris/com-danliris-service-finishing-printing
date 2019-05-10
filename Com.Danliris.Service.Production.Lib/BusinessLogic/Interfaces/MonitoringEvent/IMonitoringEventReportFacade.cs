using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.Machine;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Monitoring_Event;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Monitoring_Specification_Machine;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Monitoring_Event;
using Com.Danliris.Service.Production.Lib.Utilities;
using Com.Danliris.Service.Production.Lib.Utilities.BaseInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.MonitoringEvent
{
    public interface IMonitoringEventReportFacade : IBaseFacade<MonitoringEventModel>
    {
        IQueryable<MonitoringEventReportViewModel> GetReportQuery(string machineId, string machineEventId, string productionOrderOrderNo, DateTime? dateFrom, DateTime? dateTo, int offset);
        Tuple<List<MonitoringEventReportViewModel>, int> GetReport(string machineId, string machineEventId, string productionOrderOrderNo, DateTime? dateFrom, DateTime? dateTo, int page, int size, string Order, int offset);
        MemoryStream GenerateExcel(string machineId, string machineEventId, string productionOrderOrderNo, DateTime? dateFrom, DateTime? dateTo, int offset);
        List<MachineEventsModel> ReadByMachine(string Keyword, int machineId);
        MonitoringSpecificationMachineModel ReadMonitoringSpecMachine(int id, string productionOrderNumber, DateTime dateTimeInput);
    }
}