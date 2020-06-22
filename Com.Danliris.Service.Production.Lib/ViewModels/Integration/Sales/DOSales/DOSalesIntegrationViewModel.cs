using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Master;
using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Sales.FinishingPrinting;
using System;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Sales.DOSales
{
    public class DOSalesIntegrationViewModel
    {
        public int? Id { get; set; }
        [MaxLength(255)]
        public string Code { get; set; }
        [MaxLength(255)]
        public string DOSalesNo { get; set; }
        [MaxLength(255)]
        public string DOSalesType { get; set; }
        [MaxLength(255)]
        public string Status { get; set; }
        public bool? Accepted { get; set; }
        public bool? Declined { get; set; }
        [MaxLength(255)]
        public string Type { get; set; }
        public DateTimeOffset? Date { get; set; }
        public BuyerIntegrationViewModel Buyer { get; set; }
        [MaxLength(255)]
        public string DestinationBuyerName { get; set; }
        [MaxLength(1000)]
        public string DestinationBuyerAddress { get; set; }
        public string SalesName { get; set; }
        [MaxLength(255)]
        public string HeadOfStorage { get; set; }
        [MaxLength(255)]
        public string PackingUom { get; set; }
        [MaxLength(255)]
        public string LengthUom { get; set; }
        [MaxLength(255)]
        public string WeightUom { get; set; }
        public int? Disp { get; set; }
        public int? Op { get; set; }
        public int? Sc { get; set; }
        public string DoneBy { get; set; }
        public double? FillEachBale { get; set; }
        [MaxLength(1000)]
        public string Remark { get; set; }

    }
}
