using Com.Moonlay.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.DOSales
{
    [JsonObject(IsReference = true)]
    public class DOSalesModel : StandardEntity, IValidatableObject
    {
        [MaxLength(255)]
        public string UId { get; set; }

        [MaxLength(25)]
        public string Code { get; set; }

        public DateTimeOffset Date { get; set; }

        public int StorageId { get; set; }
        [MaxLength(250)]
        public string StorageName { get; set; }

        public int ProductionOrderId { get; set; }
        [MaxLength(25)]
        public string ProductionOrderNo { get; set; }

        public int MaterialId { get; set; }
        [MaxLength(255)]
        public string Material { get; set; }
        [MaxLength(25)]
        public string MaterialWidthFinish { get; set; }

        public int MaterialConstructionFinishId { get; set; }
        [MaxLength(250)]
        public string MaterialConstructionFinishName { get; set; }

        public int BuyerId { get; set; }
        [MaxLength(25)]
        public string BuyerCode { get; set; }
        [MaxLength(250)]
        public string BuyerName { get; set; }
        [MaxLength(250)]
        public string BuyerAddress { get; set; }
        [MaxLength(25)]
        public string BuyerType { get; set; }

        [MaxLength(25)]
        public string PackingUom { get; set; }

        [MaxLength(300)]
        public string Construction { get; set; }

        [MaxLength(25)]
        public string Status { get; set; }
        public bool Accepted { get; set; }
        public bool Declined { get; set; }

        public virtual ICollection<DOSalesDetailModel> DOSalesDetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            throw new NotImplementedException();
        }
    }
}
