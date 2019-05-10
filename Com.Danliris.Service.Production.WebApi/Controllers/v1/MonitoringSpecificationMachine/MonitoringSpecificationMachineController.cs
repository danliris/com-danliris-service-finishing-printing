using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.MonitoringSpecificationMachine;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Monitoring_Specification_Machine;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Monitoring_Specification_Machine;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Services.ValidateService;
using Com.Danliris.Service.Production.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.MonitoringSpecificationMachine
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/finishing-printing/monitoring-specification-machine")]
    [Authorize]
    public class MonitoringSpecificationMachineController : BaseController<MonitoringSpecificationMachineModel, MonitoringSpecificationMachineViewModel, IMonitoringSpecificationMachineFacade>
    {
        public MonitoringSpecificationMachineController(IIdentityService identityService, IValidateService validateService, IMonitoringSpecificationMachineFacade facade, IMapper mapper) : base(identityService, validateService, facade, mapper, "1.0.0")
        {
        }
    }
}
