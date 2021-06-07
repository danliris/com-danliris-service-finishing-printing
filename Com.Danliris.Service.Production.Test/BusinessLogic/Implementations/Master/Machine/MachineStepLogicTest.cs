using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.Machine;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.Machine;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.BusinessLogic.Implementations.Master.Machine
{
    public class MachineStepLogicTest
    {
        private const string ENTITY = "MachineStep";
        
        [MethodImpl(MethodImplOptions.NoInlining)]
        protected string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return string.Concat(sf.GetMethod().Name, "_", ENTITY);
        }

        protected ProductionDbContext GetDbContext(string testName)
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            DbContextOptionsBuilder<ProductionDbContext> optionsBuilder = new DbContextOptionsBuilder<ProductionDbContext>();
            optionsBuilder
                .UseInMemoryDatabase(testName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .UseInternalServiceProvider(serviceProvider);

            ProductionDbContext dbContext = new ProductionDbContext(optionsBuilder.Options);

            return dbContext;
        }

        protected Mock<IServiceProvider> GetServiceProviderMock(ProductionDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            return serviceProviderMock;
        }

        public MachineStepDataUtil _dataUtil(ProductionDbContext dbContext)
        {
            return new MachineStepDataUtil(dbContext);
        }

        [Fact]
        public void CreateModel_Success()
        {
            //Setup
            IIdentityService identityService = new IdentityService { Username = "Username" };

            ProductionDbContext dbContext = GetDbContext(GetCurrentMethod());
           
            //Act
            MachineStepLogic machineStepLogic = new MachineStepLogic(identityService, dbContext);
            MachineStepModel machineStepModel = _dataUtil(dbContext).GetNewdata();

            //Assert
            machineStepLogic.CreateModel(machineStepModel);

        }
    }
}
