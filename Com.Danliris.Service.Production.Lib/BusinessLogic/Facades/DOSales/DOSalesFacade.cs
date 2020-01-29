using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.DOSales;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.DOSales;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.DOSales;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Utilities;
using Com.Moonlay.NetCore.Lib;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.DOSales
{
    public class DOSalesFacade : IDOSalesFacade
    {
        private readonly ProductionDbContext dbContext;
        private readonly DbSet<DOSalesModel> dbSet;
        private readonly DOSalesLogic doSalesLogic;
        private readonly IServiceProvider ServiceProvider;

        public DOSalesFacade(IServiceProvider serviceProvider, ProductionDbContext dbContext)
        {
            ServiceProvider = serviceProvider;
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<DOSalesModel>();
            this.doSalesLogic = serviceProvider.GetService<DOSalesLogic>();
        }

        public async Task<int> CreateAsync(DOSalesModel model)
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    int index = 0;
                    do
                    {
                        model.Code = CodeGenerator.Generate();
                    }
                    while (dbSet.Any(d => d.Code.Equals(model.Code)));

                    DOSalesNumberGenerator(model, index);

                    doSalesLogic.CreateModel(model);

                    var row = await dbContext.SaveChangesAsync();
                    
                    transaction.Commit();
                    return row;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            await doSalesLogic.DeleteModel(id);
            return await dbContext.SaveChangesAsync();
        }

        public Task<DOSalesDetailModel> GetDOSalesDetail(string productName)
        {
            var doSalesDetail = dbContext.DOSalesItemDetails.Include(x => x.DOSales).FirstOrDefaultAsync(x => productName.Equals(
                string.Format("{0}/{1}/{2}", x.DOSales.DOSalesNo, x.DOSales.ProductionOrderNo, x.UnitCode), StringComparison.OrdinalIgnoreCase));

            return doSalesDetail;
        }

        public ReadResponse<DOSalesModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<DOSalesModel> query = (from doSales in dbContext.DOSalesItems
                                              where doSales.IsDeleted.Equals(false)
                                              select doSales).Include("DOSalesDetails");

            List<string> searchAttributes = new List<string>()
            {
                "DOSalesNo", "StorageName", "StorageDivision", "BuyerName", "DestinationBuyerName", "ProductionOrderNo", "Construction"
            };
            query = QueryHelper<DOSalesModel>.Search(query, searchAttributes, keyword);

            Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DOSalesModel>.Filter(query, filterDictionary);

            List<string> selectedFields = new List<string>()
            {

                "Id","DOSalesNo","DOSalesType","DOSalesDate","StorageId","StorageName","StorageDivision","HeadOfStorage","ProductionOrderId","ProductionOrderNo","MaterialId","Material","MaterialWidthFinish","MaterialConstructionFinishId","DOSalesDetails","MaterialConstructionFinishName",
                "BuyerId","BuyerCode","BuyerName","BuyerAddress","BuyerType","BuyerNPWP","DestinationBuyerId","DestinationBuyerCode","DestinationBuyerName","DestinationBuyerAddress","DestinationBuyerType","DestinationBuyerNPWP",
                "PackingUom","LengthUom","Disp","Op","Sc","Construction","Remark","Status"

            };

            Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DOSalesModel>.Order(query, orderDictionary);

            Pageable<DOSalesModel> pageable = new Pageable<DOSalesModel>(query, page - 1, size);
            List<DOSalesModel> data = pageable.Data.ToList();
            int totalData = pageable.TotalCount;

            return new ReadResponse<DOSalesModel>(data, totalData, orderDictionary, selectedFields);
        }

        public async Task<DOSalesModel> ReadByIdAsync(int id)
        {
            return await doSalesLogic.ReadModelById(id);
        }

        public async Task<int> UpdateAsync(int id, DOSalesModel model)
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    await doSalesLogic.UpdateModelAsync(id, model);
                    var row = await dbContext.SaveChangesAsync();

                    transaction.Commit();

                    return row;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }

        }

        private void DOSalesNumberGenerator(DOSalesModel model, int index)
        {
            DOSalesModel lastData = dbSet.IgnoreQueryFilters().Where(w => w.DOSalesType.Equals(model.DOSalesType)).OrderByDescending(o => o.AutoIncreament).FirstOrDefault();

            int YearNow = DateTime.Now.Year;

            if (lastData == null)
            {
                if (model.DOSalesType == "UP")
                {
                    index = 28;
                }
                else if (model.DOSalesType == "US")
                {
                    index = 8;
                }
                else if (model.DOSalesType == "JS")
                {
                    index = 98;
                }
                else if (model.DOSalesType == "USS")
                {
                    index = 14;
                }
                else if (model.DOSalesType == "JB")
                {
                    index = 2;
                }
                else if (model.DOSalesType == "UPS")
                {
                    index = 19;
                }
                else
                {
                    index = 0;
                }
                model.AutoIncreament = 1 + index;
                model.DOSalesNo = $"{model.DOSalesType}/{YearNow}/{model.AutoIncreament.ToString().PadLeft(6, '0')}";
            }
            else
            {
                if (YearNow > lastData.CreatedUtc.Year)
                {
                    model.AutoIncreament = 1 + index;
                    model.DOSalesNo = $"{model.DOSalesType}/{YearNow}/{model.AutoIncreament.ToString().PadLeft(6, '0')}";
                }
                else
                {
                    model.AutoIncreament = lastData.AutoIncreament + (1 + index);
                    model.DOSalesNo = $"{model.DOSalesType}/{YearNow}/{model.AutoIncreament.ToString().PadLeft(6, '0')}";
                }
            }
        }
    }
}
