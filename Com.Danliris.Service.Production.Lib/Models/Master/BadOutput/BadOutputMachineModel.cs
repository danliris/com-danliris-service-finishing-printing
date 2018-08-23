using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.BadOutput
{
    public class BadOutputMachineModel : StandardEntity, IValidatableObject
    {
        public int MachineId { get; set; }
        public string MachineName { get; set; }
        public string MachineCode { get; set; }

        public int BadOutputId { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
