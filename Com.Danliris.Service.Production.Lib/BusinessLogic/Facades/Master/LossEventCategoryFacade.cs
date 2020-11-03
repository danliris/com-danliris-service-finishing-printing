using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.LossEventCategory;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.LossEventCategory;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Newtonsoft.Json;
using Com.Moonlay.NetCore.Lib;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master
{
    public class LossEventCategoryFacade : ILossEventCategoryFacade
    {
        private readonly ProductionDbContext DbContext;
        private readonly DbSet<LossEventCategoryModel> DbSet;
        private readonly LossEventCategoryLogic LossEventCategoryLogic;

        public LossEventCategoryFacade(IServiceProvider serviceProvider, ProductionDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<LossEventCategoryModel>();
            LossEventCategoryLogic = serviceProvider.GetService<LossEventCategoryLogic>();
        }

        public Task<int> CreateAsync(LossEventCategoryModel model)
        {
            do
            {
                model.Code = CodeGenerator.Generate();
            }
            while (DbSet.Any(d => d.Code.Equals(model.Code)));
            LossEventCategoryLogic.CreateModel(model);
            return DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await LossEventCategoryLogic.DeleteModel(id);
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<LossEventCategoryModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<LossEventCategoryModel> query = DbSet;

            List<string> searchAttributes = new List<string>()
            {
                "LossEventProcessTypeName", "LossEventLosses", "LossesCategory", "LossEventProcessArea"
            };
            query = QueryHelper<LossEventCategoryModel>.Search(query, searchAttributes, keyword);

            Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<LossEventCategoryModel>.Filter(query, filterDictionary);

            Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<LossEventCategoryModel>.Order(query, orderDictionary);

            Pageable<LossEventCategoryModel> pageable = new Pageable<LossEventCategoryModel>(query, page - 1, size);
            List<LossEventCategoryModel> data = pageable.Data.ToList();
            int totalData = pageable.TotalCount;

            return new ReadResponse<LossEventCategoryModel>(data, totalData, orderDictionary, new List<string>());
        }

        public Task<LossEventCategoryModel> ReadByIdAsync(int id)
        {
            return LossEventCategoryLogic.ReadModelById(id);
        }

        public async Task<int> UpdateAsync(int id, LossEventCategoryModel model)
        {
            await LossEventCategoryLogic.UpdateModelAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }
    }
}
