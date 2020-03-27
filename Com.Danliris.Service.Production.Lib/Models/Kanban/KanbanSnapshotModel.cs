using Com.Danliris.Service.Finishing.Printing.Lib.Utilities;
using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.Kanban
{
    public class KanbanSnapshotModel : BaseEntity<long>
    {
        [EpplusIgnore]
        public override long Id { get; set; }
        [EpplusIgnore]
        public int DOCreatedUtcMonth { get; set; }
        [EpplusIgnore]
        public long DOCreatedUtcYear { get; set; }
        [EpplusIgnore]
        public int KanbanId { get; set; }
        [MaxLength(1024)]
        public string Buyer { get; set; }
        [Description("Nomor SPP")]
        [MaxLength(512)]
        public string SPPNo { get; set; }
        [MaxLength(2048)]
        public string Konstruksi { get; set; }
        public double Qty { get; set; }

        [EpplusIgnore]
        public int PreTreatmentInputStepIndex { get; set; }
        [EpplusIgnore]
        public int PreTreatmentOutputStepIndex { get; set; }
        [EpplusIgnore]
        public int PreTreatmentInputDate { get; set; }
        [EpplusIgnore]
        public int PreTreatmentOutputDate { get; set; }
        [Description("Pre Treatment Konstruksi")]
        [MaxLength(2048)]
        public string PreTreatmentKonstruksi { get; set; }
        [Description("Pre Treatment Cart Number")]
        [MaxLength(1024)]
        public string PreTreatmentCartNumber { get; set; }
        [Description("Pre Treatment Input Qty")]
        public double? PreTreatmentInputQty { get; set; }
        [Description("Pre Treatment Good Output Qty")]
        public double? PreTreatmentGoodOutputQty { get; set; }
        [Description("Pre Treatment Bad Output Qty")]
        public double? PreTreatmentBadOutputQty { get; set; }
        [Description("Pre Treatment Day")]
        public int PreTreatmentDay { get; set; }

        [EpplusIgnore]
        public int DyeingInputStepIndex { get; set; }
        [EpplusIgnore]
        public int DyeingOutputStepIndex { get; set; }
        [EpplusIgnore]
        public int DyeingInputDate { get; set; }
        [EpplusIgnore]
        public int DyeingOutputDate { get; set; }
        [Description("Dyeing Konstruksi")]
        [MaxLength(2048)]
        public string DyeingKonstruksi { get; set; }
        [Description("Dyeing Cart Number")]
        [MaxLength(1024)]
        public string DyeingCartNumber { get; set; }
        [Description("Dyeing Input Qty")]
        public double? DyeingInputQty { get; set; }
        [Description("Dyeing Good Output Qty")]
        public double? DyeingGoodOutputQty { get; set; }
        [Description("Dyeing Bad Output Qty")]
        public double? DyeingBadOutputQty { get; set; }
        [Description("Dyeing Day")]
        public int DyeingDay { get; set; }

        [EpplusIgnore]
        public int PrintingInputStepIndex { get; set; }
        [EpplusIgnore]
        public int PrintingOutputStepIndex { get; set; }
        [EpplusIgnore]
        public int PrintingInputDate { get; set; }
        [EpplusIgnore]
        public int PrintingOutputDate { get; set; }
        [Description("Printing Konstruksi")]
        [MaxLength(2048)]
        public string PrintingKonstruksi { get; set; }
        [Description("Printing Cart Number")]
        [MaxLength(1024)]
        public string PrintingCartNumber { get; set; }
        [Description("Printing Input Qty")]
        public double? PrintingInputQty { get; set; }
        [Description("Printing Good Output Qty")]
        public double? PrintingGoodOutputQty { get; set; }
        [Description("Printing Bad Output Qty")]
        public double? PrintingBadOutputQty { get; set; }
        [Description("Printing Day")]
        public int PrintingDay { get; set; }

        [EpplusIgnore]
        public int FinishingInputStepIndex { get; set; }
        [EpplusIgnore]
        public int FinishingOutputStepIndex { get; set; }
        [EpplusIgnore]
        public int FinishingInputDate { get; set; }
        [EpplusIgnore]
        public int FinishingOutputDate { get; set; }
        [Description("Finishing Konstruksi")]
        [MaxLength(2048)]
        public string FinishingKonstruksi { get; set; }
        [Description("Finishing Cart Number")]
        [MaxLength(1024)]
        public string FinishingCartNumber { get; set; }
        [Description("Finishing Input Qty")]
        public double? FinishingInputQty { get; set; }
        [Description("Finishing Good Output Qty")]
        public double? FinishingGoodOutputQty { get; set; }
        [Description("Finishing Bad Output Qty")]
        public double? FinishingBadOutputQty { get; set; }
        [Description("Finishing Day")]
        public int FinishingDay { get; set; }

        [EpplusIgnore]
        public int QCInputStepIndex { get; set; }
        [EpplusIgnore]
        public int QCOutputStepIndex { get; set; }
        [EpplusIgnore]
        public int QCInputDate { get; set; }
        [EpplusIgnore]
        public int QCOutputDate { get; set; }
        [Description("QC Konstruksi")]
        [MaxLength(2048)]
        public string QCKonstruksi { get; set; }
        [Description("QC Cart Number")]
        [MaxLength(1024)]
        public string QCCartNumber { get; set; }
        [Description("QC Input Qty")]
        public double? QCInputQty { get; set; }
        [Description("QC Good Output Qty")]
        public double? QCGoodOutputQty { get; set; }
        [Description("QC Bad Output Qty")]
        public double? QCBadOutputQty { get; set; }
        [Description("QC Day")]
        public int QCDay { get; set; }
    }
}
