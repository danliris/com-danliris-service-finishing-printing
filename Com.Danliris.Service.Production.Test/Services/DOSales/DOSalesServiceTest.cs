using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.DOSales;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.DOSales;
using Com.Danliris.Service.Finishing.Printing.Lib.Services.HttpClientService;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DOSales;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Services.DOSales
{
    public class DOSalesServiceTest
    {
        private const string ENTITY = "DOSales";

        [MethodImpl(MethodImplOptions.NoInlining)]
        public string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return string.Concat(sf.GetMethod().Name, "_", ENTITY);
        }

        private ProductionDbContext _dbContext(string testName)
        {
            DbContextOptionsBuilder<ProductionDbContext> optionsBuilder = new DbContextOptionsBuilder<ProductionDbContext>();
            optionsBuilder
                .UseInMemoryDatabase(testName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            ProductionDbContext dbContext = new ProductionDbContext(optionsBuilder.Options);

            return dbContext;
        }

        private DOSalesDataUtil _dataUtil(DOSalesFacade service)
        {
            return new DOSalesDataUtil(service);
        }

        private Mock<IServiceProvider> GetServiceProvider()
        {
            var serviceProvider = new Mock<IServiceProvider>();

            serviceProvider
                .Setup(x => x.GetService(typeof(IHttpClientService)))
                .Returns(new HttpClientTestService());

            serviceProvider
                .Setup(x => x.GetService(typeof(IIdentityService)))
                .Returns(new IdentityService() { Token = "Token", Username = "Test", TimezoneOffset = 7 });


            return serviceProvider;
        }

        //[Fact]
        //public async Task Should_Success_Get_Data()
        //{
        //    DOSalesFacade service = new DOSalesFacade(GetServiceProvider().Object, _dbContext(GetCurrentMethod()));
        //    var data = await _dataUtil(service).GetTestData();
        //    var Response = service.Read(1, 25, "{}", null, data.Code, "{}");
        //    Assert.NotEmpty(Response.Data);
        //}

        //[Fact]
        //public async Task Should_Success_Get_Data_By_Id()
        //{
        //    DOSalesFacade service = new DOSalesFacade(GetServiceProvider().Object, _dbContext(GetCurrentMethod()));
        //    DOSalesModel model = await _dataUtil(service).GetTestData();
        //    var Response = await service.ReadByIdAsync(model.Id);
        //    Assert.NotNull(Response);
        //}

        //[Fact]
        //public async Task Should_Success_Create_Data()
        //{
        //    DOSalesFacade service = new DOSalesFacade(GetServiceProvider().Object, _dbContext(GetCurrentMethod()));
        //    DOSalesModel model = _dataUtil(service).GetNewData();
        //    var Response = await service.CreateAsync(model);
        //    Assert.NotEqual(0, Response);
        //}

        //[Fact]
        //public void Should_No_Error_Validate_Data()
        //{
        //    DOSalesFacade service = new DOSalesFacade(GetServiceProvider().Object, _dbContext(GetCurrentMethod()));
        //    DOSalesViewModel vm = _dataUtil(service).GetDataToValidate();

        //    Assert.True(vm.Validate(null).Count() == 0);
        //}

        [Fact]
        public void Should_Success_Validate_All_Null_Data()
        {
            var vm = new DOSalesViewModel();

            Assert.True(vm.Validate(null).Count() > 0);
        }

        [Fact]
        public void Should_Success_Create_New_View_Model()
        {
            var viewModel = new DOSalesViewModel()
            {
                DOSalesDetails = new List<DOSalesDetailViewModel>()
                {
                    new DOSalesDetailViewModel()
                    {

                    }
                }
            };

            Assert.NotEmpty(viewModel.DOSalesDetails);
        }

        //[Fact]
        //public async Task Should_Success_Update_Data()
        //{
        //    DOSalesFacade service = new DOSalesFacade(GetServiceProvider().Object, _dbContext(GetCurrentMethod()));
        //    DOSalesModel model = await _dataUtil(service).GetTestData();
        //    var newModel = await service.ReadByIdAsync(model.Id);
        //    newModel.UId = "";
        //    var Response = await service.UpdateAsync(newModel.Id, newModel);
        //    Assert.NotEqual(0, Response);
        //}

        //[Fact]
        //public async Task Should_Success_Delete_Data()
        //{
        //    DOSalesFacade service = new DOSalesFacade(GetServiceProvider().Object, _dbContext(GetCurrentMethod()));
        //    DOSalesModel model = await _dataUtil(service).GetTestData();
        //    var newModel = await service.ReadByIdAsync(model.Id);

        //    var Response = await service.DeleteAsync(newModel.Id);
        //    Assert.NotEqual(0, Response);
        //}

    }
}
