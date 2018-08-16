using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.MonitoringEvent;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Monitoring_Event;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Monitoring_Event;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Services.ValidateService;
using Com.Danliris.Service.Production.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.MonitoringEvent
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/finishing-printing/monitoring-event")]
    [Authorize]
    public class MonitoringEventController : BaseController<MonitoringEventModel, MonitoringEventViewModel, IMonitoringEventFacade>
    {

        public MonitoringEventController(IIdentityService identityService, IValidateService validateService, IMonitoringEventFacade facade, IMapper mapper) : base(identityService, validateService, facade, mapper, "1.0.0")
        {
        }
    }
}
