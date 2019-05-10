using AutoMapper;
using Com.Danliris.Service.Production.Lib.BusinessLogic.Interfaces.Master;
using Com.Danliris.Service.Production.Lib.Models.Master.DurationEstimation;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Services.ValidateService;
using Com.Danliris.Service.Production.Lib.ViewModels.Master.DurationEstimation;
using Com.Danliris.Service.Production.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    }
}
