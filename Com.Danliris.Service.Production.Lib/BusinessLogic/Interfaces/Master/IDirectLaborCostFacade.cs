using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.DirectLaborCost;
using Com.Danliris.Service.Production.Lib.Utilities.BaseInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.Master
{
    public interface IDirectLaborCostFacade : IBaseFacade<DirectLaborCostModel>
    {
        Task<DirectLaborCostModel> GetForCostCalculation(int month, int year);
    }
}
