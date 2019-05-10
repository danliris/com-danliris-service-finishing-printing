using Com.Moonlay.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.Packing
{
    [JsonObject(IsReference = true)]
    public class PackingModel : StandardEntity, IValidatableObject
    {
        [MaxLength(255)]
        public string UId { get; set; }

        [MaxLength(25)]
        public string Code { get; set; }
        
        public int ProductionOrderId { get; set; }
        [MaxLength(25)]
        public string ProductionOrderNo { get; set; }
        
        public int OrderTypeId { get; set; }
        [MaxLength(25)]
        public string OrderTypeCode { get; set; }
        [MaxLength(25)]
        public string OrderTypeName { get; set; }
        
        [MaxLength(25)]
        public string SalesContractNo { get; set; }
        [MaxLength(250)]
        public string DesignCode { get; set; }
        [MaxLength(250)]
        public string DesignNumber { get; set; }
        
        public int BuyerId { get; set; }
        [MaxLength(25)]
        public string BuyerCode { get; set; }
        [MaxLength(250)]
        public string BuyerName { get; set; }
        [MaxLength(250)]
        public string BuyerAddress { get; set; }
        [MaxLength(25)]
        public string BuyerType { get; set; }

        public DateTimeOffset Date { get; set; }
        [MaxLength(25)]
        public string PackingUom { get; set; }
        [MaxLength(250)]
        public string ColorCode { get; set; }
        [MaxLength(250)]
        public string ColorName { get; set; }
        [MaxLength(250)]
        public string ColorType { get; set; }

        public int MaterialConstructionFinishId { get; set; }
        [MaxLength(250)]
        public string MaterialConstructionFinishName { get; set; }
        
        public int MaterialId { get; set; }
        [MaxLength(25)]
        public string Material { get; set; }
        [MaxLength(25)]
        public string MaterialWidthFinish { get; set; }
        
        [MaxLength(300)]
        public string Construction { get; set; }
        [MaxLength(25)]
        public string DeliveryType { get; set; }
        [MaxLength(25)]
        public string FinishedProductType { get; set; }

        [MaxLength(250)]
        public string Motif { get; set; }
        [MaxLength(25)]
        public string Status { get; set; }
        public bool Accepted { get; set; }
        public bool Declined { get; set; }
        
        public virtual ICollection<PackingDetailModel> PackingDetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
