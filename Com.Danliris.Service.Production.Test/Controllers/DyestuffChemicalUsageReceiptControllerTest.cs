using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.DyestuffChemicalUsageReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.DyestuffChemicalUsageReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DyestuffChemicalUsageReceipt;
using Com.Danliris.Service.Finishing.Printing.Test.Controller.Utils;
using Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.DyestuffChemicalReceiptUsage;
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
    public class DyestuffChemicalUsageReceiptControllerTest : BaseControllerTest<DyestuffChemicalUsageReceiptController, DyestuffChemicalUsageReceiptModel, DyestuffChemicalUsageReceiptViewModel, IDyestuffChemicalUsageReceiptFacade>
    {
        [Fact]
        public async Task GetReportPdf_WithoutException_ReturnOK()
        {
            var mockFacade = new Mock<IDyestuffChemicalUsageReceiptFacade>();
            DyestuffChemicalUsageReceiptModel model = new DyestuffChemicalUsageReceiptModel()
            {
                DyestuffChemicalUsageReceiptItems = new List<DyestuffChemicalUsageReceiptItemModel>()
                {
                    new DyestuffChemicalUsageReceiptItemModel()
                    {
                        ReceiptDate = DateTimeOffset.UtcNow,
                        Adjs1Date = DateTimeOffset.UtcNow,
                        Adjs2Date = DateTimeOffset.UtcNow,
                        Adjs3Date = DateTimeOffset.UtcNow,
                        Adjs4Date = DateTimeOffset.UtcNow,
                        DyestuffChemicalUsageReceiptItemDetails = new List<DyestuffChemicalUsageReceiptItemDetailModel>()
                        {
                            new DyestuffChemicalUsageReceiptItemDetailModel()
                            {
                                Name = "test",
                                Adjs1Quantity = 1,
                                Adjs2Quantity = 1,
                                Adjs3Quantity = 3,
                                Adjs4Quantity = 4,
                                ReceiptQuantity = 2
                            },
                            new DyestuffChemicalUsageReceiptItemDetailModel()
                            {
                                Name = "Viscositas",
                                ReceiptQuantity = 1
                            }
                        }
                    },
                    new DyestuffChemicalUsageReceiptItemModel()
                    {
                        DyestuffChemicalUsageReceiptItemDetails = new List<DyestuffChemicalUsageReceiptItemDetailModel>()
                        {
                            new DyestuffChemicalUsageReceiptItemDetailModel()
                            {
                                Name = "test"
                            },
                            new DyestuffChemicalUsageReceiptItemDetailModel()
                            {
                                Name = "Viscositas"
                            }
                        }
                    }
                }
            };
            mockFacade.Setup(f => f.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(model);

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();


            var controller = GetController((mockIdentityService, mockValidateService, mockFacade, mockMapper));
            var response = await controller.GetPdfById(It.IsAny<int>(), "7");
            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetReportPdf_WithoutException_ReturnOK_Repeat()
        {
            var mockFacade = new Mock<IDyestuffChemicalUsageReceiptFacade>();
            DyestuffChemicalUsageReceiptModel model = new DyestuffChemicalUsageReceiptModel()
            {
                RepeatedProductionOrderNo = "no",
                DyestuffChemicalUsageReceiptItems = new List<DyestuffChemicalUsageReceiptItemModel>()
                {
                    new DyestuffChemicalUsageReceiptItemModel()
                    {
                        ReceiptDate = DateTimeOffset.UtcNow,
                        Adjs1Date = DateTimeOffset.UtcNow,
                        Adjs2Date = DateTimeOffset.UtcNow,
                        Adjs3Date = DateTimeOffset.UtcNow,
                        Adjs4Date = DateTimeOffset.UtcNow,
                        DyestuffChemicalUsageReceiptItemDetails = new List<DyestuffChemicalUsageReceiptItemDetailModel>()
                        {
                            new DyestuffChemicalUsageReceiptItemDetailModel()
                            {
                                Name = "test",
                                Adjs1Quantity = 1,
                                Adjs2Quantity = 1,
                                Adjs3Quantity = 3,
                                Adjs4Quantity = 4,
                                ReceiptQuantity = 2
                            },
                            new DyestuffChemicalUsageReceiptItemDetailModel()
                            {
                                Name = "Viscositas",
                                ReceiptQuantity = 1
                            }
                        }
                    },
                    new DyestuffChemicalUsageReceiptItemModel()
                    {
                        DyestuffChemicalUsageReceiptItemDetails = new List<DyestuffChemicalUsageReceiptItemDetailModel>()
                        {
                            new DyestuffChemicalUsageReceiptItemDetailModel()
                            {
                                Name = "test"
                            },
                            new DyestuffChemicalUsageReceiptItemDetailModel()
                            {
                                Name = "Viscositas"
                            }
                        }
                    }
                }
            };
            mockFacade.Setup(f => f.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(model);

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();


            var controller = GetController((mockIdentityService, mockValidateService, mockFacade, mockMapper));
            var response = await controller.GetPdfById(It.IsAny<int>(), "7");
            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetReportPdf_WithoutException_ReturnNotFound()
        {
            var mockFacade = new Mock<IDyestuffChemicalUsageReceiptFacade>();
            mockFacade.Setup(f => f.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(default(DyestuffChemicalUsageReceiptModel));

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();

            var controller = GetController((mockIdentityService, mockValidateService, mockFacade, mockMapper));
            var response = await controller.GetPdfById(It.IsAny<int>(), "7");
            Assert.Equal((int)HttpStatusCode.NotFound, GetStatusCode(response));


        }

        [Fact]
        public async Task GetReportPdf_Exception_InternalServer()
        {
            var mockFacade = new Mock<IDyestuffChemicalUsageReceiptFacade>();
            mockFacade.Setup(f => f.ReadByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception("err:"));

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();

            var controller = GetController((mockIdentityService, mockValidateService, mockFacade, mockMapper));
            var response = await controller.GetPdfById(It.IsAny<int>(), "7");
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task GetByStrikeOff_WithoutException_ReturnOK()
        {
            var mockFacade = new Mock<IDyestuffChemicalUsageReceiptFacade>();
            mockFacade.Setup(f => f.GetDataByStrikeOff(It.IsAny<int>())).ReturnsAsync(new Tuple<DyestuffChemicalUsageReceiptModel, string>(Model, ""));

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();

            var controller = GetController((mockIdentityService, mockValidateService, mockFacade, mockMapper));
            var response = await controller.GetDataByStrikeOff(It.IsAny<int>());
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));

        }

        [Fact]
        public async Task GetByStrikeOff_WithoutException_ReturnOK_Null()
        {
            var mockFacade = new Mock<IDyestuffChemicalUsageReceiptFacade>();
            mockFacade.Setup(f => f.GetDataByStrikeOff(It.IsAny<int>())).ReturnsAsync(new Tuple<DyestuffChemicalUsageReceiptModel, string>(null, null));

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();

            var controller = GetController((mockIdentityService, mockValidateService, mockFacade, mockMapper));
            var response = await controller.GetDataByStrikeOff(It.IsAny<int>());
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));

        }

        [Fact]
        public async Task GetByStrikeOff_Exception_InternalServer()
        {
            var mockFacade = new Mock<IDyestuffChemicalUsageReceiptFacade>();
            mockFacade.Setup(f => f.GetDataByStrikeOff(It.IsAny<int>())).Throws(new Exception());

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();

            var controller = GetController((mockIdentityService, mockValidateService, mockFacade, mockMapper));
            var response = await controller.GetDataByStrikeOff(It.IsAny<int>());
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));

        }
    }
}
