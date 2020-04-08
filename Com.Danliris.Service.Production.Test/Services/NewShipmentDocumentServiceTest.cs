﻿using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.NewShipmentDocument;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.NewShipmentDocument;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.NewShipmentDocument;
using Com.Danliris.Service.Finishing.Printing.Lib.Services.HttpClientService;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Sales.DOSales;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.NewShipmentDocument;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib;
using Com.Danliris.Service.Production.Lib.Services.IdentityService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Services
{
    public class NewShipmentDocumentServiceTest
    {
        private const string ENTITY = "NewShipmentDocument";
        //private PurchasingDocumentAcceptanceDataUtil pdaDataUtil;


        [MethodImpl(MethodImplOptions.NoInlining)]
        public string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return string.Concat(sf.GetMethod().Name, "_", ENTITY);
        }

        private ProductionDbContext _dbContext(string testName)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProductionDbContext>();
            optionsBuilder
                .UseInMemoryDatabase(testName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            ProductionDbContext dbContext = new ProductionDbContext(optionsBuilder.Options);

            return dbContext;
        }

        private NewShipmentDocumentDataUtil _dataUtil(NewShipmentDocumentService service)
        {
            return new NewShipmentDocumentDataUtil(service);
        }

        private Mock<IServiceProvider> GetServiceProvider()
        {
            var serviceProvider = new Mock<IServiceProvider>();

            serviceProvider
                .Setup(x => x.GetService(typeof(IHttpClientService)))
                .Returns(new HttpClientTestService());

            serviceProvider
                .Setup(x => x.GetService(typeof(IIdentityService)))
                .Returns(new IdentityService() { Token = "Token", Username = "Test" });


            return serviceProvider;
        }

        [Fact]
        public async Task Should_Success_Get_Data()
        {
            var service = new NewShipmentDocumentService(GetServiceProvider().Object, _dbContext(GetCurrentMethod()));
            var data = await _dataUtil(service).GetTestData();
            var Response = service.Read(1, 25, "{}", null, data.Code, "{}");
            Assert.NotEmpty(Response.Data);
        }

        [Fact]
        public async Task Should_Success_Get_Data_By_Id()
        {
            var service = new NewShipmentDocumentService(GetServiceProvider().Object, _dbContext(GetCurrentMethod()));
            var model = await _dataUtil(service).GetTestData();
            var Response = await service.ReadByIdAsync(model.Id);
            Assert.NotNull(Response);
        }

        [Fact]
        public async Task Should_Success_Create_Data()
        {
            var service = new NewShipmentDocumentService(GetServiceProvider().Object, _dbContext(GetCurrentMethod()));
            var model = _dataUtil(service).GetNewData();
            var Response = await service.CreateAsync(model);
            Assert.NotEqual(0, Response);
        }

        [Fact]
        public void Should_No_Error_Validate_Data()
        {
            var service = new NewShipmentDocumentService(GetServiceProvider().Object, _dbContext(GetCurrentMethod()));
            var vm = _dataUtil(service).GetDataToValidate();

            Assert.True(vm.Validate(null).Count() == 0);
        }

        [Fact]
        public void Should_Success_Validate_All_Null_Data()
        {
            var vm = new NewShipmentDocumentViewModel();

            Assert.True(vm.Validate(null).Count() > 0);
        }

        [Fact]
        public async Task Should_Success_Update_Data()
        {
            var service = new NewShipmentDocumentService(GetServiceProvider().Object, _dbContext(GetCurrentMethod()));
            var model = await _dataUtil(service).GetTestData();
            var newModel = await service.ReadByIdAsync(model.Id);
            newModel.DeliveryReference = "NewDescription";
            var Response = await service.UpdateAsync(newModel.Id, newModel);
            Assert.NotEqual(0, Response);
        }

        [Fact]
        public async Task Should_Success_Delete_Data()
        {
            var service = new NewShipmentDocumentService(GetServiceProvider().Object, _dbContext(GetCurrentMethod()));
            var model = await _dataUtil(service).GetTestData();
            var newModel = await service.ReadByIdAsync(model.Id);

            var Response = await service.DeleteAsync(newModel.Id);
            Assert.NotEqual(0, Response);
        }

        [Fact]
        public async Task Should_Success_Get_Shipment_Product()
        {
            var service = new NewShipmentDocumentService(GetServiceProvider().Object, _dbContext(GetCurrentMethod()));
            var model = await _dataUtil(service).GetTestData();
            var createdModel = await service.ReadByIdAsync(model.Id);

            var result = await service.GetShipmentProducts(createdModel.Details.FirstOrDefault().ProductionOrderId, createdModel.BuyerId);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<NewShipmentDocumentProfile>();
            });
            var mapper = configuration.CreateMapper();

            NewShipmentDocumentViewModel vm = new NewShipmentDocumentViewModel { Id = 1 };
            NewShipmentDocumentModel model = mapper.Map<NewShipmentDocumentModel>(vm);

            Assert.Equal(vm.Id, model.Id);

        }

        [Fact]
        public void Should_Success_Instanciate_DO_Sales_Integration_View_Model()
        {

            var viewModel = new DOSalesIntegrationViewModel()
            {
                Code = "code",
                DOSalesNo = "DOSalesNo",
                DOSalesType = "DOSalesType",
                Status = "Status",
                Accepted = false,
                Declined = false,
                Type = "Type",
                Date = DateTimeOffset.UtcNow,
                Buyer = new Production.Lib.ViewModels.Integration.Master.BuyerIntegrationViewModel()
                {
                    Address = "Address",
                    City = "City",
                    Code = "Code",
                    Contact = "Contact",
                    Country = "Country",
                    Name = "Name",
                    NPWP = "NPWP",
                    Tempo = "Tempo",
                    Type = "Type",
                },
                DestinationBuyerName = "DestinationBuyerName",
                DestinationBuyerAddress = "DestinationBuyerAddress",
                SalesName = "SalesName",
                HeadOfStorage = "HeadOfStorage",
                PackingUom = "PackingUom",
                LengthUom = "LengthUom",
                WeightUom = "WeightUom",
                Disp = 1,
                Op = 1,
                Sc = 1,
                DoneBy = "DoneBy",
                FillEachBale = 100,
                Remark = "Remark",
            };

            Assert.NotNull(viewModel.Code);
            Assert.NotNull(viewModel.DOSalesNo);
            Assert.NotNull(viewModel.DOSalesType);
            Assert.NotNull(viewModel.Status);
            Assert.NotNull(viewModel.Accepted);
            Assert.NotNull(viewModel.Declined);
            Assert.NotNull(viewModel.Type);
            Assert.NotNull(viewModel.Date);
            Assert.NotNull(viewModel.Buyer);
            Assert.NotNull(viewModel.DestinationBuyerName);
            Assert.NotNull(viewModel.DestinationBuyerAddress);
            Assert.NotNull(viewModel.SalesName);
            Assert.NotNull(viewModel.HeadOfStorage);
            Assert.NotNull(viewModel.PackingUom);
            Assert.NotNull(viewModel.LengthUom);
            Assert.NotNull(viewModel.WeightUom);
            Assert.NotNull(viewModel.Disp);
            Assert.NotNull(viewModel.Op);
            Assert.NotNull(viewModel.Sc);
            Assert.NotNull(viewModel.DoneBy);
            Assert.NotNull(viewModel.FillEachBale);
            Assert.NotNull(viewModel.Remark);
        }
    }
}
