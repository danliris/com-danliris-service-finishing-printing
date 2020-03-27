using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.NewShipmentDocument
{
    public class NewShipmentDocumentModel : StandardEntity, IValidatableObject
    {
        [MaxLength(255)]
        public string UId { get; set; }
        public int BuyerId { get; set; }
        public string BuyerAddress { get; set; }
        [MaxLength(250)]
        public string BuyerCity { get; set; }
        [MaxLength(125)]
        public string BuyerCode { get; set; }
        [MaxLength(250)]
        public string BuyerContact { get; set; }
        [MaxLength(250)]
        public string BuyerCountry { get; set; }
        public string BuyerName { get; set; }
        [MaxLength(250)]
        public string BuyerNPWP { get; set; }
        [MaxLength(250)]
        public string BuyerTempo { get; set; }
        [MaxLength(250)]
        public string BuyerType { get; set; }
        [MaxLength(250)]
        public string Code { get; set; }
        //[MaxLength(250)]
        //public string DeliveryCode { get; set; }
        public int DOSalesId { get; set; }
        [MaxLength(255)]
        public string DOSalesNo { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }
        [MaxLength(250)]
        public string DeliveryReference { get; set; }
        public ICollection<NewShipmentDocumentDetailModel> Details { get; set; }
        public bool IsVoid { get; set; }
        [MaxLength(250)]
        public string ProductIdentity { get; set; }
        [MaxLength(250)]
        public string ShipmentNumber { get; set; }
        public int StorageId { get; set; }
        [MaxLength(250)]
        public string StorageCode { get; set; }
        [MaxLength(250)]
        public string StorageName { get; set; }
        [MaxLength(1000)]
        public string StorageDescription { get; set; }
        [MaxLength(250)]
        public string StorageUnitCode { get; set; }
        [MaxLength(250)]
        public string StorageUnitName { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
