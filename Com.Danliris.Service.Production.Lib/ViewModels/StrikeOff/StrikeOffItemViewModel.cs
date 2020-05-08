using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.ColorReceipt;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.StrikeOff
{
    public class StrikeOffItemViewModel : BaseViewModel
    {
        public ColorReceiptViewModel ColorReceipt { get; set; }

    }
}
