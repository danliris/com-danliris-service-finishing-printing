using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Inventory
{
    public class InventoryDocumentViewModel
    {
        public string no { get; set; }
        public string code { get; set; }
        public DateTimeOffset date { get; set; }
        public string referenceNo { get; set; }
        public string referenceType { get; set; }
        public string type { get; set; }
        public int storageId { get; set; }
        public string storageCode { get; set; }
        public string storageName { get; set; }
        public List<InventoryDocumentItemViewModel> items { get; set; }
        public string remark { get; set; }
    }
}
