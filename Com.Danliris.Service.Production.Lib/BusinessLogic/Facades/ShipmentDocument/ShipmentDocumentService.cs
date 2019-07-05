using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.ShipmentDocument;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.ShipmentDocument;
using Com.Danliris.Service.Finishing.Printing.Lib.Services.HttpClientService;
using Com.Danliris.Service.Finishing.Printing.Lib.Utilities;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Inventory;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Utilities;
using Com.Moonlay.Models;
using Com.Moonlay.NetCore.Lib;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.ShipmentDocument
{
    public class ShipmentDocumentService : IShipmentDocumentService
    {
        private const string _UserAgent = "finishing-printing-service";
        private readonly ProductionDbContext _DbContext;
        public readonly DbSet<ShipmentDocumentModel> _DbSet;
        public readonly DbSet<ShipmentDocumentDetailModel> _DetailDbSet;
        public readonly DbSet<ShipmentDocumentItemModel> _ItemDbSet;
        public readonly DbSet<ShipmentDocumentPackingReceiptItemModel> _PackingReceiptItemDbSet;
        public readonly IServiceProvider _ServiceProvider;
        protected IIdentityService _IdentityService;

        public ShipmentDocumentService(IServiceProvider serviceProvider, ProductionDbContext dbContext)
        {
            _ServiceProvider = serviceProvider;
            _DbContext = dbContext;
            _DbSet = _DbContext.Set<ShipmentDocumentModel>();
            _DetailDbSet = _DbContext.Set<ShipmentDocumentDetailModel>();
            _ItemDbSet = _DbContext.Set<ShipmentDocumentItemModel>();
            _PackingReceiptItemDbSet = _DbContext.Set<ShipmentDocumentPackingReceiptItemModel>();
            _IdentityService = _ServiceProvider.GetService<IIdentityService>();
        }
        public async Task<int> CreateAsync(ShipmentDocumentModel model)
        {
            do
            {
                model.Code = CodeGenerator.Generate();
            }
            while (_DbSet.Any(d => d.Code.Equals(model.Code)));

            EntityExtension.FlagForCreate(model, _IdentityService.Username, _UserAgent);
            _DbSet.Add(model);
            foreach (var detail in model.Details)
            {
                EntityExtension.FlagForCreate(detail, _IdentityService.Username, _UserAgent);
                _DetailDbSet.Add(detail);
                foreach (var item in detail.Items)
                {
                    EntityExtension.FlagForCreate(item, _IdentityService.Username, _UserAgent);
                    _ItemDbSet.Add(item);
                    foreach (var packingReceiptItem in item.PackingReceiptItems)
                    {
                        EntityExtension.FlagForCreate(packingReceiptItem, _IdentityService.Username, _UserAgent);
                        _PackingReceiptItemDbSet.Add(packingReceiptItem);
                    }
                }
            }
            //UpdatePackingReceiptQuantity(model);
            await CreateInventoryDocumentOut(model);

            return await _DbContext.SaveChangesAsync();
        }

        private async Task CreateInventoryDocumentIn(ShipmentDocumentModel model)
        {
            string referenceType = string.IsNullOrWhiteSpace(model.StorageName) ? model.StorageName : "";
            var inventoryDoc = new InventoryDocumentViewModel()
            {
                date = model.DeliveryDate,
                referenceType = $"Pengiriman Barang {referenceType}",
                remark = "Void Pengiriman Barang",
                type = "IN",
                storageId = model.StorageId,
                items = new List<InventoryDocumentItemViewModel>()
            };

            foreach (var detail in model.Details)
            {
                foreach (var item in detail.Items)
                {
                    foreach (var packingReceiptItem in item.PackingReceiptItems)
                    {
                        var data = new InventoryDocumentItemViewModel
                        {
                            productCode = packingReceiptItem.ProductCode,
                            productName = packingReceiptItem.ProductName,
                            productId = packingReceiptItem.ProductId,
                            remark = packingReceiptItem.Remark,
                            quantity = packingReceiptItem.Quantity,
                            uomId = packingReceiptItem.UOMId,
                            stockPlanning = 0,
                            uom = packingReceiptItem.UOMUnit
                        };
                        inventoryDoc.items.Add(data);
                    }
                }
            }

            string dailyBankTransactionUri = "inventory-documents";

            var httpClient = (IHttpClientService)_ServiceProvider.GetService(typeof(IHttpClientService));
            var response = await httpClient.PostAsync($"{APIEndpoint.Inventory}{dailyBankTransactionUri}", new StringContent(JsonConvert.SerializeObject(inventoryDoc).ToString(), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
        }

        public async Task<int> DeleteAsync(int id)
        {
            //not implemented
            var result = await _DbSet.Where(w => w.Id.Equals(id)).FirstOrDefaultAsync();
            return result.Id;
        }

        public ReadResponse<ShipmentDocumentModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<ShipmentDocumentModel> Query = _DbSet;

            Query = Query
                .Select(s => new ShipmentDocumentModel
                {
                    Id = s.Id,
                    CreatedUtc = s.CreatedUtc,
                    CreatedBy = s.CreatedBy,
                    Code = s.Code,
                    LastModifiedUtc = s.LastModifiedUtc,
                    DeliveryDate = s.DeliveryDate,
                    BuyerCode = s.BuyerCode,
                    BuyerName = s.BuyerName
                });

            List<string> searchAttributes = new List<string>()
            {
                "Code", "BuyerCode", "BuyerName","CreatedBy"
            };

            Query = QueryHelper<ShipmentDocumentModel>.Search(Query, searchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<ShipmentDocumentModel>.Filter(Query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<ShipmentDocumentModel>.Order(Query, OrderDictionary);

            Pageable<ShipmentDocumentModel> pageable = new Pageable<ShipmentDocumentModel>(Query, page - 1, size);
            List<ShipmentDocumentModel> Data = pageable.Data.ToList();

            List<ShipmentDocumentModel> list = new List<ShipmentDocumentModel>();
            list.AddRange(
               Data.Select(s => new ShipmentDocumentModel
               {
                   Id = s.Id,
                   CreatedUtc = s.CreatedUtc,
                   CreatedBy = s.CreatedBy,
                   Code = s.Code,
                   LastModifiedUtc = s.LastModifiedUtc,
                   DeliveryDate = s.DeliveryDate,
                   BuyerCode = s.BuyerCode,
                   BuyerName = s.BuyerName
               }).ToList()
            );

            int TotalData = pageable.TotalCount;

            return new ReadResponse<ShipmentDocumentModel>(list, TotalData, OrderDictionary, new List<string>());
        }

        public async Task<ShipmentDocumentModel> ReadByIdAsync(int id)
        {
            var Result = await _DbSet.FirstOrDefaultAsync(d => d.Id.Equals(id) && !d.IsDeleted);
            Result.Details = await _DetailDbSet.Where(w => w.ShipmentDocumentId.Equals(id) && !w.IsDeleted).ToListAsync();
            foreach (var detail in Result.Details)
            {
                detail.Items = await _ItemDbSet.Where(w => w.ShipmentDocumentDetailId.Equals(detail.Id) && !w.IsDeleted).ToListAsync();
                foreach (var item in detail.Items)
                {
                    item.PackingReceiptItems = await _PackingReceiptItemDbSet.Where(w => w.ShipmentDocumentItemId.Equals(item.Id) && !w.IsDeleted).ToListAsync();
                }
            }
            return Result;
        }

        //implements Void
        public async Task<int> UpdateAsync(int id, ShipmentDocumentModel model)
        {
            EntityExtension.FlagForUpdate(model, _IdentityService.Username, _UserAgent);
            _DbSet.Update(model);
            model.IsVoid = true;
            foreach (var detail in model.Details)
            {
                EntityExtension.FlagForUpdate(detail, _IdentityService.Username, _UserAgent);
                _DetailDbSet.Update(detail);
                foreach (var item in detail.Items)
                {
                    EntityExtension.FlagForUpdate(item, _IdentityService.Username, _UserAgent);
                    _ItemDbSet.Update(item);
                    foreach (var packingReceiptItem in item.PackingReceiptItems)
                    {
                        EntityExtension.FlagForUpdate(packingReceiptItem, _IdentityService.Username, _UserAgent);
                        _PackingReceiptItemDbSet.Update(packingReceiptItem);
                    }
                }
            }

            await CreateInventoryDocumentIn(model);
            return await _DbContext.SaveChangesAsync();
        }

        private async Task CreateInventoryDocumentOut(ShipmentDocumentModel model)
        {
            string referenceType = string.IsNullOrWhiteSpace(model.StorageName) ? model.StorageName : "";
            var inventoryDoc = new InventoryDocumentViewModel()
            {
                date = model.DeliveryDate,
                referenceType = $"Pengiriman Barang {referenceType}",
                referenceNo = model.Code,
                remark = "Pengiriman Barang",
                type = "OUT",
                storageId = model.StorageId,
                storageCode = model.StorageCode,
                storageName = model.StorageName,
                items = new List<InventoryDocumentItemViewModel>()
            };

            foreach (var detail in model.Details)
            {
                foreach (var item in detail.Items)
                {
                    foreach (var packingReceiptItem in item.PackingReceiptItems)
                    {
                        var data = new InventoryDocumentItemViewModel
                        {
                            productCode = packingReceiptItem.ProductCode,
                            productName = packingReceiptItem.ProductName,
                            productId = packingReceiptItem.ProductId,
                            remark = packingReceiptItem.Remark,
                            quantity = packingReceiptItem.Quantity,
                            uomId = packingReceiptItem.UOMId,
                            stockPlanning = 0,
                            uom = packingReceiptItem.UOMUnit
                        };
                        inventoryDoc.items.Add(data);
                    }
                }
            }

            string uri = "inventory-documents";

            var httpClient = (IHttpClientService)_ServiceProvider.GetService(typeof(IHttpClientService));
            var response = await httpClient.PostAsync($"{APIEndpoint.Inventory}{uri}", new StringContent(JsonConvert.SerializeObject(inventoryDoc).ToString(), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<ShipmentDocumentPackingReceiptItemModel>> GetShipmentProducts(int productionOrderId, int buyerId)
        {
            var shipmentDocumentIds = await _DbSet.Where(w => w.BuyerId.Equals(buyerId)).Select(s => s.Id).ToListAsync();
            var shipmentDocumentDetailIds = await _DetailDbSet.Where(w => w.ProductionOrderId.Equals(productionOrderId) && shipmentDocumentIds.Contains(w.ShipmentDocumentId)).Select(s => s.Id).ToListAsync();
            var shipmentDocumentItemIds = await _ItemDbSet.Where(w => shipmentDocumentDetailIds.Contains(w.ShipmentDocumentDetailId)).Select(s => s.Id).ToListAsync();

            return await _PackingReceiptItemDbSet.Where(w => shipmentDocumentItemIds.Contains(w.ShipmentDocumentItemId)).GroupBy(g => g.ProductId).Select(s => new ShipmentDocumentPackingReceiptItemModel()
            {
                Active = s.First().Active,
                ColorType = s.First().ColorType,
                CreatedAgent = s.First().CreatedAgent,
                CreatedBy = s.First().CreatedBy,
                CreatedUtc = s.First().CreatedUtc,
                DesignCode = s.First().DesignCode,
                DesignNumber = s.First().DesignNumber,
                LastModifiedAgent = s.First().LastModifiedAgent,
                LastModifiedBy = s.First().LastModifiedBy,
                LastModifiedUtc = s.First().LastModifiedUtc,
                Length = s.First().Length,
                ProductCode = s.First().ProductCode,
                ProductId = s.First().ProductId,
                ProductName = s.First().ProductName,
                Quantity = s.Sum(sum => sum.Quantity),
                UOMId = s.First().UOMId,
                UOMUnit = s.First().UOMUnit,
                Weight = s.First().Weight
            }).ToListAsync();
        }
    }
}
