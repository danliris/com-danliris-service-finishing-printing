using Com.Danliris.Service.Production.Lib.Models.Master.Instruction;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Production.Lib.BusinessLogic.Implementations.Master.Instruction
{
    public class InstructionLogic : BaseLogic<InstructionModel>
    {
        private const string UserAgent = "production-service";
        private readonly ProductionDbContext DbContext;
        private readonly DbSet<InstructionStepModel> _DbSetInstructionStep;
        private readonly DbSet<InstructionStepIndicatorModel> _DbSetInstructionStepIndicator;

        //private StepIndicatorLogic StepIndicatorLogic;

        public InstructionLogic(IIdentityService identityService, ProductionDbContext dbContext) : base(identityService, dbContext)
        {
            DbContext = dbContext;
            _DbSetInstructionStep = DbContext.Set<InstructionStepModel>();
            _DbSetInstructionStepIndicator = DbContext.Set<InstructionStepIndicatorModel>();
        }

        public override void CreateModel(InstructionModel model)
        {
            foreach (var step in model.Steps)
            {
                EntityExtension.FlagForCreate(step, IdentityService.Username, UserAgent);
                foreach (var stepIndicator in step.StepIndicators)
                {
                    EntityExtension.FlagForCreate(stepIndicator, IdentityService.Username, UserAgent);
                }
            }
            base.CreateModel(model);
        }

        public override Task<InstructionModel> ReadModelById(int id)
        {
            return DbSet.Include(d => d.Steps).ThenInclude(d => d.StepIndicators).FirstOrDefaultAsync(d => d.Id.Equals(id) && d.IsDeleted.Equals(false));
        }

        public override async Task UpdateModelAsync(int id, InstructionModel model)
        {
            if (model.Steps != null)
            {
                HashSet<int> stepIds = _DbSetInstructionStep.Where(d => d.InstructionId == id).Select(d => d.Id).ToHashSet();

                foreach (int stepId in stepIds)
                {
                    var step = model.Steps.FirstOrDefault(prop => prop.Id.Equals(stepId));
                    if (step == null)
                    {
                        step = _DbSetInstructionStep.Where(w => w.Id == stepId).FirstOrDefault();
                        var stepIndicators = _DbSetInstructionStepIndicator.Where(w => w.StepId == step.Id).ToList();
                        EntityExtension.FlagForDelete(step, IdentityService.Username, UserAgent);
                        _DbSetInstructionStep.Update(step);

                        foreach (var stepIndicator in stepIndicators)
                        {
                            EntityExtension.FlagForDelete(stepIndicator, IdentityService.Username, UserAgent);
                            _DbSetInstructionStepIndicator.Update(stepIndicator);
                        }
                    }
                    else
                    {
                        EntityExtension.FlagForUpdate(step, IdentityService.Username, UserAgent);
                        _DbSetInstructionStep.Update(step);
                        foreach (var stepIndicator in step.StepIndicators)
                        {
                            EntityExtension.FlagForUpdate(stepIndicator, IdentityService.Username, UserAgent);
                            _DbSetInstructionStepIndicator.Update(stepIndicator);
                        }
                    }
                }

                foreach (var step in model.Steps)
                {
                    if (step.Id == 0)
                    {
                        EntityExtension.FlagForCreate(step, IdentityService.Username, UserAgent);
                        _DbSetInstructionStep.Add(step);

                        foreach (var stepIndicator in step.StepIndicators)
                        {
                            EntityExtension.FlagForCreate(stepIndicator, IdentityService.Username, UserAgent);
                            _DbSetInstructionStepIndicator.Add(stepIndicator);
                        }
                    }
                }
            }
            await base.UpdateModelAsync(id, model);
        }

        public override async Task DeleteModel(int id)
        {
            var model = await ReadModelById(id);

            foreach (var step in model.Steps)
            {
                EntityExtension.FlagForDelete(step, IdentityService.Username, UserAgent);

                foreach (var stepIndicator in step.StepIndicators)
                {
                    EntityExtension.FlagForDelete(stepIndicator, IdentityService.Username, UserAgent);
                }
            }

            EntityExtension.FlagForDelete(model, IdentityService.Username, UserAgent, true);
            DbSet.Update(model);
        }
    }
}
