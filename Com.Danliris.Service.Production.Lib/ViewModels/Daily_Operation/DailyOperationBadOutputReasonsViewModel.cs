using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.BadOutput;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.Machine;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Daily_Operation
{
    public class DailyOperationBadOutputReasonsViewModel : BaseViewModel, IValidatableObject
    {
        public double? Length { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }

        //BadOutputReason
        public BadOutputViewModel BadOutput { get; set; }
        //machine
        public MachineViewModel Machine { get; set; }
        public int? DailyOperationId { get; set; }
        public virtual DailyOperationViewModel DailyOperation { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
