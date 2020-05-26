using Com.Danliris.Service.Finishing.Printing.Lib.Models.DyestuffChemicalUsageReceipt;
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

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.DyestuffChemicalUsageReceipt
{
    public class DyestuffChemicalUsageReceiptLogic : BaseLogic<DyestuffChemicalUsageReceiptModel>
    {
        private readonly ProductionDbContext _dbContext;
        public DyestuffChemicalUsageReceiptLogic(IIdentityService identityService, ProductionDbContext dbContext) : base(identityService, dbContext)
        {
            _dbContext = dbContext;
        }

        public override void CreateModel(DyestuffChemicalUsageReceiptModel model)
        {
            foreach(var item in model.DyestuffChemicalUsageReceiptItems)
            {
                EntityExtension.FlagForCreate(item, IdentityService.Username, UserAgent);
                foreach(var detail in item.DyestuffChemicalUsageReceiptItemDetails)
                {
                    EntityExtension.FlagForCreate(detail, IdentityService.Username, UserAgent);
                }
            }
            base.CreateModel(model);
        }

        public override async Task DeleteModel(int id)
        {
            var model = await ReadModelById(id);
            EntityExtension.FlagForDelete(model, IdentityService.Username, UserAgent);
            foreach (var item in model.DyestuffChemicalUsageReceiptItems)
            {
                EntityExtension.FlagForDelete(item, IdentityService.Username, UserAgent);
                foreach (var detail in item.DyestuffChemicalUsageReceiptItemDetails)
                {
                    EntityExtension.FlagForDelete(detail, IdentityService.Username, UserAgent);
                }
            }
            DbSet.Update(model);
        }

        public override async Task UpdateModelAsync(int id, DyestuffChemicalUsageReceiptModel model)
        {
            var dbModel = await ReadModelById(id);
            dbModel.ProductionOrderId = model.ProductionOrderId;
            dbModel.ProductionOrderOrderNo = model.ProductionOrderOrderNo;
            dbModel.ProductionOrderOrderQuantity = model.ProductionOrderOrderQuantity;
            dbModel.ProductionOrderMaterialConstructionId = model.ProductionOrderMaterialConstructionId;
            dbModel.ProductionOrderMaterialConstructionName = model.ProductionOrderMaterialConstructionName;
            dbModel.ProductionOrderMaterialId = model.ProductionOrderMaterialId;
            dbModel.ProductionOrderMaterialName = model.ProductionOrderMaterialName;
            dbModel.ProductionOrderMaterialWidth = model.ProductionOrderMaterialWidth;
            dbModel.StrikeOffId = model.StrikeOffId;
            dbModel.StrikeOffCode = model.StrikeOffCode;
            dbModel.StrikeOffType = model.StrikeOffType;
            dbModel.Date = model.Date;
            EntityExtension.FlagForUpdate(dbModel, IdentityService.Username, UserAgent);

            var addedItems = model.DyestuffChemicalUsageReceiptItems.Where(x => !dbModel.DyestuffChemicalUsageReceiptItems.Any(y => y.Id == x.Id)).ToList();
            var updatedItems = model.DyestuffChemicalUsageReceiptItems.Where(x => dbModel.DyestuffChemicalUsageReceiptItems.Any(y => y.Id == x.Id)).ToList();
            var deletedItems = dbModel.DyestuffChemicalUsageReceiptItems.Where(x => !model.DyestuffChemicalUsageReceiptItems.Any(y => y.Id == x.Id)).ToList();

            foreach(var item in updatedItems)
            {
                var dbItem = dbModel.DyestuffChemicalUsageReceiptItems.FirstOrDefault(x => x.Id == item.Id);

                dbItem.ColorCode = item.ColorCode;
                dbItem.Prod1Date = item.Prod1Date;
                dbItem.Prod2Date = item.Prod2Date;
                dbItem.Prod3Date = item.Prod3Date;
                dbItem.Prod4Date = item.Prod4Date;
                dbItem.Prod5Date = item.Prod5Date;
                EntityExtension.FlagForUpdate(dbItem, IdentityService.Username, UserAgent);

                var addedDetails = item.DyestuffChemicalUsageReceiptItemDetails.Where(x => !dbItem.DyestuffChemicalUsageReceiptItemDetails.Any(y => y.Id == x.Id)).ToList();
                var updatedDetails = item.DyestuffChemicalUsageReceiptItemDetails.Where(x => dbItem.DyestuffChemicalUsageReceiptItemDetails.Any(y => y.Id == x.Id)).ToList();
                var deletedDetails = dbItem.DyestuffChemicalUsageReceiptItemDetails.Where(x => !item.DyestuffChemicalUsageReceiptItemDetails.Any(y => y.Id == x.Id)).ToList();

                foreach(var detail in updatedDetails)
                {
                    var dbDetail = dbItem.DyestuffChemicalUsageReceiptItemDetails.FirstOrDefault(x => x.Id == detail.Id);

                    dbDetail.Index = detail.Index;
                    dbDetail.Name = detail.Name;
                    dbDetail.ReceiptQuantity = detail.ReceiptQuantity;
                    dbDetail.Prod1Quantity = detail.Prod1Quantity;
                    dbDetail.Prod2Quantity = detail.Prod2Quantity;
                    dbDetail.Prod3Quantity = detail.Prod3Quantity;
                    dbDetail.Prod4Quantity = detail.Prod4Quantity;
                    dbDetail.Prod5Quantity = detail.Prod5Quantity;

                    EntityExtension.FlagForUpdate(dbDetail, IdentityService.Username, UserAgent);
                }

                foreach (var detail in deletedDetails)
                {
                    EntityExtension.FlagForDelete(detail, IdentityService.Username, UserAgent);
                }

                foreach (var detail in addedDetails)
                {
                    detail.DyestuffChemicalUsageReceiptItemId = dbItem.Id;
                    EntityExtension.FlagForCreate(detail, IdentityService.Username, UserAgent);
                    dbItem.DyestuffChemicalUsageReceiptItemDetails.Add(detail);
                }
            }

            foreach(var item in deletedItems)
            {
                EntityExtension.FlagForDelete(item, IdentityService.Username, UserAgent);
                foreach (var detail in item.DyestuffChemicalUsageReceiptItemDetails)
                {
                    EntityExtension.FlagForDelete(detail, IdentityService.Username, UserAgent);
                }
            }

            foreach(var item in addedItems)
            {
                item.DyestuffChemicalUsageReceiptId = dbModel.Id;
                EntityExtension.FlagForCreate(item, IdentityService.Username, UserAgent);
                foreach (var detail in item.DyestuffChemicalUsageReceiptItemDetails)
                {
                    EntityExtension.FlagForCreate(detail, IdentityService.Username, UserAgent);
                }

                dbModel.DyestuffChemicalUsageReceiptItems.Add(item);
            }

        }

        public override async Task<DyestuffChemicalUsageReceiptModel> ReadModelById(int id)
        {
            var model = await DbSet
                .Include(s => s.DyestuffChemicalUsageReceiptItems)
                    .ThenInclude(s => s.DyestuffChemicalUsageReceiptItemDetails)
                .FirstOrDefaultAsync(s => s.Id == id);

            foreach(var item in model.DyestuffChemicalUsageReceiptItems)
            {
                item.DyestuffChemicalUsageReceiptItemDetails = item.DyestuffChemicalUsageReceiptItemDetails.OrderBy(s => s.Index).ToList();
            }

            return model;
        }
    }
}
