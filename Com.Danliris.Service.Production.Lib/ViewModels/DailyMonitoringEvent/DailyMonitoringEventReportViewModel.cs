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
        }

        public double DesignSpeed { get; set; }
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

    public class LossesCategoryComponent
    {
        public string LossEventCategory { get; set; }

        public double Value { get; set; }
    }
}
