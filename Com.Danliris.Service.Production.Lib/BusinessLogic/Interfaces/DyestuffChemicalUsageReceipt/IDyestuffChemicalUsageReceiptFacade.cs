using Com.Danliris.Service.Finishing.Printing.Lib.Models.DyestuffChemicalUsageReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DyestuffChemicalUsageReceipt;
using Com.Danliris.Service.Production.Lib.Utilities.BaseInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.DyestuffChemicalUsageReceipt
{
    public interface IDyestuffChemicalUsageReceiptFacade : IBaseFacade<DyestuffChemicalUsageReceiptModel>
    {
        Task<Tuple<DyestuffChemicalUsageReceiptModel, string>> GetDataByStrikeOff(int strikeOffId);
        IQueryable<DyestuffChemicalUsageReceiptReportViewModel> GetReportQuery(string productionOrderNo, string strikeOffCode, DateTime? dateFrom, DateTime? dateTo, int offset);
        Tuple<List<DyestuffChemicalUsageReceiptReportViewModel>, int> GetReport(string productionOrderNo, string strikeOffCode, DateTime? dateFrom, DateTime? dateTo, int page, int size, string Order, int offset);
        MemoryStream GenerateExcel(string productionOrderNo, string strikeOffCode, DateTime? dateFrom, DateTime? dateTo, int offset);
    }
}
