using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.MonitoringSpecificationMachine;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.MonitoringSpecificationMachine;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Monitoring_Specification_Machine;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.MonitoringSpecificationMachine
{
    public class MonitoringSpecificationMachineFacade : IMonitoringSpecificationMachineFacade
    {
        private readonly ProductionDbContext DbContext;
        private readonly DbSet<MonitoringSpecificationMachineModel> DbSet;
        private readonly MonitoringSpecificationMachineLogic MonitoringSpecificationMachineLogic;

        public MonitoringSpecificationMachineFacade(IServiceProvider serviceProvider, ProductionDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = DbContext.Set<MonitoringSpecificationMachineModel>();
            this.MonitoringSpecificationMachineLogic = serviceProvider.GetService<MonitoringSpecificationMachineLogic>();
        }
        public async Task<int> CreateAsync(MonitoringSpecificationMachineModel model)
        {
            this.MonitoringSpecificationMachineLogic.CreateModel(model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await MonitoringSpecificationMachineLogic.DeleteModel(id);
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<MonitoringSpecificationMachineModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            throw new NotImplementedException();
        }

        public async Task<MonitoringSpecificationMachineModel> ReadByIdAsync(int id)
        {
            return await MonitoringSpecificationMachineLogic.ReadModelById(id);
        }

        public async Task<int> UpdateAsync(int id, MonitoringSpecificationMachineModel model)
        {
            this.MonitoringSpecificationMachineLogic.UpdateModelAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }
    }
}
