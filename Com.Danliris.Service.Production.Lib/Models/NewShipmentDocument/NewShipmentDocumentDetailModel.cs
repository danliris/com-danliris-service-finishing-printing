﻿using Com.Moonlay.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.NewShipmentDocument
{
    public class NewShipmentDocumentDetailModel : StandardEntity, IValidatableObject
    {
        [MaxLength(250)]
        public string ProductionOrderColorType { get; set; }
        [MaxLength(250)]
        public string ProductionOrderDesignCode { get; set; }
        [MaxLength(250)]
        public string ProductionOrderDesignNumber { get; set; }
        public int ProductionOrderId { get; set; }
        [MaxLength(250)]
        public string ProductionOrderNo { get; set; }
        [MaxLength(250)]
        public string ProductionOrderType { get; set; }
        public int ShipmentDocumentId { get; set; }
        [ForeignKey("ShipmentDocumentId")]
        public virtual NewShipmentDocumentModel NewShipmentDocument { get; set; }
        public ICollection<NewShipmentDocumentItemModel> Items { get; set; }

        public int DetailIndex { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new System.NotImplementedException();
        }
    }
}
