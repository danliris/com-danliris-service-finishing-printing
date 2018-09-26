using Com.Danliris.Service.Finishing.Printing.Lib.Models.ReturToQC;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.ReturToQC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Danliris.Service.Production.WebApi.Utilities;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.ReturToQC;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Services.ValidateService;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.Packing;
using AutoMapper;

namespace Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.ReturToQC
{
    public class ReturToQCController : BaseController<ReturToQCModel, ReturToQCViewModel, IReturToQCFacade>
    {
        public ReturToQCController(IIdentityService identityService, IValidateService validateService, IReturToQCFacade facade, IMapper mapper) : base(identityService, validateService, facade, mapper, "1.0.0")
        {

        }
    }
}
