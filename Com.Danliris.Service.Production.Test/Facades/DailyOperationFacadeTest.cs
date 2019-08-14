using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.DailyOperation;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.DailyOperation;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.Machine;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Daily_Operation;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Daily_Operation;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils.MasterDataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Facades
{
    public class DailyOperationFacadeTest : BaseFacadeTest<ProductionDbContext, DailyOperationFacade, DailyOperationLogic, DailyOperationModel, DailyOperationDataUtil>
    {
        private const string ENTITY = "DailyOperation";

        public DailyOperationFacadeTest() : base(ENTITY)
        {
        }

        protected override DailyOperationDataUtil DataUtil(DailyOperationFacade facade, ProductionDbContext dbContext = null)
        {
            IServiceProvider serviceProvider = GetServiceProviderMock(dbContext).Object;

            MachineFacade machineFacade = new MachineFacade(serviceProvider, dbContext);
            MachineDataUtil machineDataUtil = new MachineDataUtil(machineFacade);

            KanbanFacade kanbanFacade = new KanbanFacade(serviceProvider, dbContext);
            KanbanDataUtil kanbanDataUtil = new KanbanDataUtil(machineDataUtil, kanbanFacade);

            DailyOperationDataUtil dataUtil = new DailyOperationDataUtil(kanbanDataUtil, facade);

            return dataUtil;
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(ProductionDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            MachineEventLogic machineEventLogic = new MachineEventLogic(identityService, dbContext);
            MachineStepLogic machineStepLogic = new MachineStepLogic(identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(MachineLogic)))
                .Returns(new MachineLogic(machineEventLogic, machineStepLogic, identityService, dbContext));

            serviceProviderMock
                .Setup(x => x.GetService(typeof(KanbanLogic)))
                .Returns(new KanbanLogic(identityService, dbContext));

            DailyOperationBadOutputReasonsLogic dailyOperationBadOutputReasonsLogic = new DailyOperationBadOutputReasonsLogic(identityService, dbContext);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(DailyOperationLogic)))
                .Returns(new DailyOperationLogic(dailyOperationBadOutputReasonsLogic, identityService, dbContext));

            return serviceProviderMock;
        }

        [Fact]
        public async Task GetJoinKanban_WithoutNo()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            DailyOperationFacade facade = Activator.CreateInstance(typeof(DailyOperationFacade), serviceProvider, dbContext) as DailyOperationFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = await facade.GetJoinKanban(null);

            Assert.NotNull(Response);
        }

        [Fact]
        public async Task GetJoinKanban_WithNo()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            DailyOperationFacade facade = Activator.CreateInstance(typeof(DailyOperationFacade), serviceProvider, dbContext) as DailyOperationFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = await facade.GetJoinKanban("a");

            Assert.NotNull(Response);
        }

        [Fact]
        public async Task GetReport()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            DailyOperationFacade facade = Activator.CreateInstance(typeof(DailyOperationFacade), serviceProvider, dbContext) as DailyOperationFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = facade.GetReport(-1, -1, DateTime.UtcNow.AddDays(-30), DateTime.UtcNow.AddDays(30), 7);

            Assert.NotEmpty(Response);
        }

        [Fact]
        public async Task HasOutput_False()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            DailyOperationFacade facade = Activator.CreateInstance(typeof(DailyOperationFacade), serviceProvider, dbContext) as DailyOperationFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = await facade.HasOutput(data.KanbanId, data.StepProcess);

            Assert.False(Response);
        }

        [Fact]
        public void Validate_VM_NULL()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            DailyOperationViewModel vm = new DailyOperationViewModel();
            System.ComponentModel.DataAnnotations.ValidationContext context = new System.ComponentModel.DataAnnotations.ValidationContext(vm, serviceProvider, null);
            Assert.NotEmpty(vm.Validate(context));

            vm.Type = "s";
            Assert.NotEmpty(vm.Validate(context));

            vm.Shift = "s";
            Assert.NotEmpty(vm.Validate(context));

            vm.Machine = new Lib.ViewModels.Master.Machine.MachineViewModel();
            Assert.NotEmpty(vm.Validate(context));

            vm.Step = new Lib.ViewModels.Master.Machine.MachineStepViewModel();
            Assert.NotEmpty(vm.Validate(context));

            vm.Kanban = new Lib.ViewModels.Kanban.KanbanViewModel()
            {
                CurrentStepIndex = 0,
                Instruction = new Lib.ViewModels.Kanban.KanbanInstructionViewModel()
                {
                    Steps = new List<Lib.ViewModels.Kanban.KanbanStepViewModel>()
                    {
                        new Lib.ViewModels.Kanban.KanbanStepViewModel()
                        {
                            Process = "process"
                        }
                    }
                }
            };
            Assert.NotEmpty(vm.Validate(context));


            vm.Kanban = new Lib.ViewModels.Kanban.KanbanViewModel();

            vm.Type = "input";
            Assert.NotEmpty(vm.Validate(context));

            vm.DateInput = DateTime.Now.AddDays(1);
            Assert.NotEmpty(vm.Validate(context));

            vm.DateInput = null;
            Assert.NotEmpty(vm.Validate(context));

            vm.Input = 0;
            Assert.NotEmpty(vm.Validate(context));

            vm.TimeInput = 0;
            Assert.NotEmpty(vm.Validate(context));

            vm.Type = "output";
            Assert.NotEmpty(vm.Validate(context));

            vm.DateOutput = DateTime.Now;
            vm.BadOutputReasons = new List<DailyOperationBadOutputReasonsViewModel>();
            vm.BadOutput = 1;
            Assert.NotEmpty(vm.Validate(context));

            vm.DateOutput = DateTime.Now;
            vm.BadOutputReasons = new List<DailyOperationBadOutputReasonsViewModel>();
            vm.BadOutput = 1;
            Assert.NotEmpty(vm.Validate(context));

            vm.BadOutputReasons = new List<DailyOperationBadOutputReasonsViewModel>()
            {
                new DailyOperationBadOutputReasonsViewModel()
                {
                    Length = 0
                }
            };
            vm.BadOutput = 1;
            Assert.NotEmpty(vm.Validate(context));

            vm.DateOutput = DateTime.Now.AddDays(1);
            Assert.NotEmpty(vm.Validate(context));

            vm.DateOutput = null;
            Assert.NotEmpty(vm.Validate(context));

            vm.GoodOutput = 0;
            Assert.NotEmpty(vm.Validate(context));

            vm.TimeOutput = 0;
            Assert.NotEmpty(vm.Validate(context));
        }

        [Fact]
        public async Task Validate_VM_Exist()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            DailyOperationFacade facade = Activator.CreateInstance(typeof(DailyOperationFacade), serviceProvider, dbContext) as DailyOperationFacade;

            var data = await DataUtil(facade, dbContext).GetTestData();
            DailyOperationViewModel vm = new DailyOperationViewModel()
            {
                Type = data.Type,
                Shift = data.Shift,
                Kanban = new Lib.ViewModels.Kanban.KanbanViewModel()
                {
                    Id = data.KanbanId
                },
                Step = new Lib.ViewModels.Master.Machine.MachineStepViewModel()
                {
                    StepId = data.StepId
                },
                Machine = new Lib.ViewModels.Master.Machine.MachineViewModel(),
                
            };
            System.ComponentModel.DataAnnotations.ValidationContext context = new System.ComponentModel.DataAnnotations.ValidationContext(vm, serviceProvider, null);
            Assert.NotEmpty(vm.Validate(context));
        }

    }
}
