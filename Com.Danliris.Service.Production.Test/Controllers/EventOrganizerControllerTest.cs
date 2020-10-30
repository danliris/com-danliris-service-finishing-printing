using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.EventOrganizer;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.EventOrganizer;
using Com.Danliris.Service.Finishing.Printing.Test.Controller.Utils;
using Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.Master;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Controllers
{
  public  class EventOrganizerControllerTest : BaseControllerTest<EventOrganizerController, EventOrganizer, EventOrganizerViewModel, IEventOrganizerFacade>
    {
        EventOrganizerViewModel eventOrganizerViewModel
        {
            get
            {
                return new EventOrganizerViewModel()
                {

                };
            }
        }
        [Fact]
        public  async Task GetGroupArea_ReturnOK()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(f => f.ReadByGroupArea(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new EventOrganizer());
            mocks.Mapper.Setup(f => f.Map<EventOrganizerViewModel>(It.IsAny<List<EventOrganizer>>())).Returns(eventOrganizerViewModel);
            EventOrganizerController controller = GetController(mocks);
            var response = await controller.GetGroupArea("","");

            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public async Task GetGroupArea_Return_InternalServerError()
        {
            var mocks = GetMocks();
            mocks.Facade.Setup(f => f.ReadByGroupArea(It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new Exception());
            mocks.Mapper.Setup(f => f.Map<EventOrganizerViewModel>(It.IsAny<List<EventOrganizer>>())).Returns(eventOrganizerViewModel);
            EventOrganizerController controller = GetController(mocks);
            var response = await controller.GetGroupArea("", "");

            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
