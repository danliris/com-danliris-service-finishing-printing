using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.OrderStatusReports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.OrderStatusReport
{
    public interface IOrderStatusReportService
    {
        Task<List<YearlyOrderStatusReportViewModel>> GetYearlyOrderStatusReport(int year, int orderTypeId);
        Task<List<MonthlyOrderStatusReportViewModel>> GetMonthlyOrderStatusReport(int year, int month, int orderTypeId);
        Task<List<ProductionOrderStatusReportViewModel>> GetProductionOrderStatusReport(int productionOrderId);
    }
}
