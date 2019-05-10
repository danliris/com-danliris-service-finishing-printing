using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.MachineType;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.MachineType;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Services.ValidateService;
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
    [Route("v{version:apiVersion}/master/machine-types")]
    [Authorize]
    public class MachineTypeController : BaseController<MachineTypeModel, MachineTypeViewModel, IMachineTypeFacade>
    {
        public MachineTypeController(IIdentityService identityService, IValidateService validateService, IMachineTypeFacade Facade, IMapper mapper) : base(identityService, validateService, Facade, mapper, "1.0.0")
        {
        }
    }
}
