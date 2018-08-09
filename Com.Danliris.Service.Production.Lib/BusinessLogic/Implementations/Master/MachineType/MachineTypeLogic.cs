using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.MachineType;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.MachineType
{
    public class MachineTypeLogic : BaseLogic<MachineTypeModel>
    {
        private const string UserAgent = "production-service";
        private MachineTypeIndicatorsLogic MachineTypeIndicatorsLogic;

        public MachineTypeLogic(IIdentityService identityService, ProductionDbContext dbContext) : base(identityService, dbContext)
        {
        }

        public override void CreateModel(MachineTypeModel model)
        {
            foreach (MachineTypeIndicatorsModel item in model.Indicators)
            {
                EntityExtension.FlagForCreate(item, IdentityService.Username, UserAgent);
            }
            base.CreateModel(model);
        }
    }
}
