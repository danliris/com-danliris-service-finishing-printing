using Com.Danliris.Service.Finishing.Printing.Lib.Models.PackingReceipt;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.PackingReceipt
{
    public class PackingReceiptLogic : BaseLogic<PackingReceiptModel>
    {
        private const string UserAgent = "production-service";
        private readonly ProductionDbContext dbContext;
        private readonly DbSet<PackingReceiptModel> dbSet;
        public PackingReceiptLogic(IIdentityService identityService, ProductionDbContext dbContext) : base(identityService, dbContext)
        {
        }

        public override void CreateModel(PackingReceiptModel model)
        {
            EntityExtension.FlagForCreate(model, IdentityService.Username, UserAgent);
            DbSet.Add(model);
        }
    }
}
