using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Kanban;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using Com.Moonlay.NetCore.Lib;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Kanban
{
    public class KanbanFacade : IKanbanFacade
    {
        private readonly ProductionDbContext DbContext;
        private readonly DbSet<KanbanModel> DbSet;
        private readonly KanbanLogic KanbanLogic;

        public KanbanFacade(IServiceProvider serviceProvider, ProductionDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<KanbanModel>();
            KanbanLogic = serviceProvider.GetService<KanbanLogic>();
        }

        public ReadResponse<KanbanModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<KanbanModel> query = DbSet;

            List<string> searchAttributes = new List<string>()
            {
                "Code", "ProductionOrderOrderNo", "CartCartNumber"
            };
            query = QueryHelper<KanbanModel>.Search(query, searchAttributes, keyword);

            Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<KanbanModel>.Filter(query, filterDictionary);

            List<string> selectedFields = new List<string>()
            {
                "Id", "Code", "ProductionOrder", "Cart", "Instruction", "OldKanban", "SelectedProductionOrderDetail", "LastModifiedUtc"
            };
            query = query
                .Select(field => new KanbanModel
                {
                    Id = field.Id,
                    Code = field.Code,
                    CartCartNumber = field.CartCartNumber,
                    CurrentStepIndex = field.CurrentStepIndex,
                    IsBadOutput = field.IsBadOutput,
                    IsComplete = field.IsComplete,
                    IsInactive = field.IsInactive,
                    IsReprocess = field.IsReprocess,
                    Instruction = new KanbanInstructionModel()
                    {
                        Id = field.Instruction.Id,
                        Name = field.Instruction.Name,
                        Steps = new List<KanbanStepModel>(field.Instruction.Steps.Select(i => new KanbanStepModel()
                        {
                            Id = i.Id,
                            Process = i.Process,
                            ProcessArea = i.ProcessArea
                        }))
                    },
                    LastModifiedUtc = field.LastModifiedUtc,
                    ProductionOrderOrderNo = field.ProductionOrderOrderNo,
                    SelectedProductionOrderDetailColorRequest = field.SelectedProductionOrderDetailColorRequest,
                    SelectedProductionOrderDetailColorTemplate = field.SelectedProductionOrderDetailColorTemplate,
                    SelectedProductionOrderDetailColorTypeCode = field.SelectedProductionOrderDetailColorTypeCode,
                    SelectedProductionOrderDetailColorTypeName = field.SelectedProductionOrderDetailColorTypeName,
                    SelectedProductionOrderDetailId = field.SelectedProductionOrderDetailId,
                    SelectedProductionOrderDetailQuantity = field.SelectedProductionOrderDetailQuantity,
                    SelectedProductionOrderDetailUomUnit = field.SelectedProductionOrderDetailUomUnit
                });

            Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<KanbanModel>.Order(query, orderDictionary);

            Pageable<KanbanModel> pageable = new Pageable<KanbanModel>(query, page - 1, size);
            List<KanbanModel> data = pageable.Data.ToList();
            int totalData = pageable.TotalCount;

            return new ReadResponse<KanbanModel>(data, totalData, orderDictionary, selectedFields);
        }

        public async Task<int> CreateAsync(KanbanModel model)
        {
            do
            {
                model.Code = CodeGenerator.Generate();
            }
            while (DbSet.Any(d => d.Code.Equals(model.Code)));
            KanbanLogic.CreateModel(model);
            return await DbContext.SaveChangesAsync();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<KanbanModel> ReadByIdAsync(int id)
        {
            return await KanbanLogic.ReadModelById(id);
        }

        public async Task<int> UpdateAsync(int id, KanbanModel model)
        {
            KanbanLogic.UpdateModelAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }
    }
}
