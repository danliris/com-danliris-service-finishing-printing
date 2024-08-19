using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.PdfTemplates;
using Com.Danliris.Service.Finishing.Printing.Lib.Services.HttpClientService;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Kanban;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Services.ValidateService;
using Com.Danliris.Service.Production.Lib.Utilities;
using Com.Danliris.Service.Production.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Finishing.Printing.Lib.Utilities;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.Kanban
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/production/kanbans")]
    [Authorize]
    public class KanbanController : BaseController<KanbanModel, KanbanViewModel, IKanbanFacade>
    {
        private const string LANJUT_PROSES = "Lanjut Proses";
        private const string REPROSES = "Reproses";
        private readonly IIdentityService IdentityService;
        private readonly IServiceProvider serviceProvider;
        private readonly IHttpClientService HttpClientService;
        public KanbanController(IIdentityService identityService, IValidateService validateService, IKanbanFacade facade, IMapper mapper, IServiceProvider serviceProvider) : base(identityService, validateService, facade, mapper, "1.0.0")
        {
            
            IdentityService = identityService;
            this.serviceProvider = serviceProvider;
            HttpClientService = serviceProvider.GetService<IHttpClientService>();

        }

        [HttpPost("create/carts")]
        public async Task<ActionResult> Create([FromBody] KanbanCreateViewModel viewModel)
        {
            try
            {
                VerifyUser();
                ValidateService.Validate(viewModel);

                foreach (var cart in viewModel.Carts)
                {
                    KanbanViewModel vmToCreate = new KanbanViewModel
                    {
                        Cart = cart,
                        CurrentQty = viewModel.CurrentQty ?? 0,
                        CurrentStepIndex = viewModel.CurrentStepIndex ?? 0,
                        GoodOutput = viewModel.GoodOutput ?? 0,
                        Grade = viewModel.Grade,
                        Instruction = viewModel.Instruction,
                        OldKanbanId = viewModel.OldKanbanId,
                        ProductionOrder = viewModel.ProductionOrder,
                        IsBadOutput = viewModel.IsBadOutput,
                        BadOutput = viewModel.BadOutput ?? 0,
                        IsReprocess = viewModel.IsReprocess,
                        SelectedProductionOrderDetail = viewModel.SelectedProductionOrderDetail
                    };

                    if (cart.reprocess == REPROSES || cart.reprocess == LANJUT_PROSES)
                    {
                        vmToCreate.IsReprocess = cart.IsReprocess;
                        vmToCreate.Instruction = cart.Instruction;
                    }
                    KanbanModel model = Mapper.Map<KanbanModel>(vmToCreate);
                    await Facade.CreateAsync(model);

                }

                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, General.CREATED_STATUS_CODE, General.OK_MESSAGE)
                    .Ok();
                return Created(String.Concat(Request.Path, "/", 0), Result);
            }
            catch (ServiceValidationException e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, General.BAD_REQUEST_STATUS_CODE, General.BAD_REQUEST_MESSAGE)
                    .Fail(e);
                return BadRequest(Result);
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, Result);
            }
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
                VerifyUser();

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
                    //IdentityService.Token = Request.Headers["Authorization"].FirstOrDefault().Replace("Bearer ", "");
                    var viewModel = Mapper.Map<KanbanViewModel>(model);
                    //var oldKanbanViewModel = Mapper.Map<KanbanViewModel>(oldKanbanModel);

                    //var request = new HttpRequestMessage(HttpMethod.Get, $@"{APIEndpoint.Sales}sales/production-orders/" + viewModel.ProductionOrder.Id);

                    //var client = _clientFactory.CreateClient();

                    //var http = serviceProvider.GetService<IHttpClientService>();
                    var listRO = "";
                    var httpContent = new StringContent(JsonConvert.SerializeObject(listRO), Encoding.UTF8, "application/json");
                    var token = Request.Headers["Authorization"].FirstOrDefault().Replace("Bearer ", "");
                    var costCalculationUri = $@"{APIEndpoint.Sales}sales/production-orders/" + viewModel.ProductionOrder.Id;
                    var httpResponse = await HttpClientService.SendAsync(HttpMethod.Get, costCalculationUri, token, httpContent);
                    var contentString = await httpResponse.Content.ReadAsStringAsync();

                    Dictionary<string, object> content = JsonConvert.DeserializeObject<Dictionary<string, object>>(contentString);

                    object json;
                    if (content.TryGetValue("data", out json))
                    {
                        Dictionary<string, object> spp = JsonConvert.DeserializeObject<Dictionary<string, object>>(json.ToString());
                        var tanggal = spp.TryGetValue("DeliveryDate", out json) ? (json != null ? json.ToString() : "") : "";
                        viewModel.ProductionOrder.DeliveryDate1 = Convert.ToDateTime(tanggal);
                    }

                    //
                    if (viewModel.Instruction.Name == "COATING")
                    {
                        var PdfTemplate = new KanbanPdfTemplateOld();
                        MemoryStream stream = PdfTemplate.GeneratePdfTemplateOld(viewModel, timeoffsset);
                        return new FileStreamResult(stream, "application/pdf")
                        {
                            FileDownloadName = viewModel.ProductionOrder.OrderNo + " " + viewModel.Cart.CartNumber + ".pdf"
                        };
                    }
                    else
                    {
                        var PdfTemplate = new KanbanPdfTemplate();
                        MemoryStream stream = PdfTemplate.GeneratePdfTemplate(viewModel, timeoffsset);
                        return new FileStreamResult(stream, "application/pdf")
                        {
                            FileDownloadName = viewModel.ProductionOrder.OrderNo + " " + viewModel.Cart.CartNumber + ".pdf"
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

        [HttpPut("complete/{Id}")]
        public async Task<IActionResult> UpdateIsComplete([FromRoute] int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await Facade.CompleteKanban(Id);

                return NoContent();
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
        public IActionResult GetReport(DateTime? dateFrom = null, DateTime? dateTo = null, bool? proses = null, int orderTypeId = -1, int processTypeId = -1, string orderNo = null, int page = 1, int size = 25)
        {
            try
            {
                int offSet = Convert.ToInt32(Request.Headers["x-timezone-offset"]);

                var data = Facade.GetReport(page, size, proses, orderTypeId, processTypeId, orderNo, dateFrom, dateTo, offSet);

                return Ok(new
                {
                    apiVersion = ApiVersion,
                    data = data.Data,
                    info = new
                    {
                        Count = data.Count,
                        Order = data.Order,
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

        [HttpGet("reports/downloads/xls")]
        public IActionResult GetXls(DateTime? dateFrom = null, DateTime? dateTo = null, bool? proses = null, int orderTypeId = -1, int processTypeId = -1, string orderNo = null)
        {
            try
            {
                byte[] xlsInBytes;
                int offSet = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                var xls = Facade.GenerateExcel(proses, orderTypeId, processTypeId, orderNo, dateFrom, dateTo, offSet);

                string fileName = "";
                if (dateFrom == null && dateTo == null)
                    fileName = string.Format("MONITORING KANBAN");
                else if (dateFrom != null && dateTo == null)
                    fileName = string.Format("MONITORING KANBAN {0}", dateFrom.Value.ToString("dd/MM/yyyy"));
                else if (dateFrom == null && dateTo != null)
                    fileName = string.Format("MONITORING KANBAN {0}", dateTo.GetValueOrDefault().ToString("dd/MM/yyyy"));
                else
                    fileName = string.Format("MONITORING KANBAN {0} - {1}", dateFrom.GetValueOrDefault().ToString("dd/MM/yyyy"), dateTo.Value.ToString("dd/MM/yyyy"));
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

        [HttpGet("old/{id}")]
        public virtual async Task<IActionResult> GetOldKanbanById([FromRoute] int id)
        {
            try
            {
                var model = await Facade.ReadOldKanbanByIdAsync(id);

                if (model == null)
                {
                    Dictionary<string, object> Result =
                           new ResultFormatter(ApiVersion, General.OK_STATUS_CODE, General.OK_MESSAGE)
                           .Ok<KanbanViewModel>(Mapper, new KanbanViewModel());
                    return Ok(Result);
                }
                else
                {
                    var viewModel = Mapper.Map<KanbanViewModel>(model);
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, General.OK_STATUS_CODE, General.OK_MESSAGE)
                        .Ok<KanbanViewModel>(Mapper, viewModel);
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

        [HttpGet("snapshot/downloads/xls")]
        public IActionResult GetSnapshotXLS(int month, int year)
        {
            try
            {
                byte[] xlsInBytes;
                int offSet = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                var xls = Facade.GenerateKanbanSnapshotExcel(month, year);

                string fileName = "";
                DateTime date = new DateTime(year, month, 1);
                fileName = string.Format("Kanban " + date.ToString("MMMM yyyy"));

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

        [HttpGet("read/visualization")]
        public IActionResult GetVisualization([FromQuery] string order = "{}", [FromQuery] string filter = "{}", [FromQuery] int page = 1, [FromQuery] int size = 500)
        {
            try
            {
                int offSet = Convert.ToInt32(Request.Headers["x-timezone-offset"]);

                var data = Facade.ReadVisualization(order, filter, page, size);

                return Ok(new
                {
                    apiVersion = ApiVersion,
                    data = data.Data,
                    info = new
                    {
                        Total = data.Total,
                        Count = data.Count,
                        Order = data.Order,
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
    }
}
