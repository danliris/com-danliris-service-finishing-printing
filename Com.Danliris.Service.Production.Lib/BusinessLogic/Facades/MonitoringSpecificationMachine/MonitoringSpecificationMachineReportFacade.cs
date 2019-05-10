using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.MonitoringSpecificationMachine;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.MonitoringSpecificationMachine;
using Com.Danliris.Service.Finishing.Printing.Lib.Helpers;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Monitoring_Specification_Machine;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Monitoring_Specification_Machine;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Utilities;
using Com.Moonlay.NetCore.Lib;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.MonitoringSpecificationMachine
{
    
    public class MonitoringSpecificationMachineReportFacade : IMonitoringSpecificationMachineReportFacade
    {
        private readonly ProductionDbContext DbContext;
        private readonly DbSet<MonitoringSpecificationMachineModel> DbSet;
        public MonitoringSpecificationMachineReportFacade(IServiceProvider serviceProvider, ProductionDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = DbContext.Set<MonitoringSpecificationMachineModel>();
        }

        public IQueryable<MonitoringSpecificationMachineReportViewModel> GetReportQuery(int machineId, string productionOrderNo,  DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;

            var Query = (from a in DbContext.MonitoringSpecificationMachine
                         //Conditions
                         where a.IsDeleted == false
                             && a.MachineId == (machineId == 0 ? a.MachineId : machineId)
                             && a.ProductionOrderNo == (string.IsNullOrWhiteSpace(productionOrderNo) ? a.ProductionOrderNo : productionOrderNo)
                             && a.CreatedUtc.AddHours(offset).Date >= DateFrom.Date
                             && a.CreatedUtc.AddHours(offset).Date <= DateTo.Date
                         select new MonitoringSpecificationMachineReportViewModel
                         {
                             CreatedUtc = a.CreatedUtc,
                             LastModifiedUtc = a.LastModifiedUtc,
                             DateTimeInput=a.DateTimeInput,
                             items = (from d in DbContext.MonitoringSpecificationMachineDetails
                                      where a.Id== d.MonitoringSpecificationMachineId&& d.IsDeleted == false
                                      select new ReportItem
                                      {
                                          indicator=d.Indicator,
                                          uom=d.Uom,
                                          value=d.Value
                                      }).ToList(),
                             cartNumber = a.CartNumber,
                             machine=a.MachineName,
                             orderNo=a.ProductionOrderNo
                         });
            return Query;
        }

        public Tuple<List<MonitoringSpecificationMachineReportViewModel>, int> GetReport(int machineId, string productionOrderNo, DateTime? dateFrom, DateTime? dateTo, int page, int size, string Order, int offset)
        {
            var Query = GetReportQuery(machineId, productionOrderNo, dateFrom, dateTo, offset);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Order);
            if (OrderDictionary.Count.Equals(0))
            {
                Query = Query.OrderByDescending(b => b.LastModifiedUtc);
            }

            Pageable<MonitoringSpecificationMachineReportViewModel> pageable = new Pageable<MonitoringSpecificationMachineReportViewModel>(Query, page - 1, size);
            List<MonitoringSpecificationMachineReportViewModel> Data = pageable.Data.ToList<MonitoringSpecificationMachineReportViewModel>();
            int TotalData = pageable.TotalCount;

            return Tuple.Create(Data, TotalData);
        }

        public MemoryStream GenerateExcel(int machineId, string productionOrderNo, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetReportQuery(machineId, productionOrderNo, dateFrom, dateTo, offset);
            Query = Query.OrderByDescending(b => b.LastModifiedUtc);
            DataTable result = new DataTable();
            //No	Unit	Budget	Kategori	Tanggal PR	Nomor PR	Kode Barang	Nama Barang	Jumlah	Satuan	Tanggal Diminta Datang	Status	Tanggal Diminta Datang Eksternal
            List<string> colCount = new List<string>();

            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Mesin", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Input", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jam Input", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Surat Order Produksi", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "No Kereta", DataType = typeof(String) });

            foreach(var a in Query)
            {
                foreach(var b in a.items)
                {
                    colCount.Add("");
                    result.Columns.Add(new DataColumn() { ColumnName = b.indicator +"("+b.uom+")", DataType = typeof(String) });
                }
                break;
            }

            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "", "", ""); // to allow column name to be generated properly for empty data as template
            else
            {
                int index = 0;
                foreach (var item in Query)
                {
                    index++;
                    List<string> value = new List<string>();
                    string date = item.DateTimeInput.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    
                    string time = item.DateTimeInput.ToOffset(new TimeSpan(offset, 0, 0)).ToString("hh:MM", new CultureInfo("id-ID"));
                    value.Add(index.ToString());
                    value.Add(item.machine);
                    value.Add(date);
                    value.Add(time);
                    value.Add(item.machine);
                    value.Add(item.cartNumber);
                    foreach (var val in item.items)
                    {
                        value.Add(val.value);
                    }
                    result.Rows.Add(value.ToArray() );
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Territory") }, true);
        }

        public ReadResponse<MonitoringSpecificationMachineModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateAsync(MonitoringSpecificationMachineModel model)
        {
            throw new NotImplementedException();
        }

        public Task<MonitoringSpecificationMachineModel> ReadByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(int id, MonitoringSpecificationMachineModel model)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

