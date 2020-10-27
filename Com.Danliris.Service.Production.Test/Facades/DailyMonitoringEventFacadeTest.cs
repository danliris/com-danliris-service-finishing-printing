using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.DailyMonitoringEvent;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.DailyMonitoringEvent;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.DailyMonitoringEvent;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.DailyMonitoringEvent;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DailyMonitoringEvent;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.ValidateService;
using Com.Danliris.Service.Production.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Facades
{
    public class DailyMonitoringEventFacadeTest : BaseFacadeTest<ProductionDbContext, DailyMonitoringEventFacade, DailyMonitoringEventLogic, DailyMonitoringEventModel, DailyMonitoringEventDataUtil>
    {
        private const string ENTITY = "DailyMonitoringEvent";
        public DailyMonitoringEventFacadeTest() : base(ENTITY)
        {
        }

        [Fact]
        public virtual async void Update_Success_2()
        {
            string testName = GetCurrentMethod() + " Update_Success_2";
            var dbContext = DbContext(testName);
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            DailyMonitoringEventFacade facade = new DailyMonitoringEventFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();
            var data2 = new DailyMonitoringEventModel()
            {
                Id = data.Id,
                Code = data.Code,

                DailyMonitoringEventLossEventItems = data.DailyMonitoringEventLossEventItems.Select(s => new DailyMonitoringEventLossEventItemModel()
                {
                    Time = s.Time,
                }).ToList(),
                DailyMonitoringEventProductionOrderItems = data.DailyMonitoringEventProductionOrderItems.Select(s => new DailyMonitoringEventProductionOrderItemModel()
                {
                    Speed = s.Speed
                }).ToList()
            };
            var response = await facade.UpdateAsync((int)data.Id, data2);

            Assert.NotEqual(0, response);
        }


        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DailyMonitoringEventProfile>();
            });
            var mapper = configuration.CreateMapper();

            DailyMonitoringEventViewModel vm = new DailyMonitoringEventViewModel
            {
                Id = 1,
                DailyMonitoringEventLossEventItems = new List<DailyMonitoringEventLossEventItemViewModel>()
                {
                    new DailyMonitoringEventLossEventItemViewModel()
                },
                DailyMonitoringEventProductionOrderItems = new List<DailyMonitoringEventProductionOrderItemViewModel>()
                {
                    new DailyMonitoringEventProductionOrderItemViewModel()
                }
            };
            DailyMonitoringEventModel model = mapper.Map<DailyMonitoringEventModel>(vm);

            Assert.Equal(vm.Id, model.Id);

            var vm2 = mapper.Map<DailyMonitoringEventViewModel>(model);

            Assert.Equal(vm2.Id, model.Id);
        }

        [Fact]
        public virtual void ValidateVM()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            DailyMonitoringEventFacade facade = new DailyMonitoringEventFacade(serviceProvider, dbContext);

            var data = new DailyMonitoringEventViewModel();
            var validateService = new ValidateService(serviceProvider);
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(data));

            data.DailyMonitoringEventLossEventItems = new List<DailyMonitoringEventLossEventItemViewModel>()
            {
                new DailyMonitoringEventLossEventItemViewModel()
            };

            data.DailyMonitoringEventProductionOrderItems = new List<DailyMonitoringEventProductionOrderItemViewModel>()
            {
                new DailyMonitoringEventProductionOrderItemViewModel()
            };
            Assert.ThrowsAny<ServiceValidationException>(() => validateService.Validate(data));
        }

        [Fact]
        public async Task GetReport()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            DailyMonitoringEventFacade facade = new DailyMonitoringEventFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = facade.GetReport(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(1), data.ProcessArea, data.MachineId, 7);

            Assert.NotEmpty(Response);
        }

        [Fact]
        public async Task GenerateExcel()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            DailyMonitoringEventFacade facade = new DailyMonitoringEventFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = facade.GenerateExcel(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(1), data.ProcessArea, data.MachineId, 7);

            Assert.NotNull(Response);
        }
    }
}
