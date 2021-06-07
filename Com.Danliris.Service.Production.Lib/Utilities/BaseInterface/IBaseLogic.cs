using System.Threading.Tasks;

namespace Com.Danliris.Service.Production.Lib.Utilities.BaseInterface
{
    public interface IBaseLogic<TModel>
    {
        void CreateModel(TModel model);
        Task<TModel> ReadModelById(int id);
        Task UpdateModelAsync(int id, TModel model);
        Task DeleteModel(int id);
    }
}
