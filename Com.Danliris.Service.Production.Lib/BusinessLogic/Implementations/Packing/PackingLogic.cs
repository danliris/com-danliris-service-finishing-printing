using Com.Danliris.Service.Finishing.Printing.Lib.Models.Packing;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Com.Danliris.Service.Finishing.Printing.Lib.Utilities;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Packing
{
    public class PackingLogic : BaseLogic<PackingModel>
    {
        private const string UserAgent = "production-service";
        private readonly ProductionDbContext dbContext;
        private readonly DbSet<PackingModel> dbSet;
        private readonly DbSet<PackingDetailModel> dbSetDetail;

        public PackingLogic(IIdentityService identityService, ProductionDbContext dbContext) : base(identityService, dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<PackingModel>();
            this.dbSetDetail = dbContext.Set<PackingDetailModel>();
        }

        public override void CreateModel(PackingModel model)
        {
            model.Construction = string.Format("{0} / {1} / {2}", model.Material, model.MaterialConstructionFinishName, model.MaterialWidthFinish);
            EntityExtension.FlagForCreate(model, IdentityService.Username, UserAgent);
            foreach(var detail in model.PackingDetails)
            {
                EntityExtension.FlagForCreate(detail, IdentityService.Username, UserAgent);
            }
            DbSet.Add(model);
        }

        //public async Task CreateProduct(PackingModel model)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        var uri = new Uri(string.Format("{0}{1}", APIEndpoint.Core, "master/products/packing/create"));
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", IdentityService.Token);
        //        var myContentJson = JsonConvert.SerializeObject(model);
        //        var myContent = new StringContent(myContentJson, Encoding.UTF8, "application/json");
        //        var response = await client.PostAsync(uri, myContent);
        //        response.EnsureSuccessStatusCode();
        //    }
        //}

        public override async Task DeleteModel(int id)
        {
            var model = await ReadModelById(id);
            foreach (var item in model.PackingDetails)
            {
                EntityExtension.FlagForDelete(item, IdentityService.Username, UserAgent);
            }
            EntityExtension.FlagForDelete(model, IdentityService.Username, UserAgent);
            DbSet.Update(model);
        }

        public override async Task<PackingModel> ReadModelById(int id)
        {
            var query = (from packings in dbContext.Packings
                         where packings.Id == id && packings.IsDeleted.Equals(false)
                         select packings).Include("PackingDetails");
            var result = await query.FirstOrDefaultAsync();
            return result;
        }

        public override async Task UpdateModelAsync(int id, PackingModel model)
        {
            var dbmodel = await ReadModelById(id);

            dbmodel.Accepted = model.Accepted;
            dbmodel.BuyerAddress = model.BuyerAddress;
            dbmodel.BuyerCode = model.BuyerCode;
            dbmodel.BuyerId = model.BuyerId;
            dbmodel.BuyerName = model.BuyerName;
            dbmodel.BuyerType = model.BuyerType;
            dbmodel.Code = model.Code;
            dbmodel.ColorCode = model.ColorCode;
            dbmodel.ColorName = model.ColorName;
            dbmodel.ColorType = model.ColorType;
            dbmodel.Construction = string.Format("{0} / {1} / {2}", model.Material, model.MaterialConstructionFinishName, model.MaterialWidthFinish);
            dbmodel.Date = model.Date;
            dbmodel.Declined = model.Declined;
            dbmodel.DeliveryType = model.DeliveryType;
            dbmodel.DesignCode = model.DesignCode;
            dbmodel.DesignNumber = model.DesignNumber;
            dbmodel.FinishedProductType = model.FinishedProductType;
            dbmodel.Material = model.Material;
            dbmodel.MaterialConstructionFinishId = model.MaterialConstructionFinishId;
            dbmodel.MaterialConstructionFinishName = model.MaterialConstructionFinishName;
            dbmodel.MaterialId = model.MaterialId;
            dbmodel.MaterialWidthFinish = model.MaterialWidthFinish;
            dbmodel.Motif = model.Motif;
            dbmodel.OrderTypeCode = model.OrderTypeCode;
            dbmodel.OrderTypeId = model.OrderTypeId;
            dbmodel.OrderTypeName = model.OrderTypeName;
            dbmodel.PackingUom = model.PackingUom;
            dbmodel.ProductionOrderId = model.ProductionOrderId;
            dbmodel.ProductionOrderNo = model.ProductionOrderNo;
            dbmodel.SalesContractNo = model.SalesContractNo;
            dbmodel.Status = model.Status;
            dbmodel.UId = model.UId;


   //         model.Id = id;
			//model.Construction = string.Format("{0} / {1} / {2}", model.Material, model.MaterialConstructionFinishName, model.MaterialWidthFinish);
            EntityExtension.FlagForUpdate(dbmodel, IdentityService.Username, UserAgent);
            var addedPackingDetails = model.PackingDetails.Where(x => !dbmodel.PackingDetails.Any(y => y.Id == x.Id));
            var updatedPackingDetails = model.PackingDetails.Where(x => dbmodel.PackingDetails.Any(y => y.Id == x.Id));
            var deletedPackingDetails = dbmodel.PackingDetails.Where(x => !model.PackingDetails.Any(y => y.Id == x.Id));

            foreach(var item in updatedPackingDetails)
            {
                var dbItem = dbmodel.PackingDetails.FirstOrDefault(x => x.Id == item.Id);

                dbItem.Grade = item.Grade;
                dbItem.Length = item.Length;
                dbItem.Lot = item.Lot;
                dbItem.Quantity = item.Quantity;
                dbItem.Remark = item.Remark;
                dbItem.Weight = item.Weight;

                EntityExtension.FlagForUpdate(dbItem, IdentityService.Username, UserAgent);
            }

            foreach(var item in addedPackingDetails)
            {
                item.PackingId = id;
                EntityExtension.FlagForCreate(item, IdentityService.Username, UserAgent);
                dbmodel.PackingDetails.Add(item);
            }

            foreach(var item in deletedPackingDetails)
            {
                EntityExtension.FlagForDelete(item, IdentityService.Username, UserAgent);
            }
            //foreach(var detail in model.PackingDetails)
            //{
            //    if(detail.Id == 0)
            //    {
            //        EntityExtension.FlagForCreate(detail, IdentityService.Username, UserAgent);
            //        dbSetDetail.Add(detail);
            //    }
            //    else
            //    {
            //        EntityExtension.FlagForUpdate(detail, IdentityService.Username, UserAgent);
            //    }


                
            //}
            //DbSet.Update(model);
        }
    }
}
