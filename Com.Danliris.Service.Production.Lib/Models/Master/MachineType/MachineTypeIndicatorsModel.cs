using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.MachineType
{
    public class MachineTypeIndicatorsModel : StandardEntity, IValidatableObject
    {
        public string Indicator { get; set; }
        public string DataType { get; set; }
        public string DefaultValue { get; set; }
        public string Uom { get; set; }
        public int MachineTypeId { get; set; }
        public virtual MachineTypeModel MachineType { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
