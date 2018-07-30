using Com.Danliris.Service.Production.Lib.Models.Master.Step;
using Com.Danliris.Service.Production.Lib.Utilities;
using Com.Danliris.Service.Production.Lib.Utilities.BaseInterface;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Production.Lib.BusinessLogic.Interfaces.Master
{
    public interface IStepFacade : IBaseFacade<StepModel>
    {
        ReadResponse<StepModel> ReadVM(int page, int size, string order, List<string> select, string keyword, string filter);
    }
}
