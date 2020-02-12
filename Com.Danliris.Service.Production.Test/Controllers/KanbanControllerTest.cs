using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Kanban;
using Com.Danliris.Service.Finishing.Printing.Test.Controller.Utils;
using Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.Kanban;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Services.ValidateService;
using Com.Danliris.Service.Production.Lib.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Controllers
{
    public class KanbanControllerTest : BaseControllerTest<KanbanController, KanbanModel, KanbanViewModel, IKanbanFacade>
    {
        [Fact]
        public void GetReport_WithoutException_ReturnOK()
        {
            var mockFacade = new Mock<IKanbanFacade>();
            mockFacade.Setup(x => x.GetReport(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool?>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<int>()))
                .Returns(new ReadResponse<KanbanViewModel>(new List<KanbanViewModel>(), 0, new Dictionary<string, string>(), new List<string>()));

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();

            KanbanController controller = new KanbanController(mockIdentityService.Object, mockValidateService.Object, mockFacade.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = $"{It.IsAny<int>()}";

            var response = controller.GetReport(It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<bool?>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>());
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void GetReport_WithException_ReturnError()
        {
            var mockFacade = new Mock<IKanbanFacade>();
            mockFacade.Setup(x => x.GetReport(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool?>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<int>()))
                .Throws(new Exception());

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();

            KanbanController controller = new KanbanController(mockIdentityService.Object, mockValidateService.Object, mockFacade.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = $"{It.IsAny<int>()}";

            var response = controller.GetReport(It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<bool?>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>());
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void GetReportPdf_WithoutException_ReturnOK()
        {
            var mockFacade = new Mock<IKanbanFacade>();
            mockFacade.Setup(f => f.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();
            KanbanViewModel vm = new KanbanViewModel()
            {
                ProductionOrder = new Production.Lib.ViewModels.Integration.Sales.FinishingPrinting.ProductionOrderIntegrationViewModel()
                {
                    Material = new Lib.ViewModels.Integration.Master.MaterialIntegrationViewModel(),
                    Buyer = new Production.Lib.ViewModels.Integration.Master.BuyerIntegrationViewModel(),
                    YarnMaterial = new Lib.ViewModels.Integration.Master.YarnMaterialIntegrationViewModel()
                },
                SelectedProductionOrderDetail = new Production.Lib.ViewModels.Integration.Sales.FinishingPrinting.ProductionOrderDetailIntegrationViewModel()
                {
                    ColorType = new Production.Lib.ViewModels.Integration.Master.ColorTypeIntegrationViewModel()
                },
                Cart = new CartViewModel()
                {

                },
                Instruction = new KanbanInstructionViewModel()
                {
                    Steps = new List<KanbanStepViewModel>()
                    {
                        new KanbanStepViewModel()
                        {
                            StepIndicators = new List<Production.Lib.ViewModels.Master.Step.StepIndicatorViewModel>()
                            {
                                new Production.Lib.ViewModels.Master.Step.StepIndicatorViewModel()
                                {

                                }
                            }
                        }
                    }
                }
            };
            mockMapper.Setup(s => s.Map<KanbanViewModel>(It.IsAny<KanbanModel>())).Returns(vm);
            KanbanController controller = new KanbanController(mockIdentityService.Object, mockValidateService.Object, mockFacade.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = $"{It.IsAny<int>()}";

            var response = controller.GetPDF(It.IsAny<int>());
            Assert.NotNull(response.Result);
        }

        [Fact]
        public void GetReportPdf_GetNull()
        {
            var mockFacade = new Mock<IKanbanFacade>();
            mockFacade.Setup(f => f.ReadByIdAsync(It.IsAny<int>()))
                 .ReturnsAsync(default(KanbanModel));

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();

            KanbanController controller = new KanbanController(mockIdentityService.Object, mockValidateService.Object, mockFacade.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = $"{It.IsAny<int>()}";

            var response = controller.GetPDF(It.IsAny<int>());
            Assert.Equal((int)HttpStatusCode.NotFound, GetStatusCode(response.Result));
        }

        [Fact]
        public void GetReportPdf_WithException_ReturnError()
        {
            var mockFacade = new Mock<IKanbanFacade>();
            mockFacade.Setup(f => f.ReadByIdAsync(It.IsAny<int>()))
                 .Throws(new Exception());

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();

            KanbanController controller = new KanbanController(mockIdentityService.Object, mockValidateService.Object, mockFacade.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = $"{It.IsAny<int>()}";

            var response = controller.GetPDF(It.IsAny<int>());
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response.Result));
        }

        [Fact]
        public void GetReportExcel_WithoutException_ReturnOK()
        {
            var mockFacade = new Mock<IKanbanFacade>();
            mockFacade.Setup(x => x.GenerateExcel(It.IsAny<bool?>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<int>()))
                .Returns(new MemoryStream());

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();

            KanbanController controller = new KanbanController(mockIdentityService.Object, mockValidateService.Object, mockFacade.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = $"{It.IsAny<int>()}";

            var response = controller.GetXls(It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<bool?>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>());
            Assert.NotNull(response);

            response = controller.GetXls(DateTime.UtcNow, It.IsAny<DateTime?>(), It.IsAny<bool?>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>());
            Assert.NotNull(response);

            response = controller.GetXls(It.IsAny<DateTime?>(), DateTime.UtcNow, It.IsAny<bool?>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>());
            Assert.NotNull(response);

            response = controller.GetXls(DateTime.UtcNow, DateTime.UtcNow, It.IsAny<bool?>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>());
            Assert.NotNull(response);
        }


        [Fact]
        public async void CreateCarts_OK()
        {
            var mockFacade = new Mock<IKanbanFacade>();
            mockFacade.Setup(x => x.CreateAsync(It.IsAny<KanbanModel>()))
                .ReturnsAsync(1);

            var mockMapper = new Mock<IMapper>();

            mockMapper.Setup(x => x.Map<KanbanModel>(It.IsAny<KanbanViewModel>())).Returns(new KanbanModel());

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();

            KanbanController controller = GetController((mockIdentityService, mockValidateService, mockFacade, mockMapper));
            //controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = $"{It.IsAny<int>()}";
            KanbanCreateViewModel vm = new KanbanCreateViewModel()
            {
                Carts = new List<CartViewModel>()
                {
                    new CartViewModel()
                    {

                    }
                }
            };
            var response = await controller.Create(vm);
            Assert.Equal((int)HttpStatusCode.Created, GetStatusCode(response));
        }

        [Fact]
        public async void CreateCarts_BadRequest()
        {
            var mockFacade = new Mock<IKanbanFacade>();
            mockFacade.Setup(x => x.CreateAsync(It.IsAny<KanbanModel>()))
                .ReturnsAsync(1);

            var mockMapper = new Mock<IMapper>();

            mockMapper.Setup(x => x.Map<KanbanModel>(It.IsAny<KanbanViewModel>())).Returns(new KanbanModel());

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();
            mockValidateService.Setup(x => x.Validate(It.IsAny<KanbanCreateViewModel>())).Throws(GetServiceValidationExeption());

            KanbanController controller = GetController((mockIdentityService, mockValidateService, mockFacade, mockMapper));
            //controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = $"{It.IsAny<int>()}";
            KanbanCreateViewModel vm = new KanbanCreateViewModel()
            {
                Carts = new List<CartViewModel>()
                {
                    new CartViewModel()
                    {

                    }
                }
            };
            var response = await controller.Create(vm);
            Assert.Equal((int)HttpStatusCode.BadRequest, GetStatusCode(response));
        }

        [Fact]
        public async void CreateCarts_InternalError()
        {
            var mockFacade = new Mock<IKanbanFacade>();
            mockFacade.Setup(x => x.CreateAsync(It.IsAny<KanbanModel>()))
                .ThrowsAsync(new Exception("eer"));

            var mockMapper = new Mock<IMapper>();

            mockMapper.Setup(x => x.Map<KanbanModel>(It.IsAny<KanbanViewModel>())).Returns(new KanbanModel());

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();

            KanbanController controller = GetController((mockIdentityService, mockValidateService, mockFacade, mockMapper));
            //controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = $"{It.IsAny<int>()}";
            KanbanCreateViewModel vm = new KanbanCreateViewModel()
            {
                Carts = new List<CartViewModel>()
                {
                    new CartViewModel()
                    {

                    }
                }
            };
            var response = await controller.Create(vm);
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void GetReportExcel_WithException_ReturnError()
        {
            var mockFacade = new Mock<IKanbanFacade>();
            mockFacade.Setup(x => x.GenerateExcel(It.IsAny<bool?>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<int>()))
                .Throws(new Exception());

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();

            KanbanController controller = new KanbanController(mockIdentityService.Object, mockValidateService.Object, mockFacade.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = $"{It.IsAny<int>()}";

            var response = controller.GetXls(It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<bool?>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>());
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async void GetByOldKanban_ReturnOK()
        {
            var mockFacade = new Mock<IKanbanFacade>();
            mockFacade.Setup(x => x.ReadOldKanbanByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new KanbanModel());

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();

            KanbanController controller = new KanbanController(mockIdentityService.Object, mockValidateService.Object, mockFacade.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = $"{It.IsAny<int>()}";

            var response = await controller.GetOldKanbanById(It.IsAny<int>());
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async void GetByOldKanban_ReturnOK_NULL()
        {
            var mockFacade = new Mock<IKanbanFacade>();
            mockFacade.Setup(x => x.ReadOldKanbanByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(default(KanbanModel));

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();

            KanbanController controller = new KanbanController(mockIdentityService.Object, mockValidateService.Object, mockFacade.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = $"{It.IsAny<int>()}";

            var response = await controller.GetOldKanbanById(It.IsAny<int>());
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async void GetByOldKanban_ThrowException()
        {
            var mockFacade = new Mock<IKanbanFacade>();
            mockFacade.Setup(x => x.ReadOldKanbanByIdAsync(It.IsAny<int>()))
                .ThrowsAsync(new Exception());

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();

            KanbanController controller = new KanbanController(mockIdentityService.Object, mockValidateService.Object, mockFacade.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = $"{It.IsAny<int>()}";

            var response = await controller.GetOldKanbanById(It.IsAny<int>());
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async void UpdateIsComplete_ReturnNoContent()
        {
            var mockFacade = new Mock<IKanbanFacade>();
            mockFacade.Setup(x => x.CompleteKanban(It.IsAny<int>()))
                .ReturnsAsync(1);

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();

            KanbanController controller = new KanbanController(mockIdentityService.Object, mockValidateService.Object, mockFacade.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = $"{It.IsAny<int>()}";

            var response = await controller.UpdateIsComplete(It.IsAny<int>());
            Assert.Equal((int)HttpStatusCode.NoContent, GetStatusCode(response));
        }

        [Fact]
        public async void UpdateIsComplete_ThrowException()
        {
            var mockFacade = new Mock<IKanbanFacade>();
            mockFacade.Setup(x => x.CompleteKanban(It.IsAny<int>()))
                .ThrowsAsync(new Exception());

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();

            KanbanController controller = new KanbanController(mockIdentityService.Object, mockValidateService.Object, mockFacade.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = $"{It.IsAny<int>()}";

            var response = await controller.UpdateIsComplete(It.IsAny<int>());
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
