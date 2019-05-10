using Com.Danliris.Service.Production.Lib.Models.Master.Instruction;
using Com.Danliris.Service.Production.Lib.Utilities;
using Com.Danliris.Service.Production.Lib.Utilities.BaseInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Production.Lib.BusinessLogic.Interfaces.Master
{
    public interface IInstructionFacade : IBaseFacade<InstructionModel>
    {
        ReadResponse<InstructionModel> ReadVM(int page, int size, string order, List<string> select, string keyword, string filter);
    }
}
