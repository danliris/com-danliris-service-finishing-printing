using Com.Danliris.Service.Finishing.Printing.Lib.Models.Packing;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.PackingReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.Utilities;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Inventory;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.PackingReceipt
{
    public class PackingReceiptLogic : BaseLogic<PackingReceiptModel>
    {
        private const string UserAgent = "production-service";
        private readonly ProductionDbContext dbContext;
        private readonly DbSet<PackingReceiptModel> dbSet;
        private readonly DbSet<PackingReceiptItem> dbSetItem;

        private readonly DbSet<PackingModel> dbSetPacking;
        public PackingReceiptLogic(IIdentityService identityService, ProductionDbContext dbContext) : base(identityService, dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<PackingReceiptModel>();
            this.dbSetItem = dbContext.Set<PackingReceiptItem>();

            this.dbSetPacking = dbContext.Set<PackingModel>();
        }

        public override void CreateModel(PackingReceiptModel model)
        {
            model.ReferenceNo = $"RFNO-{model.Code}";
            model.ReferenceType = $"Penerimaan Packing {model.StorageName}";
            model.Type = "IN";
            foreach (var item in model.Items)
            {
                EntityExtension.FlagForCreate(item, IdentityService.Username, UserAgent);
            }
            EntityExtension.FlagForCreate(model, IdentityService.Username, UserAgent);
            dbSet.Add(model);
        }

        public async Task UpdatePacking(PackingReceiptModel model, bool flag)
        {
            try
            {
                var result = await dbSetPacking.Where(d => d.Id.Equals(model.PackingId)).SingleOrDefaultAsync();
                if (result != null)
                {

                    result.Accepted = flag;
                    dbSetPacking.Update(result);
                }
            }
            catch (Exception ex)
            {

                throw new System.ArgumentException(ex.Message);
            }
        }

        //public async Task CreateInventory(PackingReceiptModel model)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        var uri = new Uri(string.Format("{0}{1}", APIEndpoint.Inventory, "inventory-documents"));
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", IdentityService.Token);

        //        InventoryDocumentViewModel inventoryDoc = new InventoryDocumentViewModel();
        //        inventoryDoc.referenceNo = "RFNO" + " - " + model.Code;
        //        string referenceType = string.IsNullOrWhiteSpace(model.StorageName) ? model.StorageName : "";
        //        inventoryDoc.referenceType = $"Penerimaan Packing {referenceType}";
        //        inventoryDoc.remark = " ";
        //        inventoryDoc.type = "IN";
        //        inventoryDoc.date = DateTime.UtcNow;
        //        inventoryDoc.storageId = (model.StorageId);
        //        inventoryDoc.storageCode = (model.StorageCode);
        //        inventoryDoc.storageName = model.StorageName;

        //        inventoryDoc.items = new List<InventoryDocumentItemViewModel>();

        //        foreach (var item in model.Items)
        //        {
        //            var data = new InventoryDocumentItemViewModel();
        //            data.productCode = item.ProductCode;
        //            data.productName = item.Product;
        //            data.productId = item.ProductId;
        //            data.remark = item.Remark;
        //            data.quantity = item.Quantity;
        //            data.uomId = item.UomId;
        //            data.stockPlanning = 0;
        //            data.uom = item.Uom;
        //            inventoryDoc.items.Add(data);
        //        }

        //        var myContentJson = JsonConvert.SerializeObject(inventoryDoc, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        //        var myContent = new StringContent(myContentJson, Encoding.UTF8, "application/json");
        //        var response = await client.PostAsync(uri, myContent);
        //        response.EnsureSuccessStatusCode();
        //    }
        //}

        //public async Task UpdateInventory(PackingReceiptModel model)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        var uri = new Uri(string.Format("{0}{1}", APIEndpoint.Inventory, "inventory-documents"));
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", IdentityService.Token);

        //        InventoryDocumentViewModel inventoryDoc = new InventoryDocumentViewModel();
        //        string referenceType = string.IsNullOrWhiteSpace(model.StorageName) ? model.StorageName : "";
        //        inventoryDoc.referenceType = $"Penerimaan Packing {referenceType}";
        //        inventoryDoc.remark = "VOID PACKING RECEIPT";
        //        inventoryDoc.type = "OUT";
        //        inventoryDoc.date = DateTime.UtcNow;
        //        inventoryDoc.storageId = (model.StorageId);

        //        inventoryDoc.items = new List<InventoryDocumentItemViewModel>();

        //        foreach (var item in model.Items)
        //        {
        //            var data = new InventoryDocumentItemViewModel();
        //            data.productCode = item.ProductCode;
        //            data.productName = item.Product;
        //            data.productId = item.ProductId;
        //            data.remark = item.Remark;
        //            data.quantity = item.Quantity;
        //            data.uomId = item.UomId;
        //            data.stockPlanning = 0;
        //            data.uom = item.Uom;
        //            inventoryDoc.items.Add(data);
        //        }

        //        var myContentJson = JsonConvert.SerializeObject(inventoryDoc);
        //        var myContent = new StringContent(myContentJson, Encoding.UTF8, "application/json");
        //        var response = await client.PostAsync(uri, myContent);
        //        response.EnsureSuccessStatusCode();
        //    }
        //}

        public override Task<PackingReceiptModel> ReadModelById(int id)
        {
            return dbSet.Include(res => res.Items).FirstOrDefaultAsync(d => d.Id.Equals(id) && d.IsDeleted.Equals(false));
        }

        public override async Task DeleteModel(int id)
        {
            var model = await ReadModelById(id);

            foreach (var item in model.Items)
            {
                EntityExtension.FlagForDelete(item, IdentityService.Username, UserAgent);
            }

            EntityExtension.FlagForDelete(model, IdentityService.Username, UserAgent, true);
            DbSet.Update(model);
        }

        public override async Task UpdateModelAsync(int id, PackingReceiptModel model)
        {
            if (model.Items != null)
            {
                HashSet<int> ItemId = dbSetItem.Where(d => d.PackingReceiptId == model.Id).Select(d => d.Id).ToHashSet(); ;
                foreach (var itemId in ItemId)
                {
                    PackingReceiptItem data = model.Items.FirstOrDefault(prop => prop.Id.Equals(itemId));
                    if (data == null)
                    {
                        EntityExtension.FlagForDelete(data, IdentityService.Username, UserAgent);
                        dbSetItem.Update(data);
                    }
                    else
                    {
                        EntityExtension.FlagForUpdate(data, IdentityService.Username, UserAgent);
                        dbSetItem.Update(data);
                    }

                    foreach (PackingReceiptItem item in model.Items)
                    {
                        if (item.Id == 0)
                        {
                            EntityExtension.FlagForCreate(item, IdentityService.Username, UserAgent);
                            dbSetItem.Add(item);
                        }

                    }
                }

            }

            EntityExtension.FlagForUpdate(model, IdentityService.Username, UserAgent);
            dbSet.Update(model);
            await Task.CompletedTask;
        }
    }
}
