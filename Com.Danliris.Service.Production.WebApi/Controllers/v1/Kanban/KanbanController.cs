using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Kanban;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Services.ValidateService;
using Com.Danliris.Service.Production.Lib.Utilities;
using Com.Danliris.Service.Production.WebApi.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.WebApi.Controllers.v1.Kanban
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/production/kanbans")]
    [Authorize]
    public class KanbanController : BaseController<KanbanModel, KanbanViewModel, IKanbanFacade>
    {
        public KanbanController(IIdentityService identityService, IValidateService validateService, IKanbanFacade facade, IMapper mapper) : base(identityService, validateService, facade, mapper, "1.0.0")
        {
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
                        BadOutput = (double)viewModel.BadOutput,
                        Cart = cart,
                        CurrentQty = (double)viewModel.CurrentQty,
                        CurrentStepIndex = (int)viewModel.CurrentStepIndex,
                        //GoodOutput = (double)viewModel.GoodOutput,
                        Grade = viewModel.Grade,
                        Instruction = viewModel.Instruction,
                        IsBadOutput = (bool)viewModel.IsBadOutput,
                        IsComplete = (bool)viewModel.IsComplete,
                        IsInactive = (bool)viewModel.IsInactive,
                        IsReprocess = (bool)viewModel.IsReprocess,
                        OldKanban = viewModel.OldKanban,
                        ProductionOrder = viewModel.ProductionOrder,
                        SelectedProductionOrderDetail = viewModel.SelectedProductionOrderDetail
                    };

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
    }
}
