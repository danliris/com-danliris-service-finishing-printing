using Com.Danliris.Service.Finishing.Printing.Lib.Models.StrikeOff;
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

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.StrikeOff
{
    public class StrikeOffLogic : BaseLogic<StrikeOffModel>
    {
        private readonly ProductionDbContext _dbContext;
        public StrikeOffLogic(IIdentityService identityService, ProductionDbContext dbContext) : base(identityService, dbContext)
        {
            _dbContext = dbContext;
        }

        public override void CreateModel(StrikeOffModel model)
        {
            foreach (var item in model.StrikeOffItems)
            {
                EntityExtension.FlagForCreate(item, IdentityService.Username, UserAgent);
            }

            base.CreateModel(model);
        }

        public override async Task DeleteModel(int id)
        {
            var model = await ReadModelById(id);
            EntityExtension.FlagForDelete(model, IdentityService.Username, UserAgent, true);
            foreach (var item in model.StrikeOffItems)
            {
                EntityExtension.FlagForDelete(item, IdentityService.Username, UserAgent);
            }
            DbSet.Update(model);
        }

        public override async Task UpdateModelAsync(int id, StrikeOffModel model)
        {
            var dbModel = await ReadModelById(id);
            dbModel.Code = model.Code;
            dbModel.Remark = model.Remark;
            EntityExtension.FlagForUpdate(dbModel, IdentityService.Username, UserAgent);

            var addedItems = model.StrikeOffItems.Where(x => !dbModel.StrikeOffItems.Any(y => y.Id == x.Id)).ToList();
            var updatedItems = model.StrikeOffItems.Where(x => dbModel.StrikeOffItems.Any(y => y.Id == x.Id)).ToList();
            var deletedItems = dbModel.StrikeOffItems.Where(x => !model.StrikeOffItems.Any(y => y.Id == x.Id)).ToList();


            foreach (var item in updatedItems)
            {
                var dbItem = dbModel.StrikeOffItems.FirstOrDefault(x => x.Id == item.Id);

                dbItem.ColorReceiptId = item.ColorReceiptId;
                dbItem.ColorReceiptColorCode = item.ColorReceiptColorCode;

                EntityExtension.FlagForUpdate(dbItem, IdentityService.Username, UserAgent);
            }


            foreach (var item in deletedItems)
            {
                EntityExtension.FlagForDelete(item, IdentityService.Username, UserAgent);
            }

            foreach (var item in addedItems)
            {
                item.ColorReceiptId = id;
                EntityExtension.FlagForCreate(item, IdentityService.Username, UserAgent);
                dbModel.StrikeOffItems.Add(item);
            }
        }

        public override async Task<StrikeOffModel> ReadModelById(int id)
        {
            var model = await DbSet.Include(s => s.StrikeOffItems).FirstOrDefaultAsync(s => s.Id == id);

            foreach(var item in model.StrikeOffItems)
            {
                item.ColorReceiptItems = await _dbContext.ColorReceiptItems.Where(s => s.ColorReceiptId == item.ColorReceiptId).ToListAsync();
            }

            return model;
        }

        public bool CheckDuplicateCode(string code, int id)
        {
            return DbSet.Any(s => s.Id != id && s.Code == code);
        }
    }
}
