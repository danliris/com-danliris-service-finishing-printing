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
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.Machine;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.MonitoringSpecificationMachine;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.MonitoringSpecificationMachine;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.MonitoringSpecificationMachine;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.MonitoringEvent;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.MonitoringEvent;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.MonitoringEvent;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.BadOutput;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.DailyOperation;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.DailyOperation;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.DailyOperation;
using Com.Danliris.Service.Finishing.Printing.Lib.Utilities;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Packing;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Packing;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.Packing;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.FabricQualityControl;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.FabricQualityControl;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.FabricQualityControl;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.PackingReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.PackingReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.PackingReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.ReturToQC;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.ReturToQC;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.ReturToQC;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Interfaces.ShipmentDocument;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.ShipmentDocument;
using Com.Danliris.Service.Finishing.Printing.Lib.Services.HttpClientService;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.DirectLaborCost;

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

        public void RegisterEndpoint()
        {
            APIEndpoint.Core = Configuration.GetValue<string>("CoreEndpoint") ?? Configuration["CoreEndpoint"];
            APIEndpoint.Inventory = Configuration.GetValue<string>("InventoryEndpoint") ?? Configuration["InventoryEndpoint"];
            //APIEndpoint.Production = Configuration.GetValue<string>("ProductionEndpoint") ?? Configuration["ProductionEndpoint"];
            //APIEndpoint.Purchasing = Configuration.GetValue<string>("PurchasingEndpoint") ?? Configuration["PurchasingEndpoint"];
        }

        private void RegisterServices(IServiceCollection services, bool isTest)
        {
            services
                .AddScoped<IIdentityService, IdentityService>()
                .AddScoped<IValidateService, ValidateService>();

            if (isTest == false)
            {
                services.AddScoped<IHttpClientService, HttpClientService>();
            }
        }

        private void RegisterFacades(IServiceCollection services)
        {
            services
                .AddTransient<IStepFacade, StepFacade>()
                .AddTransient<IInstructionFacade, InstructionFacade>()
                .AddTransient<IDurationEstimationFacade, DurationEstimationFacade>()
                .AddTransient<IInstructionFacade, InstructionFacade>()
                .AddTransient<IMachineTypeFacade, MachineTypeFacade>()
                .AddTransient<IMachineFacade, MachineFacade>()
                .AddTransient<IMonitoringSpecificationMachineFacade, MonitoringSpecificationMachineFacade>()
                .AddTransient<IMonitoringSpecificationMachineReportFacade, MonitoringSpecificationMachineReportFacade>()
                .AddTransient<IMonitoringEventFacade, MonitoringEventFacade>()
                .AddTransient<IMachineEventFacade, MachineEventFacade>()
                .AddTransient<IKanbanFacade, KanbanFacade>()
                .AddTransient<IBadOutputFacade, BadOutputFacade>()
                .AddScoped<IDailyOperationFacade, DailyOperationFacade>()
                .AddTransient<IPackingFacade, PackingFacade>()
                .AddTransient<IDailyOperationFacade, DailyOperationFacade>()
                .AddTransient<IPackingFacade, PackingFacade>()
                .AddTransient<IFabricQualityControlFacade, FabricQualityControlFacade>()
                .AddTransient<MachineEventFacade>()
                .AddTransient<IMonitoringEventReportFacade, MonitoringEventReportFacade>()
                .AddTransient<IPackingReceiptFacade,PackingReceiptFacade>()
                .AddTransient<IShipmentDocumentService, ShipmentDocumentService>()
                .AddTransient<IDirectLaborCostFacade, DirectLaborCostFacade>()
                .AddTransient<IReturToQCFacade, ReturToQCFacade>();
        }

        private void RegisterLogics(IServiceCollection services)
        {
            services
                .AddTransient<DirectLaborCostLogic>()
                .AddTransient<StepLogic>()
                .AddTransient<StepIndicatorLogic>()
                .AddTransient<InstructionLogic>()
                .AddTransient<DurationEstimationLogic>()
                .AddTransient<MachineTypeLogic>()
                .AddTransient<MachineTypeIndicatorsLogic>()
                .AddTransient<MachineEventLogic>()
                .AddTransient<MachineLogic>()
                .AddTransient<MachineStepLogic>()
                .AddTransient<MonitoringSpecificationMachineLogic>()
                .AddTransient<MonitoringSpecificationMachineDetailsLogic>()
                .AddTransient<MonitoringEventLogic>()
                .AddTransient<BadOutputLogic>()
                .AddTransient<BadOutputMachineLogic>()
                .AddTransient<DailyOperationBadOutputReasonsLogic>()
                .AddTransient<DailyOperationLogic>()
                .AddTransient<PackingLogic>()
                .AddTransient<FabricQualityControlLogic>()
                .AddTransient<PackingReceiptLogic>()
                .AddTransient<KanbanLogic>()
                .AddTransient<ReturToQCLogic>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString(Constant.DEFAULT_CONNECTION) ?? Configuration[Constant.DEFAULT_CONNECTION];
            string env = Configuration.GetValue<string>(Constant.ASPNETCORE_ENVIRONMENT);

            #region Register
            services.AddDbContext<ProductionDbContext>(options => options.UseSqlServer(connectionString, c => c.CommandTimeout(60)));

            RegisterServices(services, env.Equals("Test"));

            RegisterFacades(services);

            RegisterLogics(services);

            RegisterEndpoint();

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
