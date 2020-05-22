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
                foreach (var chemical in item.ChemicalItems)
                {
                    EntityExtension.FlagForCreate(chemical, IdentityService.Username, UserAgent);
                }
                foreach (var dyeStuff in item.DyeStuffItems)
                {
                    EntityExtension.FlagForCreate(dyeStuff, IdentityService.Username, UserAgent);
                }
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
                foreach (var chemical in item.ChemicalItems)
                {
                    EntityExtension.FlagForDelete(chemical, IdentityService.Username, UserAgent);
                }
                foreach (var dyeStuff in item.DyeStuffItems)
                {
                    EntityExtension.FlagForDelete(dyeStuff, IdentityService.Username, UserAgent);
                }
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

                dbItem.ColorCode = item.ColorCode;

                EntityExtension.FlagForUpdate(dbItem, IdentityService.Username, UserAgent);

                var addedChemicals = item.ChemicalItems.Where(x => !dbItem.ChemicalItems.Any(y => y.Name == x.Name)).ToList();
                var updatedChemicals = item.ChemicalItems.Where(x => dbItem.ChemicalItems.Any(y => y.Name == x.Name)).ToList();
                var deletedChemicals = dbItem.ChemicalItems.Where(x => !item.ChemicalItems.Any(y => y.Name == x.Name)).ToList();

                foreach (var chemical in updatedChemicals)
                {
                    var dbChemical = dbItem.ChemicalItems.FirstOrDefault(s => s.Name == chemical.Name);
                    dbChemical.Quantity = chemical.Quantity;
                    EntityExtension.FlagForUpdate(dbChemical, IdentityService.Username, UserAgent);
                }

                foreach(var chemical in deletedChemicals)
                {
                    EntityExtension.FlagForDelete(chemical, IdentityService.Username, UserAgent);
                }

                foreach(var chemical in addedChemicals)
                {
                    chemical.StrikeOffItemId = dbItem.Id;
                    EntityExtension.FlagForCreate(chemical, IdentityService.Username, UserAgent);
                    dbItem.ChemicalItems.Add(chemical);
                }

                var addedDyeStuffs = item.DyeStuffItems.Where(x => !dbItem.DyeStuffItems.Any(y => y.Id == x.Id)).ToList();
                var updatedDyeStuffs = item.DyeStuffItems.Where(x => dbItem.DyeStuffItems.Any(y => y.Id == x.Id)).ToList();
                var deletedDyeStuffs = dbItem.DyeStuffItems.Where(x => !item.DyeStuffItems.Any(y => y.Id == x.Id)).ToList();

                foreach (var dyeStuff in updatedDyeStuffs)
                {
                    var dbDyeStuff = dbItem.DyeStuffItems.FirstOrDefault(s => s.Id == dyeStuff.Id);
                    dbDyeStuff.ProductCode = dyeStuff.ProductCode;
                    dbDyeStuff.ProductId = dyeStuff.ProductId;
                    dbDyeStuff.ProductName = dyeStuff.ProductName;
                    dbDyeStuff.Quantity = dyeStuff.Quantity;
                    EntityExtension.FlagForUpdate(dbDyeStuff, IdentityService.Username, UserAgent);
                }

                foreach (var dyeStuff in deletedDyeStuffs)
                {
                    EntityExtension.FlagForDelete(dyeStuff, IdentityService.Username, UserAgent);
                }

                foreach (var dyeStuff in addedDyeStuffs)
                {
                    dyeStuff.StrikeOffItemId = dbItem.Id;
                    EntityExtension.FlagForCreate(dyeStuff, IdentityService.Username, UserAgent);
                    dbItem.DyeStuffItems.Add(dyeStuff);
                }
            }


            foreach (var item in deletedItems)
            {
                EntityExtension.FlagForDelete(item, IdentityService.Username, UserAgent);
                foreach (var dyeStuff in item.DyeStuffItems)
                {
                    EntityExtension.FlagForDelete(dyeStuff, IdentityService.Username, UserAgent);
                }
                foreach (var chemical in item.ChemicalItems)
                {
                    EntityExtension.FlagForDelete(chemical, IdentityService.Username, UserAgent);
                }
            }

            foreach (var item in addedItems)
            {
                item.StrikeOffId = id;
                EntityExtension.FlagForCreate(item, IdentityService.Username, UserAgent);
                foreach (var dyeStuff in item.DyeStuffItems)
                {
                    EntityExtension.FlagForCreate(dyeStuff, IdentityService.Username, UserAgent);
                }
                foreach (var chemical in item.ChemicalItems)
                {
                    EntityExtension.FlagForCreate(chemical, IdentityService.Username, UserAgent);
                }
                dbModel.StrikeOffItems.Add(item);
            }
        }

        public override async Task<StrikeOffModel> ReadModelById(int id)
        {
            var model = await DbSet.Include(s => s.StrikeOffItems).ThenInclude(s => s.ChemicalItems)
                .Include(s => s.StrikeOffItems).ThenInclude(s => s.DyeStuffItems)
                .FirstOrDefaultAsync(s => s.Id == id);

            foreach(var item in model.StrikeOffItems)
            {
                item.ChemicalItems = item.ChemicalItems.OrderBy(s => s.Index).ToList();
            }
            return model;
        }

        public bool CheckDuplicateCode(string code, int id)
        {
            return DbSet.Any(s => s.Id != id && s.Code == code);
        }
    }
}
