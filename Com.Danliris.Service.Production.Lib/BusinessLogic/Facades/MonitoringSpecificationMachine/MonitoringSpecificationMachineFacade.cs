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
using System.Linq;
using Newtonsoft.Json;
using Com.Moonlay.NetCore.Lib;

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
            do
            {
                model.Code = CodeGenerator.Generate();
            }
            while (DbSet.Any(d => d.Code.Equals(model.Code)));

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
            IQueryable<MonitoringSpecificationMachineModel> query = DbSet;

            List<string> searchAttributes = new List<string>()
            {
                "Code"
            };
            query = QueryHelper<MonitoringSpecificationMachineModel>.Search(query, searchAttributes, keyword);

            Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<MonitoringSpecificationMachineModel>.Filter(query, filterDictionary);

            List<string> selectedFields = new List<string>()
                {
                    "Id","Code","Machine","ProductionOrder","CartNumber","DateTimeInput","Details","LastModifiedUtc"
                };

            query = query
                    .Select(field => new MonitoringSpecificationMachineModel
                    {
                        Id = field.Id,
                        //Name = field.Name,
                        CartNumber = field.CartNumber,
                        ProductionOrderId = field.ProductionOrderId,
                        MachineId = field.MachineId,
                        MachineName = field.MachineName,
                        ProductionOrderNo = field.ProductionOrderNo,
                        Code = field.Code,
                        DateTimeInput = field.DateTimeInput,
                        LastModifiedUtc = field.LastModifiedUtc,
                        Details = new List<MonitoringSpecificationMachineDetailsModel>(field.Details.Select(i => new MonitoringSpecificationMachineDetailsModel
                        {
                            Indicator = i.Indicator,
                            DataType = i.DataType,
                            DefaultValue = i.DefaultValue,
                            Uom = i.Uom,
                            Value = i.Value
                        }))
                    });

            Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<MonitoringSpecificationMachineModel>.Order(query, orderDictionary);

            Pageable<MonitoringSpecificationMachineModel> pageable = new Pageable<MonitoringSpecificationMachineModel>(query, page - 1, size);
            List<MonitoringSpecificationMachineModel> data = pageable.Data.ToList();
            int totalData = pageable.TotalCount;

            return new ReadResponse<MonitoringSpecificationMachineModel>(data, totalData, orderDictionary, selectedFields);
        }

        public async Task<MonitoringSpecificationMachineModel> ReadByIdAsync(int id)
        {
            return await MonitoringSpecificationMachineLogic.ReadModelById(id);
        }

        public async Task<int> UpdateAsync(int id, MonitoringSpecificationMachineModel model)
        {
            await this.MonitoringSpecificationMachineLogic.UpdateModelAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }
    }
}
