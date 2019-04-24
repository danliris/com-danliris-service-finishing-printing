using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.PackingReceipt
{
    public class PackingReceiptModel : StandardEntity, IValidatableObject
    {
        [MaxLength(255)]
        public string UId { get; set; }
        public string Code { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Remark { get; set; }
        public bool Accepted { get; set; }
        public bool Declined { get; set; }
        public bool IsVoid { get; set; }
        public string PackingCode { get; set; }
        public int PackingId { get; set; }

        //storage
        public int StorageId { get; set; }
        public string StorageCode { get; set; }
        public string StorageName { get; set; }
        public string StorageUnitName { get; set; }
        public string StorageUnitCode { get; set; }
        public string StorageDivisionName { get; set; }
        public string StorageDivisionCode { get; set; }

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

        public ICollection<PackingReceiptItem> Items { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
