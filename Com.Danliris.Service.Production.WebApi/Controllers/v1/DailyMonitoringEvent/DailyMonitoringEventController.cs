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

                if (!dateFrom.HasValue || !dateTo.HasValue || machineId == 0 || string.IsNullOrEmpty(area) || dateFrom.GetValueOrDefault() > dateTo.GetValueOrDefault())
                {
                    return NotFound(new
                    {
                        apiVersion = ApiVersion,
                        message = General.NOT_FOUND_MESSAGE,
                        statusCode = General.NOT_FOUND_STATUS_CODE
                    });
                }

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

        [HttpGet("reports/downloads/xls")]
        public IActionResult GetXls([FromHeader(Name = "x-timezone-offset")] string timezone, DateTime? dateFrom = null, DateTime? dateTo = null, int machineId = 0, string area = null)
        {
            try
            {
                if (!dateFrom.HasValue || !dateTo.HasValue || machineId == 0 || string.IsNullOrEmpty(area) || dateFrom.GetValueOrDefault() > dateTo.GetValueOrDefault())
                {
                    return NotFound(new
                    {
                        apiVersion = ApiVersion,
                        message = General.NOT_FOUND_MESSAGE,
                        statusCode = General.NOT_FOUND_STATUS_CODE
                    });
                }

                byte[] xlsInBytes;
                int offSet = Convert.ToInt32(timezone);
                var xls = Facade.GenerateExcel(dateFrom, dateTo, area, machineId, offSet);

                string fileName = string.Format("Laporan Monitoring Event {0} - {1}", dateFrom.GetValueOrDefault().ToString("dd/MM/yyyy"), dateTo.Value.ToString("dd/MM/yyyy"));

                xlsInBytes = xls.ToArray();

                var file = File(xlsInBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
                return file;
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
