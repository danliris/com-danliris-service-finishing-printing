using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DailyMonitoringEvent
{
    public class DailyMonitoringEventReportViewModel
    {
        public DailyMonitoringEventReportViewModel()
        {
            LegalLosses = new HashSet<LegalLossesViewModel>();
            UnUtilisedCapacityLosses = new HashSet<UnUtilisedCapacityLossesViewModel>();
            ProcessDrivenLosses = new HashSet<ProcessDrivenLossesViewModel>();
            ManufacturingPerformanceLosses = new HashSet<ManufacturingPerformanceLossesViewModel>();
            LegalLossesExcel = new HashSet<LegalLossesExcelViewModel>();
            UnUtilisedCapacityLossesExcel = new HashSet<UnUtilisedCapacityLossesExcelViewModel>();
            ProcessDrivenLossesExcel = new HashSet<ProcessDrivenLossesExcelViewModel>();
            ManufacturingPerformanceLossesExcel = new HashSet<ManufacturingPerformanceLossesExcelViewModel>();
        }

        public string ProcessArea { get; set; }
        public string MachineName { get; set; }
        public double DesignSpeed { get; set; }
        public double Input { get; set; }
        public double Output { get; set; }
        public double TotalTime { get; set; }
        public double AvailableTime { get; set; }
        public double AvailableLoadingTime { get; set; }
        public double LoadingTime { get; set; }
        public double OperatingTime { get; set; }
        public double ValueOperatingTime { get; set; }
        public double IdleTime { get; set; }
        public double AssetUtilization { get; set; }
        public double OEEMMP { get; set; }

        public ICollection<LegalLossesViewModel> LegalLosses { get; set; }

        public ICollection<UnUtilisedCapacityLossesViewModel> UnUtilisedCapacityLosses { get; set; }

        public ICollection<ProcessDrivenLossesViewModel> ProcessDrivenLosses { get; set; }

        public ICollection<ManufacturingPerformanceLossesViewModel> ManufacturingPerformanceLosses { get; set; }

        public ICollection<LegalLossesExcelViewModel> LegalLossesExcel { get; set; }

        public ICollection<UnUtilisedCapacityLossesExcelViewModel> UnUtilisedCapacityLossesExcel { get; set; }

        public ICollection<ProcessDrivenLossesExcelViewModel> ProcessDrivenLossesExcel { get; set; }

        public ICollection<ManufacturingPerformanceLossesExcelViewModel> ManufacturingPerformanceLossesExcel { get; set; }
    }

    public class LegalLossesViewModel : LossesCategoryComponent
    {

    }

    public class UnUtilisedCapacityLossesViewModel : LossesCategoryComponent
    {

    }

    public class ProcessDrivenLossesViewModel : LossesCategoryComponent
    {

    }

    public class ManufacturingPerformanceLossesViewModel : LossesCategoryComponent
    {

    }

    public class LegalLossesExcelViewModel : LossesExcelComponent
    {

    }

    public class UnUtilisedCapacityLossesExcelViewModel : LossesExcelComponent
    {

    }

    public class ProcessDrivenLossesExcelViewModel : LossesExcelComponent
    {

    }

    public class ManufacturingPerformanceLossesExcelViewModel : LossesExcelComponent
    {

    }

    public class LossesCategoryComponent
    {
        public string LossEventCategory { get; set; }

        public double Value { get; set; }
    }

    public class LossesExcelComponent
    {
        public string Losses { get; set; }
        public string LossesCategory { get; set; }
        public string LossesRemarkCode { get; set; }
        public string LossesRemarkRemark { get; set; }
        public double Time { get; set; }
    }
}
