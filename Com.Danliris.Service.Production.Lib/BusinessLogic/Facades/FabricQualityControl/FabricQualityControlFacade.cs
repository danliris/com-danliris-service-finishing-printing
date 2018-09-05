using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.FabricQualityControl;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.FabricQualityControl;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.FabricQualityControl;
using Com.Danliris.Service.Production.Lib;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Com.Danliris.Service.Production.Lib.Utilities;
using System.Linq;
using Newtonsoft.Json;
using Com.Moonlay.NetCore.Lib;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.FabricQualityControl
{
    public class FabricQualityControlFacade : IFabricQualityControlFacade
    {
        private readonly ProductionDbContext DbContext;
        private readonly DbSet<FabricQualityControlModel> DbSet;
        private readonly FabricQualityControlLogic FabricQualityControlLogic;

        public FabricQualityControlFacade(IServiceProvider serviceProvider, ProductionDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<FabricQualityControlModel>();
            FabricQualityControlLogic = serviceProvider.GetService<FabricQualityControlLogic>();
        }

        public ReadResponse<FabricQualityControlModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<FabricQualityControlModel> query = DbSet;

            List<string> searchAttributes = new List<string>()
            {
                "Code", "DateIm", "ShiftIm", "OperatorIm", "ProductionOrderNo", "ProductionOrderType"
            };
            query = QueryHelper<FabricQualityControlModel>.Search(query, searchAttributes, keyword);

            Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<FabricQualityControlModel>.Filter(query, filterDictionary);

            List<string> selectedFields = new List<string>()
            {
                "Id", "Code", "CartNo", "DateIm", "IsUsed", "MachineNoIm", "OperatorIm", "ProductionOrderNo", "ProductionOrderType", "ShiftIm"
            };
            query = query
                .Select(field => new FabricQualityControlModel
                {
                    Id = field.Id,
                    Code = field.Code,
                    CartNo = field.CartNo,
                    DateIm = field.DateIm,
                    IsUsed = field.IsUsed,
                    MachineNoIm = field.MachineNoIm,
                    OperatorIm = field.OperatorIm,
                    ProductionOrderNo = field.ProductionOrderNo,
                    ProductionOrderType = field.ProductionOrderType,
                    ShiftIm = field.ShiftIm
                });

            Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<FabricQualityControlModel>.Order(query, orderDictionary);

            Pageable<FabricQualityControlModel> pageable = new Pageable<FabricQualityControlModel>(query, page - 1, size);
            List<FabricQualityControlModel> data = pageable.Data.ToList();
            int totalData = pageable.TotalCount;

            return new ReadResponse<FabricQualityControlModel>(data, totalData, orderDictionary, selectedFields);
        }

        public async Task<int> CreateAsync(FabricQualityControlModel model)
        {
            do
            {
                model.Code = CodeGenerator.Generate();
            }
            while (DbSet.Any(d => d.Code.Equals(model.Code)));
            FabricQualityControlLogic.CreateModel(model);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int id)
        {
            await FabricQualityControlLogic.DeleteModel(id);
            return await DbContext.SaveChangesAsync();
        }

        public async Task<FabricQualityControlModel> ReadByIdAsync(int id)
        {
            return await FabricQualityControlLogic.ReadModelById(id);
        }

        public async Task<int> UpdateAsync(int id, FabricQualityControlModel model)
        {
            FabricQualityControlLogic.UpdateModelAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }
    }
}
