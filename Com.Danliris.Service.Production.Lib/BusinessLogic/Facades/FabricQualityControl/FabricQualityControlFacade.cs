using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.FabricQualityControl;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.FabricQualityControl;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.FabricQualityControl;
using Com.Danliris.Service.Production.Lib;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Com.Danliris.Service.Production.Lib.Utilities;
using System.Linq;
using Newtonsoft.Json;
using Com.Moonlay.NetCore.Lib;
using System.Threading.Tasks;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.FabricQualityControl;
using System.IO;
using System.Data;
using Com.Danliris.Service.Finishing.Printing.Lib.Helpers;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.FabricQualityControl
{
    public class FabricQualityControlFacade : IFabricQualityControlFacade
    {
        private readonly ProductionDbContext DbContext;
        private readonly DbSet<FabricQualityControlModel> DbSet;
        private readonly FabricQualityControlLogic FabricQualityControlLogic;

        public FabricQualityControlFacade(IServiceProvider serviceProvider, ProductionDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<FabricQualityControlModel>();
            FabricQualityControlLogic = serviceProvider.GetService<FabricQualityControlLogic>();
        }

        public ReadResponse<FabricQualityControlModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<FabricQualityControlModel> query = DbSet;

            List<string> searchAttributes = new List<string>()
            {
                "Code", "ShiftIm", "OperatorIm", "ProductionOrderNo", "ProductionOrderType"
            };
            query = QueryHelper<FabricQualityControlModel>.Search(query, searchAttributes, keyword);

            Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<FabricQualityControlModel>.Filter(query, filterDictionary);

            List<string> selectedFields = new List<string>()
            {
                "Id", "Code", "CartNo", "DateIm", "IsUsed", "MachineNoIm", "OperatorIm", "ProductionOrderNo", "ProductionOrderType", "ShiftIm", "LastModifiedUtc"
            };
            query = query
                .Select(field => new FabricQualityControlModel
                {
                    Id = field.Id,
                    Code = field.Code,
                    CartNo = field.CartNo,
                    DateIm = field.DateIm,
                    IsUsed = field.IsUsed,
                    LastModifiedUtc = field.LastModifiedUtc,
                    MachineNoIm = field.MachineNoIm,
                    OperatorIm = field.OperatorIm,
                    ProductionOrderNo = field.ProductionOrderNo,
                    ProductionOrderType = field.ProductionOrderType,
                    ShiftIm = field.ShiftIm
                });

            Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<FabricQualityControlModel>.Order(query, orderDictionary);

            Pageable<FabricQualityControlModel> pageable = new Pageable<FabricQualityControlModel>(query, page - 1, size);
            List<FabricQualityControlModel> data = pageable.Data.ToList();
            int totalData = pageable.TotalCount;

            return new ReadResponse<FabricQualityControlModel>(data, totalData, orderDictionary, selectedFields);
        }

        public async Task<int> CreateAsync(FabricQualityControlModel model)
        {
            do
            {
                model.Code = CodeGenerator.Generate();
            }
            while (DbSet.Any(d => d.Code.Equals(model.Code)));
            FabricQualityControlLogic.CreateModel(model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await FabricQualityControlLogic.DeleteModel(id);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<FabricQualityControlModel> ReadByIdAsync(int id)
        {
            return await FabricQualityControlLogic.ReadModelById(id);
        }

        public async Task<int> UpdateAsync(int id, FabricQualityControlModel model)
        {
            FabricQualityControlLogic.UpdateModelAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<FabricQualityControlViewModel> GetReport(int page, int size, string code, int kanbanId, string productionOrderType, string productionOrderNo, string shiftIm, DateTime? dateFrom, DateTime? dateTo, int offSet)
        {
            var queries = GetReport(code, kanbanId, productionOrderType, productionOrderNo, shiftIm, dateFrom, dateTo, offSet);

            Pageable<FabricQualityControlViewModel> pageable = new Pageable<FabricQualityControlViewModel>(queries, page - 1, size);
            List<FabricQualityControlViewModel> data = pageable.Data.ToList();

            return new ReadResponse<FabricQualityControlViewModel>(queries, pageable.TotalCount, new Dictionary<string, string>(), new List<string>());
        }

        public MemoryStream GenerateExcel(string code, int kanbanId, string productionOrderType, string productionOrderNo, string shiftIm, DateTime? dateFrom, DateTime? dateTo, int offSet)
        {
            var data = GetReport(code, kanbanId, productionOrderType, productionOrderNo, shiftIm, dateFrom, dateTo, offSet);

            data = data.OrderByDescending(x => x.LastModifiedUtc).ToList();

            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Nomor Pemeriksaan Kain", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Nomor Kanban", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Nomor Kereta", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jenis Order", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Nomor Order", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Tanggal IM", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Shift", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Operator IM", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No Mesin IM", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Konstruksi", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Buyer", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Warna", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jumlah Order (meter)", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Packing Instruction", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Nomor PCS", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Panjang PCS (meter)", DataType = typeof(Int32) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Lebar PCS (meter)", DataType = typeof(Int32) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Nilai", DataType = typeof(Int32) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Grade", DataType = typeof(String) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Aval (meter)", DataType = typeof(Int32) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Sampel (meter)", DataType = typeof(Int32) });

            if (data.Count == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 0, 0, 0, "", 0, 0);
            }
            else
            {
                int index = 1;
                foreach (var item in data)
                {
                    foreach(var detail in item.FabricGradeTests)
                    {
                        dt.Rows.Add(index++, item.Code, item.KanbanCode, item.CartNo, item.ProductionOrderType, item.ProductionOrderNo,
                            item.DateIm.GetValueOrDefault().AddHours(offSet).ToString("dd/MM/yyyy"), item.ShiftIm, item.OperatorIm, item.MachineNoIm,
                            item.Construction, item.Buyer, item.Color, item.OrderQuantity.GetValueOrDefault().ToString(), item.PackingInstruction,
                            detail.PcsNo, detail.InitLength, detail.Width, detail.FinalScore, detail.Grade, detail.AvalLength, detail.SampleLength);
                    }
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(dt, "Pemeriksaan Kain") }, true);
        }

        public List<FabricQualityControlViewModel> GetReport(string code, int kanbanId, string productionOrderType, string productionOrderNo, string shiftIm, DateTime? dateFrom, DateTime? dateTo, int offSet)
        {

            IQueryable<FabricQualityControlModel> query = DbContext.FabricQualityControls.Include(x => x.FabricGradeTests).AsQueryable();

            IEnumerable<FabricQualityControlViewModel> fabricQCs;

            if (!string.IsNullOrEmpty(code))
                query = query.Where(x => x.Code == code);

            if (kanbanId != -1)
                query = query.Where(x => x.KanbanId == kanbanId);

            if (!string.IsNullOrEmpty(productionOrderType))
                query = query.Where(x => x.ProductionOrderType == productionOrderType);

            if (!string.IsNullOrEmpty(productionOrderNo))
                query = query.Where(x => x.ProductionOrderNo == productionOrderNo);

            if (!string.IsNullOrEmpty(shiftIm))
                query = query.Where(x => x.ShiftIm == shiftIm);

            if (dateFrom == null && dateTo == null)
            {
                query = query
                    .Where(x => DateTimeOffset.UtcNow.AddDays(-30).Date <= x.DateIm.AddHours(offSet).Date
                        && x.DateIm.AddHours(offSet).Date <= DateTime.UtcNow.Date);
            }
            else if (dateFrom == null && dateTo != null)
            {
                query = query
                    .Where(x => dateTo.Value.AddDays(-30).Date <= x.DateIm.AddHours(offSet).Date
                        && x.DateIm.AddHours(offSet).Date <= dateTo.Value.Date);
            }
            else if (dateTo == null && dateFrom != null)
            {
                query = query
                    .Where(x => dateFrom.Value.Date <= x.DateIm.AddHours(offSet).Date
                        && x.DateIm.AddHours(offSet).Date <= dateFrom.Value.AddDays(30).Date);
            }
            else
            {
                query = query
                    .Where(x => dateFrom.Value.Date <= x.DateIm.AddHours(offSet).Date
                        && x.DateIm.AddHours(offSet).Date <= dateTo.Value.Date);
            }

            fabricQCs = query.Select(x => new FabricQualityControlViewModel()
            {
                Code = x.Code,
                KanbanCode = x.KanbanCode,
                CartNo = x.CartNo,
                ProductionOrderType = x.ProductionOrderType,
                ProductionOrderNo = x.ProductionOrderNo,
                DateIm = x.DateIm,
                ShiftIm = x.ShiftIm,
                OperatorIm = x.OperatorIm,
                MachineNoIm = x.MachineNoIm,
                Construction = x.Construction,
                Buyer = x.Buyer,
                Color = x.Color,
                OrderQuantity = x.OrderQuantity,
                PackingInstruction = x.PackingInstruction,
                FabricGradeTests = x.FabricGradeTests.Select(y => new FabricGradeTestViewModel()
                {
                    PcsNo = y.PcsNo,
                    InitLength = y.InitLength,
                    Width = y.Width,
                    FinalScore = y.FinalScore,
                    Grade = y.Grade,
                    AvalLength = y.AvalLength,
                    SampleLength = y.SampleLength
                }).ToList(),
                LastModifiedUtc = x.LastModifiedUtc
            });

            return fabricQCs.ToList();
        }
    }
}
