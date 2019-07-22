using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.OrderStatusReport;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.OrderStatusReport
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/order-status-reports")]
    [Authorize]
    public class OrderStatusController : Controller
    {
        private readonly IIdentityService _identityService;
        private readonly IOrderStatusReportService _service;
        private readonly string _apiVersion = "1.0";

        public OrderStatusController(IIdentityService identityService, IOrderStatusReportService service)
        {
            _identityService = identityService;
            _service = service;
        }

        protected void VerifyUser()
        {
            _identityService.Username = User.Claims.ToArray().SingleOrDefault(p => p.Type.Equals("username")).Value;
            _identityService.Token = Request.Headers["Authorization"].FirstOrDefault().Replace("Bearer ", "");
            _identityService.TimezoneOffset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
        }

        [HttpGet("yearly")]
        public async Task<IActionResult> GetYearlyReport([FromQuery] int year = 0, [FromQuery] int orderTypeId = 0)
        {
            try
            {
                VerifyUser();

                if (year == 0)
                    year = DateTime.UtcNow.AddHours(_identityService.TimezoneOffset).Year;

                var result = await _service.GetYearlyOrderStatusReport(year, orderTypeId);

                return Ok(new
                {
                    apiVersion = _apiVersion,
                    data = result,
                    statusCode = General.OK_STATUS_CODE
                });
            }
            catch (Exception e)
            {
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, new
                {
                    apiVersion = _apiVersion,
                    message = e.Message,
                    statusCode = General.INTERNAL_ERROR_STATUS_CODE
                });
            }
        }

        [HttpGet("monthly")]
        public async Task<IActionResult> GetMonthlyReport([FromQuery] int year = 0, [FromQuery] int month = 0, [FromQuery] int orderTypeId = 0)
        {
            try
            {
                VerifyUser();

                if (year == 0)
                    year = DateTime.UtcNow.AddHours(_identityService.TimezoneOffset).Year;

                if (month == 0)
                    month = DateTime.UtcNow.AddHours(_identityService.TimezoneOffset).Month;

                var result = await _service.GetMonthlyOrderStatusReport(year, month, orderTypeId);

                return Ok(new
                {
                    apiVersion = _apiVersion,
                    data = result,
                    statusCode = General.OK_STATUS_CODE
                });
            }
            catch (Exception e)
            {
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, new
                {
                    apiVersion = _apiVersion,
                    message = e.Message,
                    statusCode = General.INTERNAL_ERROR_STATUS_CODE
                });
            }
        }

        [HttpGet("by-order-id")]
        public async Task<IActionResult> GetByOrderId([FromQuery] int orderId = 0)
        {
            try
            {
                VerifyUser();

                var result = await _service.GetProductionOrderStatusReport(orderId);

                return Ok(new
                {
                    apiVersion = _apiVersion,
                    data = new
                    {
                        data = result,
                        histories = new dynamic[] { }
                    },
                    statusCode = General.OK_STATUS_CODE
                });
            }
            catch (Exception e)
            {
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, new
                {
                    apiVersion = _apiVersion,
                    message = e.Message,
                    statusCode = General.INTERNAL_ERROR_STATUS_CODE
                });
            }
        }
    }
}