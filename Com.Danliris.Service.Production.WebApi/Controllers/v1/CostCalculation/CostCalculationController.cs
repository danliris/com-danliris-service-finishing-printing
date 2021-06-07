using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.CostCalculation;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.CostCalculation;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Services.ValidateService;
using Com.Danliris.Service.Production.Lib.Utilities;
using Com.Danliris.Service.Production.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.CostCalculation
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/finishing-printing/cost-calculations")]
    [Authorize]
    public class CostCalculationController : Controller
    {
        private readonly IIdentityService _identityService;
        private readonly IValidateService _validateService;
        private readonly ICostCalculationService _service;
        private const string API_VERSION = "1.0";

        public CostCalculationController(IIdentityService identityService, IValidateService validateService, ICostCalculationService service)
        {
            _identityService = identityService;
            _validateService = validateService;
            _service = service;
        }

        protected virtual void VerifyUser()
        {
            _identityService.Username = User.Claims.ToArray().SingleOrDefault(p => p.Type.Equals("username")).Value;
            _identityService.Token = Request.Headers["Authorization"].FirstOrDefault().Replace("Bearer ", "");
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int size = 25, [FromQuery] string order = "{}", [FromQuery] string keyword = null, [FromQuery] string filter = "{}")
        {
            try
            {
                var result = await _service.GetPaged(page, size, order, keyword, filter);

                return Ok(new
                {
                    apiVersion = API_VERSION,
                    statusCode = (int)HttpStatusCode.OK,
                    data = result.Data,
                    info = new
                    {
                        page,
                        size,
                        total = result.TotalData,
                        order
                    }
                });
            }
            catch (Exception e)
            {
                var errorResult = new ResultFormatter(API_VERSION, (int)HttpStatusCode.InternalServerError, e.Message + "\n" + e.StackTrace).Fail();
                return StatusCode((int)HttpStatusCode.InternalServerError, errorResult);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            try
            {
                var result = await _service.GetSingleById(id);

                if (result == null)
                {
                    var notFoundResult = new ResultFormatter(API_VERSION, (int)HttpStatusCode.NotFound, "Data Not Found!");
                    return NotFound(notFoundResult);
                }
                else
                    return Ok(new
                    {
                        apiVersion = API_VERSION,
                        statusCode = (int)HttpStatusCode.OK,
                        data = result
                    });
            }
            catch (Exception e)
            {
                var errorResult = new ResultFormatter(API_VERSION, (int)HttpStatusCode.InternalServerError, e.Message + "\n" + e.StackTrace).Fail();
                return StatusCode((int)HttpStatusCode.InternalServerError, errorResult);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CostCalculationViewModel viewModel)
        {
            try
            {
                VerifyUser();

                _validateService.Validate(viewModel);
                var createModel = viewModel.MapViewModelToCreateModel();
                var result = await _service.InsertSingle(createModel);

                return Created($"{Request.Path}/{result}", new
                {
                    apiVersion = API_VERSION,
                    statusCode = (int)HttpStatusCode.Created,
                    message = "Data Created Successfully!"
                });
            }
            catch(ServiceValidationException e)
            {
                var validationResult = new ResultFormatter(API_VERSION, (int)HttpStatusCode.BadRequest, "Data Does Not Pass Validation!").Fail(e);
                return BadRequest(validationResult);
            }
            catch (Exception e)
            {
                var errorResult = new ResultFormatter(API_VERSION, (int)HttpStatusCode.InternalServerError, e.Message + "\n" + e.StackTrace).Fail();
                return StatusCode((int)HttpStatusCode.InternalServerError, errorResult);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] CostCalculationViewModel viewModel)
        {
            try
            {
                VerifyUser();

                var existingData = await _service.IsDataExistsById(id);

                if (!existingData || id != viewModel.Id)
                {
                    var notFoundResult = new ResultFormatter(API_VERSION, (int)HttpStatusCode.NotFound, "Data Not Found!");
                    return NotFound(notFoundResult);
                }

                _validateService.Validate(viewModel);
                var result = await _service.UpdateSingle(id, viewModel);

                return NoContent();
            }
            catch (ServiceValidationException e)
            {
                var validationResult = new ResultFormatter(API_VERSION, (int)HttpStatusCode.BadRequest, "Data Does Not Pass Validation!").Fail(e);
                return BadRequest(validationResult);
            }
            catch (Exception e)
            {
                var errorResult = new ResultFormatter(API_VERSION, (int)HttpStatusCode.InternalServerError, e.Message + "\n" + e.StackTrace).Fail();
                return StatusCode((int)HttpStatusCode.InternalServerError, errorResult);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                VerifyUser();

                var existingData = await _service.IsDataExistsById(id);

                if (!existingData)
                {
                    var notFoundResult = new ResultFormatter(API_VERSION, (int)HttpStatusCode.NotFound, "Data Not Found!");
                    return NotFound(notFoundResult);
                }

                var result = await _service.DeleteSingle(id);

                return NoContent();
            }
            catch (Exception e)
            {
                var errorResult = new ResultFormatter(API_VERSION, (int)HttpStatusCode.InternalServerError, e.Message + "\n" + e.StackTrace).Fail();
                return StatusCode((int)HttpStatusCode.InternalServerError, errorResult);
            }
        }
    }
}
