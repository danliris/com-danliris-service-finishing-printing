using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Sales.FinishingPrinting;
using Com.Danliris.Service.Production.Lib.ViewModels.Master.Instruction;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Kanban
{
    public class KanbanCreateViewModel : BaseViewModel, IValidatableObject
    {
        public double BadOutput { get; set; }
        public List<CartViewModel> Carts { get; set; }
        public string Code { get; set; }
        public double CurrentQty { get; set; }
        public int CurrentStepIndex { get; set; }
        public string Grade { get; set; }
        public KanbanInstructionViewModel Instruction { get; set; }
        public bool IsBadOutput { get; set; }
        public bool IsComplete { get; set; }
        public bool IsInactive { get; set; }
        public bool IsReprocess { get; set; }
        public KanbanViewModel OldKanban { get; set; }
        public ProductionOrderIntegrationViewModel ProductionOrder { get; set; }
        public ProductionOrderDetailIntegrationViewModel SelectedProductionOrderDetail { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ProductionOrder == null || ProductionOrder.Id.Equals(0))
                yield return new ValidationResult("Surat Perintah Produksi harus diisi", new List<string> { "ProductionOrder" });

            if (string.IsNullOrWhiteSpace(Grade))
                yield return new ValidationResult("Grade harus diisi", new List<string> { "Grade" });

            int ErrorCount = 0;
            string CartErrors = "[";
            if (Carts == null || Carts.Count <= 0)
                yield return new ValidationResult("Kereta harus diisi", new List<string> { "Cart" });
            else
            {
                foreach (var Cart in Carts)
                {
                    CartErrors += "{";
                    if (string.IsNullOrWhiteSpace(Cart.CartNumber))
                    {
                        ErrorCount++;
                        CartErrors += "CartNumber : 'Nomor Kereta harus diisi', ";
                    }

                    if (Cart.Qty <= 0)
                    {
                        ErrorCount++;
                        CartErrors += "Qty: 'Kuantiti harus lebih dari 0', ";
                    }

                    if (Cart.Pcs <= 0)
                    {
                        ErrorCount++;
                        CartErrors += "Pcs: 'Pcs harus lebih dari 0', ";
                    }
                    CartErrors += "}, ";
                }
            }
            CartErrors += "]";

            if (ErrorCount > 0)
                yield return new ValidationResult(CartErrors, new List<string> { "Carts" });

            ErrorCount = 0;
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
                        StepErrors += "}, ";
                    }
                }
            }
            StepErrors = "]";
        }
    }
}
