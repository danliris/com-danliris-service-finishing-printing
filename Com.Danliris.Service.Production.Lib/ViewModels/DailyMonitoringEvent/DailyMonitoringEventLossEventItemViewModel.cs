using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.LossEventRemark;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DailyMonitoringEvent
{
    public class DailyMonitoringEventLossEventItemViewModel : BaseViewModel
    {
        public LossEventRemarkViewModel LossEventRemark { get; set; }
        public double Time { get; set; }

    }
}
