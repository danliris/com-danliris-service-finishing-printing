using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Sales.FinishingPrinting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Kanban
{
    public class KanbanVisualizationViewModel
    {
        public string Code { get; set; }
        public string DailyOperationMachine { get; set; }
        public double? InputQuantity { get; set; }
        public double? GoodOutput { get; set; }
        public double? BadOutput { get; set; }
        public string Process { get; set; }
        public string ProcessArea { get; set; }
        public DateTimeOffset? Deadline { get; set; }
        public int StepsLength { get; set; }
        public int CurrentStepIndex { get; set; }
        public CartViewModel Cart { get; set; }
        public ProductionOrderIntegrationViewModel ProductionOrder { get; set; }
        public string Type { get; set; }
    }
}
