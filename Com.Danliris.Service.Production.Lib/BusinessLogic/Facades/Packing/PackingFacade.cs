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
using System.IO;
using System.Data;
using Com.Danliris.Service.Finishing.Printing.Lib.Helpers;

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

        public MemoryStream GenerateExcel(string code, int productionOrderId, DateTime? dateFrom, DateTime? dateTo, int offSet)
        {
            var data = GetReport(code, productionOrderId, dateFrom, dateTo, offSet);

            data = data.OrderByDescending(x => x.LastModifiedUtc).ToList();

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Nomor Packing", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jenis Penyerahan", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Nomor Order", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jenis Order", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jenis Barang Jadi", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Buyer", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Konstruksi", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Motif", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Warna yang diminta", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Tanggal", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Lot", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Grade", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Berat(kg)", DataType = typeof(Int32) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Panjang(m)", DataType = typeof(Int32) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Quantity", DataType = typeof(Int32) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Total", DataType = typeof(Int32) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Keterangan", DataType = typeof(String) });


            if (data.Count == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", 0, 0, 0, 0, "");
            }
            else
            {
                int index = 1;
                foreach (var item in data)
                {
                    foreach (var detail in item.PackingDetails)
                    {
                        dt.Rows.Add(index++, item.Code, item.DeliveryType, item.ProductionOrderNo, item.OrderTypeName, item.FinishedProductType,
                            item.BuyerName, item.Construction, item.DesignCode, item.ColorName, item.Date.AddHours(offSet).ToString("dd/MM/yyyy"),
                            detail.Lot, detail.Grade, detail.Weight, detail.Length, detail.Quantity, (detail.Length * detail.Quantity), detail.Remark);
                    }
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Packing") }, true);


        }

        public List<PackingViewModel> GetReport(string code, int productionOrderId, DateTime? dateFrom, DateTime? dateTo, int offSet)
        {
            IQueryable<PackingModel> query = dbContext.Packings.Include(x => x.PackingDetails).AsQueryable();

            IEnumerable<PackingViewModel> packings;

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

            packings = query.Select(x => new PackingViewModel()
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
                    Remark = y.Remark,
                    LastModifiedUtc = y.LastModifiedUtc
                }).ToList(),
                LastModifiedUtc = x.LastModifiedUtc

            });

            return packings.ToList();
        }

        public ReadResponse<PackingViewModel> GetReport(int page, int size, string code, int productionOrderId, DateTime? dateFrom, DateTime? dateTo, int offSet)
        {
            var queries = GetReport(code, productionOrderId, dateFrom, dateTo, offSet);

            Pageable<PackingViewModel> pageable = new Pageable<PackingViewModel>(queries, page - 1, size);
            List<PackingViewModel> data = pageable.Data.ToList();

            return new ReadResponse<PackingViewModel>(queries, pageable.TotalCount, new Dictionary<string, string>(), new List<string>());
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
                "Id", "Code", "Date", "PackingDetails","OrderTypeName","BuyerName", "ProductionOrderNo", "ColorName","ColorType", "Construction", "DesignNumber", "Accepted","MaterialWidthFinish","PackingUom"
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
