using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.DailyMonitoringEvent;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.DailyMonitoringEvent;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.DailyMonitoringEvent;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.LossEventCategory;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.LossEventRemark;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.DailyMonitoringEvent;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DailyMonitoringEvent;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils.MasterDataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.Services.ValidateService;
using Com.Danliris.Service.Production.Lib.Utilities;
using Moq;
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

        protected override Mock<IServiceProvider> GetServiceProviderMock(ProductionDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(DailyMonitoringEventLogic)))
                .Returns(Activator.CreateInstance(typeof(DailyMonitoringEventLogic), identityService, dbContext) as DailyMonitoringEventLogic);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(LossEventCategoryLogic)))
                .Returns(Activator.CreateInstance(typeof(LossEventCategoryLogic), identityService, dbContext) as LossEventCategoryLogic);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(LossEventRemarkLogic)))
                .Returns(Activator.CreateInstance(typeof(LossEventRemarkLogic), identityService, dbContext) as LossEventRemarkLogic);

            return serviceProviderMock;
        }

        protected override DailyMonitoringEventDataUtil DataUtil(DailyMonitoringEventFacade facade, ProductionDbContext dbContext = null)
        {
            LossEventCategoryFacade categoryFacade = new LossEventCategoryFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            LossEventCategoryDataUtil categoryDataUtil = new LossEventCategoryDataUtil(categoryFacade);
            var category = categoryDataUtil.GetNewData();
            category.LossEventLosses = "Legal Losses";
            var result = categoryFacade.CreateAsync(category).Result;

            var category2 = categoryDataUtil.GetNewData();
            category2.LossEventLosses = "Unutilised Capacity Losses";
            result = categoryFacade.CreateAsync(category2).Result;

            var category3 = categoryDataUtil.GetNewData();
            category3.LossEventLosses = "Process Driven Losses";
            result = categoryFacade.CreateAsync(category3).Result;

            var category4 = categoryDataUtil.GetNewData();
            category4.LossEventLosses = "Manufacturing Performance Losses";
            result = categoryFacade.CreateAsync(category4).Result;

            LossEventRemarkFacade remarkFacade = new LossEventRemarkFacade(GetServiceProviderMock(dbContext).Object, dbContext);
            LossEventRemarkDataUtil remarkDataUtil = new LossEventRemarkDataUtil(remarkFacade);
            var remark = remarkDataUtil.GetNewData();
            remark.LossEventLosses = "Legal Losses";
            result = remarkFacade.CreateAsync(remark).Result;

            var remark2 = remarkDataUtil.GetNewData();
            remark2.LossEventLosses = "Unutilised Capacity Losses";
            result = remarkFacade.CreateAsync(remark2).Result;

            var remark3 = remarkDataUtil.GetNewData();
            remark3.LossEventLosses = "Process Driven Losses";
            result = remarkFacade.CreateAsync(remark3).Result;

            var remark4 = remarkDataUtil.GetNewData();
            remark4.LossEventLosses = "Manufacturing Performance Losses";
            result = remarkFacade.CreateAsync(remark4).Result;

            DailyMonitoringEventDataUtil dataUtil = new DailyMonitoringEventDataUtil(facade);
            return dataUtil;
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
