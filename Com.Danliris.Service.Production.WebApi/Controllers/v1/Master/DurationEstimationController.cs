using AutoMapper;
using Com.Danliris.Service.Production.Lib.BusinessLogic.Interfaces.Master;
using Com.Danliris.Service.Production.Lib.Models.Master.DurationEstimation;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Services.ValidateService;
using Com.Danliris.Service.Production.Lib.ViewModels.Master.DurationEstimation;
using Com.Danliris.Service.Production.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.Master
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/master/fp-duration-estimations")]
    [Authorize]
    public class DurationEstimationController : BaseController<DurationEstimationModel, DurationEstimationViewModel, IDurationEstimationFacade>
    {
        public DurationEstimationController(IIdentityService identityService, IValidateService validateService, IDurationEstimationFacade facade, IMapper mapper) : base(identityService, validateService, facade, mapper, "1.0.0")
        {
        }

        [HttpGet("by-process-type")]
        public IActionResult GetByProcessType([FromQuery] string processTypeCode)
        {
            try
            {
                var model = Facade.ReadByProcessType(processTypeCode);


                var viewModel = new DurationEstimationViewModel();
                if (model != null)
                    viewModel = Mapper.Map<DurationEstimationViewModel>(model);

                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, General.OK_STATUS_CODE, General.OK_MESSAGE)
                    .Ok(Mapper, viewModel);
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
