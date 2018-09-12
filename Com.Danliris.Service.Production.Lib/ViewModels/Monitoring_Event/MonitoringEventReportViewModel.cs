using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.Machine;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Sales.FinishingPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Monitoring_Event
{
    public class MonitoringEventReportViewModel : BaseViewModel
    {
        public string code { get; set; }
        public DateTimeOffset dateStart { get; set; }
        public DateTimeOffset dateEnd { get; set; }
        public double timeInMilisStart { get; set; }
        public double timeInMilisEnd { get; set; }
        public string cartNumber { get; set; }
        public string remark { get; set; }
        //Machine
        public int machineId { get; set; }
        public string machineName { get; set; }
        //ProductionOrder
        public int productionOrderId { get; set; }
        public string productionOrderOrderNo { get; set; }
        public string productionOrderDeliveryDate { get; set; }
        //SelectedProductionOrderDetail
        public string productionOrderDetailCode { get; set; }
        public string productionOrderDetailColorRequest { get; set; }
        public string productionOrderDetailColorTemplate { get; set; }
        public string productionOrderDetailColorTypeId { get; set; }
        public string productionOrderDetailColorType { get; set; }
        public double productionOrderDetailQuantity { get; set; }
        //MachineEvent
        public int machineEventId { get; set; }
        public string machineEventCode { get; set; }
        public string machineEventName { get; set; }

        public string machineEventCategory { get; set; }

    }
}
