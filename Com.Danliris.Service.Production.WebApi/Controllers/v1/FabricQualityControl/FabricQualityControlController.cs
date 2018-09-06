using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.FabricQualityControl;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.FabricQualityControl;
using Com.Danliris.Service.Finishing.Printing.Lib.PdfTemplates;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.FabricQualityControl;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Services.ValidateService;
using Com.Danliris.Service.Production.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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

        [HttpGet("pdf/{Id}")]
        public async Task<IActionResult> GetPDF([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var indexAcceptPdf = Request.Headers["Accept"].ToList().IndexOf("application/pdf");
                int timeoffsset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                var model = await Facade.ReadByIdAsync(Id);

                if (model == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, General.NOT_FOUND_STATUS_CODE, General.NOT_FOUND_MESSAGE)
                        .Fail();
                    return NotFound(Result);
                }
                else
                {
                    //var oldKanbanModel = !model.OldKanbanId.Equals(0) ? await Facade.ReadByIdAsync(model.OldKanbanId) : null;

                    var viewModel = Mapper.Map<FabricQualityControlViewModel>(model);
                    //var oldKanbanViewModel = Mapper.Map<KanbanViewModel>(oldKanbanModel);

                    var PdfTemplate = new FabricQualityControlPdfTemplate();
                    MemoryStream stream = PdfTemplate.GeneratePdfTemplate(viewModel, timeoffsset);
                    return new FileStreamResult(stream, "application/pdf")
                    {
                        FileDownloadName = viewModel.Code + ".pdf"
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
