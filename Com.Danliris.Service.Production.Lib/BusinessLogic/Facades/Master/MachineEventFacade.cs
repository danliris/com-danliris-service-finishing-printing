using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.Machine;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.Machine;
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
    public class MachineEventFacade : IMachineEventFacade
    {
        private readonly ProductionDbContext DbContext;
        private readonly DbSet<MachineEventsModel> DbSet;
        private readonly MachineEventLogic MachineEventLogic;
        public MachineEventFacade(IServiceProvider serviceProvider, ProductionDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = DbContext.Set<MachineEventsModel>();
            this.MachineEventLogic = serviceProvider.GetService<MachineEventLogic>();
        }

        public async Task<int> CreateAsync(MachineEventsModel model)
        {
            this.MachineEventLogic.CreateModel(model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await MachineEventLogic.DeleteModel(id);
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<MachineEventsModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<MachineEventsModel> query = DbSet;

            List<string> searchAttributes = new List<string>()
            {
                "Code", "Name"
            };
            query = QueryHelper<MachineEventsModel>.Search(query, searchAttributes, keyword);

            Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<MachineEventsModel>.Filter(query, filterDictionary);

            List<string> selectedFields = new List<string>()
                {
                    "Id", "Name", "Code", "Category", "LastModifiedUtc"
                };

            query = query
                    .Select(field => new MachineEventsModel
                    {
                        Id = field.Id,
                        Name = field.Name,
                        Code = field.Code,
                        Category = field.Category,
                        LastModifiedUtc = field.LastModifiedUtc,

                    });

            Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<MachineEventsModel>.Order(query, orderDictionary);

            Pageable<MachineEventsModel> pageable = new Pageable<MachineEventsModel>(query, page - 1, size);
            List<MachineEventsModel> data = pageable.Data.ToList();
            int totalData = pageable.TotalCount;

            return new ReadResponse<MachineEventsModel>(data, totalData, orderDictionary, selectedFields);
        }

        public async Task<MachineEventsModel> ReadByIdAsync(int id)
        {
            return await MachineEventLogic.ReadModelById(id);
        }

        public async Task<int> UpdateAsync(int id, MachineEventsModel model)
        {
            await this.MachineEventLogic.UpdateModelAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }
    }
}
