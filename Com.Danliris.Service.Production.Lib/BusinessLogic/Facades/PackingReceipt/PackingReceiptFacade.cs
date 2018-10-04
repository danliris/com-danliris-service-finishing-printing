using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.PackingReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.PackingReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.PackingReceipt;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using Com.Moonlay.NetCore.Lib;
using System.Net.Http;
using System.Net.Http.Headers;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.PackingReceipt
{
    public class PackingReceiptFacade : IPackingReceiptFacade
    {
        private readonly ProductionDbContext dbContext;
        private readonly DbSet<PackingReceiptModel> dbSet;
        private readonly PackingReceiptLogic packingReceiptLogic;

        public PackingReceiptFacade(IServiceProvider serviceProvider, ProductionDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<PackingReceiptModel>();
            this.packingReceiptLogic = serviceProvider.GetService<PackingReceiptLogic>();
        }

        public async Task<int> CreateAsync(PackingReceiptModel model)
        {
            do
            {
                model.Code = CodeGenerator.Generate();
            }
            while (dbSet.Any(d => d.Code.Equals(model.Code))) ;
            this.packingReceiptLogic.CreateModel(model);

            await this.packingReceiptLogic.UpdatePacking(model, true);
            await this.packingReceiptLogic.CreateInventory(model);

            return await dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await packingReceiptLogic.DeleteModel(id);
            return await dbContext.SaveChangesAsync();
        }

        public ReadResponse<PackingReceiptModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<PackingReceiptModel> query = dbSet;

            List<string> searchAttributes = new List<string>()
            {
                "Code"
            };
            query = QueryHelper<PackingReceiptModel>.Search(query, searchAttributes, keyword);

            Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<PackingReceiptModel>.Filter(query, filterDictionary);

            List<string> selectedFields = new List<string>()
                {
                    "Id","Code","Buyer","Storage","PackingCode","LastModifiedUtc","Date","ProductionOrderNo","ColorName","Construction","CreatedBy"
                };

            query = query
                    .Select(field => new PackingReceiptModel
                    {
                        Id = field.Id,
                        Code = field.Code,
                        Buyer = field.Buyer,
                        StorageName = field.StorageName,
                        PackingCode = field.PackingCode,
                        Date = field.Date,
                        ProductionOrderNo = field.ProductionOrderNo,
                        ColorName = field.ColorName,
                        Construction = field.Construction,
                        LastModifiedUtc = field.LastModifiedUtc,
                        CreatedBy = field.CreatedBy,
                        Items = field.Items,
                    });

            Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<PackingReceiptModel>.Order(query, orderDictionary);

            Pageable<PackingReceiptModel> pageable = new Pageable<PackingReceiptModel>(query, page - 1, size);
            List<PackingReceiptModel> data = pageable.Data.ToList();
            int totalData = pageable.TotalCount;

            return new ReadResponse<PackingReceiptModel>(data, totalData, orderDictionary, selectedFields);
        }

        public async Task<PackingReceiptModel> ReadByIdAsync(int id)
        {
            return await packingReceiptLogic.ReadModelById(id);
        }

        public async Task<int> UpdateAsync(int id, PackingReceiptModel model)
        {
            packingReceiptLogic.UpdateModelAsync(id, model);
            await this.packingReceiptLogic.UpdatePacking(model, false);
            await this.packingReceiptLogic.UpdateInventory(model);
            return await dbContext.SaveChangesAsync();
        }
    }
}
