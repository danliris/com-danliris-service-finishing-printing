using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Newtonsoft.Json.Serialization;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Services.ValidateService;
using Com.Danliris.Service.Production.Lib.BusinessLogic.Interfaces.Master;
using Com.Danliris.Service.Production.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Production.Lib.BusinessLogic.Implementations.Master.Step;
using Com.Danliris.Service.Production.WebApi.Utilities;
using Com.Danliris.Service.Production.Lib;
using IdentityServer4.AccessTokenValidation;
using Com.Danliris.Service.Production.Lib.BusinessLogic.Implementations.Master.Instruction;
using Com.Danliris.Service.Production.Lib.BusinessLogic.Implementations.Master.DurationEstimation;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.MachineType;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.Master;

namespace Com.Danliris.Service.Production.WebApi
{
    public class Startup
    {
        private readonly string[] EXPOSED_HEADERS = new string[] { "Content-Disposition", "api-version", "content-length", "content-md5", "content-type", "date", "request-id", "response-time" };
        private readonly string PRODUCTION_POLICY = "ProductionPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private void RegisterServices(IServiceCollection services)
        {
            services
                .AddScoped<IIdentityService, IdentityService>()
                .AddScoped<IValidateService, ValidateService>();
        }

        private void RegisterFacades(IServiceCollection services)
        {
            services
                .AddTransient<IStepFacade, StepFacade>()
                .AddTransient<IInstructionFacade, InstructionFacade>()
                .AddTransient<IInstructionFacade, InstructionFacade>()
                .AddTransient<IMachineTypeFacade, MachineTypeFacade>();
        }

        private void RegisterLogics(IServiceCollection services)
        {
            services
                .AddTransient<StepLogic>()
                .AddTransient<StepIndicatorLogic>()
                .AddTransient<InstructionLogic>()
                .AddTransient<DurationEstimationLogic>()
                .AddTransient<MachineTypeLogic>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString(Constant.DEFAULT_CONNECTION) ?? Configuration[Constant.DEFAULT_CONNECTION];

            #region Register
            services.AddDbContext<ProductionDbContext>(options => options.UseSqlServer(connectionString, c => c.CommandTimeout(60)));

            RegisterServices(services);

            RegisterFacades(services);

            RegisterLogics(services);

            services.AddAutoMapper();
            #endregion

            #region Authentication
            string Secret = Configuration.GetValue<string>(Constant.SECRET) ?? Configuration[Constant.SECRET];
            SymmetricSecurityKey Key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = false,
                        IssuerSigningKey = Key
                    };
                });
            #endregion

            #region CORS
            services.AddCors(options => options.AddPolicy(PRODUCTION_POLICY, builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .WithExposedHeaders(EXPOSED_HEADERS);
            }));
            #endregion

            #region API
            services
                .AddApiVersioning(options => options.DefaultApiVersion = new ApiVersion(1, 0))
                .AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters()
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ProductionDbContext>();
                context.Database.Migrate();
            }

            app.UseAuthentication();
            app.UseCors(PRODUCTION_POLICY);
            app.UseMvc();
        }
    }
}
