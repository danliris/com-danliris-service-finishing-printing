using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.ShipmentDocument;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.ShipmentDocument;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.ShipmentDocument;
using Com.Danliris.Service.Finishing.Printing.Test.Controller.Utils;
using Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.ShipmentDocument;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Services.ValidateService;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Controllers
{
    public class ShipmentContollerTest : BaseControllerTest<ShipmentDocumentController, ShipmentDocumentModel, ShipmentDocumentViewModel, IShipmentDocumentService>
    {
        [Fact]
        public void GetReportPdf_WithoutException_ReturnOK()
        {
            var mockFacade = new Mock<IShipmentDocumentService>();
            ShipmentDocumentModel model = new ShipmentDocumentModel()
            {
                Details = new List<ShipmentDocumentDetailModel>()
                {
                    new ShipmentDocumentDetailModel()
                    {
                        Items = new List<ShipmentDocumentItemModel>()
                        {
                            new ShipmentDocumentItemModel()
                            {
                                PackingReceiptItems = new List<ShipmentDocumentPackingReceiptItemModel>()
                                {
                                    new ShipmentDocumentPackingReceiptItemModel()
                                    {
                                        Quantity = 1,
                                        Length =1,
                                        Weight = 1
                                    }
                                }
                            }
                        }
                    }
                }
            };
            mockFacade.Setup(f => f.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(model);

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();


            var response = GetController((mockIdentityService, mockValidateService, mockFacade, mockMapper)).GetPdfById(It.IsAny<int>());
            Assert.NotNull(response.Result);
        }

        [Fact]
        public async Task GetReportPdf_WithoutException_ReturnNotFound()
        {
            var mockFacade = new Mock<IShipmentDocumentService>();
            mockFacade.Setup(f => f.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(default(ShipmentDocumentModel));

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();
            ShipmentDocumentViewModel vm = new ShipmentDocumentViewModel();
            mockMapper.Setup(s => s.Map<ShipmentDocumentViewModel>(It.IsAny<ShipmentDocumentModel>())).Returns(vm);

            var response = await GetController((mockIdentityService, mockValidateService, mockFacade, mockMapper)).GetPdfById(It.IsAny<int>());
            Assert.Equal((int)HttpStatusCode.NotFound, GetStatusCode(response));


        }

        [Fact]
        public async Task GetReportPdf_Exception_InternalServer()
        {
            var mockFacade = new Mock<IShipmentDocumentService>();
            mockFacade.Setup(f => f.ReadByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("err:"));

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();
            ShipmentDocumentViewModel vm = new ShipmentDocumentViewModel();
            mockMapper.Setup(s => s.Map<ShipmentDocumentViewModel>(It.IsAny<ShipmentDocumentModel>())).Returns(vm);

            var response = await GetController((mockIdentityService, mockValidateService, mockFacade, mockMapper)).GetPdfById(It.IsAny<int>());
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task GetShipmentProduct_WithoutException_ReturnOK()
        {
            var mockFacade = new Mock<IShipmentDocumentService>();
            mockFacade.Setup(f => f.GetShipmentProducts(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(new List<ShipmentDocumentPackingReceiptItemModel>());

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();
            ShipmentDocumentViewModel vm = new ShipmentDocumentViewModel();
            mockMapper.Setup(s => s.Map<ShipmentDocumentViewModel>(It.IsAny<ShipmentDocumentModel>())).Returns(vm);

            var response = await GetController((mockIdentityService, mockValidateService, mockFacade, mockMapper)).GetShipmentProducts(1, 1);
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));


        }

        [Fact]
        public async Task GetShipmentProduct_Exception_InternalServer()
        {
            var mockFacade = new Mock<IShipmentDocumentService>();
            mockFacade.Setup(f => f.GetShipmentProducts(It.IsAny<int>(), It.IsAny<int>())).ThrowsAsync(new Exception("err:"));

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();
            ShipmentDocumentViewModel vm = new ShipmentDocumentViewModel();
            mockMapper.Setup(s => s.Map<ShipmentDocumentViewModel>(It.IsAny<ShipmentDocumentModel>())).Returns(vm);

            var response = await GetController((mockIdentityService, mockValidateService, mockFacade, mockMapper)).GetShipmentProducts(It.IsAny<int>(),2);
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
