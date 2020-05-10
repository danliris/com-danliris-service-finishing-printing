using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.StrikeOff;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.ColorReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.StrikeOff;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.ColorReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.StrikeOff;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.StrikeOff;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.StrikeOff;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Facades
{
    public class StrikeOffFacadeTest
        : BaseFacadeTest<ProductionDbContext, StrikeOffFacade, StrikeOffLogic, StrikeOffModel, StrikeOffDataUtil>
    {
        private const string ENTITY = "StrikeOff";
        public StrikeOffFacadeTest() : base(ENTITY)
        {
        }

        protected override StrikeOffDataUtil DataUtil(StrikeOffFacade facade, ProductionDbContext dbContext = null)
        {
            IServiceProvider serviceProvider = GetServiceProviderMock(dbContext).Object;

            ColorReceiptFacade colorReceiptFacade = new ColorReceiptFacade(serviceProvider, dbContext);
            ColorReceiptDataUtil colorReceiptDataUtil = new ColorReceiptDataUtil(colorReceiptFacade);
            var dataUtil = new StrikeOffDataUtil(colorReceiptDataUtil, facade);
            return dataUtil;
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(ProductionDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(StrikeOffLogic)))
                .Returns(Activator.CreateInstance(typeof(StrikeOffLogic), identityService, dbContext) as StrikeOffLogic);

            serviceProviderMock.Setup(s => s.GetService(typeof(ColorReceiptLogic)))
                .Returns(new ColorReceiptLogic(identityService, dbContext));

            return serviceProviderMock;
        }

        [Fact]
        public virtual async void Update_Success_2()
        {
            string testName = GetCurrentMethod() + " Update_Success_2";
            var dbContext = DbContext(testName);
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            StrikeOffFacade facade = new StrikeOffFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();
            var data2 = new StrikeOffModel()
            {
                Id = data.Id,
                Remark = data.Remark,
                Code = data.Code,
                StrikeOffItems = data.StrikeOffItems.Select(s => new StrikeOffItemModel()
                {
                    ColorReceiptColorCode = s.ColorReceiptColorCode,
                    ColorReceiptId = s.ColorReceiptId,
                    ColorReceiptItems = s.ColorReceiptItems
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
                cfg.AddProfile<StrikeOffProfile>();
            });
            var mapper = configuration.CreateMapper();

            StrikeOffViewModel vm = new StrikeOffViewModel { Id = 1 };
            StrikeOffModel model = mapper.Map<StrikeOffModel>(vm);

            Assert.Equal(vm.Id, model.Id);

        }

        [Fact]
        public async Task ValidateVM()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            var data = new StrikeOffViewModel()
            {
                Remark = "test"
            };
            StrikeOffFacade facade = new StrikeOffFacade(serviceProvider, dbContext);
            System.ComponentModel.DataAnnotations.ValidationContext validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(data, serviceProvider, null);
            var response = data.Validate(validationContext);

            Assert.NotEmpty(response);

            var model = await DataUtil(facade, dbContext).GetTestData();

            data.Code = model.Code;
            response = data.Validate(validationContext);

            Assert.NotEmpty(response);

            data.Code = "testCodeNew"+Guid.NewGuid().ToString();
            response = data.Validate(validationContext);

            Assert.NotEmpty(response);

            data.StrikeOffItems = new List<StrikeOffItemViewModel>()
            {
               
            };
            response = data.Validate(validationContext);

            Assert.NotEmpty(response);


            data.StrikeOffItems = new List<StrikeOffItemViewModel>()
            {
                new StrikeOffItemViewModel()
            };
            response = data.Validate(validationContext);

            Assert.NotEmpty(response);

            data.StrikeOffItems = new List<StrikeOffItemViewModel>()
            {
                new StrikeOffItemViewModel()
                {
                    ColorReceipt = new Lib.ViewModels.ColorReceipt.ColorReceiptViewModel()
                }
            };
            response = data.Validate(validationContext);

            Assert.Empty(response);
        }
    }
}
