using Com.Danliris.Service.Production.Lib.BusinessLogic.Facades.Master;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Production.Lib.Models.Master.Instruction
{
    public class InstructionModel : StandardEntity, IValidatableObject
    {
        [MaxLength(255)]
        public string UId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public ICollection<InstructionStepModel> Steps { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //validationContext.Items.
            return new List<ValidationResult>();   
        }
    }
}
