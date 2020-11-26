using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.DailyOperation;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.DailyOperation;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.Machine;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Kanban;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Kanban;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils.MasterDataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Facades
{
    public class KanbanFacadeTest : BaseFacadeTest<ProductionDbContext, KanbanFacade, KanbanLogic, KanbanModel, KanbanDataUtil>
    {
        private const string ENTITY = "Kanban";

        public KanbanFacadeTest() : base(ENTITY)
        {
        }

        protected override KanbanDataUtil DataUtil(KanbanFacade facade, ProductionDbContext dbContext = null)
        {
            IServiceProvider serviceProvider = GetServiceProviderMock(dbContext).Object;

            MachineFacade machineFacade = new MachineFacade(serviceProvider, dbContext);
            MachineDataUtil machineDataUtil = new MachineDataUtil(machineFacade);

            KanbanDataUtil dataUtil = new KanbanDataUtil(machineDataUtil, facade);

            return dataUtil;
        }

        private DailyOperationDataUtil DODataUtil(DailyOperationFacade facade, KanbanFacade kanbanFacade, ProductionDbContext dbContext = null)
        {
            KanbanDataUtil kanbanDataUtil = DataUtil(kanbanFacade, dbContext);

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
        public virtual async void Get_Reports()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            KanbanFacade facade = new KanbanFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = facade.GetReport(1, 25, null,0,0,null, data.CreatedUtc.AddDays(-1), data.CreatedUtc.AddDays(1), 7);

            Assert.NotEmpty(Response.Data);
        }

        [Fact]
        public  async void CreateAsync_Success()
        {
            //Setup
            var dbContext = DbContext(GetCurrentAsyncMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            KanbanFacade facade = new KanbanFacade(serviceProvider, dbContext);

            //Act
            var newdata = await DataUtil(facade, dbContext).GetNewDataAsyncToUpdate();
            var response = await facade.CreateAsync(newdata);

            //Assert
            Assert.True(0 < response);
        }

        [Fact]
        public async void UpdateAsync_Success()
        {
            //Setup
            var dbContext = DbContext(GetCurrentAsyncMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            KanbanFacade facade = new KanbanFacade(serviceProvider, dbContext);

            //Act
            var data = await DataUtil(facade, dbContext).GetNewDataAsyncToUpdate();
         
            var response = await facade.UpdateAsync(data.Id, data);

            //Assert
            Assert.True(0 < response);
        }



        [Fact]
        public virtual async void Generate_Excels()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            KanbanFacade facade = new KanbanFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = facade.GenerateExcel(null, 0, 0, null, data.CreatedUtc.AddDays(-1), data.CreatedUtc.AddDays(1), 7);

            Assert.NotNull(Response);
        }

        [Fact]
        public void Generate_Excels_with_EmptyData()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            KanbanFacade facade = new KanbanFacade(serviceProvider, dbContext);

            var Response = facade.GenerateExcel(null, 0, 0, null, DateTime.Now, DateTime.Now, 7);

            Assert.NotNull(Response);
        }

        [Fact]
        public virtual async void Complete_Kanban()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            KanbanFacade facade = new KanbanFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = facade.CompleteKanban(data.Id);

            Assert.NotNull(Response);
        }

        [Fact]
        public virtual async void ReadOldKanban()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            KanbanFacade facade = new KanbanFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = facade.ReadOldKanbanByIdAsync(data.Id);

            Assert.NotNull(Response);
        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<KanbanProfile>();
            });
            var mapper = configuration.CreateMapper();

            KanbanViewModel vm = new KanbanViewModel { Id = 1 };
            KanbanModel model = mapper.Map<KanbanModel>(vm);

            Assert.Equal(vm.Id, model.Id);

        }

        [Fact]
        public virtual async void Generate_Excels_Snapshot()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            KanbanFacade kanbanFacade = new KanbanFacade(serviceProvider, dbContext);
            DailyOperationFacade facade = new DailyOperationFacade(serviceProvider, dbContext);
            var data = await DODataUtil(facade, kanbanFacade, dbContext).GetTestData();
            var dataOut =  DODataUtil(facade, kanbanFacade, dbContext).GetNewDataOut(data);
            var kanban = await kanbanFacade.ReadByIdAsync(data.KanbanId);
            dataOut.KanbanId = data.KanbanId;
            dataOut.StepId = kanban.Instruction.Steps.First().Id;
            dataOut.MachineId = kanban.Instruction.Steps.First().MachineId;
            var outModel = await facade.CreateAsync(dataOut);
            var Response = kanbanFacade.GenerateKanbanSnapshotExcel(data.CreatedUtc.Month, data.CreatedUtc.Year);

            Assert.NotNull(Response);
        }

        [Fact]
        public virtual void Generate_Excels_Snapshot_Empty()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;
            KanbanFacade kanbanFacade = new KanbanFacade(serviceProvider, dbContext);
            
            var Response = kanbanFacade.GenerateKanbanSnapshotExcel(DateTime.UtcNow.Month, DateTime.UtcNow.Year);

            Assert.NotNull(Response);
        }

        [Fact]
        public virtual async void Read_Visualization()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            KanbanFacade facade = new KanbanFacade(serviceProvider, dbContext);
            DailyOperationFacade doFacade = new DailyOperationFacade(serviceProvider, dbContext);
            var dataIn = await DODataUtil(doFacade, facade, dbContext).GetTestData();
            var dataOut = DODataUtil(doFacade, facade, dbContext).GetNewDataOut(dataIn);
            var kanban = await facade.ReadByIdAsync(dataIn.KanbanId);
            dataOut.KanbanId = dataIn.KanbanId;
            dataOut.StepId = kanban.Instruction.Steps.First().Id;
            dataOut.MachineId = kanban.Instruction.Steps.First().MachineId;
            var outModel = await doFacade.CreateAsync(dataOut);

            var Response = facade.ReadVisualization("{}", "{}");

            Assert.NotEmpty(Response.Data);
        }

        [Fact]
        public virtual async void Read_Visualization2()
        {
            var dbContext = DbContext(GetCurrentMethod() + "Read_Visualization2");
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            KanbanFacade facade = new KanbanFacade(serviceProvider, dbContext);
            DailyOperationFacade doFacade = new DailyOperationFacade(serviceProvider, dbContext);
            var dataIn = await DODataUtil(doFacade, facade, dbContext).GetTestData();

            var Response = facade.ReadVisualization("{}", "{}");

            Assert.NotEmpty(Response.Data);
        }
    }
}
