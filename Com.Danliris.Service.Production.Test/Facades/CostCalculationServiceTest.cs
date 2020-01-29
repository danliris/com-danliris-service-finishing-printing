using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.CostCalculation;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.CostCalculation;
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

namespace Com.Danliris.Service.Finishing.Printing.Test.Facades
{
    public class CostCalculationServiceTest
    {
        private const string ENTITY = "CostCalculation";


        [MethodImpl(MethodImplOptions.NoInlining)]
        public string GetCurrentMethod()
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            return string.Concat(sf.GetMethod().Name, "_", ENTITY);
        }

        private ProductionDbContext GetDbContext(string testName)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProductionDbContext>();
            optionsBuilder
                .UseInMemoryDatabase(testName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            var dbContext = new ProductionDbContext(optionsBuilder.Options);

            return dbContext;
        }

        private CostCalculationService GetService(ProductionDbContext dbContext, IServiceProvider serviceProvider)
        {
            return new CostCalculationService(dbContext, serviceProvider);
        }

        private CostCalculationViewModel GetInvalidViewModel()
        {
            return new CostCalculationViewModel()
            {
                Machines = new List<CostCalculationMachineViewModel>()
                {
                    new CostCalculationMachineViewModel()
                    {
                        Chemicals = new List<CostCalculationChemicalViewModel>()
                        {
                            new CostCalculationChemicalViewModel()
                        }
                    }
                }
            };
        }

        private CostCalculationViewModel GetValidViewModel()
        {
            return new CostCalculationViewModel()
            {
                ActualPrice = 1,
                CargoCost = 1,
                CurrencyRate = 1,
                Date = DateTimeOffset.Now,
                GreigeId = 1,
                InstructionId = 1,
                PreparationFabricWeight = 1,
                PreparationValue = 1,
                ProductionOrderId = 1,
                ProductionOrderNo = "OrderNo",
                ProductionUnitValue = 1,
                RFDFabricWeight = 1,
                TKLQuantity = 1,
                InsuranceCost = 1,
                Machines = new List<CostCalculationMachineViewModel>()
                {
                    new CostCalculationMachineViewModel()
                    {
                        Index =1,
                        MachineId = 1,
                        StepProcessId = 1,
                        Chemicals = new List<CostCalculationChemicalViewModel>()
                        {
                            new CostCalculationChemicalViewModel()
                            {
                                ChemicalId = 1,
                                ChemicalQuantity = 1,
                                Index = 1
                            }
                        }
                    }
                }
            };
        }

        [Fact]
        public void Should_Validate_Invalid_Data()
        {
            var viewModelToValidate = GetInvalidViewModel();
            Assert.True(viewModelToValidate.Validate(null).Count() > 0);
        }

        [Fact]
        public void Should_Validate_Invalid_Data_Null_Chemicals()
        {
            var viewModelToValidate = GetInvalidViewModel();
            viewModelToValidate.Machines = new List<CostCalculationMachineViewModel>()
            {
                new CostCalculationMachineViewModel()
            };
            Assert.True(viewModelToValidate.Validate(null).Count() > 0);
        }

        [Fact]
        public void Should_Validate_Invalid_Data_Null_Machines()
        {
            var viewModelToValidate = GetInvalidViewModel();
            viewModelToValidate.Machines = null;
            Assert.True(viewModelToValidate.Validate(null).Count() > 0);
        }

        [Fact]
        public void Should_Validate_Valid_Data()
        {
            var viewModelToValidate = GetValidViewModel();
            Assert.True(viewModelToValidate.Validate(null).Count() == 0);
        }

        [Fact]
        public async Task Should_Success_Create_Data()
        {
            var viewModelToCreate = GetValidViewModel();
            var modelToCreate = viewModelToCreate.MapViewModelToCreateModel();

            var dbContext = GetDbContext(GetCurrentMethod());

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(serviceProvider => serviceProvider.GetService(typeof(IIdentityService)))
                .Returns(new IdentityService() { TimezoneOffset = 1, Token = "token", Username = "username" });

            var service = GetService(dbContext, serviceProviderMock.Object);
            var result = await service.InsertSingle(modelToCreate);

            Assert.True(result != 0);
        }

        [Fact]
        public async Task Should_Success_Delete_Data()
        {
            var viewModelToCreate = GetValidViewModel();
            var modelToCreate = viewModelToCreate.MapViewModelToCreateModel();

            var dbContext = GetDbContext(GetCurrentMethod());

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(serviceProvider => serviceProvider.GetService(typeof(IIdentityService)))
                .Returns(new IdentityService() { TimezoneOffset = 1, Token = "token", Username = "username" });

            var service = GetService(dbContext, serviceProviderMock.Object);
            await service.InsertSingle(modelToCreate);

            var result = await service.DeleteSingle(modelToCreate.Id);

            Assert.True(result != 0);
        }

        [Fact]
        public async Task Should_Success_Get_Single_By_Id()
        {
            var viewModelToCreate = GetValidViewModel();
            var modelToCreate = viewModelToCreate.MapViewModelToCreateModel();

            var dbContext = GetDbContext(GetCurrentMethod());

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(serviceProvider => serviceProvider.GetService(typeof(IIdentityService)))
                .Returns(new IdentityService() { TimezoneOffset = 1, Token = "token", Username = "username" });

            var service = GetService(dbContext, serviceProviderMock.Object);
            await service.InsertSingle(modelToCreate);

            var result = await service.GetSingleById(modelToCreate.Id);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Null_Get_Single_By_Id_Not_Found()
        {
            var dbContext = GetDbContext(GetCurrentMethod());

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(serviceProvider => serviceProvider.GetService(typeof(IIdentityService)))
                .Returns(new IdentityService() { TimezoneOffset = 1, Token = "token", Username = "username" });

            var service = GetService(dbContext, serviceProviderMock.Object);

            var result = await service.GetSingleById(0);

            Assert.Null(result);
        }

        [Fact]
        public async Task Should_Success_Get_Paged()
        {
            var viewModelToCreate = GetValidViewModel();
            var modelToCreate = viewModelToCreate.MapViewModelToCreateModel();

            var dbContext = GetDbContext(GetCurrentMethod());

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(serviceProvider => serviceProvider.GetService(typeof(IIdentityService)))
                .Returns(new IdentityService() { TimezoneOffset = 1, Token = "token", Username = "username" });

            var service = GetService(dbContext, serviceProviderMock.Object);
            await service.InsertSingle(modelToCreate);

            var result = await service.GetPaged(1, 15, "{}", modelToCreate.ProductionOrderNo, "{}");

            Assert.True(result.Data.Count > 0);
        }

        [Fact]
        public async Task Should_IsDataExistsById_Success()
        {
            var dbContext = GetDbContext(GetCurrentMethod());

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(serviceProvider => serviceProvider.GetService(typeof(IIdentityService)))
                .Returns(new IdentityService() { TimezoneOffset = 1, Token = "token", Username = "username" });

            var service = GetService(dbContext, serviceProviderMock.Object);

            var result = await service.IsDataExistsById(0);

            Assert.False(result);
        }
    }
}
