using Com.Danliris.Service.Finishing.Printing.Lib.Models.Kanban;
using Com.Danliris.Service.Production.Lib.Models.Master.Step;
using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.Machine
{
    public class MachineModel : StandardEntity, IValidatableObject
    {
        [MaxLength(255)]
        public string UId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Process { get; set; }
        public string Manufacture { get; set; }
        public int Year { get; set; }
        public string Condition { get; set; }
        public int MonthlyCapacity { get; set; }
        public decimal Electric { get; set; }
        public decimal Steam { get; set; }
        public decimal Water { get; set; }
        public decimal Solar { get; set; }
        public decimal LPG { get; set; }

        //unit
        public int UnitId { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }
        public string UnitDivisionId { get; set; }
        public string UnitDivisionName { get; set; }

        //machine type
        public int MachineTypeId { get; set; }
        public string MachineTypeCode { get; set; }
        public string MachineTypeName { get; set; }

        public bool UseBQBS { get; set; }

        public ICollection<MachineEventsModel> MachineEvents { get; set; }
        public ICollection<MachineStepModel> MachineSteps { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}