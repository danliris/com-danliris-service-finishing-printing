using Com.Danliris.Service.Finishing.Printing.Lib.Models.CostCalculation;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.CostCalculation
{
    public class CostCalculationChemicalViewModel
    {
        public CostCalculationChemicalViewModel()
        {

        }

        public CostCalculationChemicalViewModel(CostCalculationChemicalModel entity)
        {
            Id = entity.Id;
            ChemicalId = entity.ChemicalId;
            ChemicalQuantity = entity.ChemicalQuantity;
        }

        public int Id { get; set; }
        public int ChemicalId { get; set; }
        public int ChemicalQuantity { get; set; }
        public int Index { get; set; }
    }
}