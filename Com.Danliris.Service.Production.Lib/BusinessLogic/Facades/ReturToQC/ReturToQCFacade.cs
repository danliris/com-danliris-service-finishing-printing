using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.ReturToQC;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.ReturToQC;
using Com.Danliris.Service.Finishing.Printing.Lib.Helpers;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.ReturToQC;
using Com.Danliris.Service.Finishing.Printing.Lib.Services.HttpClientService;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Inventory;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.ReturToQC;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Utilities;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Sales.FinishingPrinting;
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

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.ReturToQC
{
    public class ReturToQCFacade : IReturToQCFacade
    {
        private readonly ProductionDbContext dbContext;
        private readonly DbSet<ReturToQCModel> dbSet;
        private readonly ReturToQCLogic returToQCLogic;
        private readonly IServiceProvider ServiceProvider;

        public ReturToQCFacade(IServiceProvider serviceProvider, ProductionDbContext dbContext)
        {
            ServiceProvider = serviceProvider;
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<ReturToQCModel>();
            this.returToQCLogic = serviceProvider.GetService<ReturToQCLogic>();
        }

        public async Task<int> CreateAsync(ReturToQCModel model)
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    do
                    {
                        model.ReturNo = CodeGenerator.Generate();
                    }
                    while (dbSet.Any(d => d.ReturNo.Equals(model.ReturNo)));

                    returToQCLogic.CreateModel(model);
                    var id = await dbContext.SaveChangesAsync();

                    if (model.ReturToQCItems.Count > 0)
                    {
                        await CreateInventoryDocument(model);
                    }
                    transaction.Commit();
                    return id;

                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }

        }

        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                await returToQCLogic.DeleteModel(id);
                return await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public MemoryStream GenerateExcel(DateTime? dateFrom, DateTime? dateTo, string productionOrderNo, string returNo, string destination, string deliveryOrderNo, int offSet)
        {
            var data = GetReport(dateFrom, dateTo, productionOrderNo, returNo, destination, deliveryOrderNo, offSet);

            data = data.OrderByDescending(x => x.LastModifiedUtc).ToList();

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No. Retur", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Tanggal Retur", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Tujuan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No. DO", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Keterangan Retur", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Kode Barang", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No. Order", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Nama Barang", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Keterangan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jumlah Retur", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Panjang (Meter)", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Berat (Kg)", DataType = typeof(string) });

            if (data.Count == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "", "", "", "", null, "", null, null);
            }
            else
            {
                int index = 1;
                foreach (var returToQC in data)
                {
                    foreach (var item in returToQC.Items)
                    {
                        foreach (var detail in item.Details)
                        {
                            dt.Rows.Add(index++, returToQC.ReturNo, returToQC.Date.AddHours(offSet).ToString("dd/MM/yyyy"), returToQC.Destination,
                                returToQC.DeliveryOrderNo, returToQC.Remark, returToQC.FinishedGoodCode, item.ProductionOrder.OrderNo, detail.ProductName,
                                detail.Remark, detail.ReturQuantity.ToString("0.##"), detail.UOMUnit, detail.Length.ToString("0.##"), detail.Weight.ToString("0.##"));
                        }
                    }
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "ReturToQC") }, true);
        }

        public ReadResponse<ReturToQCViewModel> GetReport(int page, int size, DateTime? dateFrom, DateTime? dateTo, string productionOrderNo, string returNo, string destination, string deliveryOrderNo, int offSet)
        {
            var queries = GetReport(dateFrom, dateTo, productionOrderNo, returNo, destination, deliveryOrderNo, offSet);

            Pageable<ReturToQCViewModel> pageable = new Pageable<ReturToQCViewModel>(queries, page - 1, size);
            List<ReturToQCViewModel> data = pageable.Data.ToList();

            return new ReadResponse<ReturToQCViewModel>(queries, pageable.TotalCount, new Dictionary<string, string>(), new List<string>());
        }

        public List<ReturToQCViewModel> GetReport(DateTime? dateFrom, DateTime? dateTo, string productionOrderNo, string returNo, string destination, string deliveryOrderNo, int offSet)
        {
            dbContext.ReturToQCItems.Load();
            dbContext.ReturToQCItemDetails.Load();
            IQueryable<ReturToQCModel> query = dbContext.ReturToQCs.Where(x => !x.IsVoid).AsQueryable();


            if (!string.IsNullOrEmpty(returNo))
                query = query.Where(x => x.ReturNo == returNo);


            if (!string.IsNullOrEmpty(destination))
                query = query.Where(x => x.Destination == destination);

            if (!string.IsNullOrEmpty(deliveryOrderNo))
                query = query.Where(x => x.DeliveryOrderNo == deliveryOrderNo);

            if (!string.IsNullOrEmpty(productionOrderNo))
                query = query.Where(x => x.ReturToQCItems.Any(y => y.ProductionOrderNo == productionOrderNo));


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


            return query.Select(x => new ReturToQCViewModel()
            {
                Active = x.Active,
                Date = x.Date,
                DeliveryOrderNo = x.DeliveryOrderNo,
                Destination = x.Destination,
                FinishedGoodCode = x.FinishedGoodCode,
                Id = x.Id,
                IsVoid = x.IsVoid,
                IsDeleted = x.IsDeleted,
                LastModifiedUtc = x.LastModifiedUtc,
                Material = new MaterialIntegrationViewModel()
                {
                    Name = x.MaterialName,
                    Code = x.MaterialCode,
                    Id = x.MaterialId
                },
                MaterialConstruction = new MaterialConstructionIntegrationViewModel()
                {
                    Code = x.MaterialConstructionCode,
                    Id = x.MaterialConstructionId,
                    Name = x.MaterialConstructionName
                },
                MaterialWidthFinish = x.MaterialWidthFinish,
                ReturNo = x.ReturNo,
                Remark = x.Remark,
                Items = x.ReturToQCItems.Select(y => new ReturToQCItemViewModel()
                {
                    Active = y.Active,
                    Id = y.Id,
                    IsDeleted = y.IsDeleted,
                    LastModifiedUtc = y.LastModifiedUtc,
                    ProductionOrder = new ProductionOrderIntegrationViewModel()
                    {
                        OrderNo = y.ProductionOrderNo,
                        Id = y.ProductionOrderId,
                        Code = y.ProductionOrderCode
                    },
                    Details = y.ReturToQCItemDetails.Select(z => new ReturToQCItemDetailViewModel()
                    {
                        Active = z.Active,
                        ColorWay = z.ColorWay,
                        DesignCode = z.DesignCode,
                        DesignNumber = z.DesignNumber,
                        Id = z.Id,
                        IsDeleted = z.IsDeleted,
                        LastModifiedUtc = z.LastModifiedUtc,
                        Length = z.Length,
                        ProductCode = z.ProductCode,
                        ProductId = z.ProductId,
                        ProductName = z.ProductName,
                        QuantityBefore = z.QuantityBefore,
                        Remark = z.Remark,
                        ReturQuantity = z.ReturQuantity,
                        StorageCode = z.StorageCode,
                        StorageId = z.StorageId,
                        StorageName = z.StorageName,
                        UOMId = z.UOMId,
                        UOMUnit = z.UOMUnit,
                        Weight = z.Weight
                    }).ToList()
                }).ToList()
            }).ToList();

        }

        public ReadResponse<ReturToQCModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            //throw new NotImplementedException();
            try
            {
                dbContext.ReturToQCItems.Load();
                dbContext.ReturToQCItemDetails.Load();
                IQueryable<ReturToQCModel> query = dbContext.ReturToQCs.Where(d =>
                    !d.IsDeleted
                    && (d.ReturToQCItems.Count == 0 || (d.ReturToQCItems.Count > 0 && d.ReturToQCItems.Any(e =>
                        !e.IsDeleted
                        && (e.ReturToQCItemDetails.Count == 0 || (e.ReturToQCItemDetails.Count > 0 && e.ReturToQCItemDetails.Any(f =>
                            !f.IsDeleted)))))));

                List<string> searchAttributes = new List<string>()
            {
                "ReturNo", "DeliveryOrderNo", "MaterialName", "MaterialConstructionName", "Destination"
            };
                query = QueryHelper<ReturToQCModel>.Search(query, searchAttributes, keyword);

                Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
                query = QueryHelper<ReturToQCModel>.Filter(query, filterDictionary);

                List<string> selectedFields = new List<string>()
            {
                "Id", "ReturNo", "Date", "DeliveryOrderNo", "Destination", "FinishedGoodCode", "IsVoid", "Material",
                "MaterialWidthFinish" , "Remark", "MaterialConstruction"
            };

                Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
                query = QueryHelper<ReturToQCModel>.Order(query, orderDictionary);

                Pageable<ReturToQCModel> pageable = new Pageable<ReturToQCModel>(query, page - 1, size);
                List<ReturToQCModel> data = pageable.Data.ToList();
                int totalData = pageable.TotalCount;

                return new ReadResponse<ReturToQCModel>(data, totalData, orderDictionary, selectedFields);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<ReturToQCModel> ReadByIdAsync(int id)
        {
            return await returToQCLogic.ReadModelById(id);
        }

        public async Task<int> UpdateAsync(int id, ReturToQCModel model)
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    await returToQCLogic.UpdateModelAsync(id, model);
                    var modelID = await dbContext.SaveChangesAsync();

                    if (model.ReturToQCItems.Count > 0)
                    {
                        await CreateInventoryDocument(model);
                    }

                    transaction.Commit();
                    return modelID;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }
        }

        private async Task CreateInventoryDocument(ReturToQCModel model)
        {
            var client = ServiceProvider.GetService<IHttpClientService>();
            string relativePath = "inventory-documents/multi";
            try
            {
                //Uri serverUri = new Uri(Utilities.APIEndpoint.Inventory);
                //Uri relativePathUri = new Uri(relativePath, UriKind.Relative);
                //var uri = new Uri(serverUri, relativePathUri);
                var uri = string.Format("{0}{1}", Utilities.APIEndpoint.Inventory, "inventory-documents/multi");
                var listContainer = new List<StringContent>();
                if (model.ReturToQCItems != null && model.ReturToQCItems.Count != 0)
                {
                    List<InventoryDocumentViewModel> postedModels = new List<InventoryDocumentViewModel>();
                    foreach (var item in model.ReturToQCItems)
                    {
                        InventoryDocumentViewModel inventoryDoc = new InventoryDocumentViewModel
                        {
                            referenceNo = model.ReturNo + " - " + item.ProductionOrderNo,
                            referenceType = "retur-to-qc",
                            remark = "",
                            type = model.IsVoid ? "IN" : "OUT",
                            date = DateTimeOffset.UtcNow
                        };
                        var itemDetails = item.ReturToQCItemDetails.LastOrDefault();

                        if (itemDetails != null)
                        {
                            inventoryDoc.storageId = itemDetails.StorageId;
                            inventoryDoc.storageCode = itemDetails.StorageCode;
                            inventoryDoc.storageName = itemDetails.StorageName;
                        }

                        inventoryDoc.items = item.ReturToQCItemDetails.Select(x => new InventoryDocumentItemViewModel()
                        {
                            productCode = x.ProductCode,
                            productName = x.ProductName,
                            productId = x.ProductId,
                            remark = x.Remark,
                            quantity = x.ReturQuantity,
                            uomId = x.UOMId,
                            uom = x.UOMUnit,
                        }).ToList();

                        postedModels.Add(inventoryDoc);
                    }
                    var myContentJson = JsonConvert.SerializeObject(postedModels, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    var myContent = new StringContent(myContentJson, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(uri.ToString(), myContent);
                    response.EnsureSuccessStatusCode();
                }
            }
            catch (UriFormatException)
            {
                throw new UriFormatException(string.Format("Error : {0}, {1}", Utilities.APIEndpoint.Inventory, relativePath));
            }
            catch (Exception)
            {
                throw new Exception(string.Format("Error : {0}, {1}", Utilities.APIEndpoint.Inventory, relativePath));
            }

        }
    }
}
