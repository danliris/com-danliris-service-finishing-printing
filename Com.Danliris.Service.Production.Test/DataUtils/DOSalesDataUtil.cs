using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.DOSales;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.DOSales;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DOSales;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using System;
using System.Collections.Generic;
using System.Text;
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
                Code = "Code",
                Date = DateTimeOffset.UtcNow,
                StorageId = 1,
                StorageName = "StorageName",
                ProductionOrderId = 1,
                ProductionOrderNo = "ProductionOrderNo",
                MaterialId = 1,
                Material = "Material",
                MaterialWidthFinish = "MaterialWidthFinish",
                MaterialConstructionFinishId = 1,
                MaterialConstructionFinishName = "MaterialConstructionFinishName",
                BuyerId = 1,
                BuyerCode = "BuyerCode",
                BuyerName = "BuyerName",
                BuyerAddress = "BuyerAddress",
                BuyerType = "BuyerType",
                PackingUom = "PCS",
                Construction = "Construction",
                Status = "BELUM",
                //Accepted = true,
                //Declined = false,
                DOSalesDetails = new List<DOSalesDetailModel>
                {
                    new DOSalesDetailModel()
                }
            };
            return TestData;
        }

        public DOSalesViewModel GetDataToValidate()
        {
            DOSalesViewModel TestData = new DOSalesViewModel()
            {
                UId = "UId",
                Code = "Code",
                Date = DateTimeOffset.UtcNow,
                StorageId = 1,
                StorageName = "StorageName",
                ProductionOrderId = 1,
                ProductionOrderNo = "ProductionOrderNo",
                MaterialId = 1,
                Material = "Material",
                MaterialWidthFinish = "MaterialWidthFinish",
                MaterialConstructionFinishId = 1,
                MaterialConstructionFinishName = "MaterialConstructionFinishName",
                BuyerId = 1,
                BuyerCode = "BuyerCode",
                BuyerName = "BuyerName",
                BuyerAddress = "BuyerAddress",
                BuyerType = "BuyerType",
                PackingUom = "PCS",
                Construction = "Construction",
                Status = "BELUM",
                //Accepted = true,
                //Declined = false,
                DOSalesDetails = new List<DOSalesDetailViewModel>
                {
                    new DOSalesDetailViewModel()
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
