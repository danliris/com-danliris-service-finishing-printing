using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.StrikeOff;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.StrikeOff;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.StrikeOff;
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
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.StrikeOff;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.StrikeOff
{
    public class StrikeOffFacade : IStrikeOffFacade
    {
        private readonly ProductionDbContext DbContext;
        private readonly DbSet<StrikeOffModel> DbSet;
        private readonly StrikeOffLogic StrikeOffLogic;
        private readonly IServiceProvider ServiceProvider;

        public StrikeOffFacade(IServiceProvider serviceProvider, ProductionDbContext dbContext)
        {
            ServiceProvider = serviceProvider;
            DbContext = dbContext;
            DbSet = dbContext.Set<StrikeOffModel>();
            StrikeOffLogic = serviceProvider.GetService<StrikeOffLogic>();
        }


        public Task<int> CreateAsync(StrikeOffModel model)
        {
            StrikeOffLogic.CreateModel(model);
            return DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await StrikeOffLogic.DeleteModel(id);
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<StrikeOffModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<StrikeOffModel> query = DbSet.Include(s => s.StrikeOffItems);

            List<string> searchAttributes = new List<string>()
            {
                "Code"
            };
            query = QueryHelper<StrikeOffModel>.Search(query, searchAttributes, keyword);

            Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<StrikeOffModel>.Filter(query, filterDictionary);

            List<string> selectedFields = new List<string>()
            {

                "Id","Code","Type","Cloth","Remark","LastModifiedUtc","StrikeOffItems"

            };

            Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<StrikeOffModel>.Order(query, orderDictionary);

            Pageable<StrikeOffModel> pageable = new Pageable<StrikeOffModel>(query, page - 1, size);
            List<StrikeOffModel> data = pageable.Data.ToList();
            int totalData = pageable.TotalCount;

            return new ReadResponse<StrikeOffModel>(data, totalData, orderDictionary, selectedFields);
        }

        public Task<StrikeOffModel> ReadByIdAsync(int id)
        {
            return StrikeOffLogic.ReadModelById(id);
        }

        public ReadResponse<StrikeOffConsumptionViewModel> ReadForUsageReceipt(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<StrikeOffModel> query = DbSet.Include(s => s.StrikeOffItems).ThenInclude(s => s.ChemicalItems)
                .Include(s => s.StrikeOffItems).ThenInclude(d => d.DyeStuffItems);

            List<string> searchAttributes = new List<string>()
            {
                "Code"
            };
            query = QueryHelper<StrikeOffModel>.Search(query, searchAttributes, keyword);

            Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<StrikeOffModel>.Filter(query, filterDictionary);

            List<string> selectedFields = new List<string>()
            {

                "Id","Code","Type","Cloth","Remark","LastModifiedUtc","StrikeOffItems"

            };

            Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<StrikeOffModel>.Order(query, orderDictionary);

            Pageable<StrikeOffModel> pageable = new Pageable<StrikeOffModel>(query, page - 1, size);
            List<StrikeOffModel> data = pageable.Data.ToList();
            int totalData = pageable.TotalCount;
            var vm = data.Select(s => new StrikeOffConsumptionViewModel()
            {
                Id = s.Id,
                Code = s.Code,
                Type = s.Type,
                Cloth = s.Cloth,
                LastModifiedUtc = s.LastModifiedUtc,
                Remark = s.Remark,
                StrikeOffItems = s.StrikeOffItems.Select(d => new StrikeOffConsumptionItemViewModel()
                {
                    Id = d.Id,
                    ColorCode = d.ColorCode,
                    StrikeOffItemDetails = d.DyeStuffItems.Select(e => new StrikeOffConsumptionDetailViewModel()
                    {
                        Id = e.Id,
                        Name = e.ProductName,
                        Quantity = e.Quantity
                    }).Concat(d.ChemicalItems.OrderBy(e => e.Index).Select(e => new StrikeOffConsumptionDetailViewModel()
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Quantity = e.Quantity,
                    })).ToList()

                }).ToList()
            });
            return new ReadResponse<StrikeOffConsumptionViewModel>(vm.ToList(), totalData, orderDictionary, selectedFields);
        }

        public async Task<int> UpdateAsync(int id, StrikeOffModel model)
        {
            await StrikeOffLogic.UpdateModelAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }
    }
}
