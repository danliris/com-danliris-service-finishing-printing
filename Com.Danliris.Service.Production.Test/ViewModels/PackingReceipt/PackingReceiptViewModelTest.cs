using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.PackingReceipt;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.PackingReceipt
{
    public class PackingReceiptViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            var date = DateTimeOffset.Now;
            var storage = new StorageIntegrationViewModel();
            var items = new List<PackingReceiptItemViewModel>()
            {

            };
            PackingReceiptViewModel viewModel = new PackingReceiptViewModel()
            {
                UId = "UId",
                Type = "Type",
                ReferenceNo = "ReferenceNo",
                ReferenceType = "ReferenceType",
                Remark = "Remark",
                Accepted =true,
                IsVoid =true,
                Buyer = "Buyer",
                Code ="Code",
                ColorName = "ColorName",
                ColorType = "ColorType",
                Construction = "Construction",
                Date = date,
                DesignCode = "DesignCode",
                Declined =true,
                DesignNumber = "DesignNumber",
                MaterialWidthFinish = "MaterialWidthFinish",
                OrderType = "OrderType",
                PackingCode = "PackingCode",
                PackingId =1,
                PackingUom = "PackingUom",
                ProductionOrderNo = "ProductionOrderNo",
                Storage = storage,
                Items = items
            };

            Assert.True(viewModel.Accepted);
            Assert.True(viewModel.IsVoid);
            Assert.Equal("UId", viewModel.UId);
            Assert.Equal("ReferenceNo", viewModel.ReferenceNo);
            Assert.Equal("ReferenceType", viewModel.ReferenceType);
            Assert.Equal("Remark", viewModel.Remark);
            Assert.Equal("Type", viewModel.Type);
            Assert.Equal("Buyer", viewModel.Buyer);
            Assert.Equal("Code", viewModel.Code);
            Assert.Equal("ColorName", viewModel.ColorName);
            Assert.Equal("ColorType", viewModel.ColorType);
            Assert.Equal("Construction", viewModel.Construction);
            Assert.Equal(date, viewModel.Date);
            Assert.Equal("DesignCode", viewModel.DesignCode);
            Assert.True(viewModel.Declined);
            Assert.Equal("DesignNumber", viewModel.DesignNumber);
            Assert.Equal("MaterialWidthFinish", viewModel.MaterialWidthFinish);
            Assert.Equal("OrderType", viewModel.OrderType);
            Assert.Equal("PackingCode", viewModel.PackingCode);
            Assert.Equal(1, viewModel.PackingId);
            Assert.Equal("PackingUom", viewModel.PackingUom);
            Assert.Equal("ProductionOrderNo", viewModel.ProductionOrderNo);
            Assert.Equal(storage, viewModel.Storage);
            Assert.Equal(items, viewModel.Items);
        }

        [Fact]
        public void validate_default()
        {

            PackingReceiptViewModel viewModel = new PackingReceiptViewModel()
            {
              
                Items = new List<PackingReceiptItemViewModel>()
                {

                },
                //Date =DateTimeOffset.MinValue
            };

            var result = viewModel.Validate(null);
            Assert.True(0 < result.Count());
        }
    }
}
