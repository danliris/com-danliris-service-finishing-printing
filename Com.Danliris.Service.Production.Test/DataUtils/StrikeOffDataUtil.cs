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
        public StrikeOffDataUtil(StrikeOffFacade facade) : base(facade)
        {
        }

        public override StrikeOffModel GetNewData()
        {
            return new StrikeOffModel()
            {
                Code = "code",
                Cloth = "cloth",
                Type = "type",
                Remark = "remark",
                StrikeOffItems = new List<StrikeOffItemModel>()
                {
                    new StrikeOffItemModel()
                    {
                        ColorCode = "colorCode",
                        ChemicalItems = new List<StrikeOffItemChemicalItemModel>()
                        {
                            new StrikeOffItemChemicalItemModel()
                            {
                                Name = "name",
                                Quantity = 1
                            }
                        },
                        DyeStuffItems = new List<StrikeOffItemDyeStuffItemModel>()
                        {
                            new StrikeOffItemDyeStuffItemModel()
                            {
                                ProductCode = "coe",
                                ProductId = 1,
                                ProductName = "name",
                                Quantity = 1
                            }
                        }
                    }
                }
            };
        }

    }
}
