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
using System.IO;
using System.Data;
using Com.Danliris.Service.Finishing.Printing.Lib.Helpers;

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

        public ReadResponse<DailyOperationViewModel> GetReport(int page, int size, int kanbanID, int machineID, DateTime? dateFrom, DateTime? dateTo, int offSet)
        {
            var queries = GetReport(kanbanID, machineID, dateFrom, dateTo, offSet);
            Pageable<DailyOperationViewModel> pageable = new Pageable<DailyOperationViewModel>(queries, page - 1, size);
            List<DailyOperationViewModel> data = pageable.Data.ToList();

            return new ReadResponse<DailyOperationViewModel>(data, pageable.TotalCount, new Dictionary<string, string>(), new List<string>());
        }

        public List<DailyOperationViewModel> GetReport(int kanbanID, int machineID, DateTime? dateFrom, DateTime? dateTo, int offSet)
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
                    .Where(x => DateTime.UtcNow.AddHours(offSet).Date.AddDays(-30) <= x.DateInput.Value.AddHours(offSet).Date
                        && x.DateInput.Value.AddHours(offSet).Date <= DateTime.UtcNow.AddHours(offSet).Date);
            }
            else if (dateFrom == null && dateTo != null)
            {
                query = query
                    .Where(x => dateTo.Value.Date.AddDays(-30) <= x.DateInput.Value.AddHours(offSet).Date
                        && x.DateInput.Value.AddHours(offSet).Date <= dateTo.Value.Date);
            }
            else if (dateTo == null && dateFrom != null)
            {
                query = query
                    .Where(x => dateFrom.Value.Date <= x.DateInput.Value.AddHours(offSet).Date
                        && x.DateInput.Value.AddHours(offSet).Date <= dateFrom.Value.Date.AddDays(30));
            }
            else
            {
                query = query
                    .Where(x => dateFrom.Value.Date <= x.DateInput.Value.AddHours(offSet).Date
                        && x.DateInput.Value.AddHours(offSet).Date <= dateTo.Value.Date);
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
                Type = x.Type,
                LastModifiedUtc = x.LastModifiedUtc

            });
            return queries.ToList();
        }

        public MemoryStream GenerateExcel(int kanbanID, int machineID, DateTime? dateFrom, DateTime? dateTo, int offSet)
        {
            var data = GetReport(kanbanID, machineID, dateFrom, dateTo, offSet);
            data = data.OrderByDescending(x => x.LastModifiedUtc).ToList();

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No Order", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No Kereta", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Reproses", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Mesin", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Step Proses", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Material", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Warna", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Lebar Kain (inch)", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jenis Proses", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Tgl Input", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jam Input", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Input", DataType = typeof(Double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Tgl Output", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jam Output", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "BQ", DataType = typeof(Double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "BS", DataType = typeof(Double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Keterangan BQ", DataType = typeof(String) });

            if (data.Count == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", 0, "", "", 0, 0, "");
            }
            else
            {
                int index = 1;
                foreach (var item in data)
                {
                    dt.Rows.Add(index++, item.Kanban.ProductionOrder.OrderNo, item.Kanban.Cart.CartNumber, item.Kanban.IsReprocess.ToString(), item.Machine.Name,
                        item.Step.Process, item.Kanban.ProductionOrder.Material.Name, item.Kanban.SelectedProductionOrderDetail.ColorRequest, item.Kanban.FinishWidth,
                        item.Kanban.ProductionOrder.ProcessType.Name, item.DateInput == null ? "" : item.DateInput.GetValueOrDefault().AddHours(offSet).ToString("dd/MM/yyyy"), 
                        item.TimeInput == null ? "" : item.TimeInput.GetValueOrDefault().ToString(), item.Input.GetValueOrDefault(),
                        item.DateOutput == null ? "" : item.DateOutput.GetValueOrDefault().AddHours(offSet).ToString("dd/MM/yyyy"), item.TimeOutput == null ? "" : item.TimeOutput.GetValueOrDefault().ToString(), 
                        item.GoodOutput.GetValueOrDefault(), item.BadOutput.GetValueOrDefault(), "");
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Daily Operation") }, true);
        }
    }
}
