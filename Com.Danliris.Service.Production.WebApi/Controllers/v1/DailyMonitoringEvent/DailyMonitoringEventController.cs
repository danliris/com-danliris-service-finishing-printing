using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.DailyMonitoringEvent;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.DailyMonitoringEvent;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DailyMonitoringEvent;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Services.ValidateService;
using Com.Danliris.Service.Production.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.DailyMonitoringEvent
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/finishing-printing/daily-monitoring-event")]
    [Authorize]
    public class DailyMonitoringEventController : BaseController<DailyMonitoringEventModel, DailyMonitoringEventViewModel, IDailyMonitoringEventFacade>
    {
        public DailyMonitoringEventController(IIdentityService identityService, IValidateService validateService, IDailyMonitoringEventFacade facade, IMapper mapper) : base(identityService, validateService, facade, mapper, "1.0.0")
        {
        }

        [HttpGet("reports")]
        public IActionResult GetReport([FromHeader(Name = "x-timezone-offset")] string timezone, DateTime? dateFrom = null, DateTime? dateTo = null, int machineId = 0, string area = null)
        {
            try
            {
                int offSet = Convert.ToInt32(timezone);
                //int offSet = 7;
                var data = Facade.GetReport(dateFrom, dateTo, area, machineId, offSet);

                return Ok(new
                {
                    apiVersion = ApiVersion,
                    data,
                    info = new
                    {
                        Count = data.Count
                    },
                    message = General.OK_MESSAGE,
                    statusCode = General.OK_STATUS_CODE
                });
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
