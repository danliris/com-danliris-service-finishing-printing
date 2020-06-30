using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.DyestuffChemicalUsageReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.DyestuffChemicalUsageReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.PdfTemplates;
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

        [HttpGet("pdf/{id}")]
        public async Task<IActionResult> GetPdfById([FromRoute] int id, [FromHeader(Name = "x-timezone-offset")] string timezone)
        {
            try
            {
                var model = await Facade.ReadByIdAsync(id);
                if (model == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, General.NOT_FOUND_STATUS_CODE, General.NOT_FOUND_MESSAGE)
                        .Fail();
                    return NotFound(Result);
                }
                else
                {
                    int timeoffsset = Convert.ToInt32(timezone);
                    var pdfTemplate = new DyestuffChemicalUsageReceiptPdfTemplate(model, timeoffsset);
                    var stream = pdfTemplate.GeneratePdfTemplate();
                    return new FileStreamResult(stream, "application/pdf")
                    {
                        FileDownloadName = string.Format("{0} - {1}.pdf", model.ProductionOrderOrderNo, model.StrikeOffCode)
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

        [HttpGet("strike-off/{strikeOffId}")]
        public virtual async Task<IActionResult> GetDataByStrikeOff([FromRoute] int strikeOffId)
        {
            try
            {
                DyestuffChemicalUsageReceiptModel model = await Facade.GetDataByStrikeOff(strikeOffId);

                if (model == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, General.OK_STATUS_CODE, General.OK_MESSAGE)
                        .Ok<DyestuffChemicalUsageReceiptViewModel>(Mapper, null);
                    return Ok(Result);
                }
                else
                {
                    DyestuffChemicalUsageReceiptViewModel viewModel = Mapper.Map<DyestuffChemicalUsageReceiptViewModel>(model);
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, General.OK_STATUS_CODE, General.OK_MESSAGE)
                        .Ok<DyestuffChemicalUsageReceiptViewModel>(Mapper, viewModel);
                    return Ok(Result);
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
