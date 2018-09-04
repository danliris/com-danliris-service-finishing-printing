using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Packing;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.Packing;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Packing;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Utilities;
using Com.Moonlay.NetCore.Lib;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Packing
{
    public class PackingFacade : IPackingFacade
    {
        private readonly ProductionDbContext dbContext;
        private readonly DbSet<PackingModel> dbSet;
        private readonly PackingLogic packingLogic;

        public PackingFacade(IServiceProvider serviceProvider, ProductionDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<PackingModel>();
            this.packingLogic = serviceProvider.GetService<PackingLogic>();
        }

        public async Task<int> CreateAsync(PackingModel model)
        {
            do
            {
                model.Code = CodeGenerator.Generate();
            }
            while (dbSet.Any(d => d.Code.Equals(model.Code)));

            packingLogic.CreateModel(model);

            var row = await dbContext.SaveChangesAsync();
            //if(row > 0)
            //{
            //    await packingLogic.CreateProduct(model);
            //}
            return row;
        }

        public async Task<int> DeleteAsync(int id)
        {
            await packingLogic.DeleteModel(id);
            return await dbContext.SaveChangesAsync();
        }

        public ReadResponse<PackingModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<PackingModel> query = (from packings in dbContext.Packings
                                              where packings.IsDeleted.Equals(false)
                                              select packings).Include("PackingDetails");

            List<string> searchAttributes = new List<string>()
            {
                "Code", "BuyerName", "ProductionOrderNo", "ColorName", "Construction", "DesignNumber"
            };
            query = QueryHelper<PackingModel>.Search(query, searchAttributes, keyword);

            Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<PackingModel>.Filter(query, filterDictionary);

            List<string> selectedFields = new List<string>()
            {
                "Id", "Code", "Date", "BuyerName", "ProductionOrderNo", "ColorName", "Construction", "DesignNumber", "Accepted"
            };

            Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<PackingModel>.Order(query, orderDictionary);

            Pageable<PackingModel> pageable = new Pageable<PackingModel>(query, page - 1, size);
            List<PackingModel> data = pageable.Data.ToList();
            int totalData = pageable.TotalCount;

            return new ReadResponse<PackingModel>(data, totalData, orderDictionary, selectedFields);
        }

        public async Task<PackingModel> ReadByIdAsync(int id)
        {
            return await packingLogic.ReadModelById(id);
        }

        public async Task<int> UpdateAsync(int id, PackingModel model)
        {
            packingLogic.UpdateModelAsync(id, model);
            return await dbContext.SaveChangesAsync();
        }
    }
}
