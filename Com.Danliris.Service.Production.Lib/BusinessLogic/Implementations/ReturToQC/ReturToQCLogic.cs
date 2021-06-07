using Com.Danliris.Service.Finishing.Printing.Lib.Models.ReturToQC;
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
using System.Threading;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.ReturToQC
{
    public class ReturToQCLogic : BaseLogic<ReturToQCModel>
    {
        private const string UserAgent = "production-service";
        private readonly ProductionDbContext dbContext;
        private readonly DbSet<ReturToQCModel> dbSet;
        private readonly DbSet<ReturToQCItemModel> dbSetItem;
        private readonly DbSet<ReturToQCItemDetailModel> dbSetItemDetail;

        public ReturToQCLogic(IIdentityService identityService, ProductionDbContext dbContext) : base(identityService, dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<ReturToQCModel>();
            this.dbSetItem = dbContext.Set<ReturToQCItemModel>();
            this.dbSetItemDetail = dbContext.Set<ReturToQCItemDetailModel>();
        }

        public override void CreateModel(ReturToQCModel model)
        {
            foreach (var item in model.ReturToQCItems)
            {
                EntityExtension.FlagForCreate(item, IdentityService.Username, UserAgent);
                foreach (var detail in item.ReturToQCItemDetails)
                {
                    if (detail.Weight < 0)
                    {
                        detail.Weight = 0;
                    }
                    EntityExtension.FlagForCreate(detail, IdentityService.Username, UserAgent);
                }
            }

            base.CreateModel(model);
        }

        public override async Task DeleteModel(int id)
        {
            var model = await ReadModelById(id);
            EntityExtension.FlagForDelete(model, IdentityService.Username, UserAgent, true);
            foreach (var item in model.ReturToQCItems)
            {
                EntityExtension.FlagForDelete(item, IdentityService.Username, UserAgent);
                foreach (var detail in item.ReturToQCItemDetails)
                {
                    EntityExtension.FlagForDelete(detail, IdentityService.Username, UserAgent);
                }
            }
            DbSet.Update(model);
        }

        public override Task<ReturToQCModel> ReadModelById(int id)
        {
            //return base.ReadModelById(id);
            dbContext.ReturToQCItems.Load();
            dbContext.ReturToQCItemDetails.Load();
            return dbContext.ReturToQCs.FirstOrDefaultAsync(d =>
                    d.Id == id &&
                    !d.IsDeleted
                    && (d.ReturToQCItems.Count == 0 || (d.ReturToQCItems.Count > 0 && d.ReturToQCItems.Any(e =>
                        !e.IsDeleted
                        && (e.ReturToQCItemDetails.Count == 0 || (e.ReturToQCItemDetails.Count > 0 && e.ReturToQCItemDetails.Any(f =>
                            !f.IsDeleted)))))));
        }

        public override async Task UpdateModelAsync(int id, ReturToQCModel model)
        {
            EntityExtension.FlagForUpdate(model, IdentityService.Username, UserAgent);
            foreach (var item in model.ReturToQCItems)
            {
                if (item.Id == 0)
                {
                    EntityExtension.FlagForCreate(item, IdentityService.Username, UserAgent);
                    dbSetItem.Add(item);
                }
                else
                {
                    EntityExtension.FlagForUpdate(item, IdentityService.Username, UserAgent);

                }
                foreach (var detail in item.ReturToQCItemDetails)
                {
                    if (detail.Id == 0)
                    {
                        EntityExtension.FlagForCreate(detail, IdentityService.Username, UserAgent);
                        dbSetItemDetail.Add(detail);
                    }
                    else
                    {
                        EntityExtension.FlagForUpdate(detail, IdentityService.Username, UserAgent);

                    }
                }
            }
            DbSet.Update(model);
            await Task.CompletedTask;
        }

        //public async Task CreateInventoryDocument(ReturToQCModel model)
        //{
        //    using (var client = new HttpClient() { Timeout = Timeout.InfiniteTimeSpan })
        //    {
        //        string relativePath = "inventory-documents/multi";
        //        try
        //        {
        //            Uri serverUri = new Uri(APIEndpoint.Inventory);
        //            Uri relativePathUri = new Uri(relativePath, UriKind.Relative);
        //            var uri = new Uri(serverUri, relativePathUri);

        //            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", IdentityService.Token);
        //            var listContainer = new List<StringContent>();
        //            if (model.ReturToQCItems != null && model.ReturToQCItems.Count != 0)
        //            {
        //                List<InventoryDocumentViewModel> postedModels = new List<InventoryDocumentViewModel>();
        //                foreach (var item in model.ReturToQCItems)
        //                {
        //                    InventoryDocumentViewModel inventoryDoc = new InventoryDocumentViewModel
        //                    {
        //                        referenceNo = model.ReturNo + " - " + item.ProductionOrderNo,
        //                        referenceType = "retur-to-qc",
        //                        remark = "",
        //                        type = model.IsVoid ? "IN" : "OUT",
        //                        date = DateTimeOffset.UtcNow
        //                    };
        //                    var itemDetails = item.ReturToQCItemDetails.LastOrDefault();

        //                    if (itemDetails != null)
        //                    {
        //                        inventoryDoc.storageId = itemDetails.StorageId;
        //                        inventoryDoc.storageCode = itemDetails.StorageCode;
        //                        inventoryDoc.storageName = itemDetails.StorageName;
        //                    }

        //                    inventoryDoc.items = item.ReturToQCItemDetails.Select(x => new InventoryDocumentItemViewModel()
        //                    {
        //                        productCode = x.ProductCode,
        //                        productName = x.ProductName,
        //                        productId = x.ProductId,
        //                        remark = x.Remark,
        //                        quantity = x.ReturQuantity,
        //                        uomId = x.UOMId,
        //                        uom = x.UOMUnit,
        //                    }).ToList();

        //                    postedModels.Add(inventoryDoc);
        //                }
        //                var myContentJson = JsonConvert.SerializeObject(postedModels, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        //                var myContent = new StringContent(myContentJson, Encoding.UTF8, "application/json");
        //                var response = await client.PostAsync(uri, myContent);
        //                response.EnsureSuccessStatusCode();
        //            }
        //        }
        //        catch (UriFormatException)
        //        {
        //            throw new UriFormatException(string.Format("Error : {0}, {1}", APIEndpoint.Inventory, relativePath));
        //        }
        //        catch (Exception)
        //        {
        //            throw new Exception(string.Format("Error : {0}, {1}", APIEndpoint.Inventory, relativePath));
        //        }
        //    }
        //}
    }
}
