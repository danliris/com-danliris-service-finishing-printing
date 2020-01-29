using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DOSales
{
    public class DOSalesViewModel : BaseViewModel, IValidatableObject
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
        public DateTimeOffset? DOSalesDate { get; set; }

        /* Storage */
        public int? StorageId { get; set; }
        [MaxLength(255)]
        public string StorageName { get; set; }
        [MaxLength(255)]
        public string StorageDivision { get; set; }
        [MaxLength(255)]
        public string HeadOfStorage { get; set; }


        /* Production Order */
        public int? ProductionOrderId { get; set; }
        [MaxLength(255)]
        public string ProductionOrderNo { get; set; }

        /* Material */
        public int? MaterialId { get; set; }
        [MaxLength(255)]
        public string Material { get; set; }
        [MaxLength(255)]
        public string MaterialWidthFinish { get; set; }

        /* Material Construction */
        public int? MaterialConstructionFinishId { get; set; }
        [MaxLength(255)]
        public string MaterialConstructionFinishName { get; set; }

        /* Buyer */
        public int? BuyerId { get; set; }
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
        public int? DestinationBuyerId { get; set; }
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
        public int? Disp { get; set; }
        public int? Op { get; set; }
        public int? Sc { get; set; }

        [MaxLength(255)]
        public string Construction { get; set; }
        [MaxLength(1000)]
        public string Remark { get; set; }

        /* Status */
        [MaxLength(255)]
        public string Status { get; set; }
        public bool Accepted { get; set; }
        public bool Declined { get; set; }

        public ICollection<DOSalesDetailViewModel> DOSalesDetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (string.IsNullOrWhiteSpace(DOSalesType))
                yield return new ValidationResult("Kode DO Penjualan harus diisi", new List<string> { "DOSalesType" });

            if (!DOSalesDate.HasValue || DOSalesDate.Value > DateTimeOffset.Now)
                yield return new ValidationResult("Tgl penjualan harus diisi atau lebih kecil dari hari ini", new List<string> { "DOSalesDate" });

            if (string.IsNullOrWhiteSpace(StorageName))
                yield return new ValidationResult("Gudang harus diisi", new List<string> { "Storage" });

            if (string.IsNullOrWhiteSpace(HeadOfStorage))
                yield return new ValidationResult("Nama Kepala Gudang harus diisi", new List<string> { "HeadOfStorage" });

            if (string.IsNullOrWhiteSpace(ProductionOrderNo))
                yield return new ValidationResult("Nomor SPP harus diisi", new List<string> { "ProductionOrder" });

            if (!MaterialConstructionFinishId.HasValue || MaterialConstructionFinishId.Value.Equals(0))
                yield return new ValidationResult("Konstruksi Finish harus diisi", new List<string> { "MaterialConstructionFinish" });

            if (string.IsNullOrWhiteSpace(DestinationBuyerName))
                yield return new ValidationResult("Buyer tujuan harus diisi", new List<string> { "Buyer" });

            if (string.IsNullOrWhiteSpace(PackingUom))
                yield return new ValidationResult("Satuan packing harus diisi", new List<string> { "PackingUom" });
            
            if (string.IsNullOrWhiteSpace(LengthUom))
                yield return new ValidationResult("Satuan panjang harus diisi", new List<string> { "LengthUom" });

            if (!Disp.HasValue || Disp.Value.Equals(0))
                yield return new ValidationResult("Disp harus diisi", new List<string> { "Disp" });

            if (!Op.HasValue || Op.Value.Equals(0))
                yield return new ValidationResult("Op harus diisi", new List<string> { "Op" });

            if (!Sc.HasValue || Sc.Value.Equals(0))
                yield return new ValidationResult("Sc harus diisi", new List<string> { "Sc" });

            int Count = 0;
            string DetailErrors = "[";

            if (DOSalesDetails != null && DOSalesDetails.Count > 0)
            {
                foreach (var detail in DOSalesDetails)
                {
                    DetailErrors += "{";

                    var rowErrorCount = 0;

                    if (string.IsNullOrWhiteSpace(detail.UnitCode))
                    {
                        Count++;
                        rowErrorCount++;
                        DetailErrors += "UnitCode : 'Kode item harus diisi',";
                    }

                    if (string.IsNullOrWhiteSpace(detail.UnitName))
                    {
                        Count++;
                        rowErrorCount++;
                        DetailErrors += "UnitName : 'Nama item harus diisi',";
                    }

                    if (detail.TotalPacking < 0)
                    {
                        Count++;
                        rowErrorCount++;
                        DetailErrors += "TotalPacking : 'Total packing harus lebih besar dari 0',";
                    }
                    if (detail.TotalLength < 0)
                    {
                        Count++;
                        rowErrorCount++;
                        DetailErrors += "TotalLength : 'Total panjang harus lebih besar dari 0',";
                    }
                    if (detail.TotalLengthConversion < 0)
                    {
                        Count++;
                        rowErrorCount++;
                        DetailErrors += "TotalLengthConversion : 'Total panjang harus diisi',";
                    }
                    if (detail.TotalPacking==0 && detail.TotalLength==0)
                    {
                        Count++;
                        rowErrorCount++;
                        DetailErrors += "TotalPacking: 'Total packing dan total panjang harus diisi',";
                        DetailErrors += "TotalLength: 'Total packing dan total panjang harus diisi',";
                    }
                
                    if (rowErrorCount == 0)
                    {
                        var duplicateDetails = DOSalesDetails.Where(f => 
                                f.UnitCode.Equals(detail.UnitCode) &&
                                f.UnitName.Equals(detail.UnitName)
                            ).ToList();

                        if (duplicateDetails.Count > 1)
                        {
                            Count++;
                            DetailErrors += "UnitCode : 'Kode Item tidak boleh duplikat',";
                            DetailErrors += "UnitName : 'ama Item tidak boleh duplikat',";
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