using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.DirectLaborCost;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.DirectLaborCost;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Services.ValidateService;
using Com.Danliris.Service.Production.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.Master
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/master/direct-labor-cost")]
    [Authorize]
    public class DirectLaborCostController : BaseController<DirectLaborCostModel, DirectLaborCostViewModel, IDirectLaborCostFacade>
    {
        public DirectLaborCostController(IIdentityService identityService, IValidateService validateService, IDirectLaborCostFacade facade, IMapper mapper) : base(identityService, validateService, facade, mapper, "1.0.0")
        {
        }

        [HttpGet("cost-calculation")]
        public async Task<IActionResult> Get(int month, int year)
        {
            try
            {
                DirectLaborCostModel model = await Facade.GetForCostCalculation(month, year);
                DirectLaborCostViewModel viewModel = new DirectLaborCostViewModel();
                if (model != null)
                {
                    viewModel = Mapper.Map<DirectLaborCostViewModel>(model);
                }
                
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, General.OK_STATUS_CODE, General.OK_MESSAGE)
                    .Ok<DirectLaborCostViewModel>(Mapper, viewModel);
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
