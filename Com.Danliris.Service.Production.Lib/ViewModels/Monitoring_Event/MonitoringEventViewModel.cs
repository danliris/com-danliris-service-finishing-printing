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
    public class MonitoringEventViewModel : BaseViewModel, IValidatableObject
    {
        public string Code { get; set; }
        public DateTimeOffset DateStart { get; set; }
        public DateTimeOffset DateEnd { get; set; }
        public double TimeInMilisStart { get; set; }
        public double TimeInMilisEnd { get; set; }
        public string CartNumber { get; set; }
        public string Remark { get; set; }
        public MachineViewModel Machine { get; set; }
        public ProductionOrderIntegrationViewModel ProductionOrder { get; set; }
        public ProductionOrderDetailIntegrationViewModel ProductionOrderDetail { get; set; }
        public MachineEventViewModel MachineEvent { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
