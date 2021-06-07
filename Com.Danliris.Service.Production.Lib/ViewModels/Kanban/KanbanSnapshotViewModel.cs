using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Kanban
{
    public class KanbanSnapshotViewModel
    {
        public string Buyer { get; set; }
        public string SPPNo { get; set; }
        public string Konstruksi { get; set; }
        public string Qty { get; set; }
        public string PreTreatmentKonstruksi { get; set; }
        public string PreTreatmentInputQty { get; set; }
        public string PreTreatmentGoodOutputQty { get; set; }
        public string PreTreatmentBadOutputQty { get; set; }
        public string PreTreatmentDay { get; set; }
        public string DyeingKonstruksi { get; set; }
        public string DyeingInputQty { get; set; }
        public string DyeingGoodOutputQty { get; set; }
        public string DyeingBadOutputQty { get; set; }
        public string DyeingDay { get; set; }
        public string PrintingKonstruksi { get; set; }
        public string PrintingInputQty { get; set; }
        public string PrintingGoodOutputQty { get; set; }
        public string PrintingBadOutputQty { get; set; }
        public string PrintingDay { get; set; }
        public string FinishingKonstruksi { get; set; }
        public string FinishingInputQty { get; set; }
        public string FinishingGoodOutputQty { get; set; }
        public string FinishingBadOutputQty { get; set; }
        public string FinishingDay { get; set; }
        public string QCKonstruksi { get; set; }
        public string QCInputQty { get; set; }
        public string QCGoodOutputQty { get; set; }
        public string QCBadOutputQty { get; set; }
        public string QCDay { get; set; }
    }
}
