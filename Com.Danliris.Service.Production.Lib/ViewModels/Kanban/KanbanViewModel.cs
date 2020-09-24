using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Sales.FinishingPrinting;
using Com.Danliris.Service.Production.Lib.ViewModels.Master.Instruction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Kanban
{
    public class KanbanViewModel : BaseViewModel, IValidatableObject
    {
        public string UId { get; set; }
        public double? BadOutput { get; set; }
        public CartViewModel Cart { get; set; }
        public string Code { get; set; }
        public double? CurrentQty { get; set; }
        public int? CurrentStepIndex { get; set; }
        public double? GoodOutput { get; set; }
        public string Grade { get; set; }
        public KanbanInstructionViewModel Instruction { get; set; }
        public bool? IsBadOutput { get; set; }
        public bool? IsFulfilledOutput { get; set; }
        public bool? IsComplete { get; set; }
        public bool? IsInactive { get; set; }
        public bool? IsReprocess { get; set; }
        public int? OldKanbanId { get; set; }
        public string FinishWidth { get; set; }
        public ProductionOrderIntegrationViewModel ProductionOrder { get; set; }
        public ProductionOrderDetailIntegrationViewModel SelectedProductionOrderDetail { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ProductionOrder == null || ProductionOrder.Id.Equals(0))
                yield return new ValidationResult("Surat Perintah Produksi harus diisi", new List<string> { "ProductionOrder" });

            if (string.IsNullOrWhiteSpace(Grade))
                yield return new ValidationResult("Grade harus diisi", new List<string> { "Grade" });

            if (Cart == null)
                yield return new ValidationResult("Kereta harus diisi", new List<string> { "Cart" });
            else
            {

                if (string.IsNullOrWhiteSpace(Cart.CartNumber))
                    yield return new ValidationResult("Nomor Kereta harus diisi", new List<string> { "CartNumber" });
                //if (Cart.Qty <= 0)
                //    yield return new ValidationResult("Panjang harus lebih dari 0", new List<string> { "Qty" });
                //if (Cart.Pcs <= 0)
                //    yield return new ValidationResult("Jumlah PCS harus diisi", new List<string> { "Pcs" });
            }

            int ErrorCount = 0;
            string StepErrors = "[";
            if (Instruction == null || Instruction.Id.Equals(0))
                yield return new ValidationResult("Instruksi harus diisi", new List<string> { "Instruction" });
            else
            {
                if (Instruction.Steps == null || Instruction.Steps.Count <= 0)
                    yield return new ValidationResult("Step harus diisi", new List<string> { "Step" });
                else
                {
                    foreach (var step in Instruction.Steps)
                    {
                        StepErrors += "{";
                        if (string.IsNullOrWhiteSpace(step.Process))
                        {
                            ErrorCount++;
                            StepErrors += "Process : 'Proses harus diisi', ";
                        }

                        if (string.IsNullOrWhiteSpace(step.ProcessArea))
                        {
                            ErrorCount++;
                            StepErrors += "ProcessArea: 'Area harus diisi', ";
                        }

                        if (step.Deadline == null)
                        {
                            ErrorCount++;
                            StepErrors += "Deadline: 'Tanggal Deadline harus diisi', ";
                        }

                        if (step.Machine == null || string.IsNullOrWhiteSpace(step.Machine.Name))
                        {
                            ErrorCount++;
                            StepErrors += "Machine: 'Mesin harus diisi', ";
                        }
                        StepErrors += "}, ";
                    }
                }
            }
            StepErrors += "]";
            if (ErrorCount > 0)
                yield return new ValidationResult(StepErrors, new List<string> { "Steps" });
        }
    }
}
