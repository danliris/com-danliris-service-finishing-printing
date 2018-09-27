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
            foreach (var item in model.Items)
            {
                EntityExtension.FlagForCreate(item, IdentityService.Username, UserAgent);
            }
            EntityExtension.FlagForCreate(model, IdentityService.Username, UserAgent);
            dbSet.Add(model);
        }

        public async Task UpdatePacking(PackingReceiptModel model)
        {
            try
            {
                var result = await dbSetPacking.Where(d => d.Id.Equals(model.PackingId)).SingleOrDefaultAsync();
                if (result != null)
                {
                    result.Accepted = true;
                    dbSetPacking.Update(result);
                }
            }
            catch (Exception ex)
            {

                throw new System.ArgumentException(ex.Message);
            }
        }

        public async Task CreateInventory(PackingReceiptModel model)
        {
            using (var client = new HttpClient())
            {
                var uri = new Uri(string.Format("{0}{1}", APIEndpoint.Inventory, "inventory-documents"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", IdentityService.Token);

                //ProductCode = item.productCode,
                //    ProductId = item.productId,
                //    ProductName = item.productName,
                //    ProductRemark = item.remark,
                //    Quantity = item.quantity,
                //    StockPlanning = item.stockPlanning,
                //    UomId = item.uomId,
                //    UomUnit = item.uom,

                InventoryDocumentViewModel inventoryDoc = new InventoryDocumentViewModel();
                inventoryDoc.referenceNo = "RFNO" + " - " + model.Code;
                string referenceType = string.IsNullOrWhiteSpace(model.StorageName) ? model.StorageName : "";
                inventoryDoc.referenceType = $"Penerimaan Packing {referenceType}";
                inventoryDoc.remark = " ";
                inventoryDoc.type = "IN";
                inventoryDoc.date = new DateTime().ToString();
                inventoryDoc.storageId = Convert.ToString(model.StorageId);

                inventoryDoc.items = new List<object>();

                foreach (var item in model.Items)
                {
                    var data = new
                    {
                        productCode="",
                        productName = item.Product,
                        productId= item.ProductId,
                        remark=item.Remark,
                        quantity=item.Quantity,
                        uomId=item.UomId,
                        stockPlanning=0,
                        uom="",
                    };
                    inventoryDoc.items.Add(data);
                }

                var myContentJson = JsonConvert.SerializeObject(inventoryDoc);
                var myContent = new StringContent(myContentJson, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uri, myContent);
                response.EnsureSuccessStatusCode();
            }
        }


    }
}
