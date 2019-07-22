using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.OrderStatusReport;
using Com.Danliris.Service.Finishing.Printing.Lib.Services.HttpClientService;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils.MasterDataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Services.OrderStatusReportServiceTest
{
    public class OrderStatusReportServiceTest
    {
        private const string ENTITY = "OrderStatusReport";

        [MethodImpl(MethodImplOptions.NoInlining)]
        public string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return string.Concat(sf.GetMethod().Name, "_", ENTITY);
        }

        private ProductionDbContext _dbContext(string testName)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProductionDbContext>();
            optionsBuilder
                .UseInMemoryDatabase(testName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            ProductionDbContext dbContext = new ProductionDbContext(optionsBuilder.Options);

            return dbContext;
        }

        private IServiceProvider _serviceProviderStandard()
        {
            var serviceProvider = new Mock<IServiceProvider>();

            serviceProvider
                .Setup(x => x.GetService(typeof(IHttpClientService)))
                .Returns(new HttpClientTestService());

            serviceProvider
                .Setup(x => x.GetService(typeof(IIdentityService)))
                .Returns(new IdentityService() { Token = "Token", Username = "Test" });


            return serviceProvider.Object;
        }

        private IServiceProvider _serviceProviderYearly()
        {
            var serviceProvider = new Mock<IServiceProvider>();

            serviceProvider
                .Setup(x => x.GetService(typeof(IHttpClientService)))
                .Returns(new HttpResponseTestGetYearly());

            serviceProvider
                .Setup(x => x.GetService(typeof(IIdentityService)))
                .Returns(new IdentityService() { Token = "Token", Username = "Test" });


            return serviceProvider.Object;
        }

        private IServiceProvider _serviceProviderMonthly()
        {
            var serviceProvider = new Mock<IServiceProvider>();

            serviceProvider
                .Setup(x => x.GetService(typeof(IHttpClientService)))
                .Returns(new HttpResponseTestGetMonthly());

            serviceProvider
                .Setup(x => x.GetService(typeof(IIdentityService)))
                .Returns(new IdentityService() { Token = "Token", Username = "Test" });


            return serviceProvider.Object;
        }

        private KanbanDataUtil _kanbanDataUtil(ProductionDbContext dbContext)
        {
            var machineFacade = new MachineFacade(_serviceProviderStandard(), dbContext);
            var machineDataUtil = new MachineDataUtil(machineFacade);

            var kanbanFacade = new KanbanFacade(_serviceProviderStandard(), dbContext);


            //MachineDataUtil machineDataUtil, KanbanFacade facade
            return new KanbanDataUtil(machineDataUtil, kanbanFacade);
        }

        [Fact]
        public async Task Should_Success_GetYearlyOrderStatusReport()
        {
            var dbContext = _dbContext(GetCurrentMethod());

            _kanbanDataUtil(dbContext).GetNewData();

            var service = new OrderStatusReportService(dbContext, _serviceProviderYearly());

            var response = await service.GetYearlyOrderStatusReport(DateTime.Now.Year, 0);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_Success_GetMonthlyOrderStatusReport()
        {
            var dbContext = _dbContext(GetCurrentMethod());

            _kanbanDataUtil(dbContext).GetNewData();

            var service = new OrderStatusReportService(dbContext, _serviceProviderMonthly());

            var response = await service.GetMonthlyOrderStatusReport(DateTime.Now.Year, DateTime.Now.Month, 0);
            Assert.NotNull(response);
        }

        [Fact]
        public async Task Should_Success_GetProductionOrderStatusReport()
        {
            var dbContext = _dbContext(GetCurrentMethod());

            _kanbanDataUtil(dbContext).GetNewData();

            var service = new OrderStatusReportService(dbContext, _serviceProviderMonthly());

            var response = await service.GetProductionOrderStatusReport(0);
            Assert.NotNull(response);
        }
    }
}
