using Com.Danliris.Service.Finishing.Printing.Lib.Models.CostCalculation;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.CostCalculation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.CostCalculation
{
    public interface ICostCalculationService
    {
        Task<CostCalculationPagedListWrapper> GetPaged(int page, int size, string order, string keyword, string filter);
        Task<CostCalculationViewModel> GetSingleById(int id);
        Task<int> InsertSingle(CostCalculationModel createModel);
        Task<int> UpdateSingle(int id, CostCalculationViewModel updateViewModel);
        Task<int> DeleteSingle(int id);
        Task<bool> IsDataExistsById(int id);
    }
}
