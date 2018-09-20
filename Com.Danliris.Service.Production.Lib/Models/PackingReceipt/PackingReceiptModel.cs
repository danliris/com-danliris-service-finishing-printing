using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.PackingReceipt
{
    public class PackingReceiptModel : StandardEntity, IValidatableObject
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
            throw new NotImplementedException();
        }
    }
}
