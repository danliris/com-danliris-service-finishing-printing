using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.DyestuffChemicalUsageReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.DyestuffChemicalUsageReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DyestuffChemicalUsageReceipt;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Services.ValidateService;
using Com.Danliris.Service.Production.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.DyestuffChemicalReceiptUsage
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/finishing-printing/dyestuff-chemical-usage-receipt")]
    [Authorize]
    public class DyestuffChemicalUsageReceiptController : BaseController<DyestuffChemicalUsageReceiptModel, DyestuffChemicalUsageReceiptViewModel, IDyestuffChemicalUsageReceiptFacade>
    {
        public DyestuffChemicalUsageReceiptController(IIdentityService identityService, IValidateService validateService, IDyestuffChemicalUsageReceiptFacade facade, IMapper mapper) : base(identityService, validateService, facade, mapper, "1.0.0")
        {
        }
    }
}
