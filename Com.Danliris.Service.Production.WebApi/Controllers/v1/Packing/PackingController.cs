using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.Packing;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Packing;
using Com.Danliris.Service.Finishing.Printing.Lib.PdfTemplates;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Packing;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Services.ValidateService;
using Com.Danliris.Service.Production.Lib.Utilities;
using Com.Danliris.Service.Production.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.Packing
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/finishing-printing/quality-control/packings")]
    [Authorize]
    public class PackingController : BaseController<PackingModel, PackingViewModel, IPackingFacade>
    {
        public PackingController(IIdentityService identityService, IValidateService validateService, IPackingFacade facade, IMapper mapper) : base(identityService, validateService, facade, mapper, "1.0.0")
        {
        }

        [HttpGet("pdf/{Id}")]
        public new async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                PackingModel model = await Facade.ReadByIdAsync(id);
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
                    PackingPdfTemplate pdfTemplate = new PackingPdfTemplate(model, timeoffsset);
                    MemoryStream stream = pdfTemplate.GeneratePdfTemplate();
                    return new FileStreamResult(stream, "application/pdf")
                    {
                        FileDownloadName = string.Format("{0}.pdf", model.Code)
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
        public IActionResult GetReport(DateTime? dateFrom = null, DateTime? dateTo = null, string code = null, string productionOrderNo = null, int page = 1, int size = 25)
        {
            try
            {
                int offSet = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                //int offSet = 7;
                var data = Facade.GetReport(page, size, code, productionOrderNo, dateFrom, dateTo, offSet);

                return Ok(new
                {
                    apiVersion = ApiVersion,
                    data = data.Data,
                    info = new
                    {
                        Count = data.Count,
                        Orded = data.Order,
                        Selected = data.Selected
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

        //[HttpGet("reports/downloads/xls")]
        //public IActionResult GetXls(DateTime? dateFrom = null, DateTime? dateTo = null, int kanban = -1, int machine = -1)
        //{
        //    try
        //    {
        //        byte[] xlsInBytes;
        //        int offSet = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
        //        var xls = Facade.GenerateExcel(kanban, machine, dateFrom, dateTo, offSet);

        //        string fileName = "";
        //        if (dateFrom == null && dateTo == null)
        //            fileName = string.Format("Daily Operation Report");
        //        else if (dateFrom != null && dateTo == null)
        //            fileName = string.Format("Daily Operation Report {0}", dateFrom.Value.ToString("dd/MM/yyyy"));
        //        else if (dateFrom == null && dateTo != null)
        //            fileName = string.Format("Daily Operation Report {0}", dateTo.GetValueOrDefault().ToString("dd/MM/yyyy"));
        //        else
        //            fileName = string.Format("Daily Operation Report {0} - {1}", dateFrom.GetValueOrDefault().ToString("dd/MM/yyyy"), dateTo.Value.ToString("dd/MM/yyyy"));
        //        xlsInBytes = xls.ToArray();

        //        var file = File(xlsInBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        //        return file;
        //    }
        //    catch (Exception e)
        //    {
        //        Dictionary<string, object> Result =
        //          new ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, e.Message)
        //          .Fail();
        //        return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, Result);
        //    }
        //}
    }
}