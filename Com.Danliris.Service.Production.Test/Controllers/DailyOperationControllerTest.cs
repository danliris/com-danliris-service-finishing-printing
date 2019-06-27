using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.DailyOperation;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Daily_Operation;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Daily_Operation;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Kanban;
using Com.Danliris.Service.Finishing.Printing.Test.Controller.Utils;
using Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.DailyOperation;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Services.ValidateService;
using Com.Danliris.Service.Production.Lib.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Controllers
{
    public class DailyOperationControllerTest : BaseControllerTest<DailyOperationController, DailyOperationModel, DailyOperationViewModel, IDailyOperationFacade>
    {

        public override async Task GetById_NotNullModel_ReturnOK()
        {
            DailyOperationViewModel vm = new DailyOperationViewModel()
            {
                Kanban = new KanbanViewModel()
                {
                    CurrentStepIndex = 1,
                    Instruction = new KanbanInstructionViewModel()
                    {
                        Steps = new List<KanbanStepViewModel>()
                        {
                            new KanbanStepViewModel()
                            {
                                SelectedIndex = 1,
                                StepIndex = 0
                            }
                        }
                    }, 
                    Id = 1
                },
                Step = new Lib.ViewModels.Master.Machine.MachineStepViewModel()
                {
                    Process = "a"
                },
                Type = "output"
            };
            var kanbanStep = vm.Kanban.Instruction.Steps.FirstOrDefault()?.StepIndex;

            var mocks = GetMocks();
            mocks.Facade.Setup(f => f.ReadByIdAsync(It.IsAny<int>())).ReturnsAsync(Model);
            mocks.Mapper.Setup(s => s.Map<DailyOperationViewModel>(It.IsAny<DailyOperationModel>())).Returns(vm);
            int statusCode = await GetStatusCodeGetById(mocks);
            Assert.Equal((int)HttpStatusCode.OK, statusCode);
        }

        [Fact]
        public void GetReport_WithoutException_ReturnOK()
        {
            var mockFacade = new Mock<IDailyOperationFacade>();
            mockFacade.Setup(x => x.GetReport(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<int>()))
                .Returns(new ReadResponse<DailyOperationViewModel>(new List<DailyOperationViewModel>(), 0, new Dictionary<string, string>(), new List<string>()));

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();

            DailyOperationController controller = new DailyOperationController(mockIdentityService.Object, mockValidateService.Object, mockFacade.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = $"{It.IsAny<int>()}";

            var response = controller.GetReport(It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>());
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void GetReport_WithException_ReturnError()
        {
            var mockFacade = new Mock<IDailyOperationFacade>();
            mockFacade.Setup(x => x.GetReport(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<int>()))
                .Throws(new Exception());

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();

            DailyOperationController controller = new DailyOperationController(mockIdentityService.Object, mockValidateService.Object, mockFacade.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = $"{It.IsAny<int>()}";

            var response = controller.GetReport(It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>());
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void GetReportExcel_WithoutException_ReturnOK()
        {
            var mockFacade = new Mock<IDailyOperationFacade>();
            mockFacade.Setup(x => x.GenerateExcel(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<int>()))
                .Returns(new MemoryStream());

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();

            DailyOperationController controller = new DailyOperationController(mockIdentityService.Object, mockValidateService.Object, mockFacade.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = $"{It.IsAny<int>()}";

            var response = controller.GetXls(It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<int>(), It.IsAny<int>());
            Assert.NotNull(response);
        }

        [Fact]
        public void GetReportExcel_WithException_ReturnError()
        {
            var mockFacade = new Mock<IDailyOperationFacade>();
            mockFacade.Setup(x => x.GenerateExcel(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<int>()))
                .Throws(new Exception());

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();

            DailyOperationController controller = new DailyOperationController(mockIdentityService.Object, mockValidateService.Object, mockFacade.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = $"{It.IsAny<int>()}";

            var response = controller.GetXls(It.IsAny<DateTime?>(), It.IsAny<DateTime?>(), It.IsAny<int>(), It.IsAny<int>());
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async Task GetJoinKanban_WithoutException_ReturnOK()
        {
            var mockFacade = new Mock<IDailyOperationFacade>();
            mockFacade.Setup(x => x.GetJoinKanban(It.IsAny<string>()))
                .ReturnsAsync(new List<DailyOperationKanbanViewModel>());

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();

            DailyOperationController controller = new DailyOperationController(mockIdentityService.Object, mockValidateService.Object, mockFacade.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };

            var response = await controller.GetJoinKanbans(It.IsAny<string>());
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task GetJoinKanban_WithException_ThrowError()
        {
            var mockFacade = new Mock<IDailyOperationFacade>();
            mockFacade.Setup(x => x.GetJoinKanban(It.IsAny<string>()))
                .Throws(new Exception());

            var mockMapper = new Mock<IMapper>();

            var mockIdentityService = new Mock<IIdentityService>();

            var mockValidateService = new Mock<IValidateService>();

            DailyOperationController controller = new DailyOperationController(mockIdentityService.Object, mockValidateService.Object, mockFacade.Object, mockMapper.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };

            var response = await controller.GetJoinKanbans(It.IsAny<string>());
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
