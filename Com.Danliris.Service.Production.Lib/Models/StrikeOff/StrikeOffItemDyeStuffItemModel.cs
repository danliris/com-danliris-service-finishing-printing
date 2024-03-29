﻿using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.Models.StrikeOff
{
    public class StrikeOffItemDyeStuffItemModel : StandardEntity, IValidatableObject
    {
        public int ProductId { get; set; }
        [MaxLength(256)]
        public string ProductName { get; set; }
        [MaxLength(128)]
        public string ProductCode { get; set; }
        public double Quantity { get; set; }
        public string SubType { get; set; }

        public int StrikeOffItemId { get; set; }

        [ForeignKey("StrikeOffItemId")]
        public StrikeOffItemModel StrikeOffItem { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
