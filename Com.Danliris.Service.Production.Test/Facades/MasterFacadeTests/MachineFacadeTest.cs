using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.Master.Machine;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.Master.Machine;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.Machine;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Master.MachineType;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils.MasterDataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Com.Danliris.Service.Production.Lib.ViewModels.Master.Step;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Facades.MasterFacadeTests
{
    public class MachineFacadeTest : BaseFacadeTest<ProductionDbContext, MachineFacade, MachineLogic, MachineModel, MachineDataUtil>
    {
        private const string ENTITY = "Machine";

        public MachineFacadeTest() : base(ENTITY)
        {
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

            return serviceProviderMock;
        }

        [Fact]
        public void Validate_ViewModel()
        {
            MachineViewModel vm = new MachineViewModel()
            {
                Code = "code",
                Condition = "condition",
                Electric = 1,
                LPG = 1,
                MachineEvents = new List<MachineEventViewModel>()
                {
                    new MachineEventViewModel()
                    {
                        Name = "name",
                        No = "no"
                    }
                },
                MachineType = new MachineTypeViewModel()
                {
                    Indicators = new List<MachineTypeIndicatorsViewModel>()
                    {
                        new MachineTypeIndicatorsViewModel()
                        {
                            DataType = "datatype",
                            DefaultValue = "default",
                            Indicator = "indicatpr",
                            Uom = "uom"
                            
                        }
                        
                    },
                    Description = "description",
                    Name = "name"
                },
                Manufacture = "manufacture",
                MonthlyCapacity = 1,
                Name = "name",
                Process = "process",
                Solar = 1,
                Steam = 1,
                Steps = new List<StepViewModel>()
                {
                    new StepViewModel()
                    {
                        Alias = "alias",
                        Process = "process",
                        ProcessArea = "area",
                        StepIndicators = new List<StepIndicatorViewModel>()
                        {
                            new StepIndicatorViewModel()
                            {
                                Name = "name",
                                Uom = "uom",
                                Value = "value"
                            }
                        }
                    }
                },
                Unit = new UnitViewModel()
                {
                    Name = "name",
                    Division = new DivisionViewModel()
                    {
                        Name = "name"
                    }
                },
                Water = 1,
                Year = 2019
            };

            Assert.Empty(vm.Validate(null));

            MachineViewModel vm2 = new MachineViewModel();
            Assert.NotEmpty(vm2.Validate(null));
        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MachineProfile>();
            });
            var mapper = configuration.CreateMapper();

            MachineViewModel vm = new MachineViewModel { Id = 1 };
            MachineModel model = mapper.Map<MachineModel>(vm);

            Assert.Equal(vm.Id, model.Id);

        }
    }
}
