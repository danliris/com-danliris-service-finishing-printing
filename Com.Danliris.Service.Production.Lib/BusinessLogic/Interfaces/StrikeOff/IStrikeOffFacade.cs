using Com.Danliris.Service.Finishing.Printing.Lib.Models.StrikeOff;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.StrikeOff;
using Com.Danliris.Service.Production.Lib.Utilities;
using Com.Danliris.Service.Production.Lib.Utilities.BaseInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.StrikeOff
{
    public interface IStrikeOffFacade : IBaseFacade<StrikeOffModel>
    {
        ReadResponse<StrikeOffConsumptionViewModel> ReadForUsageReceipt(int page, int size, string order, List<string> select, string keyword, string filter);
    }
}
