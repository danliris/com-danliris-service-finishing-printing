using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.ReturToQC;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.ReturToQC;
using Com.Danliris.Service.Finishing.Printing.Lib.PdfTemplates;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.ReturToQC;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Services.ValidateService;
using Com.Danliris.Service.Production.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetPdfById([FromRoute] int id)
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

        [HttpGet("reports")]
        public IActionResult GetReport(DateTime? dateFrom = null, DateTime? dateTo = null, string productionOrderNo = null, string returNo = null, string destination = null, string deliveryOrderNo = null, int page = 1, int size = 25)
        {
            try
            {
                int offSet = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                //int offSet = 7;
                var data = Facade.GetReport(page, size, dateFrom, dateTo, productionOrderNo, returNo, destination, deliveryOrderNo, offSet);

                return Ok(new
                {
                    apiVersion = ApiVersion,
                    data = data.Data,
                    info = new
                    {
                        data.Count,
                        data.Order,
                        data.Selected
                    },
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
        public IActionResult GetXls(DateTime? dateFrom = null, DateTime? dateTo = null, string productionOrderNo = null, string returNo = null, string destination = null, string deliveryOrderNo = null)
        {
            try
            {
                byte[] xlsInBytes;
                int offSet = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                var xls = Facade.GenerateExcel(dateFrom, dateTo, productionOrderNo, returNo, destination, deliveryOrderNo, offSet);

                string fileName = "";
                if (dateFrom == null && dateTo == null)
                    fileName = string.Format("Retur Barang Ke QC");
                else if (dateFrom != null && dateTo == null)
                    fileName = string.Format("Retur Barang Ke QC {0}", dateFrom.Value.ToString("dd/MM/yyyy"));
                else if (dateFrom == null && dateTo != null)
                    fileName = string.Format("Retur Barang Ke QC {0}", dateTo.GetValueOrDefault().ToString("dd/MM/yyyy"));
                else
                    fileName = string.Format("Retur Barang Ke QC {0} - {1}", dateFrom.GetValueOrDefault().ToString("dd/MM/yyyy"), dateTo.Value.ToString("dd/MM/yyyy"));
                xlsInBytes = xls.ToArray();

                var file = File(xlsInBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
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
