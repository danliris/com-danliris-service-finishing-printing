using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Inventory
{
    public class InventoryDocumentItemViewModel : BaseViewModel
    {
        public int productId { get; set; }
        public string productCode { get; set; }
        public string productName { get; set; }
        public double quantity { get; set; }
        public double stockPlanning { get; set; }
        public int uomId { get; set; }
        public string uom { get; set; }
        public string remark { get; set; }
    }
}
