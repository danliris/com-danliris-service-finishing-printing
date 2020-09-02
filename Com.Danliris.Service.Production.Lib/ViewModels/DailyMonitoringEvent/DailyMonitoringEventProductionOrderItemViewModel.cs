using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Kanban;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Sales.FinishingPrinting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DailyMonitoringEvent
{
    public class DailyMonitoringEventProductionOrderItemViewModel : BaseViewModel
    {
        public KanbanViewModel Kanban { get; set; }
        
        public double Speed { get; set; }

        public double Input_BQ { get; set; }

        public double Output_BS { get; set; }
    }
}
