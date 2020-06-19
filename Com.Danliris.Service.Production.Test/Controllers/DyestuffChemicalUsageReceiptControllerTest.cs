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
                        Adjs1Date = DateTimeOffset.UtcNow,
                        Adjs2Date = DateTimeOffset.UtcNow,
                        Adjs3Date = DateTimeOffset.UtcNow,
                        Adjs4Date = DateTimeOffset.UtcNow,
                        Adjs5Date = DateTimeOffset.UtcNow,
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
            var response = await controller.GetPdfById(It.IsAny<int>(),"7");
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
    }
}
