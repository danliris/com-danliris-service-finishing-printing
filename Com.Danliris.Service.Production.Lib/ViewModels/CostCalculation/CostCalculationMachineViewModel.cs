using System.Collections.Generic;
using System.Linq;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.CostCalculation;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.CostCalculation
{
    public class CostCalculationMachineViewModel
    {
        public CostCalculationMachineViewModel()
        {

        }

        public CostCalculationMachineViewModel(CostCalculationMachineModel model, List<CostCalculationChemicalModel> costCalculationChemicals)
        {
            Id = model.Id;
            MachineId = model.MachineId;
            StepProcessId = model.StepProcessId;
            Chemicals = costCalculationChemicals.Where(entity => entity.CostCalculationMachineId == model.Id).OrderBy(entity => entity.Index).Select(entity => new CostCalculationChemicalViewModel(entity)).ToList();
        }

        public int Id { get; set; }
        public int MachineId { get; set; }
        public int StepProcessId { get; set; }
        public List<CostCalculationChemicalViewModel> Chemicals { get; set; }
        public int Index { get; set; }
    }
}
