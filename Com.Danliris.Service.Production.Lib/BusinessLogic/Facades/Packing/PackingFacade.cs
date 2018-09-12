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
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Packing;

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
            if (row > 0)
            {
                packingLogic.CreateProduct(model);
            }
            return row;
        }

        public async Task<int> DeleteAsync(int id)
        {
            await packingLogic.DeleteModel(id);
            return await dbContext.SaveChangesAsync();
        }

        public ReadResponse<PackingViewModel> GetReport(int page, int size, string code, string productionOrderNo, DateTime? dateFrom, DateTime? dateTo, int offSet)
        {
            IQueryable<PackingModel> query = dbContext.Packings.Include(x => x.PackingDetails).AsQueryable();

            IEnumerable<PackingViewModel> queries;

            if (!string.IsNullOrEmpty(code))
                query = query.Where(x => x.Code == code);

            if (!string.IsNullOrEmpty(productionOrderNo))
                query = query.Where(x => x.ProductionOrderNo == productionOrderNo);


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

            queries = query.Select(x => new PackingViewModel()
            {
                Code = x.Code,
                DeliveryType = x.DeliveryType,
                ProductionOrderNo = x.ProductionOrderNo,
                OrderTypeName = x.OrderTypeName,
                FinishedProductType = x.FinishedProductType,
                BuyerName = x.BuyerName,
                Construction = x.Construction,
                DesignCode = x.DesignCode,
                ColorName = x.ColorName,
                Date = x.Date,
                PackingDetails = x.PackingDetails.Select(y => new PackingDetailViewModel()
                {
                    Lot = y.Lot,
                    Grade = y.Grade,
                    Weight = y.Weight,
                    Length = y.Length,
                    Quantity = y.Quantity,
                    Remark = y.Remark
                }).ToList()
            });

            Pageable<PackingViewModel> pageable = new Pageable<PackingViewModel>(queries, page - 1, size);
            List<PackingViewModel> data = pageable.Data.ToList();

            return new ReadResponse<PackingViewModel>(data, pageable.TotalCount, new Dictionary<string, string>(), new List<string>());
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
