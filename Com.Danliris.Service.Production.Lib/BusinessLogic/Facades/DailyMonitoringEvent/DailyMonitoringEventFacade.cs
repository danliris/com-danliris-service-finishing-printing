using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.DailyMonitoringEvent;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.DailyMonitoringEvent;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.DailyMonitoringEvent;
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

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.DailyMonitoringEvent
{
    public class DailyMonitoringEventFacade : IDailyMonitoringEventFacade
    {
        private readonly ProductionDbContext DbContext;
        private readonly DbSet<DailyMonitoringEventModel> DbSet;
        private readonly DailyMonitoringEventLogic DailyMonitoringEventLogic;

        public DailyMonitoringEventFacade(IServiceProvider serviceProvider, ProductionDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = dbContext.Set<DailyMonitoringEventModel>();
            DailyMonitoringEventLogic = serviceProvider.GetService<DailyMonitoringEventLogic>();
        }

        public Task<int> CreateAsync(DailyMonitoringEventModel model)
        {
            do
            {
                model.Code = CodeGenerator.Generate();
            }
            while (DbSet.Any(d => d.Code.Equals(model.Code)));
            DailyMonitoringEventLogic.CreateModel(model);
            return DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await DailyMonitoringEventLogic.DeleteModel(id);
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<DailyMonitoringEventModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<DailyMonitoringEventModel> query = DbSet;

            List<string> searchAttributes = new List<string>()
            {
                "ProcessArea", "MachineName"
            };
            query = QueryHelper<DailyMonitoringEventModel>.Search(query, searchAttributes, keyword);

            Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DailyMonitoringEventModel>.Filter(query, filterDictionary);


            Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DailyMonitoringEventModel>.Order(query, orderDictionary);

            Pageable<DailyMonitoringEventModel> pageable = new Pageable<DailyMonitoringEventModel>(query, page - 1, size);
            List<DailyMonitoringEventModel> data = pageable.Data.ToList();
            int totalData = pageable.TotalCount;

            return new ReadResponse<DailyMonitoringEventModel>(data, totalData, orderDictionary, new List<string>());
        }

        public Task<DailyMonitoringEventModel> ReadByIdAsync(int id)
        {
            return DailyMonitoringEventLogic.ReadModelById(id);
        }

        public async Task<int> UpdateAsync(int id, DailyMonitoringEventModel model)
        {
            await DailyMonitoringEventLogic.UpdateModelAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }
    }
}
