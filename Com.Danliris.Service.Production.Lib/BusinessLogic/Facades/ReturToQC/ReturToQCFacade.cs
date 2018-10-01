using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.ReturToQC;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.ReturToQC;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.ReturToQC;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Utilities;
using Com.Moonlay.NetCore.Lib;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.ReturToQC
{
    public class ReturToQCFacade : IReturToQCFacade
    {
        private readonly ProductionDbContext dbContext;
        private readonly DbSet<ReturToQCModel> dbSet;
        private readonly ReturToQCLogic returToQCLogic;

        public ReturToQCFacade(IServiceProvider serviceProvider, ProductionDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<ReturToQCModel>();
            this.returToQCLogic = serviceProvider.GetService<ReturToQCLogic>();
        }

        public async Task<int> CreateAsync(ReturToQCModel model)
        {
            using(var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    do
                    {
                        model.ReturNo = CodeGenerator.Generate();
                    }
                    while (dbSet.Any(d => d.ReturNo.Equals(model.ReturNo)));

                    returToQCLogic.CreateModel(model);
                    var id = await dbContext.SaveChangesAsync();

                    if (model.ReturToQCItems.Count > 0)
                    {
                        //await returToQCLogic.CreateInventoryDocument(model);
                    }
                    transaction.Commit();
                    return id;

                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }
            
        }

        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                await returToQCLogic.DeleteModel(id);
                return await dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        public ReadResponse<ReturToQCModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            //throw new NotImplementedException();
            try
            {
                dbContext.ReturToQCItems.Load();
                dbContext.ReturToQCItemDetails.Load();
                IQueryable<ReturToQCModel> query = dbContext.ReturToQCs.Where(d => 
                    !d.IsDeleted
                    && (d.ReturToQCItems.Count == 0 || (d.ReturToQCItems.Count > 0 && d.ReturToQCItems.Any(e => 
                        !e.IsDeleted 
                        && (e.ReturToQCItemDetails.Count == 0 || (e.ReturToQCItemDetails.Count > 0 && e.ReturToQCItemDetails.Any(f => 
                            !f.IsDeleted)))))));

                List<string> searchAttributes = new List<string>()
            {
                "ReturNo", "DeliveryOrderNo", "ProductionOrderNo", "MaterialName", "MaterialConstructionName", "Destination"
            };
                query = QueryHelper<ReturToQCModel>.Search(query, searchAttributes, keyword);

                Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
                query = QueryHelper<ReturToQCModel>.Filter(query, filterDictionary);

                List<string> selectedFields = new List<string>()
            {
                "Id", "ReturNo", "Date", "DeliveryOrderNo", "Destination", "FinishedGoodCode", "IsVoid", "Material",
                "MaterialWidthFinish" , "Remark", "MaterialConstruction"
            };

                Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
                query = QueryHelper<ReturToQCModel>.Order(query, orderDictionary);

                Pageable<ReturToQCModel> pageable = new Pageable<ReturToQCModel>(query, page - 1, size);
                List<ReturToQCModel> data = pageable.Data.ToList();
                int totalData = pageable.TotalCount;

                return new ReadResponse<ReturToQCModel>(data, totalData, orderDictionary, selectedFields);
            }
            catch (Exception e)
            {
                throw e;
            }
           
        }

        public async Task<ReturToQCModel> ReadByIdAsync(int id)
        {
            return await returToQCLogic.ReadModelById(id);
        }

        public async Task<int> UpdateAsync(int id, ReturToQCModel model)
        {
            try
            {
                returToQCLogic.UpdateModelAsync(id, model);
                return await dbContext.SaveChangesAsync();
            }catch(Exception e)
            {
                throw e;
            }
            
        }
    }
}
