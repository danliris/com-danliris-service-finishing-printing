using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Monitoring_Specification_Machine
{
    public class MonitoringSpecificationMachineDetailsViewModel : BaseViewModel, IValidatableObject
    {
        public string Indicator { get; set; }
        public string DataType { get; set; }
        public string DefaultValue { get; set; }
        public double Value { get; set; }
        public string Uom { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
