using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.DyestuffChemicalUsageReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.DyestuffChemicalUsageReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.DyestuffChemicalUsageReceipt;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Newtonsoft.Json;
using Com.Moonlay.NetCore.Lib;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DyestuffChemicalUsageReceipt;
using System.IO;
using System.Data;
using System.Globalization;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.DyestuffChemicalUsageReceipt
{
    public class DyestuffChemicalUsageReceiptFacade : IDyestuffChemicalUsageReceiptFacade
    {
        private readonly ProductionDbContext DbContext;
        private readonly DbSet<DyestuffChemicalUsageReceiptModel> DbSet;
        private readonly DyestuffChemicalUsageReceiptLogic DyestuffChemicalUsageReceiptLogic;
        private readonly IServiceProvider ServiceProvider;

        public DyestuffChemicalUsageReceiptFacade(IServiceProvider serviceProvider, ProductionDbContext dbContext)
        {
            ServiceProvider = serviceProvider;
            DbContext = dbContext;
            DbSet = dbContext.Set<DyestuffChemicalUsageReceiptModel>();
            DyestuffChemicalUsageReceiptLogic = serviceProvider.GetService<DyestuffChemicalUsageReceiptLogic>();
        }

        public Task<int> CreateAsync(DyestuffChemicalUsageReceiptModel model)
        {
            DyestuffChemicalUsageReceiptLogic.CreateModel(model);
            return DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await DyestuffChemicalUsageReceiptLogic.DeleteModel(id);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<Tuple<DyestuffChemicalUsageReceiptModel, string>> GetDataByStrikeOff(int strikeOffId)
        {
            var data = await DyestuffChemicalUsageReceiptLogic.GetDataByStrikeOff(strikeOffId);
            var orderNo = DyestuffChemicalUsageReceiptLogic.GetLatestProductionOrderNoByStrikeOff(strikeOffId);
            return new Tuple<DyestuffChemicalUsageReceiptModel, string>(data, orderNo);
        }

        public ReadResponse<DyestuffChemicalUsageReceiptModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<DyestuffChemicalUsageReceiptModel> query = DbSet;

            List<string> searchAttributes = new List<string>()
            {
                "ProductionOrderOrderNo", "StrikeOffCode"
            };
            query = QueryHelper<DyestuffChemicalUsageReceiptModel>.Search(query, searchAttributes, keyword);

            Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DyestuffChemicalUsageReceiptModel>.Filter(query, filterDictionary);

            List<string> selectedFields = new List<string>()
            {

                "Id","ProductionOrder","StrikeOff","Date","LastModifiedUtc"

            };

            Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DyestuffChemicalUsageReceiptModel>.Order(query, orderDictionary);

            Pageable<DyestuffChemicalUsageReceiptModel> pageable = new Pageable<DyestuffChemicalUsageReceiptModel>(query, page - 1, size);
            List<DyestuffChemicalUsageReceiptModel> data = pageable.Data.ToList();
            int totalData = pageable.TotalCount;

            return new ReadResponse<DyestuffChemicalUsageReceiptModel>(data, totalData, orderDictionary, selectedFields);
        }

        public Task<DyestuffChemicalUsageReceiptModel> ReadByIdAsync(int id)
        {
            return DyestuffChemicalUsageReceiptLogic.ReadModelById(id);
        }

        public async Task<int> UpdateAsync(int id, DyestuffChemicalUsageReceiptModel model)
        {
            await DyestuffChemicalUsageReceiptLogic.UpdateModelAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }

        public IQueryable<DyestuffChemicalUsageReceiptReportViewModel> GetReportQuery(string productionOrderNo, string strikeOffCode, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? new DateTime(2100, 1, 1) : (DateTime)dateTo;

            var QueryReport = (from a in DbContext.DyestuffChemicalUsageReceipts
                               join b in DbContext.DyestuffChemicalUsageReceiptItems on a.Id equals b.DyestuffChemicalUsageReceiptId
                               join c in DbContext.DyestuffChemicalUsageReceiptItemDetails on b.Id equals c.DyestuffChemicalUsageReceiptItemId
                               where a.ProductionOrderOrderNo == (string.IsNullOrWhiteSpace(productionOrderNo) ? a.ProductionOrderOrderNo : productionOrderNo)
                               && a.StrikeOffCode == (string.IsNullOrWhiteSpace(strikeOffCode) ? a.StrikeOffCode : strikeOffCode)
                               && a.Date.AddHours(offset).Date >= DateFrom.Date
                               && a.Date.AddHours(offset).Date <= DateTo.Date
                               && a.IsDeleted == false
                               && b.IsDeleted == false
                               && c.IsDeleted == false
                               select new DyestuffChemicalUsageReceiptReportViewModel
                               {
                                   createdBy = a.CreatedBy,
                                   productionOrderNo = a.ProductionOrderOrderNo,
                                   strikeOff = a.StrikeOffCode,
                                   strikeOffType = a.StrikeOffType,
                                   color = b.ColorCode,
                                   date = a.Date,
                                   quantity = (int)a.ProductionOrderOrderQuantity,
                                   materialName = a.ProductionOrderMaterialName,
                                   materialWidth = a.ProductionOrderMaterialWidth,
                                   materialConstructionName = a.ProductionOrderMaterialConstructionName,
                                   name = c.Name,
                                   receiptQty = c.ReceiptQuantity,
                                   adj1Qty = c.Adjs1Quantity,
                                   adj2Qty = c.Adjs2Quantity,
                                   adj3Qty = c.Adjs3Quantity,
                                   adj4Qty = c.Adjs4Quantity,
                                   totalRealization = b.TotalRealizationQty,
                                   totalScreen = a.TotalScreen
                               });
            return QueryReport;
        }

        public Tuple<List<DyestuffChemicalUsageReceiptReportViewModel>, int> GetReport(string productionOrderNo, string strikeOffCode, DateTime? dateFrom, DateTime? dateTo, int page, int size, string Order, int offset)
        {
            var Query = GetReportQuery(productionOrderNo, strikeOffCode, dateFrom, dateTo, offset);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Order);
            if (OrderDictionary.Count.Equals(0))
            {
                Query = Query.OrderByDescending(b => b.LastModifiedUtc);
            }

            Pageable<DyestuffChemicalUsageReceiptReportViewModel> pageable = new Pageable<DyestuffChemicalUsageReceiptReportViewModel>(Query, page - 1, size);
            List<DyestuffChemicalUsageReceiptReportViewModel> Data = pageable.Data.ToList<DyestuffChemicalUsageReceiptReportViewModel>();
            int totalData = pageable.TotalCount;

            return Tuple.Create(Data, totalData);
        }

        public MemoryStream GenerateExcel(string productionOrderNo, string strikeOffCode, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var data = GetReportQuery(productionOrderNo, strikeOffCode, dateFrom, dateTo, offset);

            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? new DateTime(1970, 1, 1) : (DateTime)dateTo;

            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn() { ColumnName = "User", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "No.SPP", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Tanggal", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Quatity", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jenis Benang", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Konstruksi", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Lebar", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Design", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jenis Print", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Kode Warna", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Nama", DataType = typeof(string) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Resep", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Adjs 1 Qty", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Adjs 2 Qty", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Adjs 3 Qty", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Adjs 4 Qty", DataType = typeof(double) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Pembuatan (Kg)", DataType = typeof(decimal) });
            dt.Columns.Add(new DataColumn() { ColumnName = "Jumlah Screen", DataType = typeof(decimal) });

            if (data.Count() == 0)
            {
                dt.Rows.Add("", "", "", "", "", "", "", "", "", "", "", 0, 0, 0, 0, 0, 0.0, 0.0);
            }
            else
            {
                foreach (var model in data)
                {
                    dt.Rows.Add(model.createdBy, model.productionOrderNo, model.date.ToString("dd MM yyyy", new CultureInfo("id-ID")), model.quantity.ToString(), model.materialName, model.materialConstructionName, model.materialWidth,
                                model.strikeOff, model.strikeOffType, model.color, model.name, model.receiptQty, model.adj1Qty, model.adj2Qty, model.adj3Qty, model.adj4Qty, model.totalRealization, model.totalScreen);
                }
            }
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("sheet 1");

                var countData = data.Count();

                worksheet.Cells["A" + 1 + ":R" + 4 + ""].Style.Font.Bold = true;
                worksheet.Cells["A1"].Value = "Laporan Resep Pemakaian Dyestuff & Chemical";
                worksheet.Cells["A2"].Value = "Tanggal Awal : " + DateFrom.ToString("dd-MM-yyyy") + " - Tanggal Akhir : " + DateTo.ToString("dd-MM-yyyy");
                worksheet.Cells["A" + 1 + ":R" + 1 + ""].Merge = true;
                worksheet.Cells["A" + 2 + ":R" + 2 + ""].Merge = true;
                worksheet.Cells["A" + 1 + ":R" + 4 + ""].Style.Font.Bold = true;

                //if(countData > 0)
                //{
                //    worksheet.Cells["A" + 5 + ":A" + (4 + countData) + ""].Merge = true;
                //    worksheet.Cells["A" + 5 + ":A" + (4 + countData) + ""].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                //    worksheet.Cells["B" + 5 + ":B" + (4 + countData) + ""].Merge = true;
                //    worksheet.Cells["B" + 5 + ":B" + (4 + countData) + ""].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                //    worksheet.Cells["C" + 5 + ":C" + (4 + countData) + ""].Merge = true;
                //    worksheet.Cells["C" + 5 + ":C" + (4 + countData) + ""].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                //    worksheet.Cells["D" + 5 + ":D" + (4 + countData) + ""].Merge = true;
                //    worksheet.Cells["D" + 5 + ":D" + (4 + countData) + ""].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                //    worksheet.Cells["E" + 5 + ":E" + (4 + countData) + ""].Merge = true;
                //    worksheet.Cells["E" + 5 + ":E" + (4 + countData) + ""].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                //    worksheet.Cells["F" + 5 + ":F" + (4 + countData) + ""].Merge = true;
                //    worksheet.Cells["F" + 5 + ":F" + (4 + countData) + ""].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                //    worksheet.Cells["G" + 5 + ":G" + (4 + countData) + ""].Merge = true;
                //    worksheet.Cells["G" + 5 + ":G" + (4 + countData) + ""].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                //    worksheet.Cells["H" + 5 + ":H" + (4 + countData) + ""].Merge = true;
                //    worksheet.Cells["H" + 5 + ":H" + (4 + countData) + ""].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                //    worksheet.Cells["I" + 5 + ":I" + (4 + countData) + ""].Merge = true;
                //    worksheet.Cells["I" + 5 + ":I" + (4 + countData) + ""].Style.VerticalAlignment = ExcelVerticalAlignment.Top;

                //    worksheet.Cells["J" + 5 + ":J" + (4 + countData) + ""].Merge = true;
                //    worksheet.Cells["J" + 5 + ":J" + (4 + countData) + ""].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                //}

                worksheet.Cells.AutoFitColumns();
                worksheet.Cells["A4"].LoadFromDataTable(dt, true);

                var stream = new MemoryStream();
                package.SaveAs(stream);

                return stream;
            }
        }
    }
}
