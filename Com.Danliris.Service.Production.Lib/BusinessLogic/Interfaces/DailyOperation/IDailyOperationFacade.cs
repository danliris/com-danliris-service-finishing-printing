using Com.Danliris.Service.Finishing.Printing.Lib.Models.Daily_Operation;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Daily_Operation;
using Com.Danliris.Service.Production.Lib.Utilities;
using Com.Danliris.Service.Production.Lib.Utilities.BaseInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.DailyOperation
{
    public interface IDailyOperationFacade : IBaseFacade<DailyOperationModel>
    {
        ReadResponse<DailyOperationViewModel> GetReport(int page, int size, int kanbanID, int machineID, DateTime? dateFrom, DateTime? dateTo, int offSet);

        List<DailyOperationViewModel> GetReport(int kanbanID, int machineID, DateTime? dateFrom, DateTime? dateTo, int offSet);

        MemoryStream GenerateExcel(int kanbanID, int machineID, DateTime? dateFrom, DateTime? dateTo, int offSet);

        Task<List<DailyOperationKanbanViewModel>> GetJoinKanban(string no);

        ReadResponse<DailyOperationModel> Read(int page, int size, string order, List<string> select, string keyword, string filter, string machine, string orderNo, string cartNo, string stepProcess, DateTime? startDate, DateTime? endDate);

        Task<bool> HasOutput(int kanbanId, string stepProcess);
    }
}
