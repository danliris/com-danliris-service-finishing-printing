using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.DailyOperation;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.DailyOperation;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Daily_Operation;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using Com.Moonlay.NetCore.Lib;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.DailyOperation
{
    public class DailyOperationFacade : IDailyOperationFacade
    {
        private readonly ProductionDbContext DbContext;
        private readonly DbSet<DailyOperationModel> DbSet;
        private readonly DailyOperationLogic DailyOperationLogic;
        public DailyOperationFacade(IServiceProvider serviceProvider, ProductionDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = DbContext.Set<DailyOperationModel>();
            this.DailyOperationLogic = serviceProvider.GetService<DailyOperationLogic>();
        }

        public async Task<int> CreateAsync(DailyOperationModel model)
        {
            do
            {
                model.Code = CodeGenerator.Generate();
            }
            while (DbSet.Any(d => d.Code.Equals(model.Code)));

            this.DailyOperationLogic.CreateModel(model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await DailyOperationLogic.DeleteModel(id);
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<DailyOperationModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<DailyOperationModel> query = DbSet;

            List<string> searchAttributes = new List<string>()
            {
                "Code"
            };
            query = QueryHelper<DailyOperationModel>.Search(query, searchAttributes, keyword);

            Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DailyOperationModel>.Filter(query, filterDictionary);

            List<string> selectedFields = new List<string>()
                {
                    "Id","Code","LastModifiedUtc"
                };

            query = query
                    .Select(field => new DailyOperationModel
                    {
                        Id = field.Id,
                        Code = field.Code,

                    });

            Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DailyOperationModel>.Order(query, orderDictionary);

            Pageable<DailyOperationModel> pageable = new Pageable<DailyOperationModel>(query, page - 1, size);
            List<DailyOperationModel> data = pageable.Data.ToList();
            int totalData = pageable.TotalCount;

            return new ReadResponse<DailyOperationModel>(data, totalData, orderDictionary, selectedFields);
        }

        public async Task<DailyOperationModel> ReadByIdAsync(int id)
        {
            return await DailyOperationLogic.ReadModelById(id);
        }

        public async Task<int> UpdateAsync(int id, DailyOperationModel model)
        {
            this.DailyOperationLogic.UpdateModelAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }
    }
}
