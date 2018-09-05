using Com.Danliris.Service.Finishing.Printing.Lib.Models.FabricQualityControl;
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

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.FabricQualityControl
{
    public class FabricQualityControlLogic : BaseLogic<FabricQualityControlModel>
    {
        private const string UserAgent = "production-service";
        private readonly ProductionDbContext DbContext;
        private readonly DbSet<FabricQualityControlModel> FabricQualityControlDbSet;
        private readonly DbSet<FabricGradeTestModel> FabricGradeTestDbSet;
        private readonly DbSet<CriteriaModel> CriteriaDbSet;
        public FabricQualityControlLogic(IIdentityService identityService, ProductionDbContext dbContext) : base(identityService, dbContext)
        {
            DbContext = dbContext;
            FabricQualityControlDbSet = dbContext.Set<FabricQualityControlModel>();
            FabricGradeTestDbSet = dbContext.Set<FabricGradeTestModel>();
            CriteriaDbSet = dbContext.Set<CriteriaModel>();
        }

        public override void CreateModel(FabricQualityControlModel model)
        {

            foreach (var fabricGradeTest in model.FabricGradeTests)
            {
                EntityExtension.FlagForCreate(fabricGradeTest, IdentityService.Username, UserAgent);
            }
            base.CreateModel(model);
        }

        public override async Task DeleteModel(int id)
        {
            var model = await ReadModelById(id);
            EntityExtension.FlagForDelete(model, IdentityService.Username, UserAgent);
            foreach (var fabricGradeTest in model.FabricGradeTests)
            {
                EntityExtension.FlagForDelete(fabricGradeTest, IdentityService.Username, UserAgent);
            }
            DbSet.Update(model);
        }

        public override async Task<FabricQualityControlModel> ReadModelById(int id)
        {
            var Result = await DbSet.FirstOrDefaultAsync(d => d.Id.Equals(id) && !d.IsDeleted);
            Result.FabricGradeTests = await FabricGradeTestDbSet.Where(e => e.FabricQualityControlId.Equals(id) && !e.IsDeleted).ToListAsync();
            foreach (var fabricGradeTest in Result.FabricGradeTests)
            {
                fabricGradeTest.Criteria = await CriteriaDbSet.Where(w => w.FabricGradeTestId.Equals(fabricGradeTest.Id)).ToListAsync();
            }
            return Result;
            //return 
        }

        public override void UpdateModelAsync(int id, FabricQualityControlModel model)
        {
            HashSet<int> fabricGradeTestIds = FabricGradeTestDbSet.Where(d => d.FabricQualityControlId == model.Id && !d.IsDeleted).Select(d => d.Id).ToHashSet();

            foreach (int fabricGradeTestId in fabricGradeTestIds)
            {
                var fabricGradeTest = model.FabricGradeTests.FirstOrDefault(prop => prop.Id.Equals(fabricGradeTestId));
                if (fabricGradeTest == null)
                {
                    fabricGradeTest = FabricGradeTestDbSet.Where(w => w.Id == fabricGradeTestId).FirstOrDefault();
                    var criterion = CriteriaDbSet.Where(w => w.FabricGradeTestId == fabricGradeTest.Id).ToList();
                    EntityExtension.FlagForDelete(fabricGradeTest, IdentityService.Username, UserAgent);
                    FabricGradeTestDbSet.Update(fabricGradeTest);

                    foreach (var criteria in criterion)
                    {
                        CriteriaDbSet.Update(criteria);
                    }
                }
                else
                {
                    EntityExtension.FlagForUpdate(fabricGradeTest, IdentityService.Username, UserAgent);
                    FabricGradeTestDbSet.Update(fabricGradeTest);
                    foreach (var criteria in fabricGradeTest.Criteria)
                    {
                        CriteriaDbSet.Update(criteria);
                    }
                }
            }

            foreach (var fabricGradeTest in model.FabricGradeTests)
            {
                if (fabricGradeTest.Id == 0)
                {
                    EntityExtension.FlagForCreate(fabricGradeTest, IdentityService.Username, UserAgent);
                    FabricGradeTestDbSet.Add(fabricGradeTest);

                    foreach (var criteria in fabricGradeTest.Criteria)
                    {
                        CriteriaDbSet.Add(criteria);
                    }
                }
            }
            base.UpdateModelAsync(id, model);
        }
    }
}
