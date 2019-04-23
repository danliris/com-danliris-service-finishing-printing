using Com.Danliris.Service.Production.Lib.Utilities.BaseInterface;
using Com.Moonlay.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Test.Utils
{
    public abstract class BaseDataUtil<TFacade, TModel>
        where TFacade : IBaseFacade<TModel>
        where TModel : StandardEntity, IValidatableObject
    {
        private TFacade _facade;
        public BaseDataUtil(TFacade facade)
        {
            _facade = facade;
        }

        public virtual TModel GetNewData()
        {
            return Activator.CreateInstance(typeof(TModel)) as TModel;
        }

        public virtual Task<TModel> GetNewDataAsync()
        {
            throw new NotImplementedException();
        }


        public async Task<TModel> GetTestData()
        {
            TModel data;

            try
            {
                data = await GetNewDataAsync();
            }
            catch (Exception)
            {
                data = GetNewData();
            }

            await _facade.CreateAsync(data);
            return data;
        }
    }
}
