using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.MachineType;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.MachineType;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Utilities;
using Com.Moonlay.NetCore.Lib;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<int> DeleteAsync(int id)
        {
            await MachineTypeLogic.DeleteModel(id);
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<MachineTypeModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<MachineTypeModel> query = DbSet;

            List<string> searchAttributes = new List<string>()
            {
                "Code", "Name"
            };
            query = QueryHelper<MachineTypeModel>.Search(query, searchAttributes, keyword);

            Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<MachineTypeModel>.Filter(query, filterDictionary);

            List<string> selectedFields = new List<string>()
                {
                    "Id", "Name", "Code", "Description", "Indicators", "LastModifiedUtc"
                };

            query = query
                    .Select(field => new MachineTypeModel
                    {
                        Id = field.Id,
                        Name = field.Name,
                        Code = field.Code,
                        LastModifiedUtc = field.LastModifiedUtc,
                        Indicators = new List<MachineTypeIndicatorsModel>(field.Indicators.Select(i => new MachineTypeIndicatorsModel
                        {
                            Indicator = i.Indicator,
                            DataType = i.DataType,
                            DefaultValue = i.DefaultValue,
                            Uom = i.Uom,
                            MachineTypeId = i.MachineTypeId
                        }))
                    });

            Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<MachineTypeModel>.Order(query, orderDictionary);

            Pageable<MachineTypeModel> pageable = new Pageable<MachineTypeModel>(query, page - 1, size);
            List<MachineTypeModel> data = pageable.Data.ToList();
            int totalData = pageable.TotalCount;

            return new ReadResponse<MachineTypeModel>(data, totalData, orderDictionary, selectedFields);
        }

        public async Task<MachineTypeModel> ReadByIdAsync(int id)
        {
            return await MachineTypeLogic.ReadModelById(id);
        }

        public async Task<int> UpdateAsync(int id, MachineTypeModel model)
        {
            this.MachineTypeLogic.UpdateModelAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }
    }
}
