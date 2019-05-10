using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.PackingReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.PackingReceipt;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Test.DataUtils
{
    public class PackingReceiptDataUtil : BaseDataUtil<PackingReceiptFacade, PackingReceiptModel>
    {
        public PackingReceiptDataUtil(PackingReceiptFacade facade) : base(facade)
        {
        }

        public override PackingReceiptModel GetNewData()
        {
            PackingReceiptModel model = new PackingReceiptModel
            {
                Items = new List<PackingReceiptItem>
                {
                    new PackingReceiptItem()
                }
            };
            return model;
        }
    }
}
