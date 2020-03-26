using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.DailyOperation;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.DailyOperation;
using Com.Danliris.Service.Finishing.Printing.Lib.Helpers;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Daily_Operation;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Daily_Operation;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.Machine;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Utilities;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Master;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Sales.FinishingPrinting;
using Com.Moonlay.NetCore.Lib;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.DailyOperation
{
    public class DailyOperationFacade : IDailyOperationFacade
    {
        public readonly ProductionDbContext DbContext;
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
            var internalTransaction = DbContext.Database.CurrentTransaction == null;
            var transaction = !internalTransaction ? DbContext.Database.CurrentTransaction : DbContext.Database.BeginTransaction();

            try
            {
                int result = 0;
                do
                {
                    model.Code = CodeGenerator.Generate();
                }
                while (DbSet.Any(d => d.Code.Equals(model.Code)));

                this.DailyOperationLogic.CreateModel(model);
                result = await DbContext.SaveChangesAsync();

                DailyOperationLogic.CreateSnapshot(model);
                result += await DbContext.SaveChangesAsync();

                if (internalTransaction)
                    transaction.Commit();

                return result;
            }
            catch (Exception ex)
            {
                if (internalTransaction)
                    transaction.Rollback();
                throw ex;
            }

        }

        public async Task<int> DeleteAsync(int id)
        {
            var internalTransaction = DbContext.Database.CurrentTransaction == null;
            var transaction = !internalTransaction ? DbContext.Database.CurrentTransaction : DbContext.Database.BeginTransaction();

            try
            {
                int result = 0;

                var model = await ReadByIdAsync(id);

                DailyOperationLogic.DeleteSnapshot(model);
                result = await DbContext.SaveChangesAsync();
                await DailyOperationLogic.DeleteModel(id);
                result += await DbContext.SaveChangesAsync();

                transaction.Commit();

                return result;
            }
            catch (Exception ex)
            {
                if (internalTransaction)
                    transaction.Rollback();
                throw ex;
            }

        }

        //public ReadResponse<DailyOperationModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        //{
        //    IQueryable<DailyOperationModel> query = DbSet;

        //    List<string> searchAttributes = new List<string>()
        //    {
        //        "Code"
        //    };
        //    query = QueryHelper<DailyOperationModel>.Search(query, searchAttributes, keyword);


        //    Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
        //    query = QueryHelper<DailyOperationModel>.Filter(query, filterDictionary);

        //    List<string> selectedFields = new List<string>()
        //        {
        //            "Id","Type","GoodOutput","Step","BadOutput","Code","Machine","Kanban","Input","Shift","DateInput","DateOutput","LastModifiedUtc"
        //        };




        //    Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
        //    query = QueryHelper<DailyOperationModel>.Order(query, orderDictionary);

        //    Pageable<DailyOperationModel> pageable = new Pageable<DailyOperationModel>(query, page - 1, size);
        //    List<DailyOperationModel> data = pageable.Data.ToList();

        //    data = (from daily in data
        //            join machine in DbContext.Machine on daily.MachineId equals machine.Id
        //            join kanban in DbContext.Kanbans on daily.KanbanId equals kanban.Id
        //            select new DailyOperationModel
        //            {
        //                Id = daily.Id,
        //                Code = daily.Code,
        //                Type = daily.Type,
        //                StepProcess = daily.StepProcess,
        //                Shift = daily.Shift,
        //                Kanban = kanban,
        //                Machine = machine,
        //                DateInput = daily.DateInput,
        //                Input = daily.Input,
        //                DateOutput = daily.DateOutput,
        //                GoodOutput = daily.GoodOutput,
        //                BadOutput = daily.BadOutput,
        //                LastModifiedUtc = daily.LastModifiedUtc
        //            }).ToList();
        //    int totalData = pageable.TotalCount;

        //    return new ReadResponse<DailyOperationModel>(data, totalData, orderDictionary, selectedFields);
        //}

        public ReadResponse<DailyOperationModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<DailyOperationModel> query = DbSet;

            query = (from daily in query
                     join machine in DbContext.Machine on daily.MachineId equals machine.Id
                     join kanban in DbContext.Kanbans on daily.KanbanId equals kanban.Id
                     where !string.IsNullOrWhiteSpace(keyword) ? machine.Name.Contains(keyword) : true
                     && !string.IsNullOrWhiteSpace(keyword) ? kanban.ProductionOrderOrderNo.Contains(keyword) : true
                     && !string.IsNullOrWhiteSpace(keyword) ? daily.StepProcess.Contains(keyword) : true
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
                     });

            Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DailyOperationModel>.Order(query, orderDictionary);

            var data = query.Skip((page - 1) * size).Take(size).ToList();

            List<string> selectedFields = new List<string>()
            {
                "Id", "Type", "GoodOutput", "Step", "BadOutput", "Code", "Machine", "Kanban", "Input", "Shift", "DateInput", "DateOutput", "LastModifiedUtc"
            };

            return new ReadResponse<DailyOperationModel>(data, query.Count(), orderDictionary, selectedFields);
        }

        public ReadResponse<DailyOperationModel> Read(int page, int size, string order, List<string> select, string keyword, string filter, string machine, string orderNo, string cartNo, string stepProcess, DateTime? startDate, DateTime? endDate)
        {
            IQueryable<DailyOperationModel> query = DbSet;

            //selected column to search options
            //Nomor SPP
            //Kereta
            //Proses
            //Mesin
            if (startDate.HasValue && endDate.HasValue)
                query = query.Where(w => w.LastModifiedUtc.Date >= startDate.Value.Date && w.LastModifiedUtc.Date <= endDate.Value.Date);
            else if (startDate.HasValue)
                query = query.Where(w => w.LastModifiedUtc.Date >= startDate.Value.Date);
            else if (endDate.HasValue)
                query = query.Where(w => w.LastModifiedUtc.Date <= endDate.Value.Date);

            query = (from daily in query
                     join machines in DbContext.Machine on daily.MachineId equals machines.Id into dailyMachine
                     from machines in dailyMachine.DefaultIfEmpty()
                     join kanbans in DbContext.Kanbans on daily.KanbanId equals kanbans.Id into dailyKanban
                     from kanbans in dailyKanban.DefaultIfEmpty()
                         //where
                         //!string.IsNullOrWhiteSpace(machine) ? machines.Name.Contains(machine) : machines.Name.Equals(machines.Name)
                         //&& !string.IsNullOrWhiteSpace(cartNo) ? kanbans.CartCartNumber.Contains(cartNo) : kanbans.CartCartNumber.Equals(kanbans.CartCartNumber)
                         //&& !string.IsNullOrWhiteSpace(stepProcess) ? daily.StepProcess.Contains(stepProcess) : daily.StepProcess.Equals(daily.StepProcess)
                         //&& !string.IsNullOrWhiteSpace(orderNo) ? kanbans.ProductionOrderOrderNo.Contains(orderNo) : kanbans.ProductionOrderOrderNo.Equals(kanbans.ProductionOrderOrderNo)
                     select new DailyOperationModel
                     {
                         Id = daily.Id,
                         Code = daily.Code,
                         Type = daily.Type,
                         StepProcess = daily.StepProcess,
                         Shift = daily.Shift,
                         Kanban = kanbans,
                         Machine = machines,
                         DateInput = daily.DateInput,
                         Input = daily.Input,
                         DateOutput = daily.DateOutput,
                         GoodOutput = daily.GoodOutput,
                         BadOutput = daily.BadOutput,
                         LastModifiedUtc = daily.LastModifiedUtc
                     });

            if (!string.IsNullOrWhiteSpace(machine))
                query = query.Where(w => w.Machine.Name.Contains(machine));
            if (!string.IsNullOrWhiteSpace(cartNo))
                query = query.Where(w => w.Kanban.CartCartNumber.Contains(cartNo));
            if (!string.IsNullOrWhiteSpace(stepProcess))
                query = query.Where(w => w.StepProcess.Contains(stepProcess));
            if (!string.IsNullOrWhiteSpace(orderNo))
                query = query.Where(w => w.Kanban.ProductionOrderOrderNo.Contains(orderNo));


            Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DailyOperationModel>.Order(query, orderDictionary);

            var data = query.Skip((page - 1) * size).Take(size).ToList();

            List<string> selectedFields = new List<string>()
            {
                "Id", "Type", "GoodOutput", "Step", "BadOutput", "Code", "Machine", "Kanban", "Input", "Shift", "DateInput", "DateOutput", "LastModifiedUtc"
            };

            return new ReadResponse<DailyOperationModel>(data, query.Count(), orderDictionary, selectedFields);
        }

        public async Task<DailyOperationModel> ReadByIdAsync(int id)
        {
            return await DailyOperationLogic.ReadModelById(id);
        }

        public async Task<int> UpdateAsync(int id, DailyOperationModel model)
        {
            var internalTransaction = DbContext.Database.CurrentTransaction == null;
            var transaction = !internalTransaction ? DbContext.Database.CurrentTransaction : DbContext.Database.BeginTransaction();
            try
            {
                int result = 0;
                await this.DailyOperationLogic.UpdateModelAsync(id, model);
                result = await DbContext.SaveChangesAsync();

                DailyOperationLogic.EditSnapshot(model);

                result += await DbContext.SaveChangesAsync();

                transaction.Commit();

                return result;
            }
            catch (Exception ex)
            {
                if (internalTransaction)
                    transaction.Rollback();
                throw ex;
            }
        }

        public ReadResponse<DailyOperationViewModel> GetReport(int page, int size, int kanbanID, int machineID, DateTime? dateFrom, DateTime? dateTo, int offSet)
        {
            var queries = GetReport(kanbanID, machineID, dateFrom, dateTo, offSet);
            Pageable<DailyOperationViewModel> pageable = new Pageable<DailyOperationViewModel>(queries, page - 1, size);
            List<DailyOperationViewModel> data = pageable.Data.ToList();

            return new ReadResponse<DailyOperationViewModel>(queries, pageable.TotalCount, new Dictionary<string, string>(), new List<string>());
        }

        public List<DailyOperationViewModel> GetReport(int kanbanID, int machineID, DateTime? dateFrom, DateTime? dateTo, int offSet)
        {
            IQueryable<DailyOperationModel> query = DbContext.DailyOperation.AsQueryable();
            List<DailyOperationViewModel> dailyOperations;

            if (kanbanID != -1)
                query = query.Where(x => x.KanbanId == kanbanID);

            if (machineID != -1)
                query = query.Where(x => x.MachineId == machineID);

            if (dateFrom == null && dateTo == null)
            {
                query = query
                    .Where(x => DateTime.UtcNow.AddHours(offSet).Date.AddDays(-30) <= x.DateInput.GetValueOrDefault().AddHours(offSet).Date
                        && x.DateInput.GetValueOrDefault().AddHours(offSet).Date <= DateTime.UtcNow.AddHours(offSet).Date);
            }
            else if (dateFrom == null && dateTo != null)
            {
                query = query
                    .Where(x => dateTo.GetValueOrDefault().Date.AddDays(-30) <= x.DateInput.GetValueOrDefault().AddHours(offSet).Date
                        && x.DateInput.GetValueOrDefault().AddHours(offSet).Date <= dateTo.GetValueOrDefault().Date);
            }
            else if (dateTo == null && dateFrom != null)
            {
                query = query
                    .Where(x => dateFrom.GetValueOrDefault().Date <= x.DateInput.GetValueOrDefault().AddHours(offSet).Date
                        && x.DateInput.GetValueOrDefault().AddHours(offSet).Date <= dateFrom.GetValueOrDefault().Date.AddDays(30));
            }
            else
            {
                query = query
                    .Where(x => dateFrom.GetValueOrDefault().Date <= x.DateInput.GetValueOrDefault().AddHours(offSet).Date
                        && x.DateInput.GetValueOrDefault().AddHours(offSet).Date <= dateTo.GetValueOrDefault().Date);
            }
            dailyOperations = query.Select(x => new DailyOperationViewModel()
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
                    },
                    Id = x.KanbanId
                },
                Machine = new MachineViewModel()
                {
                    Name = x.Machine.Name,
                    Id = x.MachineId
                },
                Step = new MachineStepViewModel()
                {
                    Process = x.StepProcess,
                    Id = x.StepId
                },

                DateInput = x.DateInput,

                TimeInput = x.TimeInput,

                Input = x.Input,
                Id = x.Id,
                Active = x.Active,
                Code = x.Code,
                CreatedAgent = x.CreatedAgent,
                IsDeleted = x.IsDeleted,
                Shift = x.Shift,
                Type = x.Type,
                LastModifiedUtc = x.LastModifiedUtc

            }).ToList();

            foreach (var dailyOperation in dailyOperations)
            {
                var outputModel = GetOutputDailyOperationModel(dailyOperation.Kanban.Id, dailyOperation.Machine.Id, dailyOperation.Step.Id);
                if (outputModel != null)
                {
                    dailyOperation.BadOutput = outputModel.BadOutput;
                    dailyOperation.GoodOutput = outputModel.GoodOutput;
                    dailyOperation.DateOutput = outputModel.DateOutput;
                    dailyOperation.TimeOutput = outputModel.TimeOutput;
                    dailyOperation.BadOutputDescription = GetOutputBadDescription(outputModel);
                }

            }
            return dailyOperations;
        }

        public DailyOperationModel GetOutputDailyOperationModel(int kanbanID, int machineID, int stepID)
        {
            return DbContext.DailyOperation.Include(x => x.BadOutputReasons).FirstOrDefault(x => x.KanbanId == kanbanID && x.MachineId == machineID && x.StepId == stepID
                    && x.Input == null && x.DateOutput != null);
        }

        public string GetOutputBadDescription(DailyOperationModel outputModel)
        {
            List<string> badOutputDescription = new List<string>();
            int index = 1;
            foreach (var model in outputModel.BadOutputReasons)
            {
                badOutputDescription.Add(string.Format("{0}. {1} | {2}. {3}", index++, model.MachineName, model.BadOutputReason, model.Action));
            }

            return string.Join(Environment.NewLine, badOutputDescription);
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

        public Task<List<DailyOperationKanbanViewModel>> GetJoinKanban(string no)
        {
            IQueryable<DailyOperationKanbanViewModel> data;
            if (string.IsNullOrEmpty(no))
            {
                data = from kanban in DbContext.Kanbans
                       join daily in DbContext.DailyOperation on kanban.Id equals daily.KanbanId
                       select new DailyOperationKanbanViewModel
                       {
                           OrderNo = kanban.ProductionOrderOrderNo,
                           OrderQuantity = daily.Input.GetValueOrDefault()
                       };


            }
            else
            {
                data = from kanban in DbContext.Kanbans
                       join daily in DbContext.DailyOperation on kanban.Id equals daily.KanbanId
                       join kanbanins in DbContext.KanbanInstructions on kanban.Id equals kanbanins.KanbanId
                       join kanbansteps in DbContext.KanbanSteps on kanbanins.Id equals kanbansteps.InstructionId
                       join machine in DbContext.Machine on kanbansteps.MachineId equals machine.Id
                       where daily.Input.HasValue && (daily.Input.Value > 0) && kanban.ProductionOrderOrderNo == no
                       select new DailyOperationKanbanViewModel
                       {
                           OrderNo = kanban.ProductionOrderOrderNo,
                           OrderQuantity = daily.Input.GetValueOrDefault(),
                           Area = kanbansteps.ProcessArea,
                           Color = kanban.SelectedProductionOrderDetailColorRequest,
                           Machine = machine.Name,
                           Step = kanbansteps.Process
                       };


            }
            return data.AsNoTracking().ToListAsync();
        }

        public Task<bool> HasOutput(int kanbanId, string stepProcess)
        {
            return DbSet.AnyAsync(x => x.KanbanId == kanbanId && x.StepProcess == stepProcess && x.Type.ToLower() == "output");
        }

        //public async Task<int> ETLKanbanStepIndex(int page)
        //{
        //    return await DailyOperationLogic.ETLKanbanStepIndex(page);
        //}
    }
}
