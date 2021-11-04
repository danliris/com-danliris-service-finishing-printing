using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.Helpers;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Daily_Operation;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.Machine;
using Com.Danliris.Service.Finishing.Printing.Lib.Utilities;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Kanban;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Utilities;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Master;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Sales.FinishingPrinting;
using Com.Moonlay.NetCore.Lib;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Kanban
{
    public class KanbanFacade : IKanbanFacade
    {
        private readonly ProductionDbContext DbContext;
        private readonly DbSet<KanbanModel> DbSet;
        private readonly KanbanLogic KanbanLogic;

        public readonly DbSet<KanbanInstructionModel> KanbanInstructionDbSet;
        private readonly DbSet<KanbanStepModel> KanbanStepDbSet;
        private readonly DbSet<KanbanStepIndicatorModel> KanbanStepIndicatorDbSet;
        private readonly DbSet<MachineModel> MachineDbSet;

        private readonly string[] KanbanDataCells = { "A", "B", "C", "D" };
        private readonly string[] SnapshotDataCells = { "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P",
                                                        "Q", "R", "S","T", "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC",
                                                        "AD", "AE", "AF", "AG", "AH" };

        public KanbanFacade(IServiceProvider serviceProvider, ProductionDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<KanbanModel>();
            KanbanLogic = serviceProvider.GetService<KanbanLogic>();
            KanbanInstructionDbSet = DbContext.Set<KanbanInstructionModel>();
            KanbanStepDbSet = DbContext.Set<KanbanStepModel>();
            KanbanStepIndicatorDbSet = DbContext.Set<KanbanStepIndicatorModel>();
            MachineDbSet = DbContext.Set<MachineModel>();
        }

        public ReadResponse<KanbanModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<KanbanModel> query = DbSet;

            List<string> searchAttributes = new List<string>()
            {
                "Code", "ProductionOrderOrderNo", "CartCartNumber"
            };
            query = QueryHelper<KanbanModel>.Search(query, searchAttributes, keyword);
            object processFilterData = null;
            if (filter.Contains("CustomFilter#IntructionStepProcess"))
            {
                var processFilter = filter;

                Dictionary<string, object> processFilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(processFilter);

                foreach (var f in processFilterDictionary)
                {
                    string key = f.Key;
                    object Value = f.Value;
                    //string filterQuery = Value;
                    processFilterData = Value;
                }

                filter = "{}";


                query = query
                        .Where(x => x.Instruction.Steps.Any(y => y.Process == processFilterData.ToString()));


            }

            Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<KanbanModel>.Filter(query, filterDictionary);


            List<string> selectedFields = new List<string>()
            {
                "Id", "Code", "ProductionOrder","CurrentStepIndex", "Cart", "Instruction", "SelectedProductionOrderDetail", "LastModifiedUtc", "OldKanbanId", "IsComplete", "IsInactive",
                "IsReprocess","IsFulfilledOutput"
            };


            query = query
                .Select(field => new KanbanModel
                {
                    Id = field.Id,
                    Code = field.Code,
                    CartCartNumber = field.CartCartNumber,
                    CurrentStepIndex = field.CurrentStepIndex,
                    CartQty = field.CartQty,
                    IsBadOutput = field.IsBadOutput,
                    IsFulfilledOutput = field.IsFulfilledOutput,
                    IsComplete = field.IsComplete,
                    IsInactive = field.IsInactive,
                    IsReprocess = field.IsReprocess,
                    Instruction = new KanbanInstructionModel()
                    {
                        Id = field.Instruction.Id,
                        Name = field.Instruction.Name,
                        Steps = new List<KanbanStepModel>(field.Instruction.Steps.OrderBy(x => x.StepIndex).ThenBy(x => x.Id).Select(i => new KanbanStepModel()
                        {
                            Id = i.Id,
                            Process = i.Process,
                            ProcessArea = i.ProcessArea,
                            SelectedIndex = i.SelectedIndex,
                            StepIndex = i.StepIndex
                        }))
                    },
                    OldKanbanId = field.OldKanbanId,
                    LastModifiedUtc = field.LastModifiedUtc,
                    ProductionOrderMaterialName = field.ProductionOrderMaterialName,
                    ProductionOrderMaterialConstructionName = field.ProductionOrderMaterialConstructionName,
                    ProductionOrderYarnMaterialName = field.ProductionOrderYarnMaterialName,
                    FinishWidth = field.FinishWidth,
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
            int index = 1;
            foreach (var step in model.Instruction.Steps)
            {
                step.MachineId = step.Machine.Id;
                step.Machine = null;
                step.StepIndex = index;
                index++;
            }
            KanbanLogic.CreateModel(model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await KanbanLogic.DeleteModel(id);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<KanbanModel> ReadByIdAsync(int id)
        {

            var result = await DbSet.Include(s => s.Instruction)
                                        .ThenInclude(x => x.Steps)
                                            .ThenInclude(a => a.Machine)
                                    .Include(s => s.Instruction)
                                        .ThenInclude(x => x.Steps)
                                            .ThenInclude(a => a.StepIndicators)
                                .FirstOrDefaultAsync(x => x.Id == id);

            if (result != null && result.Instruction != null)
            {
                result.Instruction.Steps = result.Instruction.Steps.OrderBy(s => s.StepIndex).ThenBy(s => s.Id).ToList();
            }
            //var Result = await DbSet.FirstOrDefaultAsync(d => d.Id.Equals(id) && !d.IsDeleted);
            //Result.Instruction = await KanbanInstructionDbSet.FirstOrDefaultAsync(e => e.KanbanId.Equals(id) && !e.IsDeleted);
            //Result.Instruction.Steps = await KanbanStepDbSet.Where(w => w.InstructionId.Equals(Result.Instruction.Id) && !w.IsDeleted).OrderBy(x => x.StepIndex).ThenBy(x => x.Id).ToListAsync();
            //foreach (var step in Result.Instruction.Steps)
            //{
            //    step.StepIndicators = await KanbanStepIndicatorDbSet.Where(w => w.StepId.Equals(step.Id) && !w.IsDeleted).ToListAsync();
            //    step.Machine = await MachineDbSet.Where(w => w.Id.Equals(step.MachineId) && !w.IsDeleted).SingleOrDefaultAsync();
            //}
            //return Result;
            return result;
        }

        public async Task<int> UpdateAsync(int id, KanbanModel model)
        {
            int index = 1;
            foreach (var step in model.Instruction.Steps)
            {
                step.MachineId = step.Machine.Id;
                step.Machine = null;
                step.StepIndex = index;
                index++;
            }
            await KanbanLogic.UpdateModelAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<KanbanViewModel> GetReport(int page, int size, bool? proses, int orderTypeId, int processTypeId, string orderNo, DateTime? dateFrom, DateTime? dateTo, int offSet)
        {
            var queries = GetReport(proses, orderTypeId, processTypeId, orderNo, dateFrom, dateTo, offSet);
            queries = queries.OrderByDescending(x => x.LastModifiedUtc).ToList();
            Pageable<KanbanViewModel> pageable = new Pageable<KanbanViewModel>(queries, page - 1, size);
            List<KanbanViewModel> data = pageable.Data.ToList();

            return new ReadResponse<KanbanViewModel>(queries, pageable.TotalCount, new Dictionary<string, string>(), new List<string>());
        }

        public MemoryStream GenerateExcel(bool? proses, int orderTypeId, int processTypeId, string orderNo, DateTime? dateFrom, DateTime? dateTo, int offSet)
        {
            var data = GetReport(proses, orderTypeId, processTypeId, orderNo, dateFrom, dateTo, offSet);

            data = data.OrderByDescending(x => x.LastModifiedUtc).ToList();

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Nomor Order", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Tanggal", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jenis Order", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jenis Proses", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Warna", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Standar Handfeel", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Lebar Finish", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Material", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Konstruksi", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Nomor Benang", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Grade", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Nomor Kereta", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Panjang", DataType = typeof(Int64) });
            dt.Columns.Add(new DataColumn() { ColumnName = "PCS", DataType = typeof(Int64) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Step Index", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Step", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Status", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Reproses", DataType = typeof(String) });

            if (data.Count == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", 0, 0, "", "", "", "", "");
            }
            else
            {
                int index = 1;
                foreach (var item in data)
                {
                    dt.Rows.Add(index, item.ProductionOrder.OrderNo, item.CreatedUtc.AddHours(offSet).ToString("dd MMM yyyy"), item.ProductionOrder.OrderType.Name, item.ProductionOrder.ProcessType.Name,
                        item.SelectedProductionOrderDetail.ColorRequest, item.ProductionOrder.HandlingStandard, item.ProductionOrder.FinishWidth,
                        item.ProductionOrder.Material.Name, item.ProductionOrder.MaterialConstruction.Name, item.ProductionOrder.YarnMaterial.Name,
                        item.Grade, item.Cart.CartNumber, item.Cart.Qty, item.Cart.Pcs, item.Cart.Uom.Unit, string.Format("{0} / {1}", item.CurrentStepIndex.GetValueOrDefault(), item.Instruction.Steps.Count),
                        item.CurrentStepIndex.GetValueOrDefault() == 0 ? "-" : item.CurrentStepIndex.GetValueOrDefault() > item.Instruction.Steps.Count ? "REPROSES" : item.Instruction.Steps[item.CurrentStepIndex.GetValueOrDefault() - 1].Process,
                        item.IsComplete.GetValueOrDefault() ? "Complete" : item.CurrentStepIndex.GetValueOrDefault() == item.Instruction.Steps.Count ? "Pending" : "Incomplete",
                        item.IsReprocess.GetValueOrDefault() ? "Ya" : "Tidak");
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Monitoring Kanban") }, true);
        }

        public List<KanbanViewModel> GetReport(bool? proses, int orderTypeId, int processTypeId, string orderNo, DateTime? dateFrom, DateTime? dateTo, int offSet)
        {
            IQueryable<KanbanModel> query = DbContext.Kanbans.AsQueryable();

            IEnumerable<KanbanViewModel> kanbans;

            if (proses != null)
                query = query.Where(x => x.IsReprocess == proses.Value);

            if (orderTypeId != -1)
                query = query.Where(x => x.ProductionOrderOrderTypeId == orderTypeId);

            if (processTypeId != -1)
                query = query.Where(x => x.ProductionOrderProcessTypeId == processTypeId);

            if (!string.IsNullOrEmpty(orderNo))
                query = query.Where(x => x.ProductionOrderOrderNo.Contains(orderNo));


            if (dateFrom == null && dateTo == null)
            {
                query = query
                    .Where(x => DateTimeOffset.UtcNow.AddDays(-30).Date <= x.CreatedUtc.AddHours(offSet).Date
                        && x.CreatedUtc.AddHours(offSet).Date <= DateTime.UtcNow.Date);
            }
            else if (dateFrom == null && dateTo != null)
            {
                query = query
                    .Where(x => dateTo.Value.AddDays(-30).Date <= x.CreatedUtc.AddHours(offSet).Date
                        && x.CreatedUtc.AddHours(offSet).Date <= dateTo.Value.Date);
            }
            else if (dateTo == null && dateFrom != null)
            {
                query = query
                    .Where(x => dateFrom.Value.Date <= x.CreatedUtc.AddHours(offSet).Date
                        && x.CreatedUtc.AddHours(offSet).Date <= dateFrom.Value.AddDays(30).Date);
            }
            else
            {
                query = query
                    .Where(x => dateFrom.Value.Date <= x.CreatedUtc.AddHours(offSet).Date
                        && x.CreatedUtc.AddHours(offSet).Date <= dateTo.Value.Date);
            }

            kanbans = query.Select(x => new KanbanViewModel()
            {
                CreatedUtc = x.CreatedUtc.AddHours(offSet),
                ProductionOrder = new ProductionOrderIntegrationViewModel()
                {
                    OrderNo = x.ProductionOrderOrderNo,
                    OrderType = new OrderTypeIntegrationViewModel()
                    {
                        Name = x.ProductionOrderOrderTypeName
                    },
                    ProcessType = new ProcessTypeIntegrationViewModel()
                    {
                        Name = x.ProductionOrderProcessTypeName
                    },
                    HandlingStandard = x.ProductionOrderHandlingStandard,
                    FinishWidth = x.FinishWidth,
                    Material = new MaterialIntegrationViewModel()
                    {
                        Name = x.ProductionOrderMaterialName
                    },
                    MaterialConstruction = new MaterialConstructionIntegrationViewModel()
                    {
                        Name = x.ProductionOrderMaterialConstructionName
                    },
                    YarnMaterial = new YarnMaterialIntegrationViewModel()
                    {
                        Name = x.ProductionOrderYarnMaterialName
                    }

                },
                SelectedProductionOrderDetail = new ProductionOrderDetailIntegrationViewModel()
                {
                    ColorRequest = x.SelectedProductionOrderDetailColorRequest
                },
                Grade = x.Grade,
                Cart = new CartViewModel()
                {
                    CartNumber = x.CartCartNumber,
                    Qty = x.CartQty,
                    Pcs = x.CartPcs,
                    Uom = new UOMIntegrationViewModel()
                    {
                        Unit = x.CartUomUnit
                    }
                },
                IsReprocess = x.IsReprocess,
                IsComplete = x.IsComplete,
                IsInactive = x.IsInactive,
                CurrentStepIndex = x.CurrentStepIndex,
                Instruction = new KanbanInstructionViewModel()
                {
                    Steps = x.Instruction.Steps.Select(y => new KanbanStepViewModel()
                    {
                        Process = y.Process
                    }).ToList()
                },
                LastModifiedUtc = x.LastModifiedUtc
            });

            return kanbans.ToList();
        }

        public Task<int> CompleteKanban(int id)
        {
            var kanban = DbSet.FirstOrDefault(f => f.Id.Equals(id));
            kanban.IsComplete = true;
            DbSet.Update(kanban);
            return DbContext.SaveChangesAsync();
        }

        public async Task<KanbanModel> ReadOldKanbanByIdAsync(int id)
        {
            var result = await DbSet.Include(s => s.Instruction)
                                        .ThenInclude(x => x.Steps)
                                            .ThenInclude(a => a.Machine)
                                    .Include(s => s.Instruction)
                                        .ThenInclude(x => x.Steps)
                                            .ThenInclude(a => a.StepIndicators)
                                .FirstOrDefaultAsync(x => x.Id == id);

            if (result != null && result.Instruction != null)
            {
                result.Instruction.Steps = result.Instruction.Steps.OrderBy(s => s.StepIndex).ThenBy(s => s.Id).ToList();
            }

            return result;
        }

        //private List<int> DODataByDay(DateTime searchDate)
        //{
        //    var doData = DbContext.DailyOperation

        //        .Where(s => (s.DateInput.HasValue && s.DateInput.Value.Date <= searchDate.Date) || (s.DateOutput.HasValue && s.DateOutput.Value.Date >= searchDate.Date))
        //        .GroupBy(s => new { s.KanbanId, s.MachineId, s.StepId })
        //        .Where(d => d.Count() == 2 && d.Count(e => e.Type == "input") == 1 && d.Count(e => e.Type == "output") == 1).Select(s => s.Key.KanbanId).Distinct().ToList();

        //    return doData;
        //}

        //private List<int> DODataByMonth(int month, int )
        //{
        //    var doData = DbContext.DailyOperation

        //        .Where(s => (s.DateInput.HasValue && s.DateInput.Value.Month == searchDate.Month && s.DateInput.Value.Year == searchDate.Year)
        //                        || (s.DateOutput.HasValue && s.DateOutput.Value.Month == searchDate.Month && s.DateOutput.Value.Year == searchDate.Year))
        //        .GroupBy(s => new { s.KanbanId, s.MachineId, s.StepId })
        //        .Where(d => d.Count() == 2 && d.Count(e => e.Type == "input") == 1 && d.Count(e => e.Type == "output") == 1).Select(s => s.Key.KanbanId).Distinct().ToList();

        //    return doData;
        //}



        //private KeyValuePair<DataTable, string> GenerateDataTable(List<int> doData, DateTime searchDate, string title, bool isMonthly)
        //{
        //    DataTable dt = new DataTable();
        //    //dt.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(int) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Buyer", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Nomor SPP", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Qty", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Pre Treatment Qty", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Pre Treatment Day", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Dyeing Qty", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Dyeing Day", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Printing Qty", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Printing Day", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Finishing Qty", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Finishing Day", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "QC Qty", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "QC Day", DataType = typeof(string) });

        //    var selectedKanbans = DbSet.Include(s => s.Instruction).ThenInclude(s => s.Steps).Where(s => doData.Contains(s.Id)).ToList();
        //    var selectedDos = DbContext.DailyOperation.Where(s => doData.Contains(s.KanbanId)).GroupBy(s => s.KanbanId).ToList();
        //    if (selectedDos.Count() == 0)
        //    {
        //        dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "");
        //    }
        //    else
        //    {
        //        foreach (var item in selectedDos)
        //        {
        //            List<DailyOperationModel> outDailyOperations = new List<DailyOperationModel>();

        //            if (isMonthly)
        //            {
        //                outDailyOperations = item.Where(s => s.Type.ToLower() == "output"
        //                                                && s.DateOutput.GetValueOrDefault().Month <= searchDate.Month &&
        //                                                s.DateOutput.GetValueOrDefault().Year <= searchDate.Year).ToList();
        //            }
        //            else
        //            {
        //                outDailyOperations = item.Where(s => s.Type.ToLower() == "output" && s.DateOutput.GetValueOrDefault().Date <= searchDate.Date).ToList();
        //            }

        //            var inDailyOperations = item.Where(s => s.Type.ToLower() == "input" && outDailyOperations.Any(y => y.StepId == s.StepId));
        //            var kanban = selectedKanbans.FirstOrDefault(s => s.Id == item.Key);
        //            var preTreatmentArea = kanban.Instruction.Steps.Where(s => s.ProcessArea != null && s.ProcessArea.ToLower() == "area pre treatment").OrderBy(s => s.StepIndex);
        //            var dyeingArea = kanban.Instruction.Steps.Where(s => s.ProcessArea != null && s.ProcessArea.ToLower() == "area dyeing").OrderBy(s => s.StepIndex);
        //            var printingArea = kanban.Instruction.Steps.Where(s => s.ProcessArea != null && s.ProcessArea.ToLower() == "area printing").OrderBy(s => s.StepIndex);
        //            var finishingArea = kanban.Instruction.Steps.Where(s => s.ProcessArea != null && s.ProcessArea.ToLower() == "area finishing").OrderBy(s => s.StepIndex);
        //            var qcArea = kanban.Instruction.Steps.Where(s => s.ProcessArea != null && s.ProcessArea.ToLower() == "area qc").OrderBy(s => s.StepIndex);

        //            double? preTreatmentQty = 0;
        //            double? dyeingQty = 0;
        //            double? printingQty = 0;
        //            double? finishingQty = 0;
        //            double? qcQty = 0;
        //            int preTreatmentDay = 0;
        //            int dyeingDay = 0;
        //            int printingDay = 0;
        //            int finishingDay = 0;
        //            int qcDay = 0;


        //            if (preTreatmentArea.Count() > 0)
        //            {
        //                var preTreatmentStart = inDailyOperations.OrderBy(s => s.DateInput).FirstOrDefault(s => preTreatmentArea.Any(y => y.Id == s.StepId));
        //                var preTreatmentDO = outDailyOperations.OrderBy(s => s.DateOutput).LastOrDefault(s => preTreatmentArea.Any(y => y.Id == s.StepId));
        //                preTreatmentQty = preTreatmentDO?.GoodOutput.GetValueOrDefault() + preTreatmentDO?.BadOutput.GetValueOrDefault();
        //                if (preTreatmentDO != null && preTreatmentStart != null)
        //                    preTreatmentDay = (int)Math.Ceiling((preTreatmentDO.DateOutput.GetValueOrDefault() - preTreatmentStart.DateInput.GetValueOrDefault()).TotalDays);
        //            }
        //            if (dyeingArea.Count() > 0)
        //            {
        //                var dyeingStart = inDailyOperations.OrderBy(s => s.DateInput).FirstOrDefault(s => dyeingArea.Any(y => y.Id == s.StepId));
        //                var dyeingDO = outDailyOperations.OrderBy(s => s.DateOutput).LastOrDefault(s => dyeingArea.Any(y => y.Id == s.StepId));
        //                dyeingQty = dyeingDO?.GoodOutput.GetValueOrDefault() + dyeingDO?.BadOutput.GetValueOrDefault();
        //                if (dyeingDO != null && dyeingStart != null)
        //                    dyeingDay = (int)Math.Ceiling((dyeingDO.DateOutput.GetValueOrDefault() - dyeingStart.DateInput.GetValueOrDefault()).TotalDays);
        //            }

        //            if (printingArea.Count() > 0)
        //            {
        //                var printingStart = inDailyOperations.OrderBy(s => s.DateInput).FirstOrDefault(s => printingArea.Any(y => y.Id == s.StepId));
        //                var printingDO = outDailyOperations.OrderBy(s => s.DateOutput).LastOrDefault(s => printingArea.Any(y => y.Id == s.StepId));
        //                printingQty = printingDO?.GoodOutput.GetValueOrDefault() + printingDO?.BadOutput.GetValueOrDefault();
        //                if (printingDO != null && printingStart != null)
        //                    printingDay = (int)Math.Ceiling((printingDO.DateOutput.GetValueOrDefault() - printingStart.DateInput.GetValueOrDefault()).TotalDays);
        //            }

        //            if (finishingArea.Count() > 0)
        //            {
        //                var finishingStart = inDailyOperations.OrderBy(s => s.DateInput).FirstOrDefault(s => finishingArea.Any(y => y.Id == s.StepId));
        //                var finishingDO = outDailyOperations.OrderBy(s => s.DateOutput).LastOrDefault(s => finishingArea.Any(y => y.Id == s.StepId));
        //                finishingQty = finishingDO?.GoodOutput.GetValueOrDefault() + finishingDO?.BadOutput.GetValueOrDefault();
        //                if (finishingDO != null && finishingStart != null)
        //                    finishingDay = (int)Math.Ceiling((finishingDO.DateOutput.GetValueOrDefault() - finishingStart.DateInput.GetValueOrDefault()).TotalDays);
        //            }

        //            if (qcArea.Count() > 0)
        //            {
        //                var qcStart = inDailyOperations.OrderBy(s => s.DateInput).FirstOrDefault(s => qcArea.Any(y => y.Id == s.StepId));
        //                var qcDO = outDailyOperations.OrderBy(s => s.DateOutput).LastOrDefault(s => qcArea.Any(y => y.Id == s.StepId));
        //                qcQty = qcDO?.GoodOutput.GetValueOrDefault() + qcDO?.BadOutput.GetValueOrDefault();
        //                if (qcDO != null && qcStart != null)
        //                    qcDay = (int)Math.Ceiling((qcDO.DateOutput.GetValueOrDefault() - qcStart.DateInput.GetValueOrDefault()).TotalDays);
        //            }

        //            dt.Rows.Add(kanban.ProductionOrderBuyerName, kanban.ProductionOrderOrderNo, kanban.SelectedProductionOrderDetailQuantity,
        //                preTreatmentQty.GetValueOrDefault() == 0 ? "-" : preTreatmentQty.GetValueOrDefault().ToString(),
        //                preTreatmentDay == 0 ? "-" : preTreatmentDay.ToString(),
        //                dyeingQty.GetValueOrDefault() == 0 ? "-" : dyeingQty.GetValueOrDefault().ToString(),
        //                dyeingDay == 0 ? "-" : dyeingDay.ToString(),
        //                printingQty.GetValueOrDefault() == 0 ? "-" : printingQty.GetValueOrDefault().ToString(),
        //                printingDay == 0 ? "-" : printingDay.ToString(),
        //                finishingQty.GetValueOrDefault() == 0 ? "-" : finishingQty.GetValueOrDefault().ToString(),
        //                finishingDay == 0 ? "-" : finishingDay.ToString(),
        //                qcQty.GetValueOrDefault() == 0 ? "-" : qcQty.GetValueOrDefault().ToString(),
        //                qcDay == 0 ? "-" : qcDay.ToString());

        //        }
        //    }

        //    return new KeyValuePair<DataTable, string>(dt, title);
        //}

        //private (string, double?, double?, double?, int) GetDataPerArea(string konstruksi, DateTimeOffset date, IEnumerable<IGrouping<int, DailyOperationModel>> dailyOperations, IOrderedEnumerable<KanbanStepModel> steps)
        //{
        //    string areaKonstruksi = "";
        //    double? inputQty = 0;
        //    double? goodOutputQty = 0;
        //    double? badOutputQty = 0;
        //    int day = 0;
        //    var areaDos = dailyOperations.Where(s => steps.Any(y => y.Id == s.Key)).SelectMany(s => s);
        //    var areaInput = areaDos.Where(s => s.Type.ToLower() == "input").OrderBy(s => s.DateInput).FirstOrDefault();
        //    var areaOutput = areaDos.Where(s => s.Type.ToLower() == "output").OrderBy(s => s.DateOutput).LastOrDefault();

        //    inputQty = areaInput?.Input;
        //    goodOutputQty = areaOutput?.GoodOutput;
        //    badOutputQty = areaOutput?.BadOutput;
        //    if (areaOutput != null && areaInput != null)
        //    {
        //        day = (int)Math.Ceiling((areaOutput.DateOutput.GetValueOrDefault() - areaInput.DateInput.GetValueOrDefault()).TotalDays);
        //    }
        //    else if (areaInput != null)
        //    {
        //        day = (int)Math.Ceiling((date - areaInput.DateInput.GetValueOrDefault()).TotalDays);
        //    }

        //    if (day > 0)
        //        areaKonstruksi = konstruksi;

        //    return (areaKonstruksi, inputQty, goodOutputQty, badOutputQty, day);
        //}


        //private KeyValuePair<List<KanbanSnapshotViewModel>, string> GenereateList(IEnumerable<DailyOperationModel> dailyOperations, IEnumerable<KanbanModel> kanbans, DateTimeOffset date)
        //{
        //    List<KanbanSnapshotViewModel> result = new List<KanbanSnapshotViewModel>();
        //    var selectedDos = dailyOperations.GroupBy(s => s.KanbanId);
        //    if (selectedDos.Count() == 0)
        //    {
        //        result.Add(new KanbanSnapshotViewModel());
        //    }
        //    else
        //    {
        //        foreach (var item in selectedDos)
        //        {
        //            var groupedDos = item.GroupBy(s => s.StepId);
        //            var kanban = kanbans.FirstOrDefault(s => s.Id == item.Key);
        //            var preTreatmentArea = kanban.Instruction.Steps.Where(s => s.ProcessArea != null && s.ProcessArea.ToLower() == "area pre treatment").OrderBy(s => s.StepIndex);
        //            var dyeingArea = kanban.Instruction.Steps.Where(s => s.ProcessArea != null && s.ProcessArea.ToLower() == "area dyeing").OrderBy(s => s.StepIndex);
        //            var printingArea = kanban.Instruction.Steps.Where(s => s.ProcessArea != null && s.ProcessArea.ToLower() == "area printing").OrderBy(s => s.StepIndex);
        //            var finishingArea = kanban.Instruction.Steps.Where(s => s.ProcessArea != null && s.ProcessArea.ToLower() == "area finishing").OrderBy(s => s.StepIndex);
        //            var qcArea = kanban.Instruction.Steps.Where(s => s.ProcessArea != null && s.ProcessArea.ToLower() == "area qc").OrderBy(s => s.StepIndex);
        //            string konstruksi = string.Format("{0} / {1} / {2}", kanban.ProductionOrderMaterialName, kanban.ProductionOrderMaterialConstructionName, kanban.FinishWidth);
        //            double? preTreatmentInputQty = 0;
        //            double? preTreatmentGoodOutputQty = 0;
        //            double? preTreatmentBadOutputQty = 0;
        //            double? dyeingInputQty = 0;
        //            double? dyeingGoodOutputQty = 0;
        //            double? dyeingBadOutputQty = 0;
        //            double? printingInputQty = 0;
        //            double? printingGoodOutputQty = 0;
        //            double? printingBadOutputQty = 0;
        //            double? finishingInputQty = 0;
        //            double? finishingGoodOutputQty = 0;
        //            double? finishingBadOutputQty = 0;
        //            double? qcInputQty = 0;
        //            double? qcGoodOutputQty = 0;
        //            double? qcBadOutputQty = 0;
        //            int preTreatmentDay = 0;
        //            int dyeingDay = 0;
        //            int printingDay = 0;
        //            int finishingDay = 0;
        //            int qcDay = 0;
        //            string preTreatmentKonstruksi = "";
        //            string dyeingKonstruksi = "";
        //            string printingKonstruksi = "";
        //            string finishingKonstruksi = "";
        //            string qcKonstruksi = "";

        //            if (preTreatmentArea.Count() > 0)
        //            {
        //                var preTreatmentData = GetDataPerArea(konstruksi, date, groupedDos, preTreatmentArea);
        //                preTreatmentKonstruksi = preTreatmentData.Item1;
        //                preTreatmentInputQty = preTreatmentData.Item2;
        //                preTreatmentGoodOutputQty = preTreatmentData.Item3;
        //                preTreatmentBadOutputQty = preTreatmentData.Item4;
        //                preTreatmentDay = preTreatmentData.Item5;

        //            }
        //            if (dyeingArea.Count() > 0)
        //            {
        //                var dyeingData = GetDataPerArea(konstruksi, date, groupedDos, dyeingArea);
        //                dyeingKonstruksi = dyeingData.Item1;
        //                dyeingInputQty = dyeingData.Item2;
        //                dyeingGoodOutputQty = dyeingData.Item3;
        //                dyeingBadOutputQty = dyeingData.Item4;
        //                dyeingDay = dyeingData.Item5;
        //            }

        //            if (printingArea.Count() > 0)
        //            {
        //                var printingData = GetDataPerArea(konstruksi, date, groupedDos, printingArea);
        //                printingKonstruksi = printingData.Item1;
        //                printingInputQty = printingData.Item2;
        //                printingGoodOutputQty = printingData.Item3;
        //                printingBadOutputQty = printingData.Item4;
        //                printingDay = printingData.Item5;
        //            }

        //            if (finishingArea.Count() > 0)
        //            {
        //                var finishingData = GetDataPerArea(konstruksi, date, groupedDos, finishingArea);
        //                finishingKonstruksi = finishingData.Item1;
        //                finishingInputQty = finishingData.Item2;
        //                finishingGoodOutputQty = finishingData.Item3;
        //                finishingBadOutputQty = finishingData.Item4;
        //                finishingDay = finishingData.Item5;
        //            }

        //            if (qcArea.Count() > 0)
        //            {
        //                var qcData = GetDataPerArea(konstruksi, date, groupedDos, qcArea);
        //                qcKonstruksi = qcData.Item1;
        //                qcInputQty = qcData.Item2;
        //                qcGoodOutputQty = qcData.Item3;
        //                qcBadOutputQty = qcData.Item4;
        //                qcDay = qcData.Item5;
        //            }

        //            KanbanSnapshotViewModel vm = new KanbanSnapshotViewModel()
        //            {
        //                Buyer = kanban.ProductionOrderBuyerName,
        //                SPPNo = kanban.ProductionOrderOrderNo,
        //                Konstruksi = konstruksi,
        //                Qty = kanban.SelectedProductionOrderDetailQuantity.ToString(),
        //                PreTreatmentKonstruksi = preTreatmentKonstruksi,
        //                PreTreatmentInputQty = preTreatmentInputQty.GetValueOrDefault() == 0 ? "-" : preTreatmentInputQty.GetValueOrDefault().ToString(),
        //                PreTreatmentGoodOutputQty = preTreatmentGoodOutputQty.GetValueOrDefault() == 0 ? "-" : preTreatmentGoodOutputQty.GetValueOrDefault().ToString(),
        //                PreTreatmentBadOutputQty = preTreatmentBadOutputQty.GetValueOrDefault() == 0 ? "-" : preTreatmentBadOutputQty.GetValueOrDefault().ToString(),
        //                PreTreatmentDay = preTreatmentDay == 0 ? "-" : preTreatmentDay.ToString(),
        //                DyeingKonstruksi = dyeingKonstruksi,
        //                DyeingInputQty = dyeingInputQty.GetValueOrDefault() == 0 ? "-" : dyeingInputQty.GetValueOrDefault().ToString(),
        //                DyeingGoodOutputQty = dyeingGoodOutputQty.GetValueOrDefault() == 0 ? "-" : dyeingGoodOutputQty.GetValueOrDefault().ToString(),
        //                DyeingBadOutputQty = dyeingBadOutputQty.GetValueOrDefault() == 0 ? "-" : dyeingBadOutputQty.GetValueOrDefault().ToString(),
        //                DyeingDay = dyeingDay == 0 ? "-" : dyeingDay.ToString(),
        //                PrintingKonstruksi = printingKonstruksi,
        //                PrintingInputQty = printingInputQty.GetValueOrDefault() == 0 ? "-" : printingInputQty.GetValueOrDefault().ToString(),
        //                PrintingGoodOutputQty = printingGoodOutputQty.GetValueOrDefault() == 0 ? "-" : printingGoodOutputQty.GetValueOrDefault().ToString(),
        //                PrintingBadOutputQty = printingBadOutputQty.GetValueOrDefault() == 0 ? "-" : printingBadOutputQty.GetValueOrDefault().ToString(),
        //                PrintingDay = printingDay == 0 ? "-" : printingDay.ToString(),
        //                FinishingKonstruksi = finishingKonstruksi,
        //                FinishingInputQty = finishingInputQty.GetValueOrDefault() == 0 ? "-" : finishingInputQty.GetValueOrDefault().ToString(),
        //                FinishingGoodOutputQty = finishingGoodOutputQty.GetValueOrDefault() == 0 ? "-" : finishingGoodOutputQty.GetValueOrDefault().ToString(),
        //                FinishingBadOutputQty = finishingBadOutputQty.GetValueOrDefault() == 0 ? "-" : finishingBadOutputQty.GetValueOrDefault().ToString(),
        //                FinishingDay = finishingDay == 0 ? "-" : finishingDay.ToString(),
        //                QCKonstruksi = qcKonstruksi,
        //                QCInputQty = qcInputQty.GetValueOrDefault() == 0 ? "-" : qcInputQty.GetValueOrDefault().ToString(),
        //                QCGoodOutputQty = qcGoodOutputQty.GetValueOrDefault() == 0 ? "-" : qcGoodOutputQty.GetValueOrDefault().ToString(),
        //                QCBadOutputQty = qcBadOutputQty.GetValueOrDefault() == 0 ? "-" : qcBadOutputQty.GetValueOrDefault().ToString(),
        //                QCDay = qcDay == 0 ? "-" : qcDay.ToString(),
        //            };

        //            result.Add(vm);

        //        }
        //    }
        //    return new KeyValuePair<List<KanbanSnapshotViewModel>, string>(result, date.Day.ToString());
        //}

        //private KeyValuePair<DataTable, string> GenerateDataTable(IEnumerable<DailyOperationModel> dailyOperations, IEnumerable<KanbanModel> kanbans, DateTimeOffset date)
        //{
        //    DataTable dt = new DataTable();
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Buyer", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Nomor SPP", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Konstruksi", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Qty", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Pre Treatment Konstruksi", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Pre Treatment Input Qty", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Pre Treatment Good Output Qty", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Pre Treatment Bad Output Qty", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Pre Treatment Day", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Dyeing Konstruksi", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Dyeing Input Qty", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Dyeing Good Output Qty", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Dyeing Bad Output Qty", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Dyeing Day", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Printing Konstruksi", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Printing Input Qty", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Printing Good Output Qty", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Printing Bad Output Qty", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Printing Day", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Finishing Konstruksi", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Finishing Input Qty", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Finishing Good Output Qty", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Finishing Bad Output Qty", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "Finishing Day", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "QC Konstruksi", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "QC Input Qty", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "QC Good Output Qty", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "QC Bad Output Qty", DataType = typeof(string) });
        //    dt.Columns.Add(new DataColumn() { ColumnName = "QC Day", DataType = typeof(string) });

        //    var selectedDos = dailyOperations.GroupBy(s => s.KanbanId);
        //    if (selectedDos.Count() == 0)
        //    {
        //        dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
        //    }
        //    else
        //    {
        //        foreach (var item in selectedDos)
        //        {
        //            var groupedDos = item.GroupBy(s => s.StepId);
        //            var kanban = kanbans.FirstOrDefault(s => s.Id == item.Key);
        //            var preTreatmentArea = kanban.Instruction.Steps.Where(s => s.ProcessArea != null && s.ProcessArea.ToLower() == "area pre treatment").OrderBy(s => s.StepIndex);
        //            var dyeingArea = kanban.Instruction.Steps.Where(s => s.ProcessArea != null && s.ProcessArea.ToLower() == "area dyeing").OrderBy(s => s.StepIndex);
        //            var printingArea = kanban.Instruction.Steps.Where(s => s.ProcessArea != null && s.ProcessArea.ToLower() == "area printing").OrderBy(s => s.StepIndex);
        //            var finishingArea = kanban.Instruction.Steps.Where(s => s.ProcessArea != null && s.ProcessArea.ToLower() == "area finishing").OrderBy(s => s.StepIndex);
        //            var qcArea = kanban.Instruction.Steps.Where(s => s.ProcessArea != null && s.ProcessArea.ToLower() == "area qc").OrderBy(s => s.StepIndex);
        //            string konstruksi = string.Format("{0} / {1} / {2}", kanban.ProductionOrderMaterialName, kanban.ProductionOrderMaterialConstructionName, kanban.FinishWidth);
        //            double? preTreatmentInputQty = 0;
        //            double? preTreatmentGoodOutputQty = 0;
        //            double? preTreatmentBadOutputQty = 0;
        //            double? dyeingInputQty = 0;
        //            double? dyeingGoodOutputQty = 0;
        //            double? dyeingBadOutputQty = 0;
        //            double? printingInputQty = 0;
        //            double? printingGoodOutputQty = 0;
        //            double? printingBadOutputQty = 0;
        //            double? finishingInputQty = 0;
        //            double? finishingGoodOutputQty = 0;
        //            double? finishingBadOutputQty = 0;
        //            double? qcInputQty = 0;
        //            double? qcGoodOutputQty = 0;
        //            double? qcBadOutputQty = 0;
        //            int preTreatmentDay = 0;
        //            int dyeingDay = 0;
        //            int printingDay = 0;
        //            int finishingDay = 0;
        //            int qcDay = 0;
        //            string preTreatmentKonstruksi = "";
        //            string dyeingKonstruksi = "";
        //            string printingKonstruksi = "";
        //            string finishingKonstruksi = "";
        //            string qcKonstruksi = "";

        //            if (preTreatmentArea.Count() > 0)
        //            {
        //                var preTreatmentData = GetDataPerArea(konstruksi, date, groupedDos, preTreatmentArea);
        //                preTreatmentKonstruksi = preTreatmentData.Item1;
        //                preTreatmentInputQty = preTreatmentData.Item2;
        //                preTreatmentGoodOutputQty = preTreatmentData.Item3;
        //                preTreatmentBadOutputQty = preTreatmentData.Item4;
        //                preTreatmentDay = preTreatmentData.Item5;

        //            }
        //            if (dyeingArea.Count() > 0)
        //            {
        //                var dyeingData = GetDataPerArea(konstruksi, date, groupedDos, dyeingArea);
        //                dyeingKonstruksi = dyeingData.Item1;
        //                dyeingInputQty = dyeingData.Item2;
        //                dyeingGoodOutputQty = dyeingData.Item3;
        //                dyeingBadOutputQty = dyeingData.Item4;
        //                dyeingDay = dyeingData.Item5;
        //            }

        //            if (printingArea.Count() > 0)
        //            {
        //                var printingData = GetDataPerArea(konstruksi, date, groupedDos, printingArea);
        //                printingKonstruksi = printingData.Item1;
        //                printingInputQty = printingData.Item2;
        //                printingGoodOutputQty = printingData.Item3;
        //                printingBadOutputQty = printingData.Item4;
        //                printingDay = printingData.Item5;
        //            }

        //            if (finishingArea.Count() > 0)
        //            {
        //                var finishingData = GetDataPerArea(konstruksi, date, groupedDos, finishingArea);
        //                finishingKonstruksi = finishingData.Item1;
        //                finishingInputQty = finishingData.Item2;
        //                finishingGoodOutputQty = finishingData.Item3;
        //                finishingBadOutputQty = finishingData.Item4;
        //                finishingDay = finishingData.Item5;
        //            }

        //            if (qcArea.Count() > 0)
        //            {
        //                var qcData = GetDataPerArea(konstruksi, date, groupedDos, qcArea);
        //                qcKonstruksi = qcData.Item1;
        //                qcInputQty = qcData.Item2;
        //                qcGoodOutputQty = qcData.Item3;
        //                qcBadOutputQty = qcData.Item4;
        //                qcDay = qcData.Item5;
        //            }

        //            dt.Rows.Add(kanban.ProductionOrderBuyerName, kanban.ProductionOrderOrderNo, konstruksi, kanban.SelectedProductionOrderDetailQuantity,
        //                preTreatmentKonstruksi,
        //                preTreatmentInputQty.GetValueOrDefault() == 0 ? "-" : preTreatmentInputQty.GetValueOrDefault().ToString(),
        //                preTreatmentGoodOutputQty.GetValueOrDefault() == 0 ? "-" : preTreatmentGoodOutputQty.GetValueOrDefault().ToString(),
        //                preTreatmentBadOutputQty.GetValueOrDefault() == 0 ? "-" : preTreatmentBadOutputQty.GetValueOrDefault().ToString(),
        //                preTreatmentDay == 0 ? "-" : preTreatmentDay.ToString(),
        //                dyeingKonstruksi,
        //                dyeingInputQty.GetValueOrDefault() == 0 ? "-" : dyeingInputQty.GetValueOrDefault().ToString(),
        //                dyeingGoodOutputQty.GetValueOrDefault() == 0 ? "-" : dyeingGoodOutputQty.GetValueOrDefault().ToString(),
        //                dyeingBadOutputQty.GetValueOrDefault() == 0 ? "-" : dyeingBadOutputQty.GetValueOrDefault().ToString(),
        //                dyeingDay == 0 ? "-" : dyeingDay.ToString(),
        //                printingKonstruksi,
        //                printingInputQty.GetValueOrDefault() == 0 ? "-" : printingInputQty.GetValueOrDefault().ToString(),
        //                printingGoodOutputQty.GetValueOrDefault() == 0 ? "-" : printingGoodOutputQty.GetValueOrDefault().ToString(),
        //                printingBadOutputQty.GetValueOrDefault() == 0 ? "-" : printingBadOutputQty.GetValueOrDefault().ToString(),
        //                printingDay == 0 ? "-" : printingDay.ToString(),
        //                finishingKonstruksi,
        //                finishingInputQty.GetValueOrDefault() == 0 ? "-" : finishingInputQty.GetValueOrDefault().ToString(),
        //                finishingGoodOutputQty.GetValueOrDefault() == 0 ? "-" : finishingGoodOutputQty.GetValueOrDefault().ToString(),
        //                finishingBadOutputQty.GetValueOrDefault() == 0 ? "-" : finishingBadOutputQty.GetValueOrDefault().ToString(),
        //                finishingDay == 0 ? "-" : finishingDay.ToString(),
        //                qcKonstruksi,
        //                qcInputQty.GetValueOrDefault() == 0 ? "-" : qcInputQty.GetValueOrDefault().ToString(),
        //                qcGoodOutputQty.GetValueOrDefault() == 0 ? "-" : qcGoodOutputQty.GetValueOrDefault().ToString(),
        //                qcBadOutputQty.GetValueOrDefault() == 0 ? "-" : qcBadOutputQty.GetValueOrDefault().ToString(),
        //                qcDay == 0 ? "-" : qcDay.ToString());

        //        }
        //    }

        //    return new KeyValuePair<DataTable, string>(dt, date.Day.ToString());
        //}


        public MemoryStream GenerateKanbanSnapshotExcel(int month, int year)
        {
            //var monthData = GetDOData(month, year);
            //var dayData = DODataByDay(searchDate);
            //var monthData = DODataByMonth(searchDate);

            //var dayDataTable = GenerateDataTable(dayData, searchDate, searchDate.Day.ToString(), false);
            //var monthDataTable = GenerateDataTable(monthData, searchDate, searchDate.ToString("MMMM"), true);
            //var inputDailyOperations = DbContext.DailyOperation
            //    .Include(s => s.Kanban).Where(s => s.Type.ToLower() == "input" && s.CreatedUtc.Month == month && s.CreatedUtc.Year == year).ToList();
            //var stepIds = inputDailyOperations.Select(s => s.StepId).Distinct();
            //var kanbanIds = inputDailyOperations.Select(s => s.KanbanId).Distinct();
            //var machineIds = inputDailyOperations.Select(s => s.MachineId).Distinct();
            //var outputDailyOperations = DbContext.DailyOperation
            //    .Include(s => s.Kanban)
            //    .Where(s => s.Type.ToLower() == "output" && stepIds.Contains(s.StepId) && kanbanIds.Contains(s.KanbanId) && machineIds.Contains(s.MachineId)).ToList();
            //var dailyOperations = DbContext.DailyOperation
            //    .Where(s => s.CreatedUtc.Month == month && s.CreatedUtc.Year == year).ToList();
            ////var dailyOperations = DbContext.DailyOperation

            ////    .Where(s => (s.DateInput.HasValue && s.DateInput.Value.Month == month && s.DateInput.Value.Year == year)
            ////                    || (s.DateOutput.HasValue && s.DateOutput.Value.Month == month && s.DateOutput.Value.Year == year))
            ////    .GroupBy(s => new { s.KanbanId, s.MachineId, s.StepId })
            ////    .Where(d => d.Count() == 2 && d.Count(e => e.Type == "input") == 1 && d.Count(e => e.Type == "output") == 1).SelectMany(s => s).ToList();
            //var kanbandIds = dailyOperations.Select(s => s.KanbanId).Distinct();
            //var kanbans = DbSet.Include(s => s.Instruction).ThenInclude(s => s.Steps).Where(s => kanbandIds.Contains(s.Id)).ToList();
            //int lastDay = DateTime.DaysInMonth(year, month);

            ////List<KeyValuePair<List<KanbanSnapshotViewModel>, String>> lists = new List<KeyValuePair<List<KanbanSnapshotViewModel>, string>>();
            //for (int i = 1; i <= lastDay; i++)
            //{
            //    DateTimeOffset sheetDate = new DateTimeOffset(new DateTime(year, month, i));
            //    var dailyDO = dailyOperations.Where(s => s.CreatedUtc.Day == i);
            //    var dailyKanbanIds = dailyDO.Select(s => s.KanbanId).Distinct();
            //    var dailyKanbans = kanbans.Where(s => dailyKanbanIds.Contains(s.Id));

            //    var dt = GenerateDataTable(dailyDO, dailyKanbans, sheetDate);
            //    tables.Add(dt);
            //}
            var kanbanSnapshot = DbContext.KanbanSnapshots.Where(s => s.DOCreatedUtcMonth == month && s.DOCreatedUtcYear == year).ToList();
            int lastDay = DateTime.DaysInMonth(year, month);
            return CreateExcel(kanbanSnapshot, lastDay, true);
        }

        private MemoryStream CreateExcel(List<KanbanSnapshotModel> dtSourceList, int endDay, bool styling = false)
        {
            ExcelPackage package = new ExcelPackage();
            for (int i = 1; i <= endDay; i++)
            {
                var sheet = package.Workbook.Worksheets.Add(i.ToString());
                var sheetData = dtSourceList
                    .Where(s => s.PreTreatmentInputDate == i || s.PreTreatmentOutputDate == i ||
                                s.PrintingInputDate == i || s.PrintingOutputDate == i ||
                                s.DyeingInputDate == i || s.DyeingOutputDate == i ||
                                s.FinishingInputDate == i || s.FinishingOutputDate == i ||
                                s.QCInputDate == i || s.QCOutputDate == i)
                    .ToList();
                var preTreatmentCells = SnapshotDataCells.Skip(6 * 0).Take(6);
                var dyeingCells = SnapshotDataCells.Skip(6 * 1).Take(6);
                var printingCells = SnapshotDataCells.Skip(6 * 2).Take(6);
                var finishingCells = SnapshotDataCells.Skip(6 * 3).Take(6);
                var qcCells = SnapshotDataCells.Skip(6 * 4).Take(6);
                sheet.Cells[string.Format("{0}1:{1}1", KanbanDataCells.First(), SnapshotDataCells.Last())].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells[string.Format("{0}1:{1}1", KanbanDataCells.First(), SnapshotDataCells.Last())].Style.Font.Bold = true;

                sheet.Cells[string.Format("{0}1", KanbanDataCells.First())].Value = "Kanban";
                sheet.Cells[string.Format("{0}1:{1}1", KanbanDataCells.First(), KanbanDataCells.Last())].Merge = true;
                sheet.Cells[string.Format("{0}1:{1}1", KanbanDataCells.First(), KanbanDataCells.Last())].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                sheet.Cells[string.Format("{0}1", preTreatmentCells.First())].Value = "Pre Treatment";
                sheet.Cells[string.Format("{0}1:{1}1", preTreatmentCells.First(), preTreatmentCells.Last())].Merge = true;
                sheet.Cells[string.Format("{0}1:{1}1", preTreatmentCells.First(), preTreatmentCells.Last())].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                sheet.Cells[string.Format("{0}1", dyeingCells.First())].Value = "Dyeing";
                sheet.Cells[string.Format("{0}1:{1}1", dyeingCells.First(), dyeingCells.Last())].Merge = true;
                sheet.Cells[string.Format("{0}1:{1}1", dyeingCells.First(), dyeingCells.Last())].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                sheet.Cells[string.Format("{0}1", printingCells.First())].Value = "Printing";
                sheet.Cells[string.Format("{0}1:{1}1", printingCells.First(), printingCells.Last())].Merge = true;
                sheet.Cells[string.Format("{0}1:{1}1", printingCells.First(), printingCells.Last())].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                sheet.Cells[string.Format("{0}1", finishingCells.First())].Value = "Finishing";
                sheet.Cells[string.Format("{0}1:{1}1", finishingCells.First(), finishingCells.Last())].Merge = true;
                sheet.Cells[string.Format("{0}1:{1}1", finishingCells.First(), finishingCells.Last())].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                sheet.Cells[string.Format("{0}1", qcCells.First())].Value = "QC";
                sheet.Cells[string.Format("{0}1:{1}1", qcCells.First(), qcCells.Last())].Merge = true;
                sheet.Cells[string.Format("{0}1:{1}1", qcCells.First(), qcCells.Last())].Style.Border.BorderAround(ExcelBorderStyle.Medium);

                sheet.Cells[string.Format("{0}2", KanbanDataCells.First())].LoadFromCollectionFiltered(sheetData, true, OfficeOpenXml.Table.TableStyles.Light16);
                sheet.Cells[sheet.Dimension.Address].AutoFitColumns();

                for (var rowNum = 3; rowNum <= sheet.Dimension.End.Row; rowNum++)
                {
                    IEnumerable<string> preTreatmentValueAddress = preTreatmentCells.Select(s => s + rowNum.ToString());
                    IEnumerable<string> preTreatmentValueCells = preTreatmentValueAddress.Select(s => sheet.Cells[s].Value?.ToString());
                    IEnumerable<string> dyeingValueAddress = dyeingCells.Select(s => s + rowNum.ToString());
                    IEnumerable<string> dyeingValueCells = dyeingValueAddress.Select(s => sheet.Cells[s].Value?.ToString());
                    IEnumerable<string> printingValueAddress = printingCells.Select(s => s + rowNum.ToString());
                    IEnumerable<string> printingValueCells = printingValueAddress.Select(s => sheet.Cells[s].Value?.ToString());
                    IEnumerable<string> finishingValueAddress = finishingCells.Select(s => s + rowNum.ToString());
                    IEnumerable<string> finishingValueCells = finishingValueAddress.Select(s => sheet.Cells[s].Value?.ToString());
                    IEnumerable<string> qcValueAddress = qcCells.Select(s => s + rowNum.ToString());
                    IEnumerable<string> qcValueCells = qcValueAddress.Select(s => sheet.Cells[s].Value?.ToString());

                    if (string.IsNullOrEmpty(preTreatmentValueCells.FirstOrDefault()))
                    {
                        string startCell = preTreatmentValueAddress.First();
                        string endCell = preTreatmentValueAddress.Last();
                        sheet.Cells[startCell + ":" + endCell].Style.Fill.PatternType = ExcelFillStyle.None;
                    }
                    else if (preTreatmentValueCells.All(s => !string.IsNullOrEmpty(s)))
                    {
                        string startCell = preTreatmentValueAddress.First();
                        string endCell = preTreatmentValueAddress.Last();
                        sheet.Cells[startCell + ":" + endCell].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells[startCell + ":" + endCell].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);

                    }
                    else
                    {
                        string startCell = preTreatmentValueAddress.First();
                        string endCell = preTreatmentValueAddress.Last();
                        sheet.Cells[startCell + ":" + endCell].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells[startCell + ":" + endCell].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    }

                    if (string.IsNullOrEmpty(dyeingValueCells.FirstOrDefault()))
                    {
                        string startCell = dyeingValueAddress.First();
                        string endCell = dyeingValueAddress.Last();
                        sheet.Cells[startCell + ":" + endCell].Style.Fill.PatternType = ExcelFillStyle.None;
                    }
                    else if (dyeingValueCells.All(s => !string.IsNullOrEmpty(s)))
                    {
                        string startCell = dyeingValueAddress.First();
                        string endCell = dyeingValueAddress.Last();
                        sheet.Cells[startCell + ":" + endCell].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells[startCell + ":" + endCell].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);

                    }
                    else
                    {
                        string startCell = dyeingValueAddress.First();
                        string endCell = dyeingValueAddress.Last();
                        sheet.Cells[startCell + ":" + endCell].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells[startCell + ":" + endCell].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    }

                    if (string.IsNullOrEmpty(printingValueCells.FirstOrDefault()))
                    {
                        string startCell = printingValueAddress.First();
                        string endCell = printingValueAddress.Last();
                        sheet.Cells[startCell + ":" + endCell].Style.Fill.PatternType = ExcelFillStyle.None;
                    }
                    else if (printingValueCells.All(s => !string.IsNullOrEmpty(s)))
                    {
                        string startCell = printingValueAddress.First();
                        string endCell = printingValueAddress.Last();
                        sheet.Cells[startCell + ":" + endCell].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells[startCell + ":" + endCell].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);

                    }
                    else
                    {
                        string startCell = printingValueAddress.First();
                        string endCell = printingValueAddress.Last();
                        sheet.Cells[startCell + ":" + endCell].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells[startCell + ":" + endCell].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

                    }

                    if (string.IsNullOrEmpty(finishingValueCells.FirstOrDefault()))
                    {
                        string startCell = finishingValueAddress.First();
                        string endCell = finishingValueAddress.Last();
                        sheet.Cells[startCell + ":" + endCell].Style.Fill.PatternType = ExcelFillStyle.None;
                    }
                    else if (finishingValueCells.All(s => !string.IsNullOrEmpty(s)))
                    {
                        string startCell = finishingValueAddress.First();
                        string endCell = finishingValueAddress.Last();
                        sheet.Cells[startCell + ":" + endCell].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells[startCell + ":" + endCell].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);

                    }
                    else
                    {
                        string startCell = finishingValueAddress.First();
                        string endCell = finishingValueAddress.Last();
                        sheet.Cells[startCell + ":" + endCell].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells[startCell + ":" + endCell].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

                    }

                    if (string.IsNullOrEmpty(qcValueCells.FirstOrDefault()))
                    {
                        string startCell = qcValueAddress.First();
                        string endCell = qcValueAddress.Last();
                        sheet.Cells[startCell + ":" + endCell].Style.Fill.PatternType = ExcelFillStyle.None;
                    }
                    else if (qcValueCells.All(s => !string.IsNullOrEmpty(s)))
                    {
                        string startCell = qcValueAddress.First();
                        string endCell = qcValueAddress.Last();
                        sheet.Cells[startCell + ":" + endCell].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells[startCell + ":" + endCell].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);

                    }
                    else
                    {
                        string startCell = qcValueAddress.First();
                        string endCell = qcValueAddress.Last();
                        sheet.Cells[startCell + ":" + endCell].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        sheet.Cells[startCell + ":" + endCell].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                    }
                }
            }
            MemoryStream stream = new MemoryStream();
            package.SaveAs(stream);
            return stream;
        }

        public ReadResponse<KanbanVisualizationViewModel> ReadVisualization(string order, string filter, int page, int size)
        {
            IQueryable<KanbanModel> query = DbSet.Where(s => s.CurrentStepIndex != 0);

            Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<KanbanModel>.Filter(query, filterDictionary);

            Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<KanbanModel>.Order(query, orderDictionary);
            //var totalData = query.Count();
            //query = query.Skip((page - 1) * size).Take(size);
            //var countData = query.Count();

            List<KanbanVisualizationViewModel> resultData = new List<KanbanVisualizationViewModel>();
            //var re = query.ToList();

            var joinQuery = (from kanban in query
                             join dailyOperation in DbContext.DailyOperation
                             on new { KanbanId = kanban.Id, StepIndex = kanban.CurrentStepIndex }
                             equals new { dailyOperation.KanbanId, StepIndex = dailyOperation.KanbanStepIndex }
                             select new { kanban, dailyOperation }).GroupBy(s => s.kanban);

            var totalData = joinQuery.Count();

            var resultQuery = joinQuery.Skip((page - 1) * size).Take(size).ToList();
            var countData = resultQuery.Count;

            var idKanbans = resultQuery.Select(e => e.Key.Id).Distinct();
            var idMachines = resultQuery.SelectMany(e => e).Select(e => e.dailyOperation.MachineId).Distinct();
            var instructionKanbans = DbContext.KanbanInstructions.Include(s => s.Steps).Where(s => idKanbans.Contains(s.KanbanId)).ToList();
            var machines = DbContext.Machine.Where(s => idMachines.Contains(s.Id)).ToList();
            foreach (var item in resultQuery)
            {
                var instructionKanban = instructionKanbans.FirstOrDefault(s => s.KanbanId == item.Key.Id);
                //var instructionKanban = item.Key.Instruction.Steps.FirstOrDefault(s => s.KanbanId == item.Key.Id);
                var step = instructionKanban != null ? instructionKanban.Steps.FirstOrDefault(s => s.StepIndex == item.Key.CurrentStepIndex) : null;
                //var steps = instructionKanban != null ? DbContext.KanbanSteps.Where(s => s.InstructionId == instructionKanban.Id).ToList() : new List<KanbanStepModel>();
                //var step = instructionKanban != null ? DbContext.KanbanSteps.FirstOrDefault(s => s.InstructionId == instructionKanban.Id && s.StepIndex == item.Key.CurrentStepIndex) : null;
                var outputDO = item.Where(s => s.dailyOperation.Type.ToLower() == "output").OrderByDescending(s => s.dailyOperation.CreatedUtc).FirstOrDefault()?.dailyOperation;

                if (outputDO != null)
                {
                    var machine = machines.FirstOrDefault(s => s.Id == outputDO.MachineId);

                    var itemData = new KanbanVisualizationViewModel()
                    {
                        BadOutput = outputDO.BadOutput,
                        Cart = new CartViewModel()
                        {
                            CartNumber = item.Key.CartCartNumber
                        },
                        Code = item.Key.Code,
                        CurrentStepIndex = item.Key.CurrentStepIndex,
                        DailyOperationMachine = machine?.Name,
                        Deadline = step?.Deadline,
                        GoodOutput = outputDO.GoodOutput,
                        Process = step?.Process,
                        ProcessArea = step?.ProcessArea,
                        ProductionOrder = new ProductionOrderIntegrationViewModel()
                        {
                            OrderNo = item.Key.ProductionOrderOrderNo,
                            SalesContractNo = item.Key.ProductionOrderSalesContractNo,
                            DeliveryDate = item.Key.ProductionOrderDeliveryDate,
                            Buyer = new BuyerIntegrationViewModel()
                            {
                                Name = item.Key.ProductionOrderBuyerName
                            },
                            OrderQuantity = item.Key.SelectedProductionOrderDetailQuantity
                        },
                        StepsLength = instructionKanban != null ? instructionKanban.Steps.Count : 0,
                        Type = "Output"

                    };

                    resultData.Add(itemData);
                }
                else
                {
                    var inputDO = item.Where(s => s.dailyOperation.Type.ToLower() == "input").OrderByDescending(s => s.dailyOperation.CreatedUtc).FirstOrDefault()?.dailyOperation;

                    if (inputDO != null)
                    {
                        var machine = machines.FirstOrDefault(s => s.Id == inputDO.MachineId);

                        var itemData = new KanbanVisualizationViewModel()
                        {
                            Cart = new CartViewModel()
                            {
                                CartNumber = item.Key.CartCartNumber
                            },
                            Code = item.Key.Code,
                            CurrentStepIndex = item.Key.CurrentStepIndex,
                            DailyOperationMachine = machine?.Name,
                            Deadline = step?.Deadline,
                            Process = step?.Process,
                            ProcessArea = step?.ProcessArea,
                            ProductionOrder = new ProductionOrderIntegrationViewModel()
                            {
                                OrderNo = item.Key.ProductionOrderOrderNo,
                                SalesContractNo = item.Key.ProductionOrderSalesContractNo,
                                DeliveryDate = item.Key.ProductionOrderDeliveryDate,
                                Buyer = new BuyerIntegrationViewModel()
                                {
                                    Name = item.Key.ProductionOrderBuyerName
                                },
                                OrderQuantity = item.Key.SelectedProductionOrderDetailQuantity
                            },
                            StepsLength = instructionKanban != null ? instructionKanban.Steps.Count : 0,
                            Type = "Input",
                            InputQuantity = inputDO.Input

                        };

                        resultData.Add(itemData);
                    }
                }
            }


            return new ReadResponse<KanbanVisualizationViewModel>(resultData, countData, totalData, orderDictionary, new List<string>());
        }

        //private MemoryStream CreateExcel(List<KeyValuePair<DataTable, String>> dtSourceList, bool styling = false)
        //{
        //    ExcelPackage package = new ExcelPackage();
        //    foreach (KeyValuePair<DataTable, String> item in dtSourceList)
        //    {
        //        var sheet = package.Workbook.Worksheets.Add(item.Value);
        //        //sheet.Cells["A1:M1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //        //sheet.Cells["A1:M1"].Style.Font.Bold = true;
        //        //sheet.Cells["A1"].Value = "Kanban";
        //        //sheet.Cells["A1:C1"].Merge = true;
        //        //sheet.Cells["A1:C1"].Style.Border.BorderAround(ExcelBorderStyle.Medium);
        //        //sheet.Cells["D1"].Value = "Pre Treatment";
        //        //sheet.Cells["D1:E1"].Merge = true;
        //        //sheet.Cells["D1:E1"].Style.Border.BorderAround(ExcelBorderStyle.Medium);
        //        //sheet.Cells["F1"].Value = "Dyeing";
        //        //sheet.Cells["F1:G1"].Merge = true;
        //        //sheet.Cells["F1:G1"].Style.Border.BorderAround(ExcelBorderStyle.Medium);
        //        //sheet.Cells["H1"].Value = "Printing";
        //        //sheet.Cells["H1:I1"].Merge = true;
        //        //sheet.Cells["H1:I1"].Style.Border.BorderAround(ExcelBorderStyle.Medium);
        //        //sheet.Cells["J1"].Value = "Finishing";
        //        //sheet.Cells["J1:K1"].Merge = true;
        //        //sheet.Cells["J1:K1"].Style.Border.BorderAround(ExcelBorderStyle.Medium);
        //        //sheet.Cells["L1"].Value = "QC";
        //        //sheet.Cells["L1:M1"].Merge = true;
        //        //sheet.Cells["L1:M1"].Style.Border.BorderAround(ExcelBorderStyle.Medium);
        //        sheet.Cells["A1:AC1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        //        sheet.Cells["A1:AC1"].Style.Font.Bold = true;

        //        sheet.Cells["A1"].Value = "Kanban";
        //        sheet.Cells["A1:D1"].Merge = true;
        //        sheet.Cells["A1:D1"].Style.Border.BorderAround(ExcelBorderStyle.Medium);
        //        sheet.Cells["E1"].Value = "Pre Treatment";
        //        sheet.Cells["E1:I1"].Merge = true;
        //        sheet.Cells["E1:I1"].Style.Border.BorderAround(ExcelBorderStyle.Medium);
        //        sheet.Cells["J1"].Value = "Dyeing";
        //        sheet.Cells["J1:N1"].Merge = true;
        //        sheet.Cells["J1:N1"].Style.Border.BorderAround(ExcelBorderStyle.Medium);
        //        sheet.Cells["O1"].Value = "Printing";
        //        sheet.Cells["O1:S1"].Merge = true;
        //        sheet.Cells["O1:S1"].Style.Border.BorderAround(ExcelBorderStyle.Medium);
        //        sheet.Cells["T1"].Value = "Finishing";
        //        sheet.Cells["T1:X1"].Merge = true;
        //        sheet.Cells["T1:X1"].Style.Border.BorderAround(ExcelBorderStyle.Medium);
        //        sheet.Cells["Y1"].Value = "QC";
        //        sheet.Cells["Y1:AC1"].Merge = true;
        //        sheet.Cells["Y1:AC1"].Style.Border.BorderAround(ExcelBorderStyle.Medium);
        //        sheet.Cells["A2"].LoadFromDataTable(item.Key, true, (styling == true) ? OfficeOpenXml.Table.TableStyles.Light16 : OfficeOpenXml.Table.TableStyles.None);
        //        sheet.Cells[sheet.Dimension.Address].AutoFitColumns();

        //        //for (var rowNum = 3; rowNum <= sheet.Dimension.End.Row; rowNum++)
        //        //{
        //        //    string startCell = "D" + rowNum.ToString();
        //        //    string endCell = "M" + rowNum.ToString();
        //        //    IEnumerable<string> valueAddress = SnapshotDataCells.Select(s => s + rowNum.ToString());
        //        //    IEnumerable<string> valueCells = valueAddress.Select(s => sheet.Cells[s].Value?.ToString());

        //        //    sheet.Cells[startCell + ":" + endCell].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //        //    if (valueCells.Contains("-"))
        //        //    {
        //        //        sheet.Cells[startCell + ":" + endCell].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
        //        //    }
        //        //    else
        //        //    {
        //        //        sheet.Cells[startCell + ":" + endCell].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
        //        //    }
        //        //}
        //        var preTreatmentCells = SnapshotDataCells.Skip(5 * 0).Take(5);
        //        var dyeingCells = SnapshotDataCells.Skip(5 * 1).Take(5);
        //        var printingCells = SnapshotDataCells.Skip(5 * 2).Take(5);
        //        var finishingCells = SnapshotDataCells.Skip(5 * 3).Take(5);
        //        var qcCells = SnapshotDataCells.Skip(5 * 4).Take(5);
        //        for (var rowNum = 3; rowNum <= sheet.Dimension.End.Row; rowNum++)
        //        {
        //            IEnumerable<string> preTreatmentValueAddress = preTreatmentCells.Select(s => s + rowNum.ToString());
        //            IEnumerable<string> preTreatmentValueCells = preTreatmentValueAddress.Select(s => sheet.Cells[s].Value?.ToString());
        //            IEnumerable<string> dyeingValueAddress = dyeingCells.Select(s => s + rowNum.ToString());
        //            IEnumerable<string> dyeingValueCells = dyeingValueAddress.Select(s => sheet.Cells[s].Value?.ToString());
        //            IEnumerable<string> printingValueAddress = printingCells.Select(s => s + rowNum.ToString());
        //            IEnumerable<string> printingValueCells = printingValueAddress.Select(s => sheet.Cells[s].Value?.ToString());
        //            IEnumerable<string> finishingValueAddress = finishingCells.Select(s => s + rowNum.ToString());
        //            IEnumerable<string> finishingValueCells = finishingValueAddress.Select(s => sheet.Cells[s].Value?.ToString());
        //            IEnumerable<string> qcValueAddress = qcCells.Select(s => s + rowNum.ToString());
        //            IEnumerable<string> qcValueCells = qcValueAddress.Select(s => sheet.Cells[s].Value?.ToString());

        //            if (string.IsNullOrEmpty(preTreatmentValueCells.FirstOrDefault()))
        //            {
        //                string startCell = preTreatmentValueAddress.First();
        //                string endCell = preTreatmentValueAddress.Last();
        //                sheet.Cells[startCell + ":" + endCell].Style.Fill.PatternType = ExcelFillStyle.None;
        //            }
        //            else if (preTreatmentValueCells.Contains("-"))
        //            {
        //                string startCell = preTreatmentValueAddress.First();
        //                string endCell = preTreatmentValueAddress.Last();
        //                sheet.Cells[startCell + ":" + endCell].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                sheet.Cells[startCell + ":" + endCell].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
        //            }
        //            else
        //            {
        //                string startCell = preTreatmentValueAddress.First();
        //                string endCell = preTreatmentValueAddress.Last();
        //                sheet.Cells[startCell + ":" + endCell].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                sheet.Cells[startCell + ":" + endCell].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
        //            }

        //            if (string.IsNullOrEmpty(dyeingValueCells.FirstOrDefault()))
        //            {
        //                string startCell = dyeingValueAddress.First();
        //                string endCell = dyeingValueAddress.Last();
        //                sheet.Cells[startCell + ":" + endCell].Style.Fill.PatternType = ExcelFillStyle.None;
        //            }
        //            else if (dyeingValueCells.Contains("-"))
        //            {
        //                string startCell = dyeingValueAddress.First();
        //                string endCell = dyeingValueAddress.Last();
        //                sheet.Cells[startCell + ":" + endCell].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                sheet.Cells[startCell + ":" + endCell].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
        //            }
        //            else
        //            {
        //                string startCell = dyeingValueAddress.First();
        //                string endCell = dyeingValueAddress.Last();
        //                sheet.Cells[startCell + ":" + endCell].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                sheet.Cells[startCell + ":" + endCell].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
        //            }

        //            if (string.IsNullOrEmpty(printingValueCells.FirstOrDefault()))
        //            {
        //                string startCell = printingValueAddress.First();
        //                string endCell = printingValueAddress.Last();
        //                sheet.Cells[startCell + ":" + endCell].Style.Fill.PatternType = ExcelFillStyle.None;
        //            }
        //            else if (printingValueCells.Contains("-"))
        //            {
        //                string startCell = printingValueAddress.First();
        //                string endCell = printingValueAddress.Last();
        //                sheet.Cells[startCell + ":" + endCell].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                sheet.Cells[startCell + ":" + endCell].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
        //            }
        //            else
        //            {
        //                string startCell = printingValueAddress.First();
        //                string endCell = printingValueAddress.Last();
        //                sheet.Cells[startCell + ":" + endCell].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                sheet.Cells[startCell + ":" + endCell].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
        //            }

        //            if (string.IsNullOrEmpty(finishingValueCells.FirstOrDefault()))
        //            {
        //                string startCell = finishingValueAddress.First();
        //                string endCell = finishingValueAddress.Last();
        //                sheet.Cells[startCell + ":" + endCell].Style.Fill.PatternType = ExcelFillStyle.None;
        //            }
        //            else if (finishingValueCells.Contains("-"))
        //            {
        //                string startCell = finishingValueAddress.First();
        //                string endCell = finishingValueAddress.Last();
        //                sheet.Cells[startCell + ":" + endCell].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                sheet.Cells[startCell + ":" + endCell].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
        //            }
        //            else
        //            {
        //                string startCell = finishingValueAddress.First();
        //                string endCell = finishingValueAddress.Last();
        //                sheet.Cells[startCell + ":" + endCell].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                sheet.Cells[startCell + ":" + endCell].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
        //            }

        //            if (string.IsNullOrEmpty(qcValueCells.FirstOrDefault()))
        //            {
        //                string startCell = qcValueAddress.First();
        //                string endCell = qcValueAddress.Last();
        //                sheet.Cells[startCell + ":" + endCell].Style.Fill.PatternType = ExcelFillStyle.None;
        //            }
        //            else if (qcValueCells.Contains("-"))
        //            {
        //                string startCell = qcValueAddress.First();
        //                string endCell = qcValueAddress.Last();
        //                sheet.Cells[startCell + ":" + endCell].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                sheet.Cells[startCell + ":" + endCell].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
        //            }
        //            else
        //            {
        //                string startCell = qcValueAddress.First();
        //                string endCell = qcValueAddress.Last();
        //                sheet.Cells[startCell + ":" + endCell].Style.Fill.PatternType = ExcelFillStyle.Solid;
        //                sheet.Cells[startCell + ":" + endCell].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
        //            }
        //        }
        //    }
        //    MemoryStream stream = new MemoryStream();
        //    package.SaveAs(stream);
        //    return stream;
        //}
    }
}
