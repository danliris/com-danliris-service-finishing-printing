using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.EventOrganizer;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.EventOrganizer;
using Com.Danliris.Service.Production.Lib;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Production.Lib.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using Com.Moonlay.NetCore.Lib;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master
{
    public class EventOrganizerFacade : IEventOrganizerFacade
    {
        private readonly ProductionDbContext DbContext;
        private readonly DbSet<EventOrganizer> DbSet;
        public readonly EventOrganizerLogic EventOrganizerLogic;

        public EventOrganizerFacade(IServiceProvider serviceProvider, ProductionDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<EventOrganizer>();
            EventOrganizerLogic = serviceProvider.GetService<EventOrganizerLogic>();
        }
        public async Task<int> CreateAsync(EventOrganizer model)
        {
            do
            {
                model.Code = CodeGenerator.Generate();
            }
            while (DbSet.Any(d => d.Code.Equals(model.Code)));
            EventOrganizerLogic.CreateModel(model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await EventOrganizerLogic.DeleteModel(id);
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<EventOrganizer> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<EventOrganizer> query = DbSet;

            List<string> searchAttributes = new List<string>()
            {
                 "Code", "ProcessArea","kasie","kasubsie","Group"
            };
            query = QueryHelper<EventOrganizer>.Search(query, searchAttributes, keyword);

            Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<EventOrganizer>.Filter(query, filterDictionary);

            Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<EventOrganizer>.Order(query, orderDictionary);

            Pageable<EventOrganizer> pageable = new Pageable<EventOrganizer>(query, page - 1, size);
            List<EventOrganizer> data = pageable.Data.ToList();
            int totalData = pageable.TotalCount;

            return new ReadResponse<EventOrganizer>(data, totalData, orderDictionary, new List<string>());
        }

        public async Task<EventOrganizer> ReadByIdAsync(int id)
        {
            return await EventOrganizerLogic.ReadModelById(id);
        }

        public async Task<int> UpdateAsync(int id, EventOrganizer model)
        {
            await this.EventOrganizerLogic.UpdateModelAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }
    }
}
