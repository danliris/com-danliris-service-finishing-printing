using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.FabricQualityControl;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.FabricQualityControl;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.FabricQualityControl;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Services.ValidateService;
using Com.Danliris.Service.Production.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.FabricQualityControl
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/finishing-printing/quality-control/defect")]
    [Authorize]
    public class FabricQualityControlController : BaseController<FabricQualityControlModel, FabricQualityControlViewModel, IFabricQualityControlFacade>
    {
        public FabricQualityControlController(IIdentityService identityService, IValidateService validateService, IFabricQualityControlFacade facade, IMapper mapper) : base(identityService, validateService, facade, mapper, "1.0.0")
        {
        }
    }
}
