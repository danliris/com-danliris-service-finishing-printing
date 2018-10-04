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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Com.Danliris.Service.Finishing.Printing.Lib.PdfTemplates;

namespace Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.ReturToQC
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/finishing-printing/inventory/fp-retur-to-qc-docs")]
    [Authorize]
    public class ReturToQCController : BaseController<ReturToQCModel, ReturToQCViewModel, IReturToQCFacade>
    {
        public ReturToQCController(IIdentityService identityService, IValidateService validateService, IReturToQCFacade facade, IMapper mapper) : base(identityService, validateService, facade, mapper, "1.0.0")
        {

        }

        [HttpGet("pdf/{Id}")]
        public new async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                ReturToQCModel model = await Facade.ReadByIdAsync(id);
                if (model == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, General.NOT_FOUND_STATUS_CODE, General.NOT_FOUND_MESSAGE)
                        .Fail();
                    return NotFound(Result);
                }
                else
                {
                    int timeoffsset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                    ReturToQCPdfTemplate pdfTemplate = new ReturToQCPdfTemplate(model, timeoffsset);
                    MemoryStream stream = pdfTemplate.GeneratePdfTemplate();
                    return new FileStreamResult(stream, "application/pdf")
                    {
                        FileDownloadName = string.Format("{0}.pdf", model.ReturNo)
                    };
                }
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
