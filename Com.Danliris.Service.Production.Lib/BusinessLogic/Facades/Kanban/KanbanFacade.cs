using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.Helpers;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Daily_Operation;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.Machine;
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
                "Id", "Code", "ProductionOrder","CurrentStepIndex", "Cart", "Instruction", "SelectedProductionOrderDetail", "LastModifiedUtc", "OldKanbanId", "IsComplete", "IsInactive"
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

        private List<int> DODataByDay(DateTime searchDate)
        {
            var doData = DbContext.DailyOperation

                .Where(s => (s.DateInput.HasValue && s.DateInput.Value.Date <= searchDate.Date) || (s.DateOutput.HasValue && s.DateOutput.Value.Date >= searchDate.Date))
                .GroupBy(s => new { s.KanbanId, s.MachineId, s.StepId })
                .Where(d => d.Count() == 2 && d.Count(e => e.Type == "input") == 1 && d.Count(e => e.Type == "output") == 1).Select(s => s.Key.KanbanId).Distinct().ToList();

            return doData;
        }

        private List<int> DODataByMonth(DateTime searchDate)
        {
            var doData = DbContext.DailyOperation

                .Where(s => (s.DateInput.HasValue && s.DateInput.Value.Month == searchDate.Month && s.DateInput.Value.Year == searchDate.Year)
                                || (s.DateOutput.HasValue && s.DateOutput.Value.Month == searchDate.Month && s.DateOutput.Value.Year == searchDate.Year))
                .GroupBy(s => new { s.KanbanId, s.MachineId, s.StepId })
                .Where(d => d.Count() == 2 && d.Count(e => e.Type == "input") == 1 && d.Count(e => e.Type == "output") == 1).Select(s => s.Key.KanbanId).Distinct().ToList();

            return doData;
        }

        private KeyValuePair<DataTable, string> GenerateDataTable(List<int> doData, DateTime searchDate, string title, bool isMonthly)
        {
            DataTable dt = new DataTable();
            //dt.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(int) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Buyer", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Nomor SPP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Qty", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Pre Treatment Qty", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Pre Treatment Day", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Dyeing Qty", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Dyeing Day", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Printing Qty", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Printing Day", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Finishing Qty", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Finishing Day", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "QC Qty", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "QC Day", DataType = typeof(string) });

            var selectedKanbans = DbSet.Include(s => s.Instruction).ThenInclude(s => s.Steps).Where(s => doData.Contains(s.Id)).ToList();
            var selectedDos = DbContext.DailyOperation.Where(s => doData.Contains(s.KanbanId)).GroupBy(s => s.KanbanId).ToList();
            if (selectedDos.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "");
            }
            else
            {
                foreach (var item in selectedDos)
                {
                    List<DailyOperationModel> outDailyOperations = new List<DailyOperationModel>();

                    if (isMonthly)
                    {
                        outDailyOperations = item.Where(s => s.Type.ToLower() == "output"
                                                        && s.DateOutput.GetValueOrDefault().Month <= searchDate.Month &&
                                                        s.DateOutput.GetValueOrDefault().Year <= searchDate.Year).ToList();
                    }
                    else
                    {
                        outDailyOperations = item.Where(s => s.Type.ToLower() == "output" && s.DateOutput.GetValueOrDefault().Date <= searchDate.Date).ToList();
                    }

                    var inDailyOperations = item.Where(s => s.Type.ToLower() == "input" && outDailyOperations.Any(y => y.StepId == s.StepId));
                    var kanban = selectedKanbans.FirstOrDefault(s => s.Id == item.Key);
                    var preTreatmentArea = kanban.Instruction.Steps.Where(s => s.ProcessArea != null && s.ProcessArea.ToLower() == "area pre treatment").OrderBy(s => s.StepIndex);
                    var dyeingArea = kanban.Instruction.Steps.Where(s => s.ProcessArea != null && s.ProcessArea.ToLower() == "area dyeing").OrderBy(s => s.StepIndex);
                    var printingArea = kanban.Instruction.Steps.Where(s => s.ProcessArea != null && s.ProcessArea.ToLower() == "area printing").OrderBy(s => s.StepIndex);
                    var finishingArea = kanban.Instruction.Steps.Where(s => s.ProcessArea != null && s.ProcessArea.ToLower() == "area finishing").OrderBy(s => s.StepIndex);
                    var qcArea = kanban.Instruction.Steps.Where(s => s.ProcessArea != null && s.ProcessArea.ToLower() == "area qc").OrderBy(s => s.StepIndex);

                    double? preTreatmentQty = 0;
                    double? dyeingQty = 0;
                    double? printingQty = 0;
                    double? finishingQty = 0;
                    double? qcQty = 0;
                    int preTreatmentDay = 0;
                    int dyeingDay = 0;
                    int printingDay = 0;
                    int finishingDay = 0;
                    int qcDay = 0;


                    if (preTreatmentArea.Count() > 0)
                    {
                        var preTreatmentStart = inDailyOperations.OrderBy(s => s.DateInput).FirstOrDefault(s => preTreatmentArea.Any(y => y.Id == s.StepId));
                        var preTreatmentDO = outDailyOperations.OrderBy(s => s.DateOutput).LastOrDefault(s => preTreatmentArea.Any(y => y.Id == s.StepId));
                        preTreatmentQty = preTreatmentDO?.GoodOutput.GetValueOrDefault() + preTreatmentDO?.BadOutput.GetValueOrDefault();
                        if (preTreatmentDO != null && preTreatmentStart != null)
                            preTreatmentDay = (int)Math.Ceiling((preTreatmentDO.DateOutput.GetValueOrDefault() - preTreatmentStart.DateInput.GetValueOrDefault()).TotalDays);
                    }
                    if (dyeingArea.Count() > 0)
                    {
                        var dyeingStart = inDailyOperations.OrderBy(s => s.DateInput).FirstOrDefault(s => dyeingArea.Any(y => y.Id == s.StepId));
                        var dyeingDO = outDailyOperations.OrderBy(s => s.DateOutput).LastOrDefault(s => dyeingArea.Any(y => y.Id == s.StepId));
                        dyeingQty = dyeingDO?.GoodOutput.GetValueOrDefault() + dyeingDO?.BadOutput.GetValueOrDefault();
                        if (dyeingDO != null && dyeingStart != null)
                            dyeingDay = (int)Math.Ceiling((dyeingDO.DateOutput.GetValueOrDefault() - dyeingStart.DateInput.GetValueOrDefault()).TotalDays);
                    }

                    if (printingArea.Count() > 0)
                    {
                        var printingStart = inDailyOperations.OrderBy(s => s.DateInput).FirstOrDefault(s => printingArea.Any(y => y.Id == s.StepId));
                        var printingDO = outDailyOperations.OrderBy(s => s.DateOutput).LastOrDefault(s => printingArea.Any(y => y.Id == s.StepId));
                        printingQty = printingDO?.GoodOutput.GetValueOrDefault() + printingDO?.BadOutput.GetValueOrDefault();
                        if (printingDO != null && printingStart != null)
                            printingDay = (int)Math.Ceiling((printingDO.DateOutput.GetValueOrDefault() - printingStart.DateInput.GetValueOrDefault()).TotalDays);
                    }

                    if (finishingArea.Count() > 0)
                    {
                        var finishingStart = inDailyOperations.OrderBy(s => s.DateInput).FirstOrDefault(s => finishingArea.Any(y => y.Id == s.StepId));
                        var finishingDO = outDailyOperations.OrderBy(s => s.DateOutput).LastOrDefault(s => finishingArea.Any(y => y.Id == s.StepId));
                        finishingQty = finishingDO?.GoodOutput.GetValueOrDefault() + finishingDO?.BadOutput.GetValueOrDefault();
                        if (finishingDO != null && finishingStart != null)
                            finishingDay = (int)Math.Ceiling((finishingDO.DateOutput.GetValueOrDefault() - finishingStart.DateInput.GetValueOrDefault()).TotalDays);
                    }

                    if (qcArea.Count() > 0)
                    {
                        var qcStart = inDailyOperations.OrderBy(s => s.DateInput).FirstOrDefault(s => qcArea.Any(y => y.Id == s.StepId));
                        var qcDO = outDailyOperations.OrderBy(s => s.DateOutput).LastOrDefault(s => qcArea.Any(y => y.Id == s.StepId));
                        qcQty = qcDO?.GoodOutput.GetValueOrDefault() + qcDO?.BadOutput.GetValueOrDefault();
                        if (qcDO != null && qcStart != null)
                            qcDay = (int)Math.Ceiling((qcDO.DateOutput.GetValueOrDefault() - qcStart.DateInput.GetValueOrDefault()).TotalDays);
                    }

                    dt.Rows.Add(kanban.ProductionOrderBuyerName, kanban.ProductionOrderOrderNo, kanban.SelectedProductionOrderDetailQuantity,
                        preTreatmentQty.GetValueOrDefault() == 0 ? "-" : preTreatmentQty.GetValueOrDefault().ToString(),
                        preTreatmentDay == 0 ? "-" : preTreatmentDay.ToString(),
                        dyeingQty.GetValueOrDefault() == 0 ? "-" : dyeingQty.GetValueOrDefault().ToString(),
                        dyeingDay == 0 ? "-" : dyeingDay.ToString(),
                        printingQty.GetValueOrDefault() == 0 ? "-" : printingQty.GetValueOrDefault().ToString(),
                        printingDay == 0 ? "-" : printingDay.ToString(),
                        finishingQty.GetValueOrDefault() == 0 ? "-" : finishingQty.GetValueOrDefault().ToString(),
                        finishingDay == 0 ? "-" : finishingDay.ToString(),
                        qcQty.GetValueOrDefault() == 0 ? "-" : qcQty.GetValueOrDefault().ToString(),
                        qcDay == 0 ? "-" : qcDay.ToString());

                }
            }

            return new KeyValuePair<DataTable, string>(dt, title);
        }

        public MemoryStream GenerateKanbanSnapshotExcel(DateTime searchDate)
        {
            var dayData = DODataByDay(searchDate);
            var monthData = DODataByMonth(searchDate);

            var dayDataTable = GenerateDataTable(dayData, searchDate, searchDate.Day.ToString(), false);
            var monthDataTable = GenerateDataTable(monthData, searchDate, searchDate.ToString("MMMM"), true);


            return CreateExcel(new List<KeyValuePair<DataTable, string>>() { monthDataTable, dayDataTable }, true);
        }

        private MemoryStream CreateExcel(List<KeyValuePair<DataTable, String>> dtSourceList, bool styling = false)
        {
            ExcelPackage package = new ExcelPackage();
            foreach (KeyValuePair<DataTable, String> item in dtSourceList)
            {
                var sheet = package.Workbook.Worksheets.Add(item.Value);

                sheet.Cells["A1:M1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                sheet.Cells["A1:M1"].Style.Font.Bold = true;

                sheet.Cells["A1"].Value = "Kanban";
                sheet.Cells["A1:C1"].Merge = true;
                sheet.Cells["A1:C1"].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                sheet.Cells["D1"].Value = "Pre Treatment";
                sheet.Cells["D1:E1"].Merge = true;
                sheet.Cells["D1:E1"].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                sheet.Cells["F1"].Value = "Dyeing";
                sheet.Cells["F1:G1"].Merge = true;
                sheet.Cells["F1:G1"].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                sheet.Cells["H1"].Value = "Printing";
                sheet.Cells["H1:I1"].Merge = true;
                sheet.Cells["H1:I1"].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                sheet.Cells["J1"].Value = "Finishing";
                sheet.Cells["J1:K1"].Merge = true;
                sheet.Cells["J1:K1"].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                sheet.Cells["L1"].Value = "QC";
                sheet.Cells["L1:M1"].Merge = true;
                sheet.Cells["L1:M1"].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                sheet.Cells["A2"].LoadFromDataTable(item.Key, true, (styling == true) ? OfficeOpenXml.Table.TableStyles.Light16 : OfficeOpenXml.Table.TableStyles.None);
                sheet.Cells[sheet.Dimension.Address].AutoFitColumns();
            }
            MemoryStream stream = new MemoryStream();
            package.SaveAs(stream);
            return stream;
        }
    }
}
