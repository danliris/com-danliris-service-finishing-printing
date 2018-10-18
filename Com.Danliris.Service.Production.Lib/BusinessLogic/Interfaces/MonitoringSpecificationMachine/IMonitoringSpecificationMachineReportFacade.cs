using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.Machine;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Monitoring_Specification_Machine;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Monitoring_Specification_Machine;
using Com.Danliris.Service.Production.Lib.Utilities;
using Com.Danliris.Service.Production.Lib.Utilities.BaseInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.MonitoringSpecificationMachine
{
    public interface IMonitoringSpecificationMachineReportFacade : IBaseFacade<MonitoringSpecificationMachineModel>
    {
        IQueryable<MonitoringSpecificationMachineReportViewModel> GetReportQuery(int machineId, string productionOrderNo, DateTime? dateFrom, DateTime? dateTo, int offset);
        Tuple<List<MonitoringSpecificationMachineReportViewModel>, int> GetReport(int machineId, string productionOrderNo, DateTime? dateFrom, DateTime? dateTo, int page, int size, string Order, int offset);
        MemoryStream GenerateExcel(int machineId, string productionOrderNo, DateTime? dateFrom, DateTime? dateTo, int offset);
    }
}