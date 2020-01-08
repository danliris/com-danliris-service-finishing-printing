using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.DOSales;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.DOSales;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Test.DataUtils
{
    public class DOSalesDataUtil : BaseDataUtil<DOSalesFacade, DOSalesModel>
    {
        private readonly DOSalesFacade Service;

        public DOSalesDataUtil(DOSalesFacade facade) : base(facade)
        {
            Service = facade;
        }

        public override DOSalesModel GetNewData()
        {
            DOSalesModel TestData = new DOSalesModel()
            {
                UId = "UId",
                Code = "code",
                AutoIncreament = 1,
                DOSalesNo = "DOSalesNo",
                DOSalesType = "DOSalesType",
                DOSalesDate = DateTimeOffset.UtcNow,
                StorageId = 1,
                StorageName = "StorageName",
                HeadOfStorage = "HeadOfStorage",
                ProductionOrderId = 1,
                ProductionOrderNo = "ProductionOrderNo",
                MaterialId = 1,
                Material = "Material",
                MaterialWidthFinish = "MaterialWidthFinish",
                MaterialConstructionFinishId = 1,
                MaterialConstructionFinishName = "MaterialConstructionFinishName",
                BuyerAddress = "BuyerAddress",
                BuyerCode = "BuyerCode",
                BuyerId = 1,
                BuyerName = "BuyerName",
                BuyerNPWP = "BuyerNPWP",
                BuyerType = "BuyerType",
                DestinationBuyerAddress = "DestinationBuyerAddress",
                DestinationBuyerCode = "DestinationBuyerCode",
                DestinationBuyerId = 1,
                DestinationBuyerName = "DestinationBuyerName",
                DestinationBuyerNPWP = "DestinationBuyerNPWP",
                DestinationBuyerType = "DestinationBuyerType",
                PackingUom = "PackingUom",
                ImperialUom = "ImperialUom",
                MetricUom = "MetricUom",
                Disp = 1,
                Op = 1,
                Sc = 1,
                Construction = "Construction",
                Remark = "Remark",
                Status = "Status",
                //Accepted = false,
                //Declined = true,

                DOSalesDetails = new List<DOSalesDetailModel>
                {
                    new DOSalesDetailModel()
                    {
                        UnitName = "UnitName",
                        UnitCode = "UnitCode",
                        PackingQuantity = 1,
                        ImperialQuantity = 1,
                        MetricQuantity = 1
                    }
                }
            };
            return TestData;
        }

        public new async Task<DOSalesModel> GetTestData()
        {
            DOSalesModel model = GetNewData();
            await Service.CreateAsync(model);
            return await Service.ReadByIdAsync(model.Id);
        }
    }
}
