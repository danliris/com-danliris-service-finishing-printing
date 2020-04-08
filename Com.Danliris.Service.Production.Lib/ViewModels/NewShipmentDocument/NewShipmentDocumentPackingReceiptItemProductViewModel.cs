using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.NewShipmentDocument
{
    public class NewShipmentDocumentPackingReceiptItemProductViewModel
    {
        public string ProductName { get; set; }
        public double? Quantity { get; set; }
        public string QuantityUOM { get; set; }
        public double? Total { get; set; }
        # region ProductUom
        public int? UOMId { get; set; }
        public string UOMUnit { get; set; }
        #endregion
    }
}
