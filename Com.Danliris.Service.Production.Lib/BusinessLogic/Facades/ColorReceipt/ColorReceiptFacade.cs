using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.ColorReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.ColorReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.ColorReceipt;
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

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.ColorReceipt
{
    public class ColorReceiptFacade : IColorReceiptFacade
    {
        private readonly ProductionDbContext DbContext;
        private readonly DbSet<ColorReceiptModel> DbSet;
        private readonly ColorReceiptLogic ColorReceiptLogic;
        private readonly IServiceProvider ServiceProvider;

        public ColorReceiptFacade(IServiceProvider serviceProvider, ProductionDbContext dbContext)
        {
            ServiceProvider = serviceProvider;
            DbContext = dbContext;
            DbSet = dbContext.Set<ColorReceiptModel>();
            ColorReceiptLogic = serviceProvider.GetService<ColorReceiptLogic>();
        }

        public Task<int> CreateAsync(ColorReceiptModel model)
        {
            var totalData = DbSet.Count();

            string code = string.Format("{0} {1}{2}", model.ColorName, model.TechnicianName, totalData + 1);
            model.ColorCode = code;
            ColorReceiptLogic.CreateModel(model);
            return DbContext.SaveChangesAsync();

        }

        public async Task<TechnicianModel> CreateTechnician(string name)
        {
            ColorReceiptLogic.SetDefaultAllTechnician(false);
            await DbContext.SaveChangesAsync();
            var technician = ColorReceiptLogic.CreateTechnician(name);
            await DbContext.SaveChangesAsync();

            return technician;
        }

        public async Task<int> DeleteAsync(int id)
        {
            await ColorReceiptLogic.DeleteModel(id);
            return await DbContext.SaveChangesAsync();
        }

        public ReadResponse<ColorReceiptModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<ColorReceiptModel> query = DbSet.Include(s => s.ColorReceiptItems);

            List<string> searchAttributes = new List<string>()
            {
                "ColorCode","ColorName","TechnicianName"
            };
            query = QueryHelper<ColorReceiptModel>.Search(query, searchAttributes, keyword);

            Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper<ColorReceiptModel>.Filter(query, filterDictionary);

            List<string> selectedFields = new List<string>()
            {

                "Id","ColorName","ColorCode","Technician","Remark","LastModifiedUtc"

            };

            Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper<ColorReceiptModel>.Order(query, orderDictionary);

            Pageable<ColorReceiptModel> pageable = new Pageable<ColorReceiptModel>(query, page - 1, size);
            List<ColorReceiptModel> data = pageable.Data.ToList();
            int totalData = pageable.TotalCount;

            return new ReadResponse<ColorReceiptModel>(data, totalData, orderDictionary, selectedFields);
        }

        public Task<ColorReceiptModel> ReadByIdAsync(int id)
        {
            return ColorReceiptLogic.ReadModelById(id);
        }

        public async Task<int> UpdateAsync(int id, ColorReceiptModel model)
        {
            await ColorReceiptLogic.UpdateModelAsync(id, model);
            return await DbContext.SaveChangesAsync();
        }
    }
}
