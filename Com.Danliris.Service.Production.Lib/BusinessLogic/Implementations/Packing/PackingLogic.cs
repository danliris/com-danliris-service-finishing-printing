using Com.Danliris.Service.Finishing.Printing.Lib.Models.Packing;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Moonlay.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Net;
using System.Web;

namespace Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Packing
{
    public class PackingLogic : BaseLogic<PackingModel>
    {
        private const string UserAgent = "production-service";
        private readonly ProductionDbContext dbContext;
        private readonly DbSet<PackingModel> dbSet;
        private readonly DbSet<PackingDetailModel> dbSetDetail;

        public PackingLogic(IIdentityService identityService, ProductionDbContext dbContext) : base(identityService, dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<PackingModel>();
            this.dbSetDetail = dbContext.Set<PackingDetailModel>();
        }

        public override void CreateModel(PackingModel model)
        {
            EntityExtension.FlagForCreate(model, IdentityService.Username, UserAgent);
            foreach(var detail in model.PackingDetails)
            {
                EntityExtension.FlagForCreate(detail, IdentityService.Username, UserAgent);
            }
            DbSet.Add(model);
        }

        public async Task CreateProduct(PackingModel model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", IdentityService.Token);
                var myContent = JsonConvert.SerializeObject(model);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await client.PostAsync("", byteContent);
                response.EnsureSuccessStatusCode();
            }
        }

        public override async Task DeleteModel(int id)
        {
            var model = await ReadModelById(id);
            foreach (var item in model.PackingDetails)
            {
                EntityExtension.FlagForDelete(item, IdentityService.Username, UserAgent);
            }
            EntityExtension.FlagForDelete(model, IdentityService.Username, UserAgent);
            DbSet.Update(model);
        }

        public override async Task<PackingModel> ReadModelById(int id)
        {
            var query = (from packings in dbContext.Packings
                         where packings.Id == id && packings.IsDeleted.Equals(false)
                         select packings).Include("PackingDetails");
            var result = await query.FirstOrDefaultAsync();
            return result;
        }

        public override void UpdateModelAsync(int id, PackingModel model)
        {
            model.Id = id;
            EntityExtension.FlagForUpdate(model, IdentityService.Username, UserAgent);
            foreach(var detail in model.PackingDetails)
            {
                if(detail.Id == 0)
                {
                    EntityExtension.FlagForCreate(detail, IdentityService.Username, UserAgent);
                    dbSetDetail.Add(detail);
                }
                else
                {
                    EntityExtension.FlagForUpdate(detail, IdentityService.Username, UserAgent);
                }
                
            }
            DbSet.Update(model);
        }
    }
}
