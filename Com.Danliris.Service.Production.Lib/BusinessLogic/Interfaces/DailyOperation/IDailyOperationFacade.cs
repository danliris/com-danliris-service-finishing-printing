using Com.Danliris.Service.Finishing.Printing.Lib.Models.Daily_Operation;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Daily_Operation;
using Com.Danliris.Service.Production.Lib.Utilities;
using Com.Danliris.Service.Production.Lib.Utilities.BaseInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.DailyOperation
{
    public interface IDailyOperationFacade : IBaseFacade<DailyOperationModel>
    {
        ReadResponse<DailyOperationViewModel> GetReport(int page, int size, int kanbanID, int machineID, DateTimeOffset? dateFrom, DateTimeOffset? dateTo, int offSet);
    }
}
