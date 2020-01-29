using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.DOSales;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.DOSales;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DOSales;
using Com.Danliris.Service.Finishing.Printing.Test.Controller.Utils;
using Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.DOSales;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Controllers
{
    public class DOSalesControllerTest : BaseControllerTest<DOSalesController, DOSalesModel, DOSalesViewModel, IDOSalesFacade>
    {
        [Fact]
        public async void GetDOSalesDetail_OK()
        {
            var mocks = GetMocks();
            DOSalesModel model = new DOSalesModel()
            {
                DOSalesDetails = new List<DOSalesDetailModel>() { new DOSalesDetailModel() { DOSales = new DOSalesModel()} }

            };
            mocks.Mapper.Setup(f => f.Map<List<DOSalesViewModel>>(It.IsAny<List<DOSalesModel>>())).Returns(ViewModels);
            mocks.Facade.Setup(x => x.GetDOSalesDetail(It.IsAny<string>())).Returns(Task.FromResult(model.DOSalesDetails.FirstOrDefault()));

            var controller = GetController(mocks);

            var response = await controller.GetDOSalesDetail("");
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async void GetDOSalesDetail_NotFound()
        {
            var mocks = GetMocks();
            DOSalesModel model = new DOSalesModel()
            {
                DOSalesDetails = new List<DOSalesDetailModel>() { new DOSalesDetailModel() { DOSales = new DOSalesModel() } }

            };
            mocks.Mapper.Setup(f => f.Map<List<DOSalesViewModel>>(It.IsAny<List<DOSalesModel>>())).Returns(ViewModels);
            mocks.Facade.Setup(x => x.GetDOSalesDetail(It.IsAny<string>())).Returns(Task.FromResult(default(DOSalesDetailModel)));

            var controller = GetController(mocks);

            var response = await controller.GetDOSalesDetail("");
            Assert.Equal((int)HttpStatusCode.NotFound, GetStatusCode(response));
        }

        [Fact]
        public async void GetDOSalesDetail_Exception()
        {
            var mocks = GetMocks();
            DOSalesModel model = new DOSalesModel()
            {
                DOSalesDetails = new List<DOSalesDetailModel>() { new DOSalesDetailModel() { DOSales = new DOSalesModel() } }

            };
            mocks.Mapper.Setup(f => f.Map<List<DOSalesViewModel>>(It.IsAny<List<DOSalesModel>>())).Returns(ViewModels);
            mocks.Facade.Setup(x => x.GetDOSalesDetail(It.IsAny<string>())).Throws(new Exception(""));

            var controller = GetController(mocks);

            var response = await controller.GetDOSalesDetail("");
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void Get_PDF_Success()
        {
            var vm = new DOSalesViewModel()
            {
                DOSalesNo = "DOSalesNo",
                DOSalesDate = DateTimeOffset.UtcNow,
                HeadOfStorage = "HeadOfStorage",
                StorageName = "StorageName",
                StorageDivision = "StorageDivision",
                BuyerName = "BuyerName",
                PackingUom = "ROLL",
                LengthUom = "MTR",
                Disp = 1,
                Op = 1,
                Sc = 1,
                DestinationBuyerName = "DestinationBuyerName",
                Remark = "Remark",
                DOSalesDetails = new List<DOSalesDetailViewModel>()
                {
                    new DOSalesDetailViewModel()
                    {
                        TotalPacking = 1,
                        TotalLength = 1,
                        TotalLengthConversion = 1,
                        UnitCode = "UnitCode",
                        UnitName = "UnitName",
                        UnitRemark = "UnitRemark"
                    }
                }
            };
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(s => s.Map<DOSalesViewModel>(It.IsAny<DOSalesModel>()))
                .Returns(vm);
            var controller = GetController(mocks);
            var response = controller.GetPDF(1).Result;

            Assert.NotNull(response);

        }

        [Fact]
        public void Get_PDF_NotFound()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(default(DOSalesModel));
            var controller = GetController(mocks);
            var response = controller.GetPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.NotFound, statusCode);

        }

        [Fact]
        public void Get_PDF_Exception()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(x => x.ReadByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("error"));
            var controller = GetController(mocks);
            var response = controller.GetPDF(1).Result;

            int statusCode = this.GetStatusCode(response);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusCode);

        }
    }
}
