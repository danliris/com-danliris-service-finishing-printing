using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.DyestuffChemicalUsageReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.DyestuffChemicalUsageReceipt;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Test.DataUtils
{
    public class DyestuffChemicalUsageReceiptDataUtil : BaseDataUtil<DyestuffChemicalUsageReceiptFacade, DyestuffChemicalUsageReceiptModel>
    {
        public DyestuffChemicalUsageReceiptDataUtil(DyestuffChemicalUsageReceiptFacade facade) : base(facade)
        {
        }

        public override DyestuffChemicalUsageReceiptModel GetNewData()
        {
            return new DyestuffChemicalUsageReceiptModel()
            {
                ProductionOrderId = 1,
                ProductionOrderMaterialConstructionId = 1,
                ProductionOrderMaterialConstructionName = "name",
                ProductionOrderMaterialId = 1,
                ProductionOrderMaterialName = "name",
                ProductionOrderMaterialWidth = "1",
                ProductionOrderOrderNo = "no",
                ProductionOrderOrderQuantity = 1,
                StrikeOffCode = "cp",
                StrikeOffId = 1,
                StrikeOffType = "type",
                RepeatedProductionOrderNo = "no",
                Date = DateTimeOffset.UtcNow,
                DyestuffChemicalUsageReceiptItems = new List<DyestuffChemicalUsageReceiptItemModel>()
                {
                    new DyestuffChemicalUsageReceiptItemModel()
                    {
                        ColorCode = "code",
                        ReceiptDate = DateTimeOffset.UtcNow,
                        Adjs1Date = DateTimeOffset.UtcNow,
                        Adjs2Date = DateTimeOffset.UtcNow,
                        Adjs3Date = DateTimeOffset.UtcNow,
                        Adjs4Date = DateTimeOffset.UtcNow,
                        DyestuffChemicalUsageReceiptItemDetails = new List<DyestuffChemicalUsageReceiptItemDetailModel>()
                        {
                            new DyestuffChemicalUsageReceiptItemDetailModel()
                            {
                                Name = "name",
                                Adjs1Quantity = 1,
                                Adjs2Quantity = 1,
                                Adjs3Quantity = 1,
                                Adjs4Quantity = 1,
                                ReceiptQuantity = 1,
                            }
                        }
                    }
                }
            };
        }
    }
}
