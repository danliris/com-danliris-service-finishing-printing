using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.CostCalculation;
using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.CostCalculation
{
    public class CostCalculationChemicalModel : StandardEntity
    {
        public int CostCalculationId { get; set; }
        public int CostCalculationMachineId { get; set; }
        [ForeignKey("CostCalculationMachineId")]
        public virtual CostCalculationMachineModel CostCalculationMachine { get; set; }
        public int Index { get; set; }
        public int ChemicalId { get; set; }
        public int ChemicalQuantity { get; set; }
    }
}
