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
using Com.Danliris.Service.Finishing.Printing.Lib.Services.HttpClientService;
using Com.Danliris.Service.Finishing.Printing.Lib.Utilities;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Inventory;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.PackingReceipt
{
    public class PackingReceiptFacade : IPackingReceiptFacade
    {
        private readonly ProductionDbContext dbContext;
        private readonly DbSet<PackingReceiptModel> dbSet;
        private readonly PackingReceiptLogic packingReceiptLogic;
        private readonly IServiceProvider ServiceProvider;

        public PackingReceiptFacade(IServiceProvider serviceProvider, ProductionDbContext dbContext)
        {
            this.dbContext = dbContext;
            ServiceProvider = serviceProvider;
            this.dbSet = dbContext.Set<PackingReceiptModel>();
            this.packingReceiptLogic = serviceProvider.GetService<PackingReceiptLogic>();
        }

        public async Task<int> CreateAsync(PackingReceiptModel model)
        {
            do
            {
                model.Code = CodeGenerator.Generate();
            }
            while (dbSet.Any(d => d.Code.Equals(model.Code)));
            this.packingReceiptLogic.CreateModel(model);

            await this.packingReceiptLogic.UpdatePacking(model, true);
            await CreateInventory(model);

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
                    "Id", "Code", "Buyer", "Storage", "PackingCode", "LastModifiedUtc", "Date", "ProductionOrderNo", "ColorName", "Construction", "CreatedBy", "Items", "ReferenceType", "ReferenceNo"
                };

            query = query
                    .Select(field => new PackingReceiptModel
                    {
                        Id = field.Id,
                        Code = field.Code,
                        Buyer = field.Buyer,
                        StorageName = field.StorageName,
                        StorageCode = field.StorageCode,
                        StorageId = field.StorageId,
                        PackingCode = field.PackingCode,
                        Date = field.Date,
                        ProductionOrderNo = field.ProductionOrderNo,
                        ColorName = field.ColorName,
                        Construction = field.Construction,
                        LastModifiedUtc = field.LastModifiedUtc,
                        ReferenceNo = field.ReferenceNo,
                        ReferenceType = field.ReferenceType,
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
            await packingReceiptLogic.UpdateModelAsync(id, model);
            await this.packingReceiptLogic.UpdatePacking(model, false);
            await this.UpdateInventory(model);
            return await dbContext.SaveChangesAsync();
        }

        private async Task CreateInventory(PackingReceiptModel model)
        {
            var client = ServiceProvider.GetService<IHttpClientService>();

            var uri = string.Format("{0}{1}", APIEndpoint.Inventory, "inventory-documents");

            InventoryDocumentViewModel inventoryDoc = new InventoryDocumentViewModel();
            inventoryDoc.referenceNo = "RFNO" + " - " + model.Code;
            string referenceType = string.IsNullOrWhiteSpace(model.StorageName) ? model.StorageName : "";
            inventoryDoc.referenceType = $"Penerimaan Packing {referenceType}";
            inventoryDoc.remark = " ";
            inventoryDoc.type = "IN";
            inventoryDoc.date = DateTime.UtcNow;
            inventoryDoc.storageId = (model.StorageId);
            inventoryDoc.storageCode = (model.StorageCode);
            inventoryDoc.storageName = model.StorageName;

            inventoryDoc.items = new List<InventoryDocumentItemViewModel>();

            foreach (var item in model.Items)
            {
                var data = new InventoryDocumentItemViewModel
                {
                    productCode = item.ProductCode,
                    productName = item.Product,
                    productId = item.ProductId,
                    remark = item.Remark,
                    quantity = item.Quantity,
                    uomId = item.UomId,
                    stockPlanning = 0,
                    uom = item.Uom
                };
                inventoryDoc.items.Add(data);
            }

            var myContentJson = JsonConvert.SerializeObject(inventoryDoc, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var myContent = new StringContent(myContentJson, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, myContent);
            response.EnsureSuccessStatusCode();

        }

        private async Task UpdateInventory(PackingReceiptModel model)
        {
            var client = ServiceProvider.GetService<IHttpClientService>();

            var uri = string.Format("{0}{1}", APIEndpoint.Inventory, "inventory-documents");
           
            InventoryDocumentViewModel inventoryDoc = new InventoryDocumentViewModel();
            string referenceType = string.IsNullOrWhiteSpace(model.StorageName) ? model.StorageName : "";
            inventoryDoc.referenceType = $"Penerimaan Packing {referenceType}";
            inventoryDoc.referenceNo = "RFNO" + " - " + model.Code;
            inventoryDoc.remark = "VOID PACKING RECEIPT";
            inventoryDoc.type = "OUT";
            inventoryDoc.date = DateTime.UtcNow;
            inventoryDoc.storageId = (model.StorageId);

            inventoryDoc.items = new List<InventoryDocumentItemViewModel>();

            foreach (var item in model.Items)
            {
                var data = new InventoryDocumentItemViewModel();
                data.productCode = item.ProductCode;
                data.productName = item.Product;
                data.productId = item.ProductId;
                data.remark = item.Remark;
                data.quantity = item.Quantity;
                data.uomId = item.UomId;
                data.stockPlanning = 0;
                data.uom = item.Uom;
                inventoryDoc.items.Add(data);
            }

            var myContentJson = JsonConvert.SerializeObject(inventoryDoc);
            var myContent = new StringContent(myContentJson, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(uri, myContent);
            response.EnsureSuccessStatusCode();

        }
    }
}
