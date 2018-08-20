using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.Machine;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Sales.FinishingPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Monitoring_Specification_Machine
{
    public class MonitoringSpecificationMachineViewModel : BaseViewModel, IValidatableObject
    {
        public string Code { get; set; }
        public DateTimeOffset DateTimeInput { get; set; }
        public string CartNumber { get; set; }
        public MachineViewModel Machine { get; set; }
        public ProductionOrderIntegrationViewModel ProductionOrder { get; set; }
        public ICollection<MonitoringSpecificationMachineDetailsViewModel> Details { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(CartNumber))
                yield return new ValidationResult("Kereta harus diisi", new List<string> { "CartNumber" });

            if (this.DateTimeInput == null)
            {
                yield return new ValidationResult("harus di isi", new List<string> { "DateStart" });
            }

            if (this.Machine == null || this.Machine.Id.Equals(0))
                yield return new ValidationResult("Machine harus di isi", new List<string> { "Machine" });

            if (this.ProductionOrder == null || this.ProductionOrder.Id.Equals(0))
                yield return new ValidationResult("ProductionOrder harus di isi", new List<string> { "ProductionOrder" });

            if (this.Details.Count.Equals(0))
            {
                yield return new ValidationResult("Details harus di isi", new List<string> { "Details" });
            }
            else
            {
                int Count = 0;
                string Details = "[";

                foreach (MonitoringSpecificationMachineDetailsViewModel data in this.Details)
                {
                    if (data.DataType == "input skala angka")
                    {
                        string[] range = data.DefaultValue.Split("-");

                        if (Convert.ToDouble(data.Value) < Convert.ToDouble(range[0]) || Convert.ToDouble(data.Value) > Convert.ToInt32(range[1]) || Convert.ToDouble(data.Value).Equals(0))
                        {
                            Count++;
                            Details += "{ 'input tidak sesuai' }, ";
                        }
                    }

                    if (data.DataType == "input teks")
                    {

                        if (string.IsNullOrWhiteSpace(data.Value))
                        {
                            Count++;
                            Details += "{ 'harus di isi' }, ";
                        }
                    }

                }

                Details += "]";

                if (Count > 0)
                {
                    yield return new ValidationResult(Details, new List<string> { "Details" });
                }
            }
        }
    }
}
