using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.LossEvent;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.LossEvent;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using Com.Moonlay.NetCore.Lib;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master
{
    public class LossEventFacade : ILossEventFacade
    {
        private readonly ProductionDbContext DbContext;
        private readonly DbSet<LossEventModel> DbSet;
        private readonly LossEventLogic LossEventLogic;

        public LossEventFacade(IServiceProvider serviceProvider, ProductionDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<LossEventModel>();
            LossEventLogic = serviceProvider.GetService<LossEventLogic>();
        }

        public Task<int> CreateAsync(LossEventModel model)
        {
            do
            {
                model.Code = CodeGenerator.Generate();
            }
            while (DbSet.Any(d => d.Code.Equals(model.Code)));
            LossEventLogic.CreateModel(model);
            return DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await LossEventLogic.DeleteModel(id);
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<LossEventModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<LossEventModel> query = DbSet;

            List<string> searchAttributes = new List<string>()
            {
                "ProcessTypeName", "Losses"
            };
            query = QueryHelper<LossEventModel>.Search(query, searchAttributes, keyword);

            Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<LossEventModel>.Filter(query, filterDictionary);

            Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<LossEventModel>.Order(query, orderDictionary);

            Pageable<LossEventModel> pageable = new Pageable<LossEventModel>(query, page - 1, size);
            List<LossEventModel> data = pageable.Data.ToList();
            int totalData = pageable.TotalCount;

            return new ReadResponse<LossEventModel>(data, totalData, orderDictionary, new List<string>());
        }

        public Task<LossEventModel> ReadByIdAsync(int id)
        {
            return LossEventLogic.ReadModelById(id);
        }

        public async Task<int> UpdateAsync(int id, LossEventModel model)
        {
            await LossEventLogic.UpdateModelAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }
    }
}
