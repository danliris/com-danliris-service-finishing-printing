using Com.Danliris.Service.Finishing.Printing.Lib.Models.Packing;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Packing
{
    public class PackingViewModel : BaseViewModel, IValidatableObject
    {
        public string UId { get; set; }
        [StringLength(25)]
        public string Code { get; set; }
        public int? ProductionOrderId { get; set; }
        [StringLength(25)]
        public string ProductionOrderNo { get; set; }

        public int? OrderTypeId { get; set; }
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

        public int? BuyerId { get; set; }
        [StringLength(25)]
        public string BuyerCode { get; set; }
        [StringLength(250)]
        public string BuyerName { get; set; }
        [StringLength(250)]
        public string BuyerAddress { get; set; }
        [StringLength(25)]
        public string BuyerType { get; set; }

        public DateTimeOffset? Date { get; set; }
        [StringLength(25)]
        public string PackingUom { get; set; }
        [StringLength(250)]
        public string ColorCode { get; set; }
        [StringLength(250)]
        public string ColorName { get; set; }
        [StringLength(250)]
        public string ColorType { get; set; }

        public int? MaterialConstructionFinishId { get; set; }
        [StringLength(250)]
        public string MaterialConstructionFinishName { get; set; }

        public int? MaterialId { get; set; }
        [StringLength(25)]
        public string Material { get; set; }
        [StringLength(25)]
        public string MaterialWidthFinish { get; set; }

        [StringLength(300)]
        public string Construction { get; set; }

        [StringLength(25)]
        public string DeliveryType { get; set; }
        [StringLength(25)]
        public string FinishedProductType { get; set; }

        [StringLength(250)]
        public string Motif { get; set; }
        [StringLength(25)]
        public string Status { get; set; }
        public bool? Accepted { get; set; }
        public bool? Declined { get; set; }

        public ICollection<PackingDetailViewModel> PackingDetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(ProductionOrderNo))
                yield return new ValidationResult("Nomor Order harus diisi", new List<string> { "ProductionOrder" });

            if (string.IsNullOrWhiteSpace(DeliveryType))
                yield return new ValidationResult("Jenis Pengiriman harus diisi", new List<string> { "DeliveryType" });

            if (string.IsNullOrWhiteSpace(FinishedProductType))
                yield return new ValidationResult("Jenis Barang Jadi harus diisi", new List<string> { "FinishedProductType" });

            if (!BuyerId.HasValue || BuyerId.Value.Equals(0))
                yield return new ValidationResult("Tujuan Pengiriman harus diisi", new List<string> { "Buyer" });

            if (!MaterialConstructionFinishId.HasValue || MaterialConstructionFinishId.Value.Equals(0))
                yield return new ValidationResult("Konstruksi Finish harus diisi", new List<string> { "MaterialConstructionFinish" });

            if (!Date.HasValue || Date.Value > DateTimeOffset.Now)
                yield return new ValidationResult("Tanggal Penyerahan harus diisi atau lebih kecil sama dengan hari ini", new List<string> { "Date" });

            if (string.IsNullOrWhiteSpace(MaterialWidthFinish))
                yield return new ValidationResult("Lebar Finish harus diisi", new List<string> { "MaterialWidthFinish" });

            if (string.IsNullOrWhiteSpace(PackingUom))
                yield return new ValidationResult("Satuan harus diisi", new List<string> { "PackingUom" });

            int Count = 0;
            string DetailErrors = "[";

            if (PackingDetails != null && PackingDetails.Count > 0)
            {
                foreach (var detail in PackingDetails)
                {
                    DetailErrors += "{";

                    var rowErrorCount = 0;

                    if (string.IsNullOrWhiteSpace(detail.Lot))
                    {
                        Count++;
                        rowErrorCount++;
                        DetailErrors += "Lot : 'Lot harus diisi',";
                    }

                    if (!detail.Quantity.HasValue || detail.Quantity.Value <= 0)
                    {
                        Count++;
                        rowErrorCount++;
                        DetailErrors += "Quantity : 'Kuantitas harus lebih besar dari 0',";
                    }
                    if (!detail.Length.HasValue || detail.Length.Value <= 0)
                    {
                        Count++;
                        rowErrorCount++;
                        DetailErrors += "Length : 'Panjang harus lebih besar dari 0',";
                    }
                    if (string.IsNullOrWhiteSpace(detail.Grade))
                    {
                        Count++;
                        rowErrorCount++;
                        DetailErrors += "Grade : 'Grade harus diisi',";
                    }

                    if (rowErrorCount == 0)
                    {
                        var duplicateDetails = PackingDetails.Where(f => f.Grade.Equals(detail.Grade) && f.Lot.Equals(detail.Lot) && f.Length.GetValueOrDefault().Equals(detail.Length.GetValueOrDefault())).ToList();

                        if (duplicateDetails.Count > 1)
                        {
                            Count++;
                            DetailErrors += "Grade : 'Lot, Grade, dan Panjang tidak boleh duplikat',";
                            DetailErrors += "Length : 'Lot, Grade, dan Panjang tidak boleh duplikat',";
                            DetailErrors += "Lot : 'Lot, Grade, dan Panjang tidak boleh duplikat',";
                        }
                    }


                    //var duplicate = PackingDetails.Where(w => w.Grade.Equals(detail.Grade) && w.Lot.Equals(detail.Lot) && w.Length.HasValue ? w.Length.Value.Equals(detail.Length.Value) : true).FirstOrDefault();

                    //if (!duplicate.Equals(null))
                    //{
                    //    Count++;
                    //    DetailErrors += "Lot : 'Lot duplikat',";
                    //    DetailErrors += "Grade : 'Grade duplikat',";
                    //    DetailErrors += "Length : 'Panjang duplikat'";
                    //}
                    DetailErrors += "}, ";
                }
            }
            else
            {
                yield return new ValidationResult("Detail harus diisi", new List<string> { "PackingDetail" });
            }

            DetailErrors += "]";

            if (Count > 0)
                yield return new ValidationResult(DetailErrors, new List<string> { "PackingDetails" });
            //else
            //    foreach (var detail in PackingDetails)
            //    {
            //        var duplicateDetail = PackingDetails.FirstOrDefault(f => f.Lot.Equals(detail.Lot) && f.Grade.Equals(detail.Grade) && f.Length.Value.Equals())
            //    }
        }
    }
}
