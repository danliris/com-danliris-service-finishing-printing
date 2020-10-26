using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.DailyMonitoringEvent;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.DailyMonitoringEvent;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.DailyMonitoringEvent;
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
using System.IO;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DailyMonitoringEvent;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.DailyMonitoringEvent
{
    public class DailyMonitoringEventFacade : IDailyMonitoringEventFacade
    {
        private readonly ProductionDbContext DbContext;
        private readonly DbSet<DailyMonitoringEventModel> DbSet;
        private readonly DailyMonitoringEventLogic DailyMonitoringEventLogic;

        private const string LEGAL_LOSSES = "LEGAL LOSSES";
        private const string UNUTILISED_CAPACITY_LOSSES = "UNUTILISED CAPACITY LOSSES";
        private const string PROCESS_DRIVEN_LOSSES = "PROCESS DRIVEN LOSSES";
        private const string MANUFACTURING_PERFORMANCE_LOSSES = "MANUFACTURING PERFORMANCE LOSSES";
        private const string PLANNED_STOPPED_TIME = "PLANNED STOPPED TIME";

        public DailyMonitoringEventFacade(IServiceProvider serviceProvider, ProductionDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = dbContext.Set<DailyMonitoringEventModel>();
            DailyMonitoringEventLogic = serviceProvider.GetService<DailyMonitoringEventLogic>();
        }

        public Task<int> CreateAsync(DailyMonitoringEventModel model)
        {
            do
            {
                model.Code = CodeGenerator.Generate();
            }
            while (DbSet.Any(d => d.Code.Equals(model.Code)));
            DailyMonitoringEventLogic.CreateModel(model);
            return DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await DailyMonitoringEventLogic.DeleteModel(id);
            return await DbContext.SaveChangesAsync();
        }

        public MemoryStream GenerateExcel(DateTime? dateFrom, DateTime? dateTo, string area, int machineId, int offSet)
        {
            throw new NotImplementedException();
        }

        public List<DailyMonitoringEventReportViewModel> GetReport(DateTime? dateFrom, DateTime? dateTo, string area, int machineId, int offSet)
        {
            IQueryable<DailyMonitoringEventModel> query = DbSet.Include(s => s.DailyMonitoringEventLossEventItems).Include(s => s.DailyMonitoringEventProductionOrderItems);

            if (dateFrom.HasValue && dateTo.HasValue)
            {
                query = query.Where(s => dateFrom.GetValueOrDefault().Date <= s.Date.AddHours(offSet).Date.Date
                        && s.Date.AddHours(offSet).Date.Date <= dateTo.GetValueOrDefault().Date);
            }

            if (!string.IsNullOrEmpty(area))
            {
                query = query.Where(s => s.ProcessArea == area);
            }

            if (machineId != 0)
            {
                query = query.Where(s => s.MachineId == machineId);
            }

            var groupedData = query
                            .GroupBy(s => new { s.MachineId, s.ProcessArea });

            var result = query
                           .GroupBy(s => new { s.MachineId, s.ProcessArea })
                           .Select(s => new DailyMonitoringEventReportViewModel()
                           {
                               DesignSpeed = s.SelectMany(e => e.DailyMonitoringEventProductionOrderItems).Sum(e => e.Speed) / s.SelectMany(e => e.DailyMonitoringEventProductionOrderItems).Count(),
                               Output = s.First().MachineUseBQBS ? s.SelectMany(e => e.DailyMonitoringEventProductionOrderItems).Sum(e => e.Input_BQ + e.Output_BS) : s.SelectMany(e => e.DailyMonitoringEventProductionOrderItems).Sum(e => e.Output_BS),
                               TotalTime = s.GroupBy(e => new { e.Date, e.Shift }).Count() * 8,

                           }).ToList();


            var groupedLegalLosses = groupedData.ToList().Select(s => s.SelectMany(e => e.DailyMonitoringEventLossEventItems).Where(e => e.LossEventLosses.ToUpper() == LEGAL_LOSSES)).FirstOrDefault();
            var unUtilisedCapacityLosses = groupedData.ToList().Select(s => s.SelectMany(e => e.DailyMonitoringEventLossEventItems).Where(e => e.LossEventLosses.ToUpper() == UNUTILISED_CAPACITY_LOSSES)).FirstOrDefault();
            var processDrivenLosses = groupedData.ToList().Select(s => s.SelectMany(e => e.DailyMonitoringEventLossEventItems).Where(e => e.LossEventLosses.ToUpper() == PROCESS_DRIVEN_LOSSES)).FirstOrDefault();
            var manufacturingPerformanceLosses = groupedData.ToList().Select(s => s.SelectMany(e => e.DailyMonitoringEventLossEventItems).Where(e => e.LossEventLosses.ToUpper() == MANUFACTURING_PERFORMANCE_LOSSES)).FirstOrDefault();

            var reportData = result.FirstOrDefault();

            if (reportData != null)
            {
                var legalLossesMonitoringEvent = groupedLegalLosses.GroupBy(e => e.LossEventLossesCategory)
                                                .Select(e => new LegalLossesViewModel()
                                                {
                                                    LossEventCategory = e.Key,
                                                    Value = e.Sum(r => r.Time) / 60
                                                }).ToList();
                var unUtilisedCapacityLossesMonitoringEvent = unUtilisedCapacityLosses.GroupBy(e => e.LossEventLossesCategory)
                                                .Select(e => new UnUtilisedCapacityLossesViewModel()
                                                {
                                                    LossEventCategory = e.Key,
                                                    Value = e.Sum(r => r.Time) / 60
                                                }).ToList();
                var processDrivenLossesMonitoringEvent = processDrivenLosses.GroupBy(e => e.LossEventLossesCategory)
                                                .Select(e => new ProcessDrivenLossesViewModel()
                                                {
                                                    LossEventCategory = e.Key,
                                                    Value = e.Sum(r => r.Time) / 60
                                                }).ToList();
                var manufacturingPerformanceLossesMonitoringEvent = manufacturingPerformanceLosses.GroupBy(e => e.LossEventLossesCategory)
                                                .Select(e => new ManufacturingPerformanceLossesViewModel()
                                                {
                                                    LossEventCategory = e.Key,
                                                    Value = e.Sum(r => r.Time) / 60
                                                }).ToList();

                reportData.LegalLosses = DbContext.LossEventCategories.Where(s => s.LossEventProcessArea == area && s.LossEventLosses.ToUpper() == LEGAL_LOSSES)
                                            .Select(s => new LegalLossesViewModel()
                                            {
                                                LossEventCategory = s.LossesCategory
                                            }).ToList();

                foreach (var item in reportData.LegalLosses)
                {
                    var category = legalLossesMonitoringEvent.FirstOrDefault(s => s.LossEventCategory == item.LossEventCategory);
                    if (category != null)
                    {
                        item.Value = category.Value;
                    }
                }

                reportData.UnUtilisedCapacityLosses = DbContext.LossEventCategories.Where(s => s.LossEventProcessArea == area && s.LossEventLosses.ToUpper() == UNUTILISED_CAPACITY_LOSSES)
                                            .Select(s => new UnUtilisedCapacityLossesViewModel()
                                            {
                                                LossEventCategory = s.LossesCategory
                                            }).ToList();

                foreach (var item in reportData.UnUtilisedCapacityLosses)
                {
                    var category = unUtilisedCapacityLossesMonitoringEvent.FirstOrDefault(s => s.LossEventCategory == item.LossEventCategory);
                    if (category != null)
                    {
                        item.Value = category.Value;
                    }

                    if(item.LossEventCategory.ToUpper() == PLANNED_STOPPED_TIME)
                    {
                        var dates = new List<DateTime>();

                        for (var dt = dateFrom.GetValueOrDefault(); dt <= dateTo.GetValueOrDefault(); dt = dt.AddDays(1))
                        {
                            dates.Add(dt);
                        }

                        var stoppedHours = dates.Count(s => s.DayOfWeek == DayOfWeek.Saturday || s.DayOfWeek == DayOfWeek.Sunday) * 24;

                        item.Value = item.Value + stoppedHours;
                    }
                }

                reportData.ProcessDrivenLosses = DbContext.LossEventCategories.Where(s => s.LossEventProcessArea == area && s.LossEventLosses.ToUpper() == PROCESS_DRIVEN_LOSSES)
                                            .Select(s => new ProcessDrivenLossesViewModel()
                                            {
                                                LossEventCategory = s.LossesCategory
                                            }).ToList();

                foreach (var item in reportData.ProcessDrivenLosses)
                {
                    var category = processDrivenLossesMonitoringEvent.FirstOrDefault(s => s.LossEventCategory == item.LossEventCategory);
                    if (category != null)
                    {
                        item.Value = category.Value;
                    }
                }

                reportData.ManufacturingPerformanceLosses = DbContext.LossEventCategories.Where(s => s.LossEventProcessArea == area && s.LossEventLosses.ToUpper() == MANUFACTURING_PERFORMANCE_LOSSES)
                                            .Select(s => new ManufacturingPerformanceLossesViewModel()
                                            {
                                                LossEventCategory = s.LossesCategory
                                            }).ToList();

                foreach (var item in reportData.ManufacturingPerformanceLosses)
                {
                    var category = manufacturingPerformanceLossesMonitoringEvent.FirstOrDefault(s => s.LossEventCategory == item.LossEventCategory);
                    if (category != null)
                    {
                        item.Value = category.Value;
                    }
                }

                reportData.AvailableTime = reportData.TotalTime + reportData.LegalLosses.Sum(e => e.Value);
                reportData.AvailableLoadingTime = reportData.AvailableTime + reportData.UnUtilisedCapacityLosses.Sum(e => e.Value);
                reportData.ValueOperatingTime = reportData.Output / (reportData.DesignSpeed * 60);
                reportData.OperatingTime = reportData.ValueOperatingTime + reportData.ManufacturingPerformanceLosses.Sum(e => e.Value);
                reportData.LoadingTime = reportData.OperatingTime + reportData.ProcessDrivenLosses.Sum(e => e.Value);
                reportData.IdleTime = reportData.AvailableLoadingTime - reportData.LoadingTime;
                reportData.AssetUtilization = (reportData.ValueOperatingTime / reportData.TotalTime) * 100;
                reportData.OEEMMP = (reportData.ValueOperatingTime / reportData.LoadingTime) * 100;

            }

            return result;
        }

        public ReadResponse<DailyMonitoringEventModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<DailyMonitoringEventModel> query = DbSet;

            List<string> searchAttributes = new List<string>()
            {
                "ProcessArea", "MachineName"
            };
            query = QueryHelper<DailyMonitoringEventModel>.Search(query, searchAttributes, keyword);

            Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<DailyMonitoringEventModel>.Filter(query, filterDictionary);


            Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<DailyMonitoringEventModel>.Order(query, orderDictionary);

            Pageable<DailyMonitoringEventModel> pageable = new Pageable<DailyMonitoringEventModel>(query, page - 1, size);
            List<DailyMonitoringEventModel> data = pageable.Data.ToList();
            int totalData = pageable.TotalCount;

            return new ReadResponse<DailyMonitoringEventModel>(data, totalData, orderDictionary, new List<string>());
        }

        public Task<DailyMonitoringEventModel> ReadByIdAsync(int id)
        {
            return DailyMonitoringEventLogic.ReadModelById(id);
        }

        public async Task<int> UpdateAsync(int id, DailyMonitoringEventModel model)
        {
            await DailyMonitoringEventLogic.UpdateModelAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }
    }
}
