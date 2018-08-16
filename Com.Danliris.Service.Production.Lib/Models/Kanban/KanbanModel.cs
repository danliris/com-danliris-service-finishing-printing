using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.Kanban
{
    public class KanbanModel : StandardEntity, IValidatableObject
    {
        public double BadOutput { get; set; }
        public string CartCartNumber { get; set; }
        public string CartCode { get; set; }
        public double CartQty { get; set; }
        public double CartPcs { get; set; }
        public string CartUomUnit { get; set; }
        public string Code { get; set; }
        public double CurrentQty { get; set; }
        public int CurrentStepIndex { get; set; }
        public double GoodOutput { get; set; }
        public string Grade { get; set; }
        public virtual KanbanInstructionModel Instruction { get; set; }
        public int InstructionId { get; set; }
        public bool IsBadOutput { get; set; }
        public bool IsComplete { get; set; }
        public bool IsInactive { get; set; }
        public bool IsReprocess { get; set; }
        public int OldKanbanId { get; set; }
        public int ProductionOrderId { get; set; }
        public string ProductionOrderOrderNo { get; set; }
        public int ProductionOrderOrderTypeId { get; set; }
        public string ProductionOrderOrderTypeCode { get; set; }
        public string ProductionOrderOrderTypeName { get; set; }
        public int ProductionOrderProcessTypeId { get; set; }
        public string ProductionOrderProcessTypeCode { get; set; }
        public string ProductionOrderProcessTypeName { get; set; }
        public int ProductionOrderMaterialId { get; set; }
        public string ProductionOrderMaterialCode { get; set; }
        public string ProductionOrderMaterialName { get; set; }
        public int ProductionOrderMaterialConstructionId { get; set; }
        public string ProductionOrderMaterialConstructionCode { get; set; }
        public string ProductionOrderMaterialConstructionName { get; set; }
        public int ProductionOrderYarnMaterialId { get; set; }
        public string ProductionOrderYarnMaterialCode { get; set; }
        public string ProductionOrderYarnMaterialName { get; set; }
        public string ProductionOrderHandlingStandard { get; set; }
        public string FinishWidth { get; set; }
        public int SelectedProductionOrderDetailId { get; set; }
        public string SelectedProductionOrderDetailColorRequest { get; set; }
        public string SelectedProductionOrderDetailColorTemplate { get; set; }
        public string SelectedProductionOrderDetailColorTypeCode { get; set; }
        public string SelectedProductionOrderDetailColorTypeName { get; set; }
        public double SelectedProductionOrderDetailQuantity { get; set; }
        public string SelectedProductionOrderDetailUomUnit { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
