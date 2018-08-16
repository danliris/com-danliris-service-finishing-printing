using Com.Moonlay.Models;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.Kanban
{
    public class KanbanStepIndicatorModel : StandardEntity
    {
        public string Name { get; set; }
        public string Uom { get; set; }
        public double Value { get; set; }
        public int StepId { get; set; }
        public virtual KanbanStepModel Step { get; set; }
    }
}