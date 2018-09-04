using Com.Danliris.Service.Finishing.Printing.Lib.Models.Packing;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Packing
{
    public class PackingViewModel : BaseViewModel, IValidatableObject
    {
        [StringLength(25)]
        public string Code { get; set; }
        public int ProductionOrderId { get; set; }
        [StringLength(25)]
        public string ProductionOrderNo { get; set; }

        public int OrderTypeId { get; set; }
        [StringLength(25)]
        public string OrderTypeCode { get; set; }
        [StringLength(25)]
        public string OrderTypeName { get; set; }

        [StringLength(25)]
        public string SalesContractNo { get; set; }
        [StringLength(250)]
        public string DesignCode { get; set; }
        [StringLength(250)]
        public string DesignNumber { get; set; }

        public int BuyerId { get; set; }
        [StringLength(25)]
        public string BuyerCode { get; set; }
        [StringLength(250)]
        public string BuyerName { get; set; }
        [StringLength(250)]
        public string BuyerAddress { get; set; }
        [StringLength(25)]
        public string BuyerType { get; set; }

        public DateTimeOffset Date { get; set; }
        [StringLength(25)]
        public string PackingUom { get; set; }
        [StringLength(250)]
        public string ColorCode { get; set; }
        [StringLength(250)]
        public string ColorName { get; set; }
        [StringLength(250)]
        public string ColorType { get; set; }

        public int MaterialConstructionFinishId { get; set; }
        [StringLength(250)]
        public string MaterialConstructionFinishName { get; set; }

        public int MaterialId { get; set; }
        [StringLength(25)]
        public string Material { get; set; }
        [StringLength(25)]
        public string MaterialWidthFinish { get; set; }

        [StringLength(300)]
        public string Construction { get { return string.IsNullOrWhiteSpace(this.Construction) ? 
                    this.Construction : string.Format("{0} / {1} / {2}", Material, MaterialConstructionFinishName, MaterialWidthFinish); }
            set { this.Construction = value; } }

        [StringLength(25)]
        public string DeliveryType { get; set; }
        [StringLength(25)]
        public string FinishedProductType { get; set; }

        [StringLength(250)]
        public string Motif { get; set; }
        [StringLength(25)]
        public string Status { get; set; }
        public bool Accepted { get; set; }
        public bool Declined { get; set; }

        public ICollection<PackingDetailViewModel> PackingDetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(ProductionOrderNo))
                yield return new ValidationResult("ProductionOrderNo harus diisi", new List<string> { "ProductionOrderNo" });
        }
    }
}
