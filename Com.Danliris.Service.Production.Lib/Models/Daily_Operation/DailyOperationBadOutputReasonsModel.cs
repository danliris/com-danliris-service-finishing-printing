using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.Daily_Operation
{
    public class DailyOperationBadOutputReasonsModel : StandardEntity, IValidatableObject
    {
        public double? Length { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }

        //BadOutputReason
        public int BadOutputId { get; set; }
        public string BadOutputCode { get; set; }
        public string BadOutputReason { get; set; }

        //machine
        public int MachineId { get; set; }
        public string MachineCode { get; set; }
        public string MachineName { get; set; }

        public int? DailyOperationId { get; set; }
        public virtual DailyOperationModel DailyOperation { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
