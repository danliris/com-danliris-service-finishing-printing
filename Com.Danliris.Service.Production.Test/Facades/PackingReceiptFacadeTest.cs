using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.PackingReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.PackingReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.PackingReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.PackingReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.Services.HttpClientService;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.PackingReceipt;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Facades
{
    public class PackingReceiptFacadeTest : BaseFacadeTest<ProductionDbContext, PackingReceiptFacade, PackingReceiptLogic, PackingReceiptModel, PackingReceiptDataUtil>
    {
        private const string ENTITY = "PackingReceipt";
        public PackingReceiptFacadeTest() : base(ENTITY)
        {
        }

        protected override Mock<IServiceProvider> GetServiceProviderMock(ProductionDbContext dbContext)
        {
            var serviceProviderMock = new Mock<IServiceProvider>();

            IIdentityService identityService = new IdentityService { Username = "Username" };

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IHttpClientService)))
                .Returns(new HttpClientTestService());

            serviceProviderMock
                .Setup(x => x.GetService(typeof(IdentityService)))
                .Returns(identityService);

            serviceProviderMock
                .Setup(x => x.GetService(typeof(PackingReceiptLogic)))
                .Returns(new PackingReceiptLogic(identityService, dbContext));

            return serviceProviderMock;
        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PackingReceiptProfile>();
            });
            var mapper = configuration.CreateMapper();

            PackingReceiptViewModel vm = new PackingReceiptViewModel { Id = 1 };
            PackingReceiptModel model = mapper.Map<PackingReceiptModel>(vm);

            Assert.Equal(vm.Id, model.Id);

        }
    }
}
