using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.DOSales;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.DOSales;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Test.DataUtils
{
    public class DOSalesDataUtil : BaseDataUtil<DOSalesFacade, DOSalesModel>
    {
        public DOSalesDataUtil(DOSalesFacade facade) : base(facade)
        {
        }

        public override DOSalesModel GetNewData()
        {
            DOSalesModel model = new DOSalesModel
            {
                Date = DateTimeOffset.UtcNow,
                DOSalesDetails = new List<DOSalesDetailModel>
                {
                    new DOSalesDetailModel()
                }
            };
            return model;
        }
    }
}
