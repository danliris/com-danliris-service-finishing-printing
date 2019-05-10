using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Production.Lib.Utilities.BaseInterface
{
    public interface IBaseFacade<TModel>
    {
        ReadResponse<TModel> Read(int page, int size, string order, List<string> select, string keyword, string filter);
        Task<int> CreateAsync(TModel model);
        Task<TModel> ReadByIdAsync(int id);
        Task<int> UpdateAsync(int id, TModel model);
        Task<int> DeleteAsync(int id);
    }
}
