using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.Machine;
using Com.Danliris.Service.Production.Lib.Models.Master.Instruction;
using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.Kanban
{
    public class KanbanStepModel : StandardEntity
    {
        public string Alias { get; set; }
        public string Code { get; set; }
        public DateTimeOffset Deadline { get; set; }
        public string Process { get; set; }
        public string ProcessArea { get; set; }
        public int InstructionId { get; set; }
        public virtual KanbanInstructionModel Instruction { get; set; }
        public int MachineId { get; set; }
        public int SelectedIndex { get; set; }
        public ICollection<KanbanStepIndicatorModel> StepIndicators { get; set; }
        public virtual MachineModel Machine { get; set; }
    }
}