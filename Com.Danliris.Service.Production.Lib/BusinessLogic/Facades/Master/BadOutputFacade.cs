using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.BadOutput;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.BadOutput;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Com.Moonlay.NetCore.Lib;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master
{
    public class BadOutputFacade : IBadOutputFacade
    {
        private readonly ProductionDbContext DbContext;
        private readonly DbSet<BadOutputModel> DbSet;
        private readonly BadOutputLogic BadOutputLogic;

        public BadOutputFacade(IServiceProvider serviceProvider, ProductionDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<BadOutputModel>();
            BadOutputLogic = serviceProvider.GetService<BadOutputLogic>();
        }

        public async Task<int> CreateAsync(BadOutputModel model)
        {
            do
            {
                model.Code = CodeGenerator.Generate();
            }
            while (DbSet.Any(d => d.Code.Equals(model.Code)));
            this.BadOutputLogic.CreateModel(model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await BadOutputLogic.DeleteModel(id);
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<BadOutputModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<BadOutputModel> query = DbSet;

            List<string> searchAttributes = new List<string>()
            {
                "Code", "Reason"
            };
            query = QueryHelper<BadOutputModel>.Search(query, searchAttributes, keyword);

            Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<BadOutputModel>.Filter(query, filterDictionary);

            List<string> selectedFields = new List<string>()
                {
                    "Id", "Reason", "Code", "MachineDetails", "LastModifiedUtc"
                };

            query = query
                    .Select(field => new BadOutputModel
                    {
                        Id = field.Id,
                        Reason = field.Reason,
                        Code = field.Code,
                        LastModifiedUtc = field.LastModifiedUtc,
                        MachineDetails = new List<BadOutputMachineModel>(field.MachineDetails.Select(i => new BadOutputMachineModel
                        {
                            MachineCode = i.MachineCode,
                            MachineId = i.MachineId,
                            MachineName = i.MachineName,                        
                        }))
                    });

            Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<BadOutputModel>.Order(query, orderDictionary);

            Pageable<BadOutputModel> pageable = new Pageable<BadOutputModel>(query, page - 1, size);
            List<BadOutputModel> data = pageable.Data.ToList();
            int totalData = pageable.TotalCount;

            return new ReadResponse<BadOutputModel>(data, totalData, orderDictionary, selectedFields);
        }

        public async Task<BadOutputModel> ReadByIdAsync(int id)
        {
            return await BadOutputLogic.ReadModelById(id);
        }

        public async Task<int> UpdateAsync(int id, BadOutputModel model)
        {
            await BadOutputLogic.UpdateModelAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }
    }
}
