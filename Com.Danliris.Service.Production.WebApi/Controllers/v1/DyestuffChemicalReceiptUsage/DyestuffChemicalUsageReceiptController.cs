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
                    if (model.StrikeOffType == "GROUND PRINTING") {
                        int timeoffsset = Convert.ToInt32(timezone);
                        var pdfTemplate = new DyestuffChemicalUsageReceiptPdfTemplate(model, timeoffsset);
                        var stream = pdfTemplate.GeneratePdfTemplateGround();
                        return new FileStreamResult(stream, "application/pdf")
                        {
                            FileDownloadName = string.Format("{0} - {1}.pdf", model.ProductionOrderOrderNo, model.StrikeOffCode)
                        };
                    }else{
                        int timeoffsset = Convert.ToInt32(timezone);
                        var pdfTemplate = new DyestuffChemicalUsageReceiptPdfTemplate(model, timeoffsset);
                        var stream = pdfTemplate.GeneratePdfTemplate();
                        return new FileStreamResult(stream, "application/pdf")
                        {
                            FileDownloadName = string.Format("{0} - {1}.pdf", model.ProductionOrderOrderNo, model.StrikeOffCode)
                        };

                    }
                    
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
                var data = await Facade.GetDataByStrikeOff(strikeOffId);

                if (data.Item1 == null)
                {
                    var objectData = new
                    {
                        Data = data.Item1,
                        OrderNo = data.Item2
                    };
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, General.OK_STATUS_CODE, General.OK_MESSAGE)
                        .Ok();
                    Result.Add("data", objectData);
                    return Ok(Result);
                }
                else
                {
                    DyestuffChemicalUsageReceiptViewModel viewModel = Mapper.Map<DyestuffChemicalUsageReceiptViewModel>(data.Item1);
                    var objectData = new
                    {
                        Data = viewModel,
                        OrderNo = data.Item2
                    };
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, General.OK_STATUS_CODE, General.OK_MESSAGE)
                        .Ok();

                    Result.Add("data", objectData);
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

        [HttpGet("reports")]
        public IActionResult Get(string productionOrderNo, string strikeOffCode, DateTime? dateFrom, DateTime? dateTo, int page, int size, string Order = "{}")
        {
            int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
            string accept = Request.Headers["Accept"];

            try
            {
                var data = Facade.GetReport(productionOrderNo, strikeOffCode, dateFrom, dateTo, page, size, Order, offset);

                return Ok(new
                {
                    apiVersion = ApiVersion,
                    data = data.Item1,
                    info = new { total = data.Item2 },
                    message = General.OK_MESSAGE,
                    statusCode = General.OK_STATUS_CODE
                });
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, Result);
            }

        }

        [HttpGet("reports/downloads/xls")]
        public IActionResult GetXls(string productionOrderNo, string strikeOffCode, DateTime? dateFrom, DateTime? dateTo, int offset)
        {
            try
            {
                byte[] xlsInBytes;
                int offSet = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                var xls = Facade.GenerateExcel(productionOrderNo, strikeOffCode, dateFrom, dateTo, offSet);

                string filename = "";
                if (dateFrom == null && dateTo == null)
                    filename = string.Format("Laporan Resep Pemakaian Dyestuff & Chemical");
                else if (dateFrom != null && dateTo == null)
                    filename = string.Format("Laporan Resep Pemakaian Dyestuff & Chemical", dateFrom.Value.ToString("dd/MM/yyyy"));
                else if (dateFrom == null && dateTo != null)
                    filename = string.Format("Laporan Resep Pemakaian Dyestuff & Chemical", dateTo.GetValueOrDefault().ToString("dd/MM/yyyy"));
                else
                    filename = string.Format("Laporan Resep Pemakaian Dyestuff & Chemical", dateFrom.GetValueOrDefault().ToString("dd/MM/yyyy"), dateTo.Value.ToString("dd/MM/yyyy"));
                xlsInBytes = xls.ToArray();

                var file = File(xlsInBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
                return file;
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
