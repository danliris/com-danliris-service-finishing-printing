using Com.Danliris.Service.Finishing.Printing.Lib.Models.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.Machine;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Kanban
{
    public class KanbanLogic : BaseLogic<KanbanModel>
    {
        private const string UserAgent = "production-service";
        private readonly ProductionDbContext DbContext;
        private readonly DbSet<KanbanInstructionModel> KanbanInstructionDbSet;
        private readonly DbSet<KanbanStepModel> KanbanStepDbSet;
        private readonly DbSet<KanbanStepIndicatorModel> KanbanStepIndicatorDbSet;
        private readonly DbSet<MachineModel> MachineDbSet;
        public KanbanLogic(IIdentityService identityService, ProductionDbContext dbContext) : base(identityService, dbContext)
        {
            DbContext = dbContext;
            KanbanInstructionDbSet = dbContext.Set<KanbanInstructionModel>();
            KanbanStepDbSet = dbContext.Set<KanbanStepModel>();
            KanbanStepIndicatorDbSet = dbContext.Set<KanbanStepIndicatorModel>();
            MachineDbSet = dbContext.Set<MachineModel>();
        }

        public override void CreateModel(KanbanModel model)
        {
            EntityExtension.FlagForCreate(model.Instruction, IdentityService.Username, UserAgent);

            if (model.OldKanbanId > 0)
            {
                var OldKanban = DbSet.FirstOrDefault(a => a.Id == model.OldKanbanId);
                OldKanban.IsInactive = true;
                OldKanban.IsComplete = true;
                DbSet.Update(OldKanban);
            }

            foreach (var step in model.Instruction.Steps)
            {
                EntityExtension.FlagForCreate(step, IdentityService.Username, UserAgent);
                foreach (var stepIndicator in step.StepIndicators)
                {
                    EntityExtension.FlagForCreate(stepIndicator, IdentityService.Username, UserAgent);
                }
            }
            base.CreateModel(model);
        }

        public override async Task DeleteModel(int id)
        {
            var model = await ReadModelById(id);
            EntityExtension.FlagForDelete(model, IdentityService.Username, UserAgent);
            EntityExtension.FlagForDelete(model.Instruction, IdentityService.Username, UserAgent);
            foreach (var step in model.Instruction.Steps)
            {
                EntityExtension.FlagForDelete(step, IdentityService.Username, UserAgent);
                foreach (var stepIndicator in step.StepIndicators)
                {
                    EntityExtension.FlagForDelete(stepIndicator, IdentityService.Username, UserAgent);
                }
            }
            DbSet.Update(model);
        }

        public override async Task<KanbanModel> ReadModelById(int id)
        {
            var Result = await DbSet.FirstOrDefaultAsync(d => d.Id.Equals(id) && !d.IsDeleted);
            Result.Instruction = await KanbanInstructionDbSet.FirstOrDefaultAsync(e => e.KanbanId.Equals(id) && !e.IsDeleted);
            Result.Instruction.Steps = await KanbanStepDbSet.Where(w => w.InstructionId.Equals(Result.Instruction.Id) && !w.IsDeleted).ToListAsync();
            foreach (var step in Result.Instruction.Steps)
            {
                step.StepIndicators = await KanbanStepIndicatorDbSet.Where(w => w.StepId.Equals(step.Id) && !w.IsDeleted).ToListAsync();
                step.Machine = await MachineDbSet.Where(w => w.Id.Equals(step.MachineId) && !w.IsDeleted).SingleOrDefaultAsync();
            }
            return Result;
        }

        public override void UpdateModelAsync(int id, KanbanModel model)
        {
            if (model.Instruction != null && model.Instruction.Steps != null)
            {
                EntityExtension.FlagForUpdate(model.Instruction, IdentityService.Username, UserAgent);
                HashSet<int> stepIds = KanbanStepDbSet.Where(d => d.InstructionId == model.Instruction.Id).Select(d => d.Id).ToHashSet();

                foreach (int stepId in stepIds)
                {
                    var step = model.Instruction.Steps.FirstOrDefault(prop => prop.Id.Equals(stepId));
                    if (step == null)
                    {
                        step = KanbanStepDbSet.Where(w => w.Id == stepId).FirstOrDefault();
                        var stepIndicators = KanbanStepIndicatorDbSet.Where(w => w.StepId == step.Id).ToList();
                        EntityExtension.FlagForDelete(step, IdentityService.Username, UserAgent);
                        KanbanStepDbSet.Update(step);

                        foreach (var stepIndicator in stepIndicators)
                        {
                            EntityExtension.FlagForDelete(stepIndicator, IdentityService.Username, UserAgent);
                            KanbanStepIndicatorDbSet.Update(stepIndicator);
                        }
                    }
                    else
                    {
                        //if (step.InstructionId <= 0)
                        //    step.InstructionId = model.Instruction.Id;
                        

                        if (!step.StepIndicators.Any(stepIndicator => !(stepIndicator.Id <= 0)))
                        {
                            var existingStepIndicators = KanbanStepIndicatorDbSet.Where(stepIndicator => stepIndicator.StepId.Equals(step.Id)).ToList();
                            foreach (var existingStepIndicator in existingStepIndicators)
                            {
                                EntityExtension.FlagForDelete(existingStepIndicator, IdentityService.Username, UserAgent);
                            }
                            KanbanStepIndicatorDbSet.UpdateRange(existingStepIndicators);
                            foreach (var stepIndicator in step.StepIndicators)
                            {
                                //stepIndicator.Id = 0;
                                EntityExtension.FlagForCreate(stepIndicator, IdentityService.Username, UserAgent);
                                KanbanStepIndicatorDbSet.Add(stepIndicator);
                            }
                        }
                        else
                            foreach (var stepIndicator in step.StepIndicators)
                            {
                                EntityExtension.FlagForUpdate(stepIndicator, IdentityService.Username, UserAgent);
                                KanbanStepIndicatorDbSet.Update(stepIndicator);
                            }

                        EntityExtension.FlagForUpdate(step, IdentityService.Username, UserAgent);
                        KanbanStepDbSet.Update(step);
                    }
                }

                foreach (var step in model.Instruction.Steps)
                {
                    if (step.Id == 0)
                    {
                        EntityExtension.FlagForCreate(step, IdentityService.Username, UserAgent);
                        KanbanStepDbSet.Add(step);

                        foreach (var stepIndicator in step.StepIndicators)
                        {
                            EntityExtension.FlagForCreate(stepIndicator, IdentityService.Username, UserAgent);
                            KanbanStepIndicatorDbSet.Add(stepIndicator);
                        }
                    }
                }
            }
            base.UpdateModelAsync(id, model);
        }
    }
}
