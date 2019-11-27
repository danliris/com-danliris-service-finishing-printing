
using Com.Danliris.Service.Finishing.Printing.Lib.Models.DOSales;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.DOSales
{
    public class DOSalesLogic : BaseLogic<DOSalesModel>
    {
        private const string UserAgent = "production-service";
        private readonly ProductionDbContext dbContext;
        private readonly DbSet<DOSalesModel> dbSet;
        private readonly DbSet<DOSalesDetailModel> dbSetDetail;

        public DOSalesLogic(IIdentityService identityService, ProductionDbContext dbContext) : base(identityService, dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<DOSalesModel>();
            this.dbSetDetail = dbContext.Set<DOSalesDetailModel>();
        }

        public override void CreateModel(DOSalesModel model)
        {
            model.Construction = string.Format("{0} / {1} / {2}", model.Material, model.MaterialConstructionFinishName, model.MaterialWidthFinish);
            EntityExtension.FlagForCreate(model, IdentityService.Username, UserAgent);
            foreach (var detail in model.DOSalesDetails)
            {
                EntityExtension.FlagForCreate(detail, IdentityService.Username, UserAgent);
            }
            DbSet.Add(model);
        }

        public override async Task DeleteModel(int id)
        {
            var model = await ReadModelById(id);
            foreach (var item in model.DOSalesDetails)
            {
                EntityExtension.FlagForDelete(item, IdentityService.Username, UserAgent);
            }
            EntityExtension.FlagForDelete(model, IdentityService.Username, UserAgent);
            DbSet.Update(model);
        }

        public override async Task<DOSalesModel> ReadModelById(int id)
        {
            var query = (from doSales in dbContext.DOSalesItems
                         where doSales.Id == id && doSales.IsDeleted.Equals(false)
                         select doSales).Include("DOSalesDetails");
            var result = await query.FirstOrDefaultAsync();
            return result;
        }

        public override async Task UpdateModelAsync(int id, DOSalesModel model)
        {
            var dbmodel = await ReadModelById(id);

            dbmodel.Accepted = model.Accepted;
            dbmodel.BuyerAddress = model.BuyerAddress;
            dbmodel.BuyerCode = model.BuyerCode;
            dbmodel.BuyerId = model.BuyerId;
            dbmodel.BuyerName = model.BuyerName;
            dbmodel.BuyerType = model.BuyerType;
            dbmodel.StorageId = model.StorageId;
            dbmodel.StorageName = model.StorageName;
            dbmodel.Code = model.Code;
            dbmodel.Construction = string.Format("{0} / {1} / {2}", model.Material, model.MaterialConstructionFinishName, model.MaterialWidthFinish);
            dbmodel.Date = model.Date;
            dbmodel.Declined = model.Declined;
            dbmodel.Material = model.Material;
            dbmodel.MaterialConstructionFinishId = model.MaterialConstructionFinishId;
            dbmodel.MaterialConstructionFinishName = model.MaterialConstructionFinishName;
            dbmodel.MaterialId = model.MaterialId;
            dbmodel.MaterialWidthFinish = model.MaterialWidthFinish;
            dbmodel.PackingUom = model.PackingUom;
            dbmodel.ProductionOrderId = model.ProductionOrderId;
            dbmodel.ProductionOrderNo = model.ProductionOrderNo;
            dbmodel.Status = model.Status;
            dbmodel.UId = model.UId;

            EntityExtension.FlagForUpdate(dbmodel, IdentityService.Username, UserAgent);
            var addedDOSalesDetails = model.DOSalesDetails.Where(x => !dbmodel.DOSalesDetails.Any(y => y.Id == x.Id));
            var updatedDOSalesDetails = model.DOSalesDetails.Where(x => dbmodel.DOSalesDetails.Any(y => y.Id == x.Id));
            var deletedDOSalesDetails = dbmodel.DOSalesDetails.Where(x => !model.DOSalesDetails.Any(y => y.Id == x.Id));

            foreach (var item in updatedDOSalesDetails)
            {
                var dbItem = dbmodel.DOSalesDetails.FirstOrDefault(x => x.Id == item.Id);

                dbItem.UnitName = item.UnitName;
                dbItem.UnitCode = item.UnitCode;
                dbItem.Length = item.Length;
                dbItem.Quantity = item.Quantity;
                dbItem.Weight = item.Weight;
                dbItem.Remark = item.Remark;

                EntityExtension.FlagForUpdate(dbItem, IdentityService.Username, UserAgent);
            }

            foreach (var item in addedDOSalesDetails)
            {
                item.DOSalesId = id;
                EntityExtension.FlagForCreate(item, IdentityService.Username, UserAgent);
                dbmodel.DOSalesDetails.Add(item);
            }

            foreach (var item in deletedDOSalesDetails)
            {
                EntityExtension.FlagForDelete(item, IdentityService.Username, UserAgent);
            }
        }
    }
}
