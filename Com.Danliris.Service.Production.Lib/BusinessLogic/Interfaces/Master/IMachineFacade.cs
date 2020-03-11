using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.Machine;
using Com.Danliris.Service.Production.Lib.Utilities;
using Com.Danliris.Service.Production.Lib.Utilities.BaseInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.Master
{
    public interface IMachineFacade : IBaseFacade<MachineModel>
    {
        ReadResponse<MachineModel> GetDyeingPrintingMachine(int page, int size, string order, List<string> select, string keyword, string filter);
    }
}
