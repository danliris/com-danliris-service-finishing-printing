using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.DyestuffChemicalUsageReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.DyestuffChemicalUsageReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.DyestuffChemicalUsageReceipt;
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

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.DyestuffChemicalUsageReceipt
{
    public class DyestuffChemicalUsageReceiptFacade : IDyestuffChemicalUsageReceiptFacade
    {
        private readonly ProductionDbContext DbContext;
        private readonly DbSet<DyestuffChemicalUsageReceiptModel> DbSet;
        private readonly DyestuffChemicalUsageReceiptLogic DyestuffChemicalUsageReceiptLogic;
        private readonly IServiceProvider ServiceProvider;

        public DyestuffChemicalUsageReceiptFacade(IServiceProvider serviceProvider, ProductionDbContext dbContext)
        {
            ServiceProvider = serviceProvider;
            DbContext = dbContext;
            DbSet = dbContext.Set<DyestuffChemicalUsageReceiptModel>();
            DyestuffChemicalUsageReceiptLogic = serviceProvider.GetService<DyestuffChemicalUsageReceiptLogic>();
        }

        public Task<int> CreateAsync(DyestuffChemicalUsageReceiptModel model)
        {
            DyestuffChemicalUsageReceiptLogic.CreateModel(model);
            return DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await DyestuffChemicalUsageReceiptLogic.DeleteModel(id);
            return await DbContext.SaveChangesAsync();
        }

        public Task<DyestuffChemicalUsageReceiptModel> GetDataByStrikeOff(int strikeOffId)
        {
            return DyestuffChemicalUsageReceiptLogic.GetDataByStrikeOff(strikeOffId);
        }

        public ReadResponse<DyestuffChemicalUsageReceiptModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<DyestuffChemicalUsageReceiptModel> query = DbSet;

            List<string> searchAttributes = new List<string>()
            {
                "ProductionOrderOrderNo", "StrikeOffCode"
            };
            query = QueryHelper<DyestuffChemicalUsageReceiptModel>.Search(query, searchAttributes, keyword);

            Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyestuffChemicalUsageReceiptModel>.Filter(query, filterDictionary);

            List<string> selectedFields = new List<string>()
            {

                "Id","ProductionOrder","StrikeOff","Date","LastModifiedUtc"

            };

            Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyestuffChemicalUsageReceiptModel>.Order(query, orderDictionary);

            Pageable<DyestuffChemicalUsageReceiptModel> pageable = new Pageable<DyestuffChemicalUsageReceiptModel>(query, page - 1, size);
            List<DyestuffChemicalUsageReceiptModel> data = pageable.Data.ToList();
            int totalData = pageable.TotalCount;

            return new ReadResponse<DyestuffChemicalUsageReceiptModel>(data, totalData, orderDictionary, selectedFields);
        }

        public Task<DyestuffChemicalUsageReceiptModel> ReadByIdAsync(int id)
        {
            return DyestuffChemicalUsageReceiptLogic.ReadModelById(id);
        }

        public async Task<int> UpdateAsync(int id, DyestuffChemicalUsageReceiptModel model)
        {
            await DyestuffChemicalUsageReceiptLogic.UpdateModelAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }
    }
}
