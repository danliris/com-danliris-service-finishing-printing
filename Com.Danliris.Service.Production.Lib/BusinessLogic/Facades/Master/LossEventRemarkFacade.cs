using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.LossEventRemark;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.LossEventRemark;
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

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master
{
    public class LossEventRemarkFacade : ILossEventRemarkFacade
    {
        private readonly ProductionDbContext DbContext;
        private readonly DbSet<LossEventRemarkModel> DbSet;
        private readonly LossEventRemarkLogic LossEventRemarkLogic;

        public LossEventRemarkFacade(IServiceProvider serviceProvider, ProductionDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<LossEventRemarkModel>();
            LossEventRemarkLogic = serviceProvider.GetService<LossEventRemarkLogic>();
        }

        public Task<int> CreateAsync(LossEventRemarkModel model)
        {
            do
            {
                model.Code = CodeGenerator.Generate();
            }
            while (DbSet.Any(d => d.Code.Equals(model.Code)));
            LossEventRemarkLogic.CreateModel(model);
            return DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await LossEventRemarkLogic.DeleteModel(id);
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<LossEventRemarkModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<LossEventRemarkModel> query = DbSet;

            List<string> searchAttributes = new List<string>()
            {
                "LossEventProcessTypeName", "LossEventLosses", "LossEventCategoryLossesCategory", "ProductionLossCode", "Remark", "LossEventProcessArea"
            };
            query = QueryHelper<LossEventRemarkModel>.Search(query, searchAttributes, keyword);

            Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<LossEventRemarkModel>.Filter(query, filterDictionary);

            Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<LossEventRemarkModel>.Order(query, orderDictionary);

            Pageable<LossEventRemarkModel> pageable = new Pageable<LossEventRemarkModel>(query, page - 1, size);
            List<LossEventRemarkModel> data = pageable.Data.ToList();
            int totalData = pageable.TotalCount;

            return new ReadResponse<LossEventRemarkModel>(data, totalData, orderDictionary, new List<string>());
        }

        public Task<LossEventRemarkModel> ReadByIdAsync(int id)
        {
            return LossEventRemarkLogic.ReadModelById(id);
        }

        public async Task<int> UpdateAsync(int id, LossEventRemarkModel model)
        {
            await LossEventRemarkLogic.UpdateModelAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }
    }
}
