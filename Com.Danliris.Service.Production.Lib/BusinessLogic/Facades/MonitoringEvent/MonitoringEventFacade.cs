using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.MonitoringEvent;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.MonitoringEvent;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Monitoring_Event;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Utilities;
using Com.Moonlay.NetCore.Lib;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.MonitoringEvent
{
    public class MonitoringEventFacade : IMonitoringEventFacade
    {
        private readonly ProductionDbContext DbContext;
        private readonly DbSet<MonitoringEventModel> DbSet;
        private readonly MonitoringEventLogic MonitoringEventLogic;

        public MonitoringEventFacade(IServiceProvider serviceProvider, ProductionDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = DbContext.Set<MonitoringEventModel>();
            this.MonitoringEventLogic = serviceProvider.GetService<MonitoringEventLogic>();
        }
        public async Task<int> CreateAsync(MonitoringEventModel model)
        {
            do
            {
                model.Code = CodeGenerator.Generate();
            }
            while (DbSet.Any(d => d.Code.Equals(model.Code)));

            this.MonitoringEventLogic.CreateModel(model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await MonitoringEventLogic.DeleteModel(id);
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<MonitoringEventModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<MonitoringEventModel> query = DbSet;

            List<string> searchAttributes = new List<string>()
            {
                "Code"
            };
            query = QueryHelper<MonitoringEventModel>.Search(query, searchAttributes, keyword);

            Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<MonitoringEventModel>.Filter(query, filterDictionary);

            List<string> selectedFields = new List<string>()
                {
                    "Id","Code","DateStart","DateEnd", "LastModifiedUtc"
                };

            query = query
                    .Select(field => new MonitoringEventModel
                    {
                        Id = field.Id,
                        //Name = field.Name,
                        Code = field.Code,
                        DateStart = field.DateStart,
                        DateEnd = field.DateEnd,
                        LastModifiedUtc = field.LastModifiedUtc,
                        //Indicators = new List<MachineTypeIndicatorsModel>(field.Indicators.Select(i => new MachineTypeIndicatorsModel
                        //{
                        //    Indicator = i.Indicator,
                        //    DataType = i.DataType,
                        //    DefaultValue = i.DefaultValue,
                        //    Uom = i.Uom,
                        //    MachineTypeId = i.MachineTypeId
                        //}))
                    });

            Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<MonitoringEventModel>.Order(query, orderDictionary);

            Pageable<MonitoringEventModel> pageable = new Pageable<MonitoringEventModel>(query, page - 1, size);
            List<MonitoringEventModel> data = pageable.Data.ToList();
            int totalData = pageable.TotalCount;

            return new ReadResponse<MonitoringEventModel>(data, totalData, orderDictionary, selectedFields);
        }

        public async Task<MonitoringEventModel> ReadByIdAsync(int id)
        {
            return await MonitoringEventLogic.ReadModelById(id);
        }

        public async Task<int> UpdateAsync(int id, MonitoringEventModel model)
        {
            this.MonitoringEventLogic.UpdateModelAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }
    }
}
