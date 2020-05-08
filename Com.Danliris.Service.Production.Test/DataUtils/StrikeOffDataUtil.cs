using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.StrikeOff;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.ColorReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.StrikeOff;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Test.DataUtils
{
    public class StrikeOffDataUtil : BaseDataUtil<StrikeOffFacade, StrikeOffModel>
    {
        ColorReceiptDataUtil _colorReceiptDataUtil;
        public StrikeOffDataUtil(ColorReceiptDataUtil colorReceiptDataUtil, StrikeOffFacade facade) : base(facade)
        {
            _colorReceiptDataUtil = colorReceiptDataUtil;
        }

        public override async Task<StrikeOffModel> GetNewDataAsync()
        {
            var data = await _colorReceiptDataUtil.GetTestData();
            return new StrikeOffModel()
            {
                Code = "code",
                Remark = "remark",
                StrikeOffItems = new List<StrikeOffItemModel>()
                {
                    new StrikeOffItemModel()
                    {
                        ColorReceiptColorCode = data.ColorCode,
                        ColorReceiptId = data.Id,
                        ColorReceiptItems = data.ColorReceiptItems
                    }
                }
            };
        }
    }
}
