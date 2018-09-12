using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.DailyOperation;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Daily_Operation;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Daily_Operation;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Services.ValidateService;
using Com.Danliris.Service.Production.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.DailyOperation
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/finishing-printing/daily-operations")]
    [Authorize]
    public class DailyOperationController : BaseController<DailyOperationModel, DailyOperationViewModel, IDailyOperationFacade>
    {
        public DailyOperationController(IIdentityService identityService, IValidateService validateService, IDailyOperationFacade facade, IMapper mapper) : base(identityService, validateService, facade, mapper, "1.0.0")
        {

        }

        [HttpGet("reports")]
        public IActionResult GetReport(DateTimeOffset? dateFrom = null, DateTimeOffset? dateTo = null, int kanban = -1, int machine = -1, int page = 1, int size = 25)
        {
            try
            {
                var data = Facade.GetReport(page, size, kanban, machine, dateFrom, dateTo);

                return Ok(new
                {
                    apiVersion = ApiVersion,
                    data = data.Data,
                    info = new
                    {
                        Count = data.Count,
                        Orded = data.Order,
                        Selected = data.Selected
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
