using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Dynamic.Core;
using Newtonsoft.Json;
using Com.Moonlay.NetCore.Lib;
using Com.Danliris.Service.Production.Lib.BusinessLogic.Interfaces.Master;
using Com.Danliris.Service.Production.Lib.Models.Master.Step;
using Com.Danliris.Service.Production.Lib.BusinessLogic.Implementations.Master.Step;
using Com.Danliris.Service.Production.Lib.Utilities;

namespace Com.Danliris.Service.Production.Lib.BusinessLogic.Facades.Master
{
    public class StepFacade : IStepFacade
    {
        private readonly ProductionDbContext DbContext;
        private readonly DbSet<StepModel> DbSet;
        private readonly StepLogic StepLogic;
        private readonly StepIndicatorLogic StepIndicatorLogic;

        public StepFacade(IServiceProvider serviceProvider, ProductionDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<StepModel>();
            StepLogic = serviceProvider.GetService<StepLogic>();
            StepIndicatorLogic = serviceProvider.GetService<StepIndicatorLogic>();
        }

        public ReadResponse<StepModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<StepModel> query = DbSet;

            List<string> searchAttributes = new List<string>()
            {
                "Code", "Alias", "Process"
            };
            query = QueryHelper<StepModel>.Search(query, searchAttributes, keyword);

            Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<StepModel>.Filter(query, filterDictionary);

            List<string> selectedFields = new List<string>()
                {
                    "Id", "Alias", "Code", "Process", "ProcessArea", "StepIndicators"
                };
            query = query
                .Select(field => new StepModel
                {
                    Id = field.Id,
                    Alias = field.Alias,
                    Code = field.Code,
                    Process = field.Process,
                    ProcessArea = field.ProcessArea,
                    StepIndicators = new List<StepIndicatorModel>(field.StepIndicators.Select(i => new StepIndicatorModel
                    {
                        Name = i.Name,
                        Uom = i.Uom,
                        Value = i.Value
                    }))
                });

            Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<StepModel>.Order(query, orderDictionary);

            Pageable<StepModel> pageable = new Pageable<StepModel>(query, page - 1, size);
            List<StepModel> data = pageable.Data.ToList();
            int totalData = pageable.TotalCount;

            return new ReadResponse<StepModel>(data, totalData, orderDictionary, selectedFields);
        }

        public async Task<int> Create(StepModel model)
        {
            StepLogic.CreateModel(model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<StepModel> ReadById(int id)
        {
            return await StepLogic.ReadModelById(id);
        }

        public async Task<int> Update(int id, StepModel model)
        {
            await StepLogic.UpdateModelStep(id, model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            await StepLogic.DeleteModel(id);
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<StepModel> ReadVM(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<StepModel> query = DbSet;

            List<string> searchAttributes = new List<string>()
            {
                "Code", "Alias", "Process"
            };
            query = QueryHelper<StepModel>.Search(query, searchAttributes, keyword);

            Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<StepModel>.Filter(query, filterDictionary);

            List<string> selectedFields = new List<string>()
                {
                    "Alias", "Code", "Process", "ProcessArea", "StepIndicators"
                };
            query = query
                .Select(field => new StepModel
                {
                    Alias = field.Alias,
                    Code = field.Code,
                    Process = field.Process,
                    ProcessArea = field.ProcessArea,
                    StepIndicators = new List<StepIndicatorModel>(field.StepIndicators.Select(i => new StepIndicatorModel
                    {
                        Name = i.Name,
                        Uom = i.Uom,
                        Value = i.Value
                    }))
                });

            Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<StepModel>.Order(query, orderDictionary);

            Pageable<StepModel> pageable = new Pageable<StepModel>(query, page - 1, size);
            List<StepModel> data = pageable.Data.ToList();
            int totalData = pageable.TotalCount;

            return new ReadResponse<StepModel>(data, totalData, orderDictionary, selectedFields);
        }
    }
}
