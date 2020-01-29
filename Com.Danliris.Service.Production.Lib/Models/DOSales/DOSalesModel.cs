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
        [MaxLength(255)]
        public string Code { get; set; }
        public long AutoIncreament { get; set; }
        [MaxLength(255)]
        public string DOSalesNo { get; set; }
        [MaxLength(255)]
        public string DOSalesType { get; set; }
        public DateTimeOffset DOSalesDate { get; set; }

        /* Storage */
        public int StorageId { get; set; }
        [MaxLength(255)]
        public string StorageName { get; set; }
        [MaxLength(255)]
        public string StorageDivision { get; set; }
        [MaxLength(255)]
        public string HeadOfStorage { get; set; }


        /* Production Order */
        public int ProductionOrderId { get; set; }
        [MaxLength(255)]
        public string ProductionOrderNo { get; set; }

        /* Material */
        public int MaterialId { get; set; }
        [MaxLength(255)]
        public string Material { get; set; }
        [MaxLength(255)]
        public string MaterialWidthFinish { get; set; }

        /* Material Construction */
        public int MaterialConstructionFinishId { get; set; }
        [MaxLength(255)]
        public string MaterialConstructionFinishName { get; set; }

        /* Buyer */
        public int BuyerId { get; set; }
        [MaxLength(255)]
        public string BuyerCode { get; set; }
        [MaxLength(255)]
        public string BuyerName { get; set; }
        [MaxLength(1000)]
        public string BuyerAddress { get; set; }
        [MaxLength(255)]
        public string BuyerType { get; set; }
        [MaxLength(255)]
        public string BuyerNPWP { get; set; }

        /*Destination Buyer */
        public int DestinationBuyerId { get; set; }
        [MaxLength(255)]
        public string DestinationBuyerCode { get; set; }
        [MaxLength(255)]
        public string DestinationBuyerName { get; set; }
        [MaxLength(1000)]
        public string DestinationBuyerAddress { get; set; }
        [MaxLength(255)]
        public string DestinationBuyerType { get; set; }
        [MaxLength(255)]
        public string DestinationBuyerNPWP { get; set; }

        /* Uom */
        [MaxLength(255)]
        public string PackingUom { get; set; }
        [MaxLength(255)]
        public string LengthUom { get; set; }

        /* Footer Information */
        public int Disp { get; set; }
        public int Op { get; set; }
        public int Sc { get; set; }

        [MaxLength(255)]
        public string Construction { get; set; }
        [MaxLength(1000)]
        public string Remark { get; set; }

        /* Status */
        [MaxLength(255)]
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
