using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.ShipmentDocument;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.ShipmentDocument;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.ShipmentDocument;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Services.ValidateService;
using Com.Danliris.Service.Production.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.ShipmentDocument
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/finishing-printing/inventory/fp-shipment-documents")]
    [Authorize]
    public class ShipmentDocumentController : BaseController<ShipmentDocumentModel, ShipmentDocumentViewModel, IShipmentDocumentService>
    {
        public ShipmentDocumentController(IIdentityService identityService, IValidateService validateService, IShipmentDocumentService facade, IMapper mapper) : base(identityService, validateService, facade, mapper, "1.0.0")
        {
        }
    }
}
