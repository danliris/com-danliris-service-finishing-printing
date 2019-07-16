using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.OperationalCost;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.OperationalCost;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Globalization;
using Newtonsoft.Json;
using Com.Moonlay.NetCore.Lib;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master
{
    public class OperationalCostFacade : IOperationalCostFacade
    {
        private readonly ProductionDbContext DbContext;
        private readonly DbSet<OperationalCostModel> DbSet;
        private readonly OperationalCostLogic OperationalCostLogic;

        public OperationalCostFacade(IServiceProvider serviceProvider, ProductionDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<OperationalCostModel>();
            OperationalCostLogic = serviceProvider.GetService<OperationalCostLogic>();
        }

        public async Task<int> CreateAsync(OperationalCostModel model)
        {
            do
            {
                model.Code = CodeGenerator.Generate();
            }
            while (DbSet.Any(d => d.Code.Equals(model.Code)));
            OperationalCostLogic.CreateModel(model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await OperationalCostLogic.DeleteModel(id);
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<OperationalCostModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<OperationalCostModel> query = DbSet;

            List<string> searchAttributes = new List<string>()
            {
                "Code", "Month", "Year"
            };
            if (!string.IsNullOrEmpty(keyword))
            {
                if (DateTime.TryParseExact(keyword, "MMMM", CultureInfo.GetCultureInfo("en-ID"), DateTimeStyles.None, out DateTime dateTime))
                {
                    query = query.Where(x => x.Month == dateTime.Month ||
                                            x.Year.ToString().Contains(keyword) ||
                                            x.Code.Contains(keyword));
                }
                else
                {
                    query = query.Where(x => x.Year.ToString().Contains(keyword) ||
                                            x.Code.Contains(keyword));
                }
            }
            //query = QueryHelper<DirectLaborCostModel>.Search(query, searchAttributes, keyword);

            Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<OperationalCostModel>.Filter(query, filterDictionary);

            List<string> selectedFields = new List<string>()
                {
                    "Id", "Code", "LastModifiedUtc", "Month", "Year"
                };
            query = query
                .Select(field => new OperationalCostModel
                {
                    Id = field.Id,
                    Code = field.Code,
                    LastModifiedUtc = field.LastModifiedUtc,
                    Month = field.Month,
                    Year = field.Year
                });

            Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<OperationalCostModel>.Order(query, orderDictionary);

            Pageable<OperationalCostModel> pageable = new Pageable<OperationalCostModel>(query, page - 1, size);
            List<OperationalCostModel> data = pageable.Data.ToList();
            int totalData = pageable.TotalCount;

            return new ReadResponse<OperationalCostModel>(data, totalData, orderDictionary, selectedFields);
        }

        public async Task<OperationalCostModel> ReadByIdAsync(int id)
        {
            return await OperationalCostLogic.ReadModelById(id);
        }

        public async Task<int> UpdateAsync(int id, OperationalCostModel model)
        {
            OperationalCostLogic.UpdateModelAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }
    }
}
