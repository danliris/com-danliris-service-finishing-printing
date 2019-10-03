using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.CostCalculation;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.CostCalculation;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Utilities;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.CostCalculation
{
    public class CostCalculationService : ICostCalculationService
    {
        private const string UserAgent = "Service";
        private readonly ProductionDbContext _dbContext;
        private readonly DbSet<CostCalculationModel> _costCalculationDbSet;
        private readonly DbSet<CostCalculationMachineModel> _costCalculationMachineDbSet;
        private readonly DbSet<CostCalculationChemicalModel> _costCalculationChemicalDbSet;
        private readonly IIdentityService _identityService;

        public CostCalculationService(ProductionDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;

            _costCalculationDbSet = dbContext.Set<CostCalculationModel>();
            _costCalculationMachineDbSet = dbContext.Set<CostCalculationMachineModel>();
            _costCalculationChemicalDbSet = dbContext.Set<CostCalculationChemicalModel>();

            _identityService = (IIdentityService)serviceProvider.GetService(typeof(IIdentityService));
        }

        public async Task<int> DeleteSingle(int id)
        {
            var costCalculation = await _costCalculationDbSet.FirstOrDefaultAsync(entity => entity.Id == id);
            EntityExtension.FlagForDelete(costCalculation, _identityService.Username, UserAgent);
            _costCalculationDbSet.Update(costCalculation);

            var costCalculationMachines = await _costCalculationMachineDbSet.Where(entity => entity.CostCalculationId == id).ToListAsync();
            costCalculationMachines = costCalculationMachines.Select(entity =>
            {
                EntityExtension.FlagForDelete(entity, _identityService.Username, UserAgent);
                return entity;
            }).ToList();
            _costCalculationMachineDbSet.UpdateRange(costCalculationMachines);

            var costCalculationChemicals = await _costCalculationChemicalDbSet.Where(entity => entity.CostCalculationId == id).ToListAsync();
            costCalculationChemicals = costCalculationChemicals.Select(entity =>
            {
                EntityExtension.FlagForDelete(entity, _identityService.Username, UserAgent);
                return entity;
            }).ToList();
            _costCalculationChemicalDbSet.UpdateRange(costCalculationChemicals);

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<CostCalculationPagedListWrapper> GetPaged(int page, int size, string order, string keyword, string filter)
        {
            var query = _costCalculationDbSet.AsQueryable();

            var filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<CostCalculationModel>.Filter(query, filterDictionary);

            if (!string.IsNullOrWhiteSpace(keyword))
                query = query.Where(entity => entity.ProductionOrderNo.Contains(keyword) || entity.BuyerName.Contains(keyword) || entity.InstructionName.Contains(keyword));

            var orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<CostCalculationModel>.Order(query, orderDictionary);

            var result = new CostCalculationPagedListWrapper()
            {
                Data = await query.Skip((page - 1) * size).Take(size).Select(entity => new CostCalculationPagedListData(entity)).ToListAsync(),
                TotalData = await query.CountAsync()
            };
            return result;
        }

        public async Task<CostCalculationViewModel> GetSingleById(int id)
        {
            var costCalculation = await _costCalculationDbSet.FirstOrDefaultAsync(entity => entity.Id == id);
            var costCalculationMachines = _costCalculationMachineDbSet.Where(entity => entity.CostCalculationId == id).OrderBy(entity => entity.Index).ToList();
            var costCalculationChemicals = _costCalculationChemicalDbSet.Where(entity => entity.CostCalculationId == id).ToList();

            if (costCalculation != null)
                return new CostCalculationViewModel(costCalculation, costCalculationMachines, costCalculationChemicals);
            else 
                return null;
        }

        public async Task<int> InsertSingle(CostCalculationModel createModel)
        {
            do
            {
                createModel.Code = CodeGenerator.Generate();
            }
            while (_costCalculationDbSet.Any(entity => entity.Code.Equals(createModel.Code)));

            EntityExtension.FlagForCreate(createModel, _identityService.Username, UserAgent);

            createModel.Machines.ToList().ForEach(machine =>
            {
                EntityExtension.FlagForCreate(machine, _identityService.Username, UserAgent);
                machine.Chemicals.ToList().ForEach(chemical =>
                {
                    EntityExtension.FlagForCreate(chemical, _identityService.Username, UserAgent);
                });
            });

            _costCalculationDbSet.Add(createModel);
            await _dbContext.SaveChangesAsync();

            var chemicalModels = createModel.Machines.ToList().SelectMany(entity =>
            {
                var result = entity.Chemicals.ToList().Select(chemical =>
                {
                    chemical.CostCalculationId = createModel.Id;
                    return chemical;
                });
                return result;
            });

            _costCalculationChemicalDbSet.UpdateRange(chemicalModels);
            return await _dbContext.SaveChangesAsync();
        }

        public Task<bool> IsDataExistsById(int id)
        {
            return _costCalculationDbSet.AnyAsync(entity => entity.Id == id);
        }

        public Task<int> UpdateSingle(int id, CostCalculationViewModel updateViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
