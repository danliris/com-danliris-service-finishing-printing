using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.MachineType;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.MachineType;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master
{
    public class MachineTypeFacade : IMachineTypeFacade
    {
        private readonly ProductionDbContext DbContext;
        private readonly DbSet<MachineTypeModel> DbSet;
        private readonly MachineTypeLogic MachineTypeLogic;

        public MachineTypeFacade(IServiceProvider serviceProvider, ProductionDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = DbContext.Set<MachineTypeModel>();
            this.MachineTypeLogic = serviceProvider.GetService<MachineTypeLogic>();
        }

        public async Task<int> CreateAsync(MachineTypeModel model)
        {
            this.MachineTypeLogic.CreateModel(model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            await MachineTypeLogic.DeleteModel(id);
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<MachineTypeModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            throw new NotImplementedException();
        }

        public async Task<MachineTypeModel> ReadByIdAsync(int id)
        {
            return await MachineTypeLogic.ReadModelById(id);
        }

        public Task<int> Update(int id, MachineTypeModel model)
        {
            throw new NotImplementedException();
        }
    }
}
