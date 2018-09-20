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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (this.Date == null)
                yield return new ValidationResult("Tanggal harus diisi", new List<string> { "Date" });
        }
    }
}
