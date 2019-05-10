using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.MonitoringEvent;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Monitoring_Event;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Test.DataUtils
{
    public class MonitoringEventDataUtil : BaseDataUtil<MonitoringEventFacade, MonitoringEventModel>
    {
        public MonitoringEventDataUtil(MonitoringEventFacade facade) : base(facade)
        {
        }
    }
}
