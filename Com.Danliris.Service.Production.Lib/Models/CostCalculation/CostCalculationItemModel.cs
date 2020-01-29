using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.CostCalculation;
using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.CostCalculation
{
    public class CostCalculationMachineModel : StandardEntity
    {
        public int CostCalculationId { get; set; }
        [ForeignKey("CostCalculationId")]
        public virtual CostCalculationModel CostCalculation { get; set; }
        public int Index { get; set; }
        public int MachineId { get; set; }
        public ICollection<CostCalculationChemicalModel> Chemicals { get; set; }
        public int StepProcessId { get; set; }
    }
}
