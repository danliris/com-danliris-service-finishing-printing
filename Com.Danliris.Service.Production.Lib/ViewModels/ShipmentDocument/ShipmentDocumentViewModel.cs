using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.ShipmentDocument
{
    public class ShipmentDocumentViewModel : BaseViewModel, IValidatableObject
    {
        public string UId { get; set; }
        public BuyerIntegrationViewModel Buyer { get; set; }
        public string Code { get; set; }
        public string DeliveryCode { get; set; }
        public DateTimeOffset DeliveryDate { get; set; }
        public string DeliveryReference { get; set; }
        public List<ShipmentDocumentDetailViewModel> Details { get; set; }
        public bool? IsVoid { get; set; }
        public string ProductIdentity { get; set; }
        public string ShipmentNumber { get; set; }
        public StorageIntegrationViewModel Storage { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Buyer == null || Buyer.Id.Equals(0))
                yield return new ValidationResult("Buyer harus diisi", new List<string> { "Buyer" });

            if (DeliveryDate == null || DeliveryDate > DateTimeOffset.UtcNow)
                yield return new ValidationResult("Tanggal pengiriman harus diisi", new List<string> { "DeliveryDate" });

            if (string.IsNullOrWhiteSpace(DeliveryCode))
                yield return new ValidationResult("Kode pengiriman harus diisi", new List<string> { "DeliveryCode" });

            if (string.IsNullOrWhiteSpace(ProductIdentity))
                yield return new ValidationResult("Kode Produk harus diisi", new List<string> { "ProductIdentity" });

            if (string.IsNullOrWhiteSpace(ShipmentNumber))
                yield return new ValidationResult("NO. harus diisi", new List<string> { "ShipmentNumber" });

            if (Details == null || Details.Count <= 0)
                yield return new ValidationResult("Detail harus diisi", new List<string> { "Detail" });
        }
    }
}
