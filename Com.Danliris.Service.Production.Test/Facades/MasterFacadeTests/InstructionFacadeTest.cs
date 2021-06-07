using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils.MasterDataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.AutoMapperProfiles.Master;
using Com.Danliris.Service.Production.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Production.Lib.BusinessLogic.Implementations.Master.Instruction;
using Com.Danliris.Service.Production.Lib.Models.Master.Instruction;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.ViewModels.Master.Instruction;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Facades.MasterFacadeTests
{
    public class InstructionFacadeTest : BaseFacadeTest<ProductionDbContext, InstructionFacade, InstructionLogic, InstructionModel, InstructionDataUtil>
    {
        private const string ENTITY = "Instruction";

        public InstructionFacadeTest() : base(ENTITY)
        {
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(ProductionDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(InstructionLogic)))
                .Returns(new InstructionLogic(identityService, dbContext));

            return serviceProviderMock;
        }

        [Fact]
        public async void Get_Read_VM()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            InstructionFacade facade = Activator.CreateInstance(typeof(InstructionFacade), serviceProvider, dbContext) as InstructionFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = facade.ReadVM(1, 25, "{}", new List<string>(), "test", "{}");

            Assert.NotEmpty(Response.Data);
        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<InstructionProfile>();
            });
            var mapper = configuration.CreateMapper();

            InstructionViewModel vm = new InstructionViewModel { Id = 1 };
            InstructionModel model = mapper.Map<InstructionModel>(vm);

            Assert.Equal(vm.Id, model.Id);

        }
    }
}
