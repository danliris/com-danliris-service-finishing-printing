using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.DirectLaborCost;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.DirectLaborCost;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Utilities;
using Com.Moonlay.NetCore.Lib;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master
{
    public class DirectLaborCostFacade : IDirectLaborCostFacade
    {
        private readonly ProductionDbContext DbContext;
        private readonly DbSet<DirectLaborCostModel> DbSet;
        private readonly DirectLaborCostLogic DirectLaborCostLogic;

        public DirectLaborCostFacade(IServiceProvider serviceProvider, ProductionDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<DirectLaborCostModel>();
            DirectLaborCostLogic = serviceProvider.GetService<DirectLaborCostLogic>();
        }

        public async Task<int> CreateAsync(DirectLaborCostModel model)
        {
            do
            {
                model.Code = CodeGenerator.Generate();
            }
            while (DbSet.Any(d => d.Code.Equals(model.Code)));
            DirectLaborCostLogic.CreateModel(model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await DirectLaborCostLogic.DeleteModel(id);
            return await DbContext.SaveChangesAsync();
        }

        public Task<DirectLaborCostModel> GetForCostCalculation(int month, int year)
        {
            return DbSet.FirstOrDefaultAsync(d => !d.IsDeleted && d.Month == month && d.Year == year);
        }

        public ReadResponse<DirectLaborCostModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<DirectLaborCostModel> query = DbSet;

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
            query = QueryHelper<DirectLaborCostModel>.Filter(query, filterDictionary);

            List<string> selectedFields = new List<string>()
                {
                    "Id", "Code", "LastModifiedUtc", "Month", "Year", "LaborTotal", "WageTotal"
                };
            query = query
                .Select(field => new DirectLaborCostModel
                {
                    Id = field.Id,
                    Code = field.Code,
                    LastModifiedUtc = field.LastModifiedUtc,
                    Month = field.Month,
                    Year = field.Year,
                    LaborTotal = field.LaborTotal,
                    WageTotal = field.WageTotal
                });

            Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DirectLaborCostModel>.Order(query, orderDictionary);

            Pageable<DirectLaborCostModel> pageable = new Pageable<DirectLaborCostModel>(query, page - 1, size);
            List<DirectLaborCostModel> data = pageable.Data.ToList();
            int totalData = pageable.TotalCount;

            return new ReadResponse<DirectLaborCostModel>(data, totalData, orderDictionary, selectedFields);
        }

        public async Task<DirectLaborCostModel> ReadByIdAsync(int id)
        {
            return await DirectLaborCostLogic.ReadModelById(id);
        }

        public async Task<int> UpdateAsync(int id, DirectLaborCostModel model)
        {
            DirectLaborCostLogic.UpdateModelAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }
    }
}
