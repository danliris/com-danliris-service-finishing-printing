
using Com.Danliris.Service.Finishing.Printing.Lib.Models.DOSales;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

            dbmodel.UId = model.UId;
            dbmodel.Code = model.Code;
            dbmodel.AutoIncreament = model.AutoIncreament;
            dbmodel.DOSalesNo = model.DOSalesNo;
            dbmodel.DOSalesType = model.DOSalesType;
            dbmodel.DOSalesDate = model.DOSalesDate;
            dbmodel.StorageId = model.StorageId;
            dbmodel.StorageName = model.StorageName;
            dbmodel.StorageDivision = model.StorageDivision;
            dbmodel.HeadOfStorage = model.HeadOfStorage;
            dbmodel.ProductionOrderId = model.ProductionOrderId;
            dbmodel.ProductionOrderNo = model.ProductionOrderNo;
            dbmodel.MaterialId = model.MaterialId;
            dbmodel.Material = model.Material;
            dbmodel.MaterialWidthFinish = model.MaterialWidthFinish;
            dbmodel.MaterialConstructionFinishId = model.MaterialConstructionFinishId;
            dbmodel.MaterialConstructionFinishName = model.MaterialConstructionFinishName;
            dbmodel.BuyerAddress = model.BuyerAddress;
            dbmodel.BuyerCode = model.BuyerCode;
            dbmodel.BuyerId = model.BuyerId;
            dbmodel.BuyerName = model.BuyerName;
            dbmodel.BuyerNPWP = model.BuyerNPWP;
            dbmodel.BuyerType = model.BuyerType;
            dbmodel.DestinationBuyerAddress = model.DestinationBuyerAddress;
            dbmodel.DestinationBuyerCode = model.DestinationBuyerCode;
            dbmodel.DestinationBuyerId = model.DestinationBuyerId;
            dbmodel.DestinationBuyerName = model.DestinationBuyerName;
            dbmodel.DestinationBuyerNPWP = model.DestinationBuyerNPWP;
            dbmodel.DestinationBuyerType = model.DestinationBuyerType;
            dbmodel.PackingUom = model.PackingUom;
            dbmodel.LengthUom = model.LengthUom;
            dbmodel.Disp = model.Disp;
            dbmodel.Op = model.Op;
            dbmodel.Sc = model.Sc;
            dbmodel.Construction = string.Format("{0} / {1} / {2}", model.Material, model.MaterialConstructionFinishName, model.MaterialWidthFinish);
            dbmodel.Remark = model.Remark;
            dbmodel.Status = model.Status; 
            dbmodel.Accepted = model.Accepted;
            dbmodel.Declined = model.Declined;

            EntityExtension.FlagForUpdate(dbmodel, IdentityService.Username, UserAgent);
            var addedDOSalesDetails = model.DOSalesDetails.Where(x => !dbmodel.DOSalesDetails.Any(y => y.Id == x.Id));
            var updatedDOSalesDetails = model.DOSalesDetails.Where(x => dbmodel.DOSalesDetails.Any(y => y.Id == x.Id));
            var deletedDOSalesDetails = dbmodel.DOSalesDetails.Where(x => !model.DOSalesDetails.Any(y => y.Id == x.Id));

            foreach (var item in updatedDOSalesDetails)
            {
                var dbItem = dbmodel.DOSalesDetails.FirstOrDefault(x => x.Id == item.Id);

                dbItem.UnitCode  = item.UnitCode;
                dbItem.UnitName = item.UnitName;
                dbItem.UnitRemark = item.UnitRemark;
                dbItem.TotalPacking = item.TotalPacking;
                dbItem.TotalLength = item.TotalLength;
                dbItem.TotalLengthConversion = item.TotalLengthConversion;

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
