using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Utilities.BaseClass;
using Com.Danliris.Service.Production.Lib.Utilities.BaseInterface;
using Com.Moonlay.Data.EntityFrameworkCore;
using Com.Moonlay.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Utils
{
    public abstract class BaseFacadeTest<TDbContext, TFacade, TLogic, TModel, TDataUtil>
        where TDbContext : StandardDbContext
        where TFacade : class, IBaseFacade<TModel>
        where TLogic : BaseLogic<TModel>
        where TModel : StandardEntity, IValidatableObject
        where TDataUtil : BaseDataUtil<TFacade, TModel>
    {
        private string _entity;

        public BaseFacadeTest(string entity)
        {
            _entity = entity;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return string.Concat(sf.GetMethod().Name, "_", _entity);
        }

        private TDbContext DbContext(string testName)
        {
            DbContextOptionsBuilder<TDbContext> optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
            optionsBuilder
                .UseInMemoryDatabase(testName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            TDbContext dbContext = Activator.CreateInstance(typeof(TDbContext), optionsBuilder.Options) as TDbContext;

            return dbContext;
        }

        protected virtual Mock<IServiceProvider> GetServiceProviderMock(TDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(TLogic)))
                .Returns(Activator.CreateInstance(typeof(TLogic), identityService, dbContext) as TLogic);

            return serviceProviderMock;
        }

        protected virtual TDataUtil DataUtil(TFacade facade, TDbContext dbContext = null)
        {
            TDataUtil dataUtil = Activator.CreateInstance(typeof(TDataUtil), facade) as TDataUtil;
            return dataUtil;
        }

        [Fact]
        public virtual async void Create_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            TFacade facade = Activator.CreateInstance(typeof(TFacade), serviceProvider, dbContext) as TFacade;

            var data = DataUtil(facade).GetNewData();

            var response = await facade.CreateAsync(data);

            Assert.NotEqual(0, response);
        }

        [Fact]
        public virtual async void Get_All_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            TFacade facade = Activator.CreateInstance(typeof(TFacade), serviceProvider, dbContext) as TFacade;

            var data = await DataUtil(facade).GetTestData();

            var Response = facade.Read(1, 25, "{}", new List<string>(), "", "{}");

            Assert.NotEmpty(Response.Data);
        }

        [Fact]
        public virtual async void Get_By_Id_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            TFacade facade = Activator.CreateInstance(typeof(TFacade), serviceProvider, dbContext) as TFacade;

            var data = await DataUtil(facade).GetTestData();

            var Response = facade.ReadByIdAsync((int)data.Id);

            Assert.NotEqual(0, Response.Id);
        }

        [Fact]
        public virtual async void Update_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            TFacade facade = Activator.CreateInstance(typeof(TFacade), serviceProvider, dbContext) as TFacade;

            var data = await DataUtil(facade).GetTestData();

            var response = await facade.UpdateAsync((int)data.Id, data);

            Assert.NotEqual(0, response);
        }

        [Fact]
        public virtual async void Delete_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            TFacade facade = Activator.CreateInstance(typeof(TFacade), serviceProvider, dbContext) as TFacade;
            var data = await DataUtil(facade).GetTestData();

            var Response = await facade.DeleteAsync((int)data.Id);
            Assert.NotEqual(0, Response);
        }
    }
}
