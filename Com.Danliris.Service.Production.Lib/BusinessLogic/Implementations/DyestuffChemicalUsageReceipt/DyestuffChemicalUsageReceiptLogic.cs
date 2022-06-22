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
            foreach (var item in model.DyestuffChemicalUsageReceiptItems)
            {
                EntityExtension.FlagForCreate(item, IdentityService.Username, UserAgent);
                foreach (var detail in item.DyestuffChemicalUsageReceiptItemDetails)
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

            foreach (var item in updatedItems)
            {
                var dbItem = dbModel.DyestuffChemicalUsageReceiptItems.FirstOrDefault(x => x.Id == item.Id);

                dbItem.ColorCode = item.ColorCode;
                dbItem.ReceiptDate = item.ReceiptDate;
                dbItem.Adjs1Date = item.Adjs1Date;
                dbItem.Adjs2Date = item.Adjs2Date;
                dbItem.Adjs3Date = item.Adjs3Date;
                dbItem.Adjs4Date = item.Adjs4Date;
                dbItem.TotalRealizationQty = item.TotalRealizationQty;
                EntityExtension.FlagForUpdate(dbItem, IdentityService.Username, UserAgent);

                var addedDetails = item.DyestuffChemicalUsageReceiptItemDetails.Where(x => !dbItem.DyestuffChemicalUsageReceiptItemDetails.Any(y => y.Id == x.Id)).ToList();
                var updatedDetails = item.DyestuffChemicalUsageReceiptItemDetails.Where(x => dbItem.DyestuffChemicalUsageReceiptItemDetails.Any(y => y.Id == x.Id)).ToList();
                var deletedDetails = dbItem.DyestuffChemicalUsageReceiptItemDetails.Where(x => !item.DyestuffChemicalUsageReceiptItemDetails.Any(y => y.Id == x.Id)).ToList();

                foreach (var detail in updatedDetails)
                {
                    var dbDetail = dbItem.DyestuffChemicalUsageReceiptItemDetails.FirstOrDefault(x => x.Id == detail.Id);

                    dbDetail.Index = detail.Index;
                    dbDetail.Name = detail.Name;
                    dbDetail.ReceiptQuantity = detail.ReceiptQuantity;
                    dbDetail.Adjs1Quantity = detail.Adjs1Quantity;
                    dbDetail.Adjs2Quantity = detail.Adjs2Quantity;
                    dbDetail.Adjs3Quantity = detail.Adjs3Quantity;
                    dbDetail.Adjs4Quantity = detail.Adjs4Quantity;

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

            foreach (var item in deletedItems)
            {
                EntityExtension.FlagForDelete(item, IdentityService.Username, UserAgent);
                foreach (var detail in item.DyestuffChemicalUsageReceiptItemDetails)
                {
                    EntityExtension.FlagForDelete(detail, IdentityService.Username, UserAgent);
                }
            }

            foreach (var item in addedItems)
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

            foreach (var item in model.DyestuffChemicalUsageReceiptItems)
            {
                item.DyestuffChemicalUsageReceiptItemDetails = item.DyestuffChemicalUsageReceiptItemDetails.OrderBy(s => s.Index).ToList();
            }

            return model;
        }

        public async Task<DyestuffChemicalUsageReceiptModel> GetDataByStrikeOff(int strikeOffId)
        {
            var model = await DbSet
                .Include(s => s.DyestuffChemicalUsageReceiptItems)
                    .ThenInclude(s => s.DyestuffChemicalUsageReceiptItemDetails)
                .OrderByDescending(s => s.Date)
                .FirstOrDefaultAsync(s => s.StrikeOffId == strikeOffId);

            if (model == null)
                return model;

            foreach (var item in model.DyestuffChemicalUsageReceiptItems)
            {
                item.DyestuffChemicalUsageReceiptItemDetails = item.DyestuffChemicalUsageReceiptItemDetails.OrderBy(s => s.Index).ToList();
            }

            return model;
        }

        public string GetLatestProductionOrderNoByStrikeOff(int strikeOffId)
        {
            var data = DbSet
                .Where(s => s.StrikeOffId == strikeOffId)
                .OrderByDescending(s => s.Date)
                .Take(3);

            string result = string.Join(", ", data.Select(s => s.ProductionOrderOrderNo));
            return result;
        }
    }
}
