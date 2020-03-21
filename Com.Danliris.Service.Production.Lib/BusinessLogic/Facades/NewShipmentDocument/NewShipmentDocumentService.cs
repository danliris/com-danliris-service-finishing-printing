using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.NewShipmentDocument;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.NewShipmentDocument;
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

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.NewShipmentDocument
{
    public class NewShipmentDocumentService : INewShipmentDocumentService
    {
        private const string _UserAgent = "finishing-printing-service";
        private readonly ProductionDbContext _DbContext;
        public readonly DbSet<NewShipmentDocumentModel> _DbSet;
        public readonly DbSet<NewShipmentDocumentDetailModel> _DetailDbSet;
        public readonly DbSet<NewShipmentDocumentItemModel> _ItemDbSet;
        public readonly DbSet<NewShipmentDocumentPackingReceiptItemModel> _PackingReceiptItemDbSet;
        public readonly IServiceProvider _ServiceProvider;
        protected IIdentityService _IdentityService;

        public NewShipmentDocumentService(IServiceProvider serviceProvider, ProductionDbContext dbContext)
        {
            _ServiceProvider = serviceProvider;
            _DbContext = dbContext;
            _DbSet = _DbContext.Set<NewShipmentDocumentModel>();
            _DetailDbSet = _DbContext.Set<NewShipmentDocumentDetailModel>();
            _ItemDbSet = _DbContext.Set<NewShipmentDocumentItemModel>();
            _PackingReceiptItemDbSet = _DbContext.Set<NewShipmentDocumentPackingReceiptItemModel>();
            _IdentityService = _ServiceProvider.GetService<IIdentityService>();
        }
        public async Task<int> CreateAsync(NewShipmentDocumentModel model)
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

        private async Task CreateInventoryDocumentIn(NewShipmentDocumentModel model)
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

        public ReadResponse<NewShipmentDocumentModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<NewShipmentDocumentModel> Query = _DbSet.Include(x => x.Details).ThenInclude(x => x.Items).ThenInclude(x => x.PackingReceiptItems);


            List<string> searchAttributes = new List<string>()
            {
                "Code", "BuyerCode", "BuyerName","CreatedBy"
            };

            Query = QueryHelper<NewShipmentDocumentModel>.Search(Query, searchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<NewShipmentDocumentModel>.Filter(Query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<NewShipmentDocumentModel>.Order(Query, OrderDictionary);

            Pageable<NewShipmentDocumentModel> pageable = new Pageable<NewShipmentDocumentModel>(Query, page - 1, size);
            List<NewShipmentDocumentModel> Data = pageable.Data.ToList();

            List<NewShipmentDocumentModel> list = new List<NewShipmentDocumentModel>();
            list.AddRange(
               Data.Select(s => new NewShipmentDocumentModel
               {
                   Id = s.Id,
                   CreatedUtc = s.CreatedUtc,
                   CreatedBy = s.CreatedBy,
                   Code = s.Code,
                   LastModifiedUtc = s.LastModifiedUtc,
                   DeliveryCode = s.DeliveryCode,
                   DeliveryDate = s.DeliveryDate,
                   BuyerId = s.BuyerId,
                   BuyerAddress = s.BuyerAddress,
                   BuyerNPWP = s.BuyerNPWP,
                   BuyerCode = s.BuyerCode,
                   BuyerName = s.BuyerName,
                   Details = s.Details.Select(t => new NewShipmentDocumentDetailModel
                   {
                       Items = t.Items.Select(u => new NewShipmentDocumentItemModel
                       {
                           PackingReceiptItems = u.PackingReceiptItems.Select(v => new NewShipmentDocumentPackingReceiptItemModel
                           {
                               ProductName = v.ProductName,
                               ProductCode = v.ProductCode,
                               Quantity = v.Quantity,
                               Weight = v.Weight,
                               Length = v.Length,
                               UOMId = v.UOMId,
                               UOMUnit = v.UOMUnit,

                           }).ToList(),
                       }).ToList(),
                   }).ToList(),
               }).ToList()
            );

            int TotalData = pageable.TotalCount;

            return new ReadResponse<NewShipmentDocumentModel>(list, TotalData, OrderDictionary, new List<string>());
        }

        public async Task<NewShipmentDocumentModel> ReadByIdAsync(int id)
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
        public async Task<int> UpdateAsync(int id, NewShipmentDocumentModel model)
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

        private async Task CreateInventoryDocumentOut(NewShipmentDocumentModel model)
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

        public async Task<List<NewShipmentDocumentPackingReceiptItemModel>> GetShipmentProducts(int productionOrderId, int buyerId)
        {
            var shipmentDocumentIds = await _DbSet.Where(w => w.BuyerId.Equals(buyerId)).Select(s => s.Id).ToListAsync();
            var shipmentDocumentDetailIds = await _DetailDbSet.Where(w => w.ProductionOrderId.Equals(productionOrderId) && shipmentDocumentIds.Contains(w.ShipmentDocumentId)).Select(s => s.Id).ToListAsync();
            var shipmentDocumentItemIds = await _ItemDbSet.Where(w => shipmentDocumentDetailIds.Contains(w.ShipmentDocumentDetailId)).Select(s => s.Id).ToListAsync();

            return await _PackingReceiptItemDbSet.Where(w => shipmentDocumentItemIds.Contains(w.ShipmentDocumentItemId)).GroupBy(g => g.ProductId).Select(s => new NewShipmentDocumentPackingReceiptItemModel()
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
