using Com.Danliris.Service.Production.Lib.BusinessLogic.Implementations.Master.DurationEstimation;
using Com.Danliris.Service.Production.Lib.BusinessLogic.Interfaces.Master;
using Com.Danliris.Service.Production.Lib.Models.Master.DurationEstimation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Dynamic.Core;
using Newtonsoft.Json;
using Com.Moonlay.NetCore.Lib;
using Com.Danliris.Service.Production.Lib.Models.Master.Step;
using Com.Danliris.Service.Production.Lib.BusinessLogic.Implementations.Master.Step;
using Com.Danliris.Service.Production.Lib.Utilities;


namespace Com.Danliris.Service.Production.Lib.BusinessLogic.Facades.Master
{
    public class DurationEstimationFacade : IDurationEstimationFacade
    {
        private readonly ProductionDbContext DbContext;
        private readonly DbSet<DurationEstimationModel> DbSet;
        private readonly DurationEstimationLogic DurationEstimationLogic;

        public DurationEstimationFacade(IServiceProvider serviceProvider, ProductionDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<DurationEstimationModel>();
            DurationEstimationLogic = serviceProvider.GetService<DurationEstimationLogic>();
        }

        public ReadResponse<DurationEstimationModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<DurationEstimationModel> query = DbSet;

            List<string> searchAttributes = new List<string>()
            {
                "Code", "ProcessTypeName", "ProcessTypeCode"
            };
            query = QueryHelper<DurationEstimationModel>.Search(query, searchAttributes, keyword);

            Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DurationEstimationModel>.Filter(query, filterDictionary);

            List<string> selectedFields = new List<string>()
            {
                "Id", "Code", "Areas", "ProcessType"
            };
            query = query
                .Select(field => new DurationEstimationModel
                {
                    Id = field.Id,
                    Code = field.Code,
                    ProcessTypeCode = field.ProcessTypeCode,
                    ProcessTypeId = field.ProcessTypeId,
                    ProcessTypeName = field.ProcessTypeName,
                    OrderTypeCode = field.OrderTypeCode,
                    OrderTypeId = field.OrderTypeId,
                    OrderTypeName = field.OrderTypeName,
                    Areas = new List<DurationEstimationAreaModel>(field.Areas.Select(i => new DurationEstimationAreaModel
                    {
                        Name = i.Name,
                        Duration = i.Duration
                    }))
                });

            Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DurationEstimationModel>.Order(query, orderDictionary);

            Pageable<DurationEstimationModel> pageable = new Pageable<DurationEstimationModel>(query, page - 1, size);
            List<DurationEstimationModel> data = pageable.Data.ToList();
            int totalData = pageable.TotalCount;

            return new ReadResponse<DurationEstimationModel>(data, totalData, orderDictionary, selectedFields);
        }

        public async Task<int> CreateAsync(DurationEstimationModel model)
        {
            do
            {
                model.Code = CodeGenerator.Generate();
            }
            while (DbSet.Any(d => d.Code.Equals(model.Code)));
            DurationEstimationLogic.CreateModel(model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<DurationEstimationModel> ReadByIdAsync(int id)
        {
            return await DurationEstimationLogic.ReadModelById(id);
        }

        public async Task<int> UpdateAsync(int id, DurationEstimationModel model)
        {
            await DurationEstimationLogic.UpdateModelAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await DurationEstimationLogic.DeleteModel(id);
            return await DbContext.SaveChangesAsync();
        }

        public DurationEstimationModel ReadByProcessType(string processTypeCode)
        {
            return DbSet.Include(i => i.Areas).FirstOrDefault(f => f.ProcessTypeCode.Equals(processTypeCode));
        }
    }
}
