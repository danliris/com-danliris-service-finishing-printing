using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Sales.FinishingPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Monitoring_Specification_Machine
{
    public class MonitoringSpecificationMachineViewModel : BaseViewModel, IValidatableObject
    {
        public string Code { get; set; }
        public DateTimeOffset DateTime { get; set; }
        public string CartNumber { get; set; }
        public MachineViewModel Machine { get; set; }
        public ProductionOrderIntegrationViewModel ProductionOrder { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
