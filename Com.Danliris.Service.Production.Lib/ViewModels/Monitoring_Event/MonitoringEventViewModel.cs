using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.Machine;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Sales.FinishingPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Monitoring_Event
{
    public class MonitoringEventViewModel : BaseViewModel, IValidatableObject
    {
        public string UId { get; set; }
        public string Code { get; set; }
        public DateTimeOffset? DateStart { get; set; }
        public DateTimeOffset? DateEnd { get; set; }
        public double? TimeInMilisStart { get; set; }
        public double? TimeInMilisEnd { get; set; }
        public string CartNumber { get; set; }
        public string Remark { get; set; }
        public MachineViewModel Machine { get; set; }
        public ProductionOrderIntegrationViewModel ProductionOrder { get; set; }
        public ProductionOrderDetailIntegrationViewModel ProductionOrderDetail { get; set; }
        public MachineEventViewModel MachineEvent { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            DateTimeOffset dateNow = DateTimeOffset.UtcNow;

            if (string.IsNullOrWhiteSpace(CartNumber))
                yield return new ValidationResult("Kereta harus diisi", new List<string> { "CartNumber" });

            if (this.Machine == null || this.Machine.Id.Equals(0))
                yield return new ValidationResult("Mesin harus di isi", new List<string> { "Machine" });

            if (this.ProductionOrder == null || this.ProductionOrder.Id.Equals(0))
                yield return new ValidationResult("Production Order harus di isi", new List<string> { "ProductionOrder" });

            if (this.ProductionOrderDetail == null || this.ProductionOrderDetail.Id.Equals(0))
                yield return new ValidationResult("Production Order Detail harus di isi", new List<string> { "ProductionOrderDetail" });

            if (this.MachineEvent == null || this.MachineEvent.Id.Equals(0))
                yield return new ValidationResult("MachineEvent harus di isi", new List<string> { "MachineEvent" });


            if (this.DateStart == null)
            {
                yield return new ValidationResult("harus di isi", new List<string> { "DateStart" });
            }
            else if (this.DateStart != null)
            {
                if (this.DateStart > dateNow)
                {
                    yield return new ValidationResult("tanggal mulai lebih dari hari ini", new List<string> { "DateStart" });
                }
            }

            if (this.DateEnd == null)
            {
                yield return new ValidationResult("harus di isi", new List<string> { "DateEnd" });
            }
            else if (this.DateEnd != null)
            {
                if (this.DateEnd > dateNow)
                {
                    yield return new ValidationResult("tanggal selesai lebih dari hari ini", new List<string> { "DateEnd" });
                }else if (this.DateStart > this.DateEnd)
                {
                    yield return new ValidationResult("tanggal mulai lebih dari tanggal selesai", new List<string> { "DateEnd" });
                    yield return new ValidationResult("tanggal mulai lebih dari tanggal selesai", new List<string> { "DateStart" });
                }
            }

        }
    }
}
