using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.DailyOperation;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.DailyOperation;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Daily_Operation;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using Com.Moonlay.NetCore.Lib;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.Machine;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore.Internal;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Daily_Operation;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Kanban;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Sales.FinishingPrinting;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.Machine;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Master;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.DailyOperation
{
    public class DailyOperationFacade : IDailyOperationFacade
    {
        private readonly ProductionDbContext DbContext;
        public readonly DbSet<DailyOperationModel> DbSet;
        private readonly DailyOperationLogic DailyOperationLogic;
        public DailyOperationFacade(IServiceProvider serviceProvider, ProductionDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = DbContext.Set<DailyOperationModel>();
            this.DailyOperationLogic = serviceProvider.GetService<DailyOperationLogic>();
        }

        public async Task<int> CreateAsync(DailyOperationModel model)
        {
            do
            {
                model.Code = CodeGenerator.Generate();
            }
            while (DbSet.Any(d => d.Code.Equals(model.Code)));

            this.DailyOperationLogic.CreateModel(model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await DailyOperationLogic.DeleteModel(id);
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<DailyOperationModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<DailyOperationModel> query = DbSet;

            List<string> searchAttributes = new List<string>()
            {
                "Code"
            };
            query = QueryHelper<DailyOperationModel>.Search(query, searchAttributes, keyword);


            Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DailyOperationModel>.Filter(query, filterDictionary);

            List<string> selectedFields = new List<string>()
                {
                    "Id","Type","GoodOutput","Step","BadOutput","Code","Machine","Kanban","Input","Shift","DateInput","DateOutput","LastModifiedUtc"
                };

            query = from daily in query
                    join machine in DbContext.Machine on daily.MachineId equals machine.Id
                    join kanban in DbContext.Kanbans on daily.KanbanId equals kanban.Id
                    select new DailyOperationModel
                    {
                        Id = daily.Id,
                        Code = daily.Code,
                        Type = daily.Type,
                        StepProcess = daily.StepProcess,
                        Shift = daily.Shift,
                        Kanban = kanban,
                        Machine = machine,
                        DateInput = daily.DateInput,
                        Input = daily.Input,
                        DateOutput = daily.DateOutput,
                        GoodOutput = daily.GoodOutput,
                        BadOutput = daily.BadOutput,
                        LastModifiedUtc = daily.LastModifiedUtc
                    };


            Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DailyOperationModel>.Order(query, orderDictionary);

            Pageable<DailyOperationModel> pageable = new Pageable<DailyOperationModel>(query, page - 1, size);
            List<DailyOperationModel> data = pageable.Data.ToList();
            int totalData = pageable.TotalCount;

            return new ReadResponse<DailyOperationModel>(data, totalData, orderDictionary, selectedFields);
        }

        public async Task<DailyOperationModel> ReadByIdAsync(int id)
        {
            return await DailyOperationLogic.ReadModelById(id);
        }

        public async Task<int> UpdateAsync(int id, DailyOperationModel model)
        {
            this.DailyOperationLogic.UpdateModelAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<DailyOperationViewModel> GetReport(int page, int size, int kanbanID, int machineID, DateTimeOffset? dateFrom, DateTimeOffset? dateTo)
        {
            IQueryable<DailyOperationModel> query = DbContext.DailyOperation.AsQueryable();
            IEnumerable<DailyOperationViewModel> queries;

            if (kanbanID != -1)
                query = query.Where(x => x.KanbanId == kanbanID);

            if (machineID != -1)
                query = query.Where(x => x.MachineId == machineID);
            
            if (dateFrom == null && dateTo == null)
            {
                query = query
                    .Where(x => DateTimeOffset.UtcNow.Date.AddDays(-30) <= x.DateInput.Value.Date 
                        && x.DateInput.Value.Date <= DateTimeOffset.UtcNow.Date);
            }
            else if (dateFrom == null && dateTo != null)
            {
                query = query
                    .Where(x => dateTo.Value.Date.AddDays(-30) <= x.DateInput.Value.Date
                        && x.DateInput.Value.Date <= dateTo.Value.Date);
            }
            else if (dateTo == null && dateFrom != null)
            {
                query = query
                    .Where(x =>  dateFrom.Value.Date <= x.DateInput.Value.Date
                        && x.DateInput.Value.Date <= dateFrom.Value.Date.AddDays(30));
            }
            else
            {
                query = query
                    .Where(x => dateFrom.Value.Date <= x.DateInput.Value.Date
                        && x.DateInput.Value.Date <= dateTo.Value.Date);
            }
            queries = query.Select(x => new DailyOperationViewModel()
            {
                Kanban = new KanbanViewModel()
                {
                    ProductionOrder = new ProductionOrderIntegrationViewModel()
                    {
                        OrderNo = x.Kanban.ProductionOrderOrderNo,
                        Material = new MaterialIntegrationViewModel()
                        {
                            Name = x.Kanban.ProductionOrderMaterialName
                        },
                        FinishWidth = x.Kanban.FinishWidth,
                        ProcessType = new ProcessTypeIntegrationViewModel()
                        {
                            Name = x.Kanban.ProductionOrderProcessTypeName
                        }
                    },
                    Cart = new CartViewModel()
                    {
                        CartNumber = x.Kanban.CartCartNumber
                    },
                    IsReprocess = x.Kanban.IsReprocess,
                    SelectedProductionOrderDetail = new ProductionOrderDetailIntegrationViewModel()
                    {
                        ColorRequest = x.Kanban.SelectedProductionOrderDetailColorRequest
                    }
                },
                Machine = new MachineViewModel()
                {
                    Name = x.Machine.Name
                },
                Step = new MachineStepViewModel()
                {
                    Process = x.StepProcess
                },
                BadOutput = x.BadOutput,
                GoodOutput = x.GoodOutput,
                DateInput = x.DateInput,
                DateOutput = x.DateOutput,
                TimeInput = x.TimeInput,
                TimeOutput = x.TimeOutput,
                Input = x.Input,
                Id = x.Id,
                Active = x.Active,
                Code = x.Code,
                CreatedAgent = x.CreatedAgent,
                IsDeleted = x.IsDeleted,
                Shift = x.Shift,
                Type = x.Type

            });
            Pageable<DailyOperationViewModel> pageable = new Pageable<DailyOperationViewModel>(queries, page - 1, size);
            List<DailyOperationViewModel> data = pageable.Data.ToList();

            return new ReadResponse<DailyOperationViewModel>(data, pageable.TotalCount, new Dictionary<string, string>(), new List<string>());
        }
    }
}
