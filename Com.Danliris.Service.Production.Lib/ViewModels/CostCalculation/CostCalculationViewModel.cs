using Com.Danliris.Service.Finishing.Printing.Lib.Models.CostCalculation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.CostCalculation
{
    public class CostCalculationViewModel : IValidatableObject
    {
        public CostCalculationViewModel()
        {
            Date = DateTimeOffset.Now;
        }

        public CostCalculationViewModel(CostCalculationModel costCalculation, List<CostCalculationMachineModel> costCalculationMachines, List<CostCalculationChemicalModel> costCalculationChemicals)
        {
            Id = costCalculation.Id;
            InstructionId = costCalculation.InstructionId;
            PreparationValue = costCalculation.PreparationValue;
            CurrencyRate = costCalculation.CurrencyRate;
            ProductionUnitValue = costCalculation.ProductionUnitValue;
            TKLQuantity = costCalculation.TKLQuantity;
            GreigeId = costCalculation.GreigeId;
            PreparationFabricWeight = costCalculation.PreparationFabricWeight;
            RFDFabricWeight = costCalculation.RFDFabricWeight;
            ActualPrice = costCalculation.ActualPrice;
            CargoCost = costCalculation.CargoCost;
            InsuranceCost = costCalculation.InsuranceCost;
            Remark = costCalculation.Remark;

            Machines = costCalculationMachines.Select(entity => new CostCalculationMachineViewModel(entity, costCalculationChemicals)).ToList();
        }

        public int Id { get; set; }
        public int InstructionId { get; set; }
        public double PreparationValue { get; set; }
        public double CurrencyRate { get; set; }
        public double ProductionUnitValue { get; set; }
        public int TKLQuantity { get; set; }
        public int GreigeId { get; set; }
        public double PreparationFabricWeight { get; set; }
        public double RFDFabricWeight { get; set; }
        public double ActualPrice { get; set; }
        public double CargoCost { get; set; }
        public double InsuranceCost { get; set; }
        public double Remark { get; set; }
        public List<CostCalculationMachineViewModel> Machines { get; set; }
        public string BuyerName { get; set; }
        public DateTimeOffset Date { get; set; }
        public string GreigeName { get; set; }
        public string InstructionName { get; set; }
        public int ProductionOrderId { get; set; }
        public string ProductionOrderNo { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ProductionOrderId == 0)
                yield return new ValidationResult("Nomor SPP harus diisi!", new List<string> { "ProductionOrder" });

            if (InstructionId == 0)
                yield return new ValidationResult("Instruksi harus diisi!", new List<string> { "Instruction" });

            if (PreparationValue <= 0)
                yield return new ValidationResult("Preparasi harus lebih dari 0!", new List<string> { "PreparationValue" });

            if (CurrencyRate <= 0)
                yield return new ValidationResult("Kurs harus lebih dari 0!", new List<string> { "CurrencyRate" });

            if (ProductionUnitValue <= 0)
                yield return new ValidationResult("Produksi Unit harus lebih dari 0!", new List<string> { "ProductionUnitValue" });

            if (TKLQuantity <= 0)
                yield return new ValidationResult("Jumlah TKL harus lebih dari 0!", new List<string> { "TKLQuantity" });

            if (GreigeId == 0)
                yield return new ValidationResult("Greige harus diisi!", new List<string> { "Greige" });

            if (PreparationFabricWeight <= 0)
                yield return new ValidationResult("Berat Kain Prep harus lebih dari 0!", new List<string> { "PreparationFabricWeight" });

            if (RFDFabricWeight <= 0)
                yield return new ValidationResult("Berat Kain RFD harus lebih dari 0!", new List<string> { "RFDFabricWeight" });

            if (ActualPrice <= 0)
                yield return new ValidationResult("Harga Real harus lebih dari 0!", new List<string> { "ActualPrice" });

            if (CargoCost <= 0)
                yield return new ValidationResult("Biaya Kargo harus lebih dari 0!", new List<string> { "CargoCost" });

            if (InsuranceCost <= 0)
                yield return new ValidationResult("Asuransi harus lebih dari 0!", new List<string> { "InsuranceCost" });

            if (Machines == null || Machines.Count == 0)
                yield return new ValidationResult("Asuransi harus lebih dari 0!", new List<string> { "Machine" });

            else
            {
                var anyError = false;
                var machinesErrors = "[ ";
                foreach (var machine in Machines)
                {
                    machinesErrors += "{";

                    if (machine.MachineId == 0)
                    {
                        anyError = true;
                        machinesErrors += "Machine: 'Mesin harus diisi!', ";
                    }

                    if (machine.StepProcessId == 0)
                    {
                        anyError = true;
                        machinesErrors += "StepProcess: 'Proses harus diisi!', ";
                    }

                    if (machine.Chemicals == null || machine.Chemicals.Count == 0)
                    {
                        anyError = true;
                        machinesErrors += "Chemical: 'Biaya Chemical harus diisi!', ";
                    } 
                    else
                    {
                        machinesErrors += "Chemicals: [ ";

                        foreach (var chemical in machine.Chemicals)
                        {
                            machinesErrors += "{";

                            if (chemical.ChemicalId == 0)
                            {
                                anyError = true;
                                machinesErrors += "Chemical: 'Chemical harus diisi!', ";
                            }

                            if (chemical.ChemicalQuantity <= 0)
                            {
                                anyError = true;
                                machinesErrors += "ChemicalQuantity: 'Chemical Quantity harus lebih dari 0!', ";
                            }

                            machinesErrors += "}, ";
                        }

                        machinesErrors += "], ";
                    }

                    machinesErrors += "}, ";
                }
                machinesErrors += " ]";

                if (anyError)
                {
                    yield return new ValidationResult(machinesErrors, new List<string> { "Machines" });
                }
            }
        }

        public CostCalculationModel MapViewModelToCreateModel()
        {
            return new CostCalculationModel()
            {
                ActualPrice = ActualPrice,
                BuyerName = BuyerName,
                CargoCost = CargoCost,
                CurrencyRate = CurrencyRate,
                Date = Date,
                GreigeId = GreigeId,
                GreigeName = GreigeName,
                InstructionId = InstructionId,
                InstructionName = InstructionName,
                InsuranceCost = InsuranceCost,
                PreparationFabricWeight = PreparationFabricWeight,
                PreparationValue = PreparationValue,
                ProductionOrderId = ProductionOrderId,
                ProductionOrderNo = ProductionOrderNo,
                ProductionUnitValue = ProductionUnitValue,
                Remark = Remark,
                RFDFabricWeight = RFDFabricWeight,
                TKLQuantity = TKLQuantity,
                Machines = Machines.Select(machineEntity => new CostCalculationMachineModel()
                {
                    Index = machineEntity.Index,
                    StepProcessId = machineEntity.StepProcessId,
                    MachineId = machineEntity.MachineId,
                    Chemicals = machineEntity.Chemicals.Select(chemicalEntity => new CostCalculationChemicalModel()
                    {
                        ChemicalId = chemicalEntity.ChemicalId,
                        ChemicalQuantity = chemicalEntity.ChemicalQuantity,
                        Index = chemicalEntity.Index
                    }).ToList()

                }).ToList()
            };
        }
    }
}
