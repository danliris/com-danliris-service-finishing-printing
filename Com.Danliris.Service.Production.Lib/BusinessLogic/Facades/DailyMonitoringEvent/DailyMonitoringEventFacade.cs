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
using OfficeOpenXml;
using OfficeOpenXml.Style;

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
            var data = GetReport(dateFrom, dateTo, area, machineId, offSet);

            var memoryStream = new MemoryStream();

            using (ExcelPackage package = new ExcelPackage(memoryStream))
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet 1");

                int x = 2;
                worksheet.Cells[x, 2].Value = "Machine";
                worksheet.Cells[x, 3].Value = "AVAILABLE TIME";
                worksheet.Cells[x, 4].Value = "AVAILABLE LOADING TIME";
                worksheet.Cells[x, 5].Value = "OPERATING TIME";
                worksheet.Cells[x, 6].Value = "VALUE OPERATING TIME";
                worksheet.Cells[x, 7].Value = "IDLE TIME";
                worksheet.Cells[x, 8].Value = "Asset Utilization";
                worksheet.Cells[x, 9].Value = "Overall Equipment Efficiency MMP";

                worksheet.Cells[x, 2, x, 9].Style.Font.Bold = true;


                x++;
                foreach (var item in data)
                {
                    worksheet.Cells[x, 2].Value = item.MachineName;
                    worksheet.Cells[x, 2].Style.Font.Bold = true;
                    worksheet.Cells[x, 3].Value = Convert.ToDecimal(item.AvailableTime.ToString("F2"));
                    worksheet.Cells[x, 4].Value = Convert.ToDecimal(item.AvailableLoadingTime.ToString("F2"));
                    worksheet.Cells[x, 5].Value = Convert.ToDecimal(item.OperatingTime.ToString("F2"));
                    worksheet.Cells[x, 6].Value = Convert.ToDecimal(item.ValueOperatingTime.ToString("F2"));
                    worksheet.Cells[x, 7].Value = Convert.ToDecimal(item.IdleTime.ToString("F2"));
                    worksheet.Cells[x, 8].Value = Convert.ToDecimal(item.AssetUtilization.ToString("F2"));
                    worksheet.Cells[x, 9].Value = Convert.ToDecimal(item.OEEMMP.ToString("F2"));
                    x++;
                }

                worksheet.Cells[2, 2, 3, 9].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                worksheet.Cells[2, 2, 3, 9].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                worksheet.Cells[2, 2, 3, 9].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                worksheet.Cells[2, 2, 3, 9].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                x++;
                var monitoring = data.FirstOrDefault();
                worksheet.Cells[x, 6].Value = monitoring?.MachineName;
                worksheet.Cells[x, 6].Style.Font.Bold = true;
                worksheet.Cells[x, 6, x, 7].Merge = true;
                worksheet.Cells[x, 6, x, 7].Style.WrapText = true;
                worksheet.Cells[x, 6, x, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                x++;
                worksheet.Cells[x, 4].Value = "Input";
                worksheet.Cells[x, 4].Style.Font.Bold = true;
                worksheet.Cells[x, 4, x, 5].Merge = true;
                worksheet.Cells[x, 4, x, 5].Style.WrapText = true;
                worksheet.Cells[x, 4, x, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                worksheet.Cells[x, 6].Value = Convert.ToDecimal(monitoring?.Input.ToString("F2"));
                worksheet.Cells[x, 7].Value = Convert.ToDecimal(monitoring?.DesignSpeed.ToString("F2")) + " mtr/menit";
                worksheet.Cells[x, 6, x, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                x++;
                worksheet.Cells[x, 4].Value = "Output";
                worksheet.Cells[x, 4].Style.Font.Bold = true;
                worksheet.Cells[x, 4, x, 5].Merge = true;
                worksheet.Cells[x, 4, x, 5].Style.WrapText = true;
                worksheet.Cells[x, 4, x, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                worksheet.Cells[x, 6].Value = Convert.ToDecimal(monitoring?.Output.ToString("F2"));
                worksheet.Cells[x, 7].Value = "";
                worksheet.Cells[x, 6, x, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                x++;
                worksheet.Cells[x, 4].Value = "";
                worksheet.Cells[x, 4].Style.Font.Bold = true;
                worksheet.Cells[x, 4, x, 5].Merge = true;
                worksheet.Cells[x, 4, x, 5].Style.WrapText = true;
                worksheet.Cells[x, 4, x, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                worksheet.Cells[x, 6].Value = "";
                worksheet.Cells[x, 7].Value = "";

                x++;
                worksheet.Cells[x, 4].Value = "Total Loss Time";
                worksheet.Cells[x, 4].Style.Font.Bold = true;
                worksheet.Cells[x, 4, x, 5].Merge = true;
                worksheet.Cells[x, 4, x, 5].Style.WrapText = true;
                worksheet.Cells[x, 4, x, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                worksheet.Cells[x, 6].Value = Convert.ToDecimal(monitoring?.TotalTime.ToString("F2")) + " Jam";
                worksheet.Cells[x, 7].Value = "";
                worksheet.Cells[x, 6, x, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                worksheet.Cells[5, 6, 9, 7].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                worksheet.Cells[5, 6, 9, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                worksheet.Cells[5, 6, 9, 7].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                worksheet.Cells[5, 6, 9, 7].Style.Border.Right.Style = ExcelBorderStyle.Medium;

                x++;

                int start = x;
                int lossIndex = x;
                if (monitoring != null)
                {
                    var legalLosses = monitoring.LegalLossesExcel.FirstOrDefault();
                    var legalRemarkCount = monitoring.LegalLossesExcel.Count;
                    if (legalLosses != null)
                    {
                        worksheet.Cells[lossIndex, 2].Value = legalLosses.Losses;
                        worksheet.Cells[lossIndex, 2].Style.Font.Bold = true;
                        worksheet.Cells[lossIndex, 2, lossIndex + legalRemarkCount - 1, 2].Merge = true;
                        worksheet.Cells[lossIndex, 2, lossIndex + legalRemarkCount - 1, 2].Style.WrapText = true;
                        worksheet.Cells[lossIndex, 2, lossIndex + legalRemarkCount - 1, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[lossIndex, 2, lossIndex + legalRemarkCount - 1, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        var category = monitoring.LegalLossesExcel.GroupBy(s => s.LossesCategory);

                        int legalIndex = lossIndex;
                        foreach (var item in category)
                        {
                            worksheet.Cells[legalIndex, 3].Value = item.Key;
                            worksheet.Cells[legalIndex, 3, legalIndex + item.Count() - 1, 3].Merge = true;
                            worksheet.Cells[legalIndex, 3, legalIndex + item.Count() - 1, 3].Style.WrapText = true;
                            worksheet.Cells[legalIndex, 3, legalIndex + item.Count() - 1, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            worksheet.Cells[legalIndex, 3, legalIndex + item.Count() - 1, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                            var totalTime = item.Sum(s => s.Time);
                            if (totalTime == 0)
                            {
                                worksheet.Cells[legalIndex, 7].Value = "-";
                            }
                            else
                            {
                                worksheet.Cells[legalIndex, 7].Value = Convert.ToDecimal(totalTime.ToString("F2"));
                            }
                            worksheet.Cells[legalIndex, 7, legalIndex + item.Count() - 1, 7].Merge = true;
                            worksheet.Cells[legalIndex, 7, legalIndex + item.Count() - 1, 7].Style.WrapText = true;
                            worksheet.Cells[legalIndex, 7, legalIndex + item.Count() - 1, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            worksheet.Cells[legalIndex, 7, legalIndex + item.Count() - 1, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                            legalIndex += item.Count();
                        }

                        foreach (var item in monitoring.LegalLossesExcel)
                        {
                            worksheet.Cells[lossIndex, 4].Value = item.LossesRemarkCode;
                            worksheet.Cells[lossIndex, 5].Value = item.LossesRemarkRemark;


                            if (item.Time == 0)
                            {

                                worksheet.Cells[lossIndex, 6].Value = "-";
                                worksheet.Cells[lossIndex, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            }
                            else
                            {
                                worksheet.Cells[lossIndex, 6].Value = Convert.ToDecimal(item.Time.ToString("F2"));
                            }
                            lossIndex++;
                        }

                    }

                    var unUtilisedLosses = monitoring.UnUtilisedCapacityLossesExcel.FirstOrDefault();
                    var unUtilisedRemarkCount = monitoring.UnUtilisedCapacityLossesExcel.Count;
                    if (unUtilisedLosses != null)
                    {
                        worksheet.Cells[lossIndex, 2].Value = unUtilisedLosses.Losses;
                        worksheet.Cells[lossIndex, 2].Style.Font.Bold = true;
                        worksheet.Cells[lossIndex, 2, lossIndex + unUtilisedRemarkCount - 1, 2].Merge = true;
                        worksheet.Cells[lossIndex, 2, lossIndex + unUtilisedRemarkCount - 1, 2].Style.WrapText = true;
                        worksheet.Cells[lossIndex, 2, lossIndex + unUtilisedRemarkCount - 1, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[lossIndex, 2, lossIndex + unUtilisedRemarkCount - 1, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        var category = monitoring.UnUtilisedCapacityLossesExcel.GroupBy(s => s.LossesCategory);

                        int unUtilisedIndex = lossIndex;
                        foreach (var item in category)
                        {
                            worksheet.Cells[unUtilisedIndex, 3].Value = item.Key;
                            worksheet.Cells[unUtilisedIndex, 3, unUtilisedIndex + item.Count() - 1, 3].Merge = true;
                            worksheet.Cells[unUtilisedIndex, 3, unUtilisedIndex + item.Count() - 1, 3].Style.WrapText = true;
                            worksheet.Cells[unUtilisedIndex, 3, unUtilisedIndex + item.Count() - 1, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            worksheet.Cells[unUtilisedIndex, 3, unUtilisedIndex + item.Count() - 1, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                            var totalTime = item.Sum(s => s.Time);
                            if (totalTime == 0)
                            {
                                worksheet.Cells[unUtilisedIndex, 7].Value = "-";
                            }
                            else
                            {
                                worksheet.Cells[unUtilisedIndex, 7].Value = Convert.ToDecimal(totalTime.ToString("F2"));
                            }

                            worksheet.Cells[unUtilisedIndex, 7, unUtilisedIndex + item.Count() - 1, 7].Merge = true;
                            worksheet.Cells[unUtilisedIndex, 7, unUtilisedIndex + item.Count() - 1, 7].Style.WrapText = true;
                            worksheet.Cells[unUtilisedIndex, 7, unUtilisedIndex + item.Count() - 1, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            worksheet.Cells[unUtilisedIndex, 7, unUtilisedIndex + item.Count() - 1, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                            unUtilisedIndex += item.Count();
                        }

                        foreach (var item in monitoring.UnUtilisedCapacityLossesExcel)
                        {
                            worksheet.Cells[lossIndex, 4].Value = item.LossesRemarkCode;
                            worksheet.Cells[lossIndex, 5].Value = item.LossesRemarkRemark;


                            if (item.Time == 0)
                            {

                                worksheet.Cells[lossIndex, 6].Value = "-";
                                worksheet.Cells[lossIndex, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            }
                            else
                            {
                                worksheet.Cells[lossIndex, 6].Value = Convert.ToDecimal(item.Time.ToString("F2"));
                            }
                            lossIndex++;
                        }

                    }


                    var processLosses = monitoring.ProcessDrivenLossesExcel.FirstOrDefault();
                    var processRemarkCount = monitoring.ProcessDrivenLossesExcel.Count;
                    if (processLosses != null)
                    {
                        worksheet.Cells[lossIndex, 2].Value = processLosses.Losses;
                        worksheet.Cells[lossIndex, 2].Style.Font.Bold = true;
                        worksheet.Cells[lossIndex, 2, lossIndex + processRemarkCount - 1, 2].Merge = true;
                        worksheet.Cells[lossIndex, 2, lossIndex + processRemarkCount - 1, 2].Style.WrapText = true;
                        worksheet.Cells[lossIndex, 2, lossIndex + processRemarkCount - 1, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[lossIndex, 2, lossIndex + processRemarkCount - 1, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        var category = monitoring.ProcessDrivenLossesExcel.GroupBy(s => s.LossesCategory);

                        int processIndex = lossIndex;
                        foreach (var item in category)
                        {
                            worksheet.Cells[processIndex, 3].Value = item.Key;
                            worksheet.Cells[processIndex, 3, processIndex + item.Count() - 1, 3].Merge = true;
                            worksheet.Cells[processIndex, 3, processIndex + item.Count() - 1, 3].Style.WrapText = true;
                            worksheet.Cells[processIndex, 3, processIndex + item.Count() - 1, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            worksheet.Cells[processIndex, 3, processIndex + item.Count() - 1, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                            var totalTime = item.Sum(s => s.Time);
                            if (totalTime == 0)
                            {
                                worksheet.Cells[processIndex, 7].Value = "-";
                            }
                            else
                            {
                                worksheet.Cells[processIndex, 7].Value = Convert.ToDecimal(totalTime.ToString("F2"));
                            }

                            worksheet.Cells[processIndex, 7, processIndex + item.Count() - 1, 7].Merge = true;
                            worksheet.Cells[processIndex, 7, processIndex + item.Count() - 1, 7].Style.WrapText = true;
                            worksheet.Cells[processIndex, 7, processIndex + item.Count() - 1, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            worksheet.Cells[processIndex, 7, processIndex + item.Count() - 1, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                            processIndex += item.Count();
                        }

                        foreach (var item in monitoring.ProcessDrivenLossesExcel)
                        {
                            worksheet.Cells[lossIndex, 4].Value = item.LossesRemarkCode;
                            worksheet.Cells[lossIndex, 5].Value = item.LossesRemarkRemark;


                            if (item.Time == 0)
                            {

                                worksheet.Cells[lossIndex, 6].Value = "-";
                                worksheet.Cells[lossIndex, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            }
                            else
                            {
                                worksheet.Cells[lossIndex, 6].Value = Convert.ToDecimal(item.Time.ToString("F2"));
                            }
                            lossIndex++;
                        }

                    }

                    var manufacturingLosses = monitoring.ManufacturingPerformanceLossesExcel.FirstOrDefault();
                    var manufacturingRemarkCount = monitoring.ManufacturingPerformanceLossesExcel.Count;
                    if (manufacturingLosses != null)
                    {
                        worksheet.Cells[lossIndex, 2].Value = manufacturingLosses.Losses;
                        worksheet.Cells[lossIndex, 2].Style.Font.Bold = true;
                        worksheet.Cells[lossIndex, 2, lossIndex + manufacturingRemarkCount - 1, 2].Merge = true;
                        worksheet.Cells[lossIndex, 2, lossIndex + manufacturingRemarkCount - 1, 2].Style.WrapText = true;
                        worksheet.Cells[lossIndex, 2, lossIndex + manufacturingRemarkCount - 1, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[lossIndex, 2, lossIndex + manufacturingRemarkCount - 1, 2].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                        var category = monitoring.ManufacturingPerformanceLossesExcel.GroupBy(s => s.LossesCategory);

                        int manufacturingIndex = lossIndex;
                        foreach (var item in category)
                        {
                            worksheet.Cells[manufacturingIndex, 3].Value = item.Key;
                            worksheet.Cells[manufacturingIndex, 3, manufacturingIndex + item.Count() - 1, 3].Merge = true;
                            worksheet.Cells[manufacturingIndex, 3, manufacturingIndex + item.Count() - 1, 3].Style.WrapText = true;
                            worksheet.Cells[manufacturingIndex, 3, manufacturingIndex + item.Count() - 1, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            worksheet.Cells[manufacturingIndex, 3, manufacturingIndex + item.Count() - 1, 3].Style.VerticalAlignment = ExcelVerticalAlignment.Center;


                            var totalTime = item.Sum(s => s.Time);
                            if (totalTime == 0)
                            {
                                worksheet.Cells[manufacturingIndex, 7].Value = "-";
                            }
                            else
                            {
                                worksheet.Cells[manufacturingIndex, 7].Value = Convert.ToDecimal(totalTime.ToString("F2"));
                            }

                            worksheet.Cells[manufacturingIndex, 7, manufacturingIndex + item.Count() - 1, 7].Merge = true;
                            worksheet.Cells[manufacturingIndex, 7, manufacturingIndex + item.Count() - 1, 7].Style.WrapText = true;
                            worksheet.Cells[manufacturingIndex, 7, manufacturingIndex + item.Count() - 1, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            worksheet.Cells[manufacturingIndex, 7, manufacturingIndex + item.Count() - 1, 7].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                            manufacturingIndex += item.Count();
                        }

                        foreach (var item in monitoring.ManufacturingPerformanceLossesExcel)
                        {
                            worksheet.Cells[lossIndex, 4].Value = item.LossesRemarkCode;
                            worksheet.Cells[lossIndex, 5].Value = item.LossesRemarkRemark;


                            if (item.Time == 0)
                            {

                                worksheet.Cells[lossIndex, 6].Value = "-";
                                worksheet.Cells[lossIndex, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            }
                            else
                            {
                                worksheet.Cells[lossIndex, 6].Value = Convert.ToDecimal(item.Time.ToString("F2"));
                            }
                            lossIndex++;
                        }

                    }

                    if(lossIndex > start)
                    {
                        worksheet.Cells[start, 2, lossIndex - 1, 7].Style.Border.Top.Style = ExcelBorderStyle.Medium;
                        worksheet.Cells[start, 2, lossIndex - 1, 7].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                        worksheet.Cells[start, 2, lossIndex - 1, 7].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                        worksheet.Cells[start, 2, lossIndex - 1, 7].Style.Border.Right.Style = ExcelBorderStyle.Medium;
                    }
                    
                }

                
                worksheet.Cells[worksheet.Dimension.Address].Style.Font.Size = 12;
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                worksheet.Column(2).Width = 20;
                worksheet.Column(3).Width = 40;
                package.Save();
            }
            memoryStream.Position = 0;
            return memoryStream;
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
                               Output = s.SelectMany(e => e.DailyMonitoringEventProductionOrderItems).Sum(e => e.Output_BS),
                               Input = s.SelectMany(e => e.DailyMonitoringEventProductionOrderItems).Sum(e => e.Input_BQ),
                               TotalTime = s.GroupBy(e => new { e.Date, e.Shift }).Count() * 8,
                               ProcessArea = s.Key.ProcessArea,
                               MachineName = s.First().MachineName
                           }).ToList();


            var groupedLegalLosses = groupedData.ToList().Select(s => s.SelectMany(e => e.DailyMonitoringEventLossEventItems).Where(e => e.LossEventLosses.ToUpper() == LEGAL_LOSSES)).FirstOrDefault();
            var groupedUnUtilisedCapacityLosses = groupedData.ToList().Select(s => s.SelectMany(e => e.DailyMonitoringEventLossEventItems).Where(e => e.LossEventLosses.ToUpper() == UNUTILISED_CAPACITY_LOSSES)).FirstOrDefault();
            var groupedProcessDrivenLosses = groupedData.ToList().Select(s => s.SelectMany(e => e.DailyMonitoringEventLossEventItems).Where(e => e.LossEventLosses.ToUpper() == PROCESS_DRIVEN_LOSSES)).FirstOrDefault();
            var groupedManufacturingPerformanceLosses = groupedData.ToList().Select(s => s.SelectMany(e => e.DailyMonitoringEventLossEventItems).Where(e => e.LossEventLosses.ToUpper() == MANUFACTURING_PERFORMANCE_LOSSES)).FirstOrDefault();

            var reportData = result.FirstOrDefault();

            if (reportData != null)
            {
                var legalLossesMonitoringEvent = groupedLegalLosses.GroupBy(e => e.LossEventLossesCategory)
                                                .Select(e => new LegalLossesViewModel()
                                                {
                                                    LossEventCategory = e.Key,
                                                    Value = e.Sum(r => r.Time) / 60
                                                }).ToList();
                var unUtilisedCapacityLossesMonitoringEvent = groupedUnUtilisedCapacityLosses.GroupBy(e => e.LossEventLossesCategory)
                                                .Select(e => new UnUtilisedCapacityLossesViewModel()
                                                {
                                                    LossEventCategory = e.Key,
                                                    Value = e.Sum(r => r.Time) / 60
                                                }).ToList();
                var processDrivenLossesMonitoringEvent = groupedProcessDrivenLosses.GroupBy(e => e.LossEventLossesCategory)
                                                .Select(e => new ProcessDrivenLossesViewModel()
                                                {
                                                    LossEventCategory = e.Key,
                                                    Value = e.Sum(r => r.Time) / 60
                                                }).ToList();
                var manufacturingPerformanceLossesMonitoringEvent = groupedManufacturingPerformanceLosses.GroupBy(e => e.LossEventLossesCategory)
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

                    if (item.LossEventCategory.ToUpper() == PLANNED_STOPPED_TIME)
                    {
                        var dates = new List<DateTime>();

                        for (var dt = dateFrom.GetValueOrDefault(); dt <= dateTo.GetValueOrDefault(); dt = dt.AddDays(1))
                        {
                            dates.Add(dt);
                        }

                        var stoppedHours = dates.Count(s => s.DayOfWeek == DayOfWeek.Saturday || s.DayOfWeek == DayOfWeek.Sunday) * 24;

                        item.Value += stoppedHours;
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


                reportData.LegalLossesExcel = DbContext.LossEventRemarks.Where(s => s.LossEventProcessArea == area && s.LossEventLosses.ToUpper() == LEGAL_LOSSES)
                                            .Select(s => new LegalLossesExcelViewModel()
                                            {
                                                Losses = s.LossEventLosses,
                                                LossesCategory = s.LossEventCategoryLossesCategory,
                                                LossesRemarkCode = s.ProductionLossCode,
                                                LossesRemarkRemark = s.Remark
                                            }).ToList();

                foreach (var item in reportData.LegalLossesExcel)
                {
                    var remark = groupedLegalLosses.FirstOrDefault(s => s.LossEventProductionLossCode == item.LossesRemarkCode);
                    if (remark != null)
                    {
                        item.Time = remark.Time / 60;
                    }
                }

                reportData.UnUtilisedCapacityLossesExcel = DbContext.LossEventRemarks.Where(s => s.LossEventProcessArea == area && s.LossEventLosses.ToUpper() == UNUTILISED_CAPACITY_LOSSES)
                                            .Select(s => new UnUtilisedCapacityLossesExcelViewModel()
                                            {
                                                Losses = s.LossEventLosses,
                                                LossesCategory = s.LossEventCategoryLossesCategory,
                                                LossesRemarkCode = s.ProductionLossCode,
                                                LossesRemarkRemark = s.Remark
                                            }).ToList();

                foreach (var item in reportData.UnUtilisedCapacityLossesExcel)
                {
                    var remark = groupedUnUtilisedCapacityLosses.FirstOrDefault(s => s.LossEventProductionLossCode == item.LossesRemarkCode);
                    if (remark != null)
                    {
                        item.Time = remark.Time / 60;
                    }
                }

                reportData.ProcessDrivenLossesExcel = DbContext.LossEventRemarks.Where(s => s.LossEventProcessArea == area && s.LossEventLosses.ToUpper() == PROCESS_DRIVEN_LOSSES)
                                            .Select(s => new ProcessDrivenLossesExcelViewModel()
                                            {
                                                Losses = s.LossEventLosses,
                                                LossesCategory = s.LossEventCategoryLossesCategory,
                                                LossesRemarkCode = s.ProductionLossCode,
                                                LossesRemarkRemark = s.Remark
                                            }).ToList();

                foreach (var item in reportData.ProcessDrivenLossesExcel)
                {
                    var remark = groupedProcessDrivenLosses.FirstOrDefault(s => s.LossEventProductionLossCode == item.LossesRemarkCode);
                    if (remark != null)
                    {
                        item.Time = remark.Time / 60;
                    }
                }

                reportData.ManufacturingPerformanceLossesExcel = DbContext.LossEventRemarks.Where(s => s.LossEventProcessArea == area && s.LossEventLosses.ToUpper() == MANUFACTURING_PERFORMANCE_LOSSES)
                                            .Select(s => new ManufacturingPerformanceLossesExcelViewModel()
                                            {
                                                Losses = s.LossEventLosses,
                                                LossesCategory = s.LossEventCategoryLossesCategory,
                                                LossesRemarkCode = s.ProductionLossCode,
                                                LossesRemarkRemark = s.Remark
                                            }).ToList();

                foreach (var item in reportData.ManufacturingPerformanceLossesExcel)
                {
                    var remark = groupedManufacturingPerformanceLosses.FirstOrDefault(s => s.LossEventProductionLossCode == item.LossesRemarkCode);
                    if (remark != null)
                    {
                        item.Time = remark.Time / 60;
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
