using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.DOSales;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.DOSales;
using Com.Danliris.Service.Finishing.Printing.Lib.Helpers;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.DOSales;
using Com.Danliris.Service.Finishing.Printing.Lib.Services.HttpClientService;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DOSales;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Utilities;
using Com.Moonlay.NetCore.Lib;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
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
                    do
                    {
                        model.Code = CodeGenerator.Generate();
                    }
                    while (dbSet.Any(d => d.Code.Equals(model.Code)));

                    doSalesLogic.CreateModel(model);

                    var row = await dbContext.SaveChangesAsync();
                    //if (row > 0)
                    //{
                    //    await CreateProduct(model);
                    //}
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
                string.Format("{0}/{1}/{2}", x.DOSales.ProductionOrderNo, x.DOSales.Construction, x.Length), StringComparison.OrdinalIgnoreCase));

            return doSalesDetail;
        }

        public List<DOSalesViewModel> GetReport(string code, int productionOrderId, DateTime? dateFrom, DateTime? dateTo, int offSet)
        {
            IQueryable<DOSalesModel> query = dbContext.DOSalesItems.Include(x => x.DOSalesDetails).AsQueryable();

            IEnumerable<DOSalesViewModel> doSales;

            if (!string.IsNullOrEmpty(code))
                query = query.Where(x => x.Code == code);

            if (productionOrderId != -1)
                query = query.Where(x => x.ProductionOrderId == productionOrderId);


            if (dateFrom == null && dateTo == null)
            {
                query = query
                    .Where(x => DateTimeOffset.UtcNow.AddDays(-30).Date <= x.Date.AddHours(offSet).Date
                        && x.Date.AddHours(offSet).Date <= DateTime.UtcNow.Date);
            }
            else if (dateFrom == null && dateTo != null)
            {
                query = query
                    .Where(x => dateTo.Value.AddDays(-30).Date <= x.Date.AddHours(offSet).Date
                        && x.Date.AddHours(offSet).Date <= dateTo.Value.Date);
            }
            else if (dateTo == null && dateFrom != null)
            {
                query = query
                    .Where(x => dateFrom.Value.Date <= x.Date.AddHours(offSet).Date
                        && x.Date.AddHours(offSet).Date <= dateFrom.Value.AddDays(30).Date);
            }
            else
            {
                query = query
                    .Where(x => dateFrom.Value.Date <= x.Date.AddHours(offSet).Date
                        && x.Date.AddHours(offSet).Date <= dateTo.Value.Date);
            }

            doSales = query.Select(x => new DOSalesViewModel()
            {
                Code = x.Code,
                StorageName = x.StorageName,
                ProductionOrderNo = x.ProductionOrderNo,
                BuyerName = x.BuyerName,
                Construction = x.Construction,
                Date = x.Date,
                DOSalesDetails = x.DOSalesDetails.Select(y => new DOSalesDetailViewModel()
                {
                    UnitName = y.UnitName,
                    UnitCode = y.UnitCode,
                    Weight = y.Weight,
                    Length = y.Length,
                    Quantity = y.Quantity,
                    Remark = y.Remark,
                    LastModifiedUtc = y.LastModifiedUtc
                }).ToList(),
                LastModifiedUtc = x.LastModifiedUtc

            });

            return doSales.ToList();
        }

        public ReadResponse<DOSalesViewModel> GetReport(int page, int size, string code, int productionOrderId, DateTime? dateFrom, DateTime? dateTo, int offSet)
        {
            var queries = GetReport(code, productionOrderId, dateFrom, dateTo, offSet);

            Pageable<DOSalesViewModel> pageable = new Pageable<DOSalesViewModel>(queries, page - 1, size);
            List<DOSalesViewModel> data = pageable.Data.ToList();

            return new ReadResponse<DOSalesViewModel>(queries, pageable.TotalCount, new Dictionary<string, string>(), new List<string>());
        }

        public ReadResponse<DOSalesModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<DOSalesModel> query = (from doSales in dbContext.DOSalesItems
                                              where doSales.IsDeleted.Equals(false)
                                              select doSales).Include("DOSalesDetails");

            List<string> searchAttributes = new List<string>()
            {
                "Code", "StorageName", "BuyerName", "ProductionOrderNo", "Construction"
            };
            query = QueryHelper<DOSalesModel>.Search(query, searchAttributes, keyword);

            Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DOSalesModel>.Filter(query, filterDictionary);

            List<string> selectedFields = new List<string>()
            {

                "Id", "Code", "Date", "StorageName", "BuyerName", "DOSalesDetails", "ProductionOrderNo", "Construction", "Accepted", "MaterialWidthFinish", "PackingUom"

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

        //private async Task CreateProduct(DOSalesModel model)
        //{
        //    var client = (IHttpClientService)ServiceProvider.GetService(typeof(IHttpClientService));
        //    var uri = string.Format("{0}{1}", Utilities.APIEndpoint.Core, "master/products/do-sales/create");
        //    var myContentJson = JsonConvert.SerializeObject(model);
        //    var myContent = new StringContent(myContentJson, Encoding.UTF8, "application/json");
        //    var response = await client.PostAsync(uri, myContent);
        //    response.EnsureSuccessStatusCode();
        //}
    }
}
