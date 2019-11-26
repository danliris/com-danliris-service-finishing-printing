using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DOSales
{
    public class DOSalesViewModel : BaseViewModel, IValidatableObject
    {
        [MaxLength(255)]
        public string UId { get; set; }

        [MaxLength(25)]
        public string Code { get; set; }

        public DateTimeOffset? Date { get; set; }

        public int? StorageId { get; set; }
        [MaxLength(250)]
        public string StorageName { get; set; }

        public int ProductionOrderId { get; set; }
        [MaxLength(25)]
        public string ProductionOrderNo { get; set; }

        public int? MaterialId { get; set; }
        [MaxLength(255)]
        public string Material { get; set; }
        [MaxLength(25)]
        public string MaterialWidthFinish { get; set; }

        public int? MaterialConstructionFinishId { get; set; }
        [MaxLength(250)]
        public string MaterialConstructionFinishName { get; set; }

        public int? BuyerId { get; set; }
        [MaxLength(25)]
        public string BuyerCode { get; set; }
        [MaxLength(250)]
        public string BuyerName { get; set; }
        [MaxLength(250)]
        public string BuyerAddress { get; set; }
        [MaxLength(25)]
        public string BuyerType { get; set; }


        [StringLength(25)]
        public string PackingUom { get; set; }

        [MaxLength(300)]
        public string Construction { get; set; }

        [MaxLength(25)]
        public string Status { get; set; }
        public bool Accepted { get; set; }
        public bool Declined { get; set; }

        public ICollection<DOSalesDetailViewModel> DOSalesDetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (!Date.HasValue || Date.Value > DateTimeOffset.Now)
                yield return new ValidationResult("Tanggal Pengiriman harus diisi atau lebih kecil sama dengan hari ini", new List<string> { "Date" });

            //if (!StorageId.HasValue || StorageId.Value.Equals(0))
            if (string.IsNullOrWhiteSpace(StorageName))
                yield return new ValidationResult("Gudang harus diisi", new List<string> { "Storage" });

            if (string.IsNullOrWhiteSpace(ProductionOrderNo))
                yield return new ValidationResult("Nomor SPP harus diisi", new List<string> { "ProductionOrder" });

            if (!MaterialConstructionFinishId.HasValue || MaterialConstructionFinishId.Value.Equals(0))
                yield return new ValidationResult("Konstruksi harus diisi", new List<string> { "MaterialConstructionFinish" });

            if (string.IsNullOrWhiteSpace(Material))
                yield return new ValidationResult("Material harus diisi", new List<string> { "Material" });

            if (string.IsNullOrWhiteSpace(MaterialWidthFinish))
                yield return new ValidationResult("Lebar Finish harus diisi", new List<string> { "MaterialWidthFinish" });

            if (!BuyerId.HasValue || BuyerId.Value.Equals(0))
                yield return new ValidationResult("Buyer harus diisi", new List<string> { "Buyer" });

            if (string.IsNullOrWhiteSpace(PackingUom))
                yield return new ValidationResult("Satuan harus diisi", new List<string> { "PackingUom" });

            int Count = 0;
            string DetailErrors = "[";

            if (DOSalesDetails != null && DOSalesDetails.Count > 0)
            {
                foreach (var detail in DOSalesDetails)
                {
                    DetailErrors += "{";

                    var rowErrorCount = 0;

                    if (string.IsNullOrWhiteSpace(detail.UnitName))
                    {
                        Count++;
                        rowErrorCount++;
                        DetailErrors += "UnitName : 'Nama item harus diisi',";
                    }

                    if (string.IsNullOrWhiteSpace(detail.UnitCode))
                    {
                        Count++;
                        rowErrorCount++;
                        DetailErrors += "UnitCode : 'Kode item harus diisi',";
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
                    if (string.IsNullOrWhiteSpace(detail.Remark))
                    {
                        Count++;
                        rowErrorCount++;
                        DetailErrors += "Remark : 'Remark harus diisi',";
                    }

                    if (rowErrorCount == 0)
                    {
                        var duplicateDetails = DOSalesDetails.Where(f => f.UnitName.Equals(detail.UnitName) && f.UnitCode.Equals(detail.UnitCode) && f.Length.GetValueOrDefault().Equals(detail.Length.GetValueOrDefault())).ToList();

                        if (duplicateDetails.Count > 1)
                        {
                            Count++;
                            DetailErrors += "UnitName : 'Nama Item, Kode Item, dan Panjang tidak boleh duplikat',";
                            DetailErrors += "UnitCode : Nama Item, Kode Item, dan Panjang tidak boleh duplikat',";
                            DetailErrors += "Length : 'Nama Item, Kode Item, dan Panjang tidak boleh duplikat',";
                        }
                    }
                    DetailErrors += "}, ";
                }
            }
            else
            {
                yield return new ValidationResult("Detail harus diisi", new List<string> { "DOSalesDetail" });
            }

            DetailErrors += "]";

            if (Count > 0)
                yield return new ValidationResult(DetailErrors, new List<string> { "DOSalesDetails" });

        }
    }
}