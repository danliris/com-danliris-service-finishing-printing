using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.DailyOperation;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Daily_Operation;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Daily_Operation;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Services.ValidateService;
using Com.Danliris.Service.Production.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.DailyOperation
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/finishing-printing/daily-operations")]
    [Authorize]
    public class DailyOperationController : BaseController<DailyOperationModel, DailyOperationViewModel, IDailyOperationFacade>
    {
        private readonly DateTime _StartDate;
        private readonly DateTime _EndDate;

        public DailyOperationController(IIdentityService identityService, IValidateService validateService, IDailyOperationFacade facade, IMapper mapper) : base(identityService, validateService, facade, mapper, "1.0.0")
        {
            _StartDate = DateTime.Now;
            _EndDate = DateTime.Now.AddDays(-1);
        }

        public override async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                DailyOperationModel model = await Facade.ReadByIdAsync(id);

                if (model == null)
                {
                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, General.NOT_FOUND_STATUS_CODE, General.NOT_FOUND_MESSAGE)
                        .Fail();
                    return NotFound(Result);
                }
                else
                {
                    DailyOperationViewModel viewModel = Mapper.Map<DailyOperationViewModel>(model);
                    var stepCurrent = viewModel.Kanban.Instruction.Steps.FirstOrDefault(s => s.SelectedIndex == (viewModel.Kanban.CurrentStepIndex + 1));
                    //if (stepCurrent.Process == viewModel.Step.Process)
                    //{
                    //    if (viewModel.Type == "input")
                    //    {
                    //        if(await Facade.HasOutput(viewModel.Kanban.Id, viewModel.Step.Process))
                    //        {
                    //            viewModel.IsChangeable = false;
                    //        }
                    //        else
                    //        {
                    //            viewModel.IsChangeable = true;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        viewModel.IsChangeable = true;
                    //    }
                    //}
                    //else
                    //{
                    //    viewModel.IsChangeable = false;
                    //}

                    var hasOutput = await Facade.HasOutput(viewModel.Kanban.Id, viewModel.Step.Process);
                    viewModel.IsChangeable = (stepCurrent == null) || ((stepCurrent.Process == viewModel.Step.Process) && (viewModel.Type == "output" || (viewModel.Type == "input" && !hasOutput)));

                    Dictionary<string, object> Result =
                        new ResultFormatter(ApiVersion, General.OK_STATUS_CODE, General.OK_MESSAGE)
                        .Ok<DailyOperationViewModel>(Mapper, viewModel);
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
        public IActionResult GetReport(DateTime? dateFrom = null, DateTime? dateTo = null, int kanban = -1, int machine = -1, int page = 1, int size = 25)
        {
            try
            {
                int offSet = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                //int offSet = 7;
                var data = Facade.GetReport(page, size, kanban, machine, dateFrom, dateTo, offSet);

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
        public IActionResult GetXls(DateTime? dateFrom = null, DateTime? dateTo = null, int kanban = -1, int machine = -1)
        {
            try
            {
                byte[] xlsInBytes;
                int offSet = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
                var xls = Facade.GenerateExcel(kanban, machine, dateFrom, dateTo, offSet);

                string fileName = "";
                if (dateFrom == null && dateTo == null)
                    fileName = string.Format("Daily Operation Report");
                else if (dateFrom != null && dateTo == null)
                    fileName = string.Format("Daily Operation Report {0}", dateFrom.Value.ToString("dd/MM/yyyy"));
                else if (dateFrom == null && dateTo != null)
                    fileName = string.Format("Daily Operation Report {0}", dateTo.GetValueOrDefault().ToString("dd/MM/yyyy"));
                else
                    fileName = string.Format("Daily Operation Report {0} - {1}", dateFrom.GetValueOrDefault().ToString("dd/MM/yyyy"), dateTo.Value.ToString("dd/MM/yyyy"));
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

        [HttpGet("by-selected-column")]
        public IActionResult GetBySelectedColumn(DateTime? startDate, DateTime? endDate, int page = 1, int size = 25, string order = "{}", [Bind(Prefix = "Select[]")]List<string> select = null, string keyword = null, string filter = "{}", string machine = "", string orderNo = "", string cartNo = "", string stepProcess = "")
        {
            //startDate = startDate.HasValue ? startDate.Value : DateTime.Now.AddDays(-1);
            //endDate = endDate.HasValue ? endDate.Value : DateTime.Now;
            try
            {
                var read = Facade.Read(page, size, order, select, keyword, filter, machine, orderNo, cartNo, stepProcess, startDate, endDate);

                List<DailyOperationViewModel> dataVM = Mapper.Map<List<DailyOperationViewModel>>(read.Data);

                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, General.OK_STATUS_CODE, General.OK_MESSAGE)
                    .Ok(Mapper, dataVM, page, size, read.Count, dataVM.Count, read.Order, read.Selected);
                return Ok(Result);
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

        [HttpGet("filter-options")]
        public IActionResult GetFilterOptions()
        {
            return Ok(new
            {
                apiVersion = ApiVersion,
                data = new List<string>()
                    {
                        "Nomor SPP",
                        "Kereta",
                        "Proses",
                        "Mesin"
                    },
                message = General.OK_MESSAGE,
                statusCode = General.OK_STATUS_CODE
            });
        }

        [HttpGet("production-order-report")]
        public async Task<IActionResult> GetJoinKanbans(string no)
        {
            try
            {
                var data = await Facade.GetJoinKanban(no);
                return Ok(new
                {
                    apiVersion = ApiVersion,
                    data,
                    info = new
                    {
                        data.Count
                    },
                    message = General.OK_MESSAGE,
                    statusCode = General.OK_STATUS_CODE
                });

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
