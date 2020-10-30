using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.EventOrganizer;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.EventOrganizer;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Services.ValidateService;
using Com.Danliris.Service.Production.Lib.Utilities;
using Com.Danliris.Service.Production.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.Master
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/master/event-organizer")]
    [Authorize]
    public class EventOrganizerController : BaseController<EventOrganizer, EventOrganizerViewModel, IEventOrganizerFacade>
    {
        public EventOrganizerController(IIdentityService identityService, IValidateService validateService, IEventOrganizerFacade facade, IMapper mapper) : base(identityService, validateService, facade, mapper, "1.0.0")
        {
        }

        [HttpGet("group-area")]
        public async Task<IActionResult> GetGroupArea([FromQuery] string area = null, [FromQuery] string group = null)
        {
            try
            {
                VerifyUser();
                var model =await Facade.ReadByGroupArea(area, group);

               
                var viewModel = Mapper.Map<EventOrganizerViewModel>(model);
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, General.OK_STATUS_CODE, General.OK_MESSAGE)
                    .Ok<EventOrganizerViewModel>(Mapper, viewModel);
                return Ok(Result);

               
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                     new ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, e.Message)
                     .Fail();
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }
    }
}
