using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.PackingReceipt
{
    public class PackingReceiptViewModel : BaseViewModel, IValidatableObject
    {
        public string Code { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Remark { get; set; }
        public bool Accepted { get; set; }
        public bool Declined { get; set; }
        public bool IsVoid { get; set; }
        public string PackingCode { get; set; }
        public int PackingId { get; set; }


        public string ReferenceNo { get; set; }
        public string ReferenceType { get; set; }
        public string Type { get; set; }
        public string ProductionOrderNo { get; set; }
        public string Buyer { get; set; }
        public string ColorName { get; set; }
        public string Construction { get; set; }
        public string MaterialWidthFinish { get; set; }
        public string PackingUom { get; set; }
        public string OrderType { get; set; }
        public string ColorType { get; set; }
        public string DesignCode { get; set; }
        public string DesignNumber { get; set; }
        public StorageIntegrationViewModel Storage { get; set; }
        public ICollection<PackingReceiptItemViewModel> Items { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (this.Date == null)
                yield return new ValidationResult("Tanggal harus diisi", new List<string> { "Date" });
            if (this.PackingId.Equals(0))
            {
                yield return new ValidationResult("packing harus diisi", new List<string> { "PackingId" });
            }
            if (this.Storage == null)
            {
                yield return new ValidationResult("Storage harus diisi", new List<string> { "Storage" });
            }
            if (this.Items.Count == 0)
            {
                yield return new ValidationResult("Items harus diisi", new List<string> { "Items" });
            }
        }
    }
}
