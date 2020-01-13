using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.DOSales;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.DOSales;
using Com.Danliris.Service.Finishing.Printing.Lib.PdfTemplates;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DOSales;
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

namespace Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.DOSales
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/finishing-printing/do-sales-items")]
    [Authorize]
    public class DOSalesController : BaseController<DOSalesModel, DOSalesViewModel, IDOSalesFacade>
    {
        public DOSalesController(IIdentityService identityService, IValidateService validateService, IDOSalesFacade facade, IMapper mapper) : base(identityService, validateService, facade, mapper, "1.0.0")
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
                DOSalesModel model = await Facade.ReadByIdAsync(Id);

                if (model == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, General.NOT_FOUND_STATUS_CODE, General.NOT_FOUND_MESSAGE)
                        .Fail();
                    return NotFound(Result);
                }
                else
                {
                    DOSalesViewModel viewModel = Mapper.Map<DOSalesViewModel>(model);

                    DOSalesPdfTemplate PdfTemplate = new DOSalesPdfTemplate();
                    MemoryStream stream = PdfTemplate.GeneratePdfTemplate(viewModel, timeoffsset);
                    return new FileStreamResult(stream, "application/pdf")
                    {
                        FileDownloadName = "DO_Penjualan/" + viewModel.DOSalesNo + ".pdf"
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

        [HttpGet("details/by-product-name")]
        public async Task<IActionResult> GetDOSalesDetail([FromQuery] string productName)
        {
            try
            {
                var model = await Facade.GetDOSalesDetail(productName);


                if (model == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, General.NOT_FOUND_STATUS_CODE, General.NOT_FOUND_MESSAGE)
                        .Fail();
                    return NotFound(Result);
                }
                else
                {
                    DOSalesModel data = model.DOSales;
                    data.DOSalesDetails = new List<DOSalesDetailModel>()
                    {
                        new DOSalesDetailModel()
                        {
                            UnitCode = model.UnitCode,
                            UnitName = model.UnitName,
                            UnitRemark = model.UnitRemark,
                            DOSalesId = model.DOSalesId,
                            TotalPacking = model.TotalPacking,
                            TotalLength = model.TotalLength,
                            TotalLengthConversion = model.TotalLengthConversion,
                            Id = model.Id
                        }
                    };

                    var viewModel = Mapper.Map<DOSalesViewModel>(data);

                    var newResult = new
                    {
                        doSales = viewModel,
                        name = productName
                    };
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, General.OK_STATUS_CODE, General.OK_MESSAGE)
                        .Ok<object>(Mapper, newResult);

                    return Ok(Result);
                }
            }
            catch (Exception ex)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, ex.Message)
                    .Fail();
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, Result);
            }

        }
    }
}
