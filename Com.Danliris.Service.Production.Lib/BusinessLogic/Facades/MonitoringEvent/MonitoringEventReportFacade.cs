using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.MonitoringEvent;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.MonitoringEvent;
using Com.Danliris.Service.Finishing.Printing.Lib.Helpers;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Monitoring_Event;
using Com.Danliris.Service.Finishing.Printing.Lib.Utilities;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Monitoring_Event;
using Com.Moonlay.NetCore.Lib;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.Machine;
using Com.Danliris.Service.Production.Lib.Utilities;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Monitoring_Specification_Machine;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.MonitoringEvent
{
    public class MonitoringEventReportFacade : IMonitoringEventReportFacade
    {
        private readonly ProductionDbContext DbContext;
        private readonly DbSet<MonitoringEventModel> DbSet;
        private readonly DbSet<MachineEventsModel> dbset;
        private readonly DbSet<MonitoringSpecificationMachineModel> dBset;
        private IdentityService IdentityService;
        private MonitoringEventLogic MonitoringEventLogic;
        public MonitoringEventReportFacade(IServiceProvider serviceProvider, ProductionDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = this.DbContext.Set<MonitoringEventModel>();
            this.dbset = this.DbContext.Set<MachineEventsModel>();
            this.dBset = this.DbContext.Set<MonitoringSpecificationMachineModel>();
            this.IdentityService = serviceProvider.GetService<IdentityService>();
            this.MonitoringEventLogic = serviceProvider.GetService<MonitoringEventLogic>();
        }
        public IQueryable<MonitoringEventReportViewModel> GetReportQuery(string machineId, string machineEventId, string productionOrderOrderNo, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            DateTime DateFrom = dateFrom == null ? new DateTime(1970, 1, 1) : (DateTime)dateFrom;
            DateTime DateTo = dateTo == null ? DateTime.Now : (DateTime)dateTo;
            var machineId_Int = Convert.ToInt32(machineId);
            var machineEventId_Int = Convert.ToInt32(machineEventId);
            var Query = (from a in DbContext.MonitoringEvent
                             //Conditions
                         where a.IsDeleted == false
                             && a.MachineId == (string.IsNullOrWhiteSpace(machineId) ? a.MachineId : machineId_Int)
                             && a.MachineEventId == (string.IsNullOrWhiteSpace(machineEventId) ? a.MachineEventId : machineEventId_Int)
                             && a.ProductionOrderOrderNo == (string.IsNullOrWhiteSpace(productionOrderOrderNo) ? a.ProductionOrderOrderNo : productionOrderOrderNo)
                             && a.DateStart.AddHours(offset).Date >= DateFrom.Date
                             && a.DateStart.AddHours(offset).Date <= DateTo.Date
                         select new MonitoringEventReportViewModel
                         {
                             CreatedUtc = a.CreatedUtc,
                             code = a.Code,
                             dateStart = a.DateStart,
                             dateEnd = a.DateEnd,
                             timeInMilisStart = a.TimeInMilisStart,
                             timeInMilisEnd = a.TimeInMilisEnd,
                             cartNumber = a.CartNumber,
                             remark = a.Remark,
                             machineId = a.MachineId,
                             machineName = a.MachineName,
                             machineEventId = a.MachineEventId,
                             machineEventName = a.MachineEventName,
                             machineEventCode = a.MachineEventCode,
                             machineEventCategory = a.MachineEventCategory,
                             productionOrderId = a.ProductionOrderId,
                             productionOrderOrderNo = a.ProductionOrderOrderNo,
                             productionOrderDeliveryDate = a.ProductionOrderDeliveryDate,
                             productionOrderDetailCode = a.ProductionOrderDetailCode,
                             productionOrderDetailColorRequest = a.ProductionOrderDetailColorRequest,
                             productionOrderDetailColorTemplate = a.ProductionOrderDetailColorTemplate,
                             productionOrderDetailColorType = a.ProductionOrderDetailColorType,
                             productionOrderDetailColorTypeId = a.ProductionOrderDetailColorTypeId,
                             LastModifiedUtc = a.LastModifiedUtc
                         });
            return Query;
        }

        public Tuple<List<MonitoringEventReportViewModel>, int> GetReport(string machineId, string machineEventId, string productionOrderOrderNo, DateTime? dateFrom, DateTime? dateTo, int page, int size, string Order, int offset)
        {
            var Query = GetReportQuery(machineId, machineEventId, productionOrderOrderNo, dateFrom, dateTo, offset);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Order);
            if (OrderDictionary.Count.Equals(0))
            {
                Query = Query.OrderByDescending(b => b.LastModifiedUtc);
            }

            Pageable<MonitoringEventReportViewModel> pageable = new Pageable<MonitoringEventReportViewModel>(Query, page - 1, size);
            List<MonitoringEventReportViewModel> Data = pageable.Data.ToList<MonitoringEventReportViewModel>();
            int TotalData = pageable.TotalCount;

            return Tuple.Create(Data, TotalData);
        }

        public MemoryStream GenerateExcel(string machineId, string machineEventId, string productionOrderOrderNo, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            var Query = GetReportQuery(machineId, machineEventId, productionOrderOrderNo, dateFrom, dateTo, offset);
            Query = Query.OrderByDescending(b => b.LastModifiedUtc);
            DataTable result = new DataTable();
            //No	Unit	Budget	Kategori	Tanggal PR	Nomor PR	Kode Barang	Nama Barang	Jumlah	Satuan	Tanggal Diminta Datang	Status	Tanggal Diminta Datang Eksternal


            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Mesin", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nomor Order Produksi", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Warna", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Mulai", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jam Mulai", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal Selesai", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jam Selesai", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nomor Kereta", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Event", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Keterangan", DataType = typeof(String) });
            
            if (Query.ToArray().Count() == 0)
                result.Rows.Add("", "", "", "", "", "", "", "", "", "", ""); // to allow column name to be generated properly for empty data as template
            else
            {
                int index = 0;
                foreach (var item in Query)
                {
                    index++;
                    string dateStart = item.dateStart == null ? "-" : item.dateStart.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    string dateEnd = item.dateEnd == null ? "-" : item.dateEnd.ToOffset(new TimeSpan(offset, 0, 0)).ToString("dd MMM yyyy", new CultureInfo("id-ID"));
                    string timeStart = item.dateStart == null ? "-" : item.dateStart.ToOffset(new TimeSpan(offset, 0, 0)).ToString("HH:mm", new CultureInfo("id-ID"));
                    string timeEnd = item.dateEnd == null ? "-" : item.dateEnd.ToOffset(new TimeSpan(offset, 0, 0)).ToString("HH:mm", new CultureInfo("id-ID"));
                    //DateTimeOffset dateStart = item.dateStart ?? new DateTime(1970, 1, 1);
                    //DateTimeOffset dateEnd = item.dateEnd ?? new DateTime(1970, 1, 1);
                    result.Rows.Add(index, item.machineName, item.productionOrderOrderNo, item.productionOrderDetailColorRequest, dateStart, timeStart, dateEnd, timeEnd, 
                        item.cartNumber, item.machineEventName, item.remark);
                }
            }

            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Territory") }, true);
        }

        public List<MachineEventsModel> ReadByMachine(string Keyword, int machineId)
        {
            IQueryable<MachineEventsModel> Query = this.dbset;
            //var MachineId = Convert.ToInt32(machineId);
            List<string> searchAttributes = new List<string>()
            {
                "Name"
            };

            Query = QueryHelper<MachineEventsModel>.Search(Query, searchAttributes, Keyword); // kalo search setelah Select dengan .Where setelahnya maka case sensitive, kalo tanpa .Where tidak masalah

            Query = Query
                .Where(m => m.IsDeleted == false && m.MachineId == machineId)
                .Select(s => new MachineEventsModel
                {
                    Id = s.Id,
                    Code = s.Code,
                    Name = s.Name,
                    No = s.No,
                    MachineId = s.MachineId,
                    LastModifiedUtc = s.LastModifiedUtc,
                });

            //Dictionary<string, string> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Filter);
            //Query = QueryHelper<DeliveryOrder>.ConfigureFilter(Query, FilterDictionary);

            return Query.ToList();
        }

        public MonitoringSpecificationMachineModel ReadMonitoringSpecMachine(int id, string productionOrderNumber, DateTime dateTimeInput)
         {
            var Result = dBset.Where(m => m.MachineId == id && m.ProductionOrderNo == productionOrderNumber && m.DateTimeInput <= dateTimeInput)
                .Include(m => m.Details)
                .OrderByDescending(m => m.DateTimeInput)
                .FirstOrDefault();

            return Result;
        }

        public ReadResponse<MonitoringEventModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            throw new NotImplementedException();
        }

        public Task<int> CreateAsync(MonitoringEventModel model)
        {
            throw new NotImplementedException();
        }

        public Task<MonitoringEventModel> ReadByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(int id, MonitoringEventModel model)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
