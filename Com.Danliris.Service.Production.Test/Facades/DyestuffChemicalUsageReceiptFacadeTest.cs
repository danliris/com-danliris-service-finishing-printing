using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.DyestuffChemicalUsageReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.DyestuffChemicalUsageReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.DyestuffChemicalUsageReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.DyestuffChemicalUsageReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.DyestuffChemicalUsageReceipt;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Facades
{
    public class DyestuffChemicalUsageReceiptFacadeTest
        : BaseFacadeTest<ProductionDbContext, DyestuffChemicalUsageReceiptFacade, DyestuffChemicalUsageReceiptLogic, DyestuffChemicalUsageReceiptModel, DyestuffChemicalUsageReceiptDataUtil>
    {
        private const string ENTITY = "DyestuffChemicalUsageReceipt";
        public DyestuffChemicalUsageReceiptFacadeTest() : base(ENTITY)
        {
        }

        [Fact]
        public virtual async void Update_Success_2()
        {
            string testName = GetCurrentMethod() + " Update_Success_2";
            var dbContext = DbContext(testName);
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            DyestuffChemicalUsageReceiptFacade facade = new DyestuffChemicalUsageReceiptFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();
            var data2 = new DyestuffChemicalUsageReceiptModel()
            {
                Id = data.Id,
                ProductionOrderId = data.ProductionOrderId,
                ProductionOrderMaterialConstructionId = data.ProductionOrderMaterialConstructionId,
                ProductionOrderMaterialConstructionName = data.ProductionOrderMaterialConstructionName,
                ProductionOrderMaterialId = data.ProductionOrderMaterialId,
                ProductionOrderMaterialName = data.ProductionOrderMaterialName,
                ProductionOrderMaterialWidth = data.ProductionOrderMaterialWidth,
                ProductionOrderOrderNo = data.ProductionOrderOrderNo,
                ProductionOrderOrderQuantity = data.ProductionOrderOrderQuantity,
                StrikeOffCode = data.StrikeOffCode,
                Date = data.Date,
                StrikeOffId = data.StrikeOffId,
                StrikeOffType = data.StrikeOffType,
                DyestuffChemicalUsageReceiptItems = data.DyestuffChemicalUsageReceiptItems.Select(d => new DyestuffChemicalUsageReceiptItemModel()
                {
                    ColorCode = d.ColorCode,
                    Prod1Date = d.Prod1Date,
                    Prod2Date = d.Prod2Date,
                    Prod3Date = d.Prod3Date,
                    Prod4Date = d.Prod4Date,
                    Prod5Date = d.Prod5Date,
                    DyestuffChemicalUsageReceiptItemDetails = d.DyestuffChemicalUsageReceiptItemDetails.Select(e => new DyestuffChemicalUsageReceiptItemDetailModel()
                    {
                        Index = e.Index,
                        Name = e.Name,
                        Prod1Quantity = e.Prod1Quantity,
                        Prod2Quantity = e.Prod2Quantity,
                        Prod3Quantity = e.Prod3Quantity,
                        Prod4Quantity = e.Prod4Quantity,
                        Prod5Quantity = e.Prod5Quantity,
                        ReceiptQuantity = e.ReceiptQuantity
                    }).ToList(),
                }).ToArray()
            };
            var response = await facade.UpdateAsync((int)data.Id, data2);

            Assert.NotEqual(0, response);
        }

        [Fact]
        public virtual async void Update_Success_3()
        {
            string testName = GetCurrentMethod() + " Update_Success_3";
            var dbContext = DbContext(testName);
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            DyestuffChemicalUsageReceiptFacade facade = new DyestuffChemicalUsageReceiptFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();
            var data2 = new DyestuffChemicalUsageReceiptModel()
            {
                Id = data.Id,
                ProductionOrderId = data.ProductionOrderId,
                ProductionOrderMaterialConstructionId = data.ProductionOrderMaterialConstructionId,
                ProductionOrderMaterialConstructionName = data.ProductionOrderMaterialConstructionName,
                ProductionOrderMaterialId = data.ProductionOrderMaterialId,
                ProductionOrderMaterialName = data.ProductionOrderMaterialName,
                ProductionOrderMaterialWidth = data.ProductionOrderMaterialWidth,
                ProductionOrderOrderNo = data.ProductionOrderOrderNo,
                ProductionOrderOrderQuantity = data.ProductionOrderOrderQuantity,
                StrikeOffCode = data.StrikeOffCode,
                Date = data.Date,
                StrikeOffId = data.StrikeOffId,
                StrikeOffType = data.StrikeOffType,
                DyestuffChemicalUsageReceiptItems = data.DyestuffChemicalUsageReceiptItems.Select(d => new DyestuffChemicalUsageReceiptItemModel()
                {
                    ColorCode = d.ColorCode,
                    Prod1Date = d.Prod1Date,
                    Prod2Date = d.Prod2Date,
                    Prod3Date = d.Prod3Date,
                    Prod4Date = d.Prod4Date,
                    Prod5Date = d.Prod5Date,
                    DyestuffChemicalUsageReceiptItemDetails = d.DyestuffChemicalUsageReceiptItemDetails.Select(e => new DyestuffChemicalUsageReceiptItemDetailModel()
                    {
                        Index = e.Index,
                        Name = e.Name,
                        Prod1Quantity = e.Prod1Quantity,
                        Prod2Quantity = e.Prod2Quantity,
                        Prod3Quantity = e.Prod3Quantity,
                        Prod4Quantity = e.Prod4Quantity,
                        Prod5Quantity = e.Prod5Quantity,
                        ReceiptQuantity = e.ReceiptQuantity
                    }).ToList(),
                    Id = d.Id
                }).ToArray()
            };
            var response = await facade.UpdateAsync((int)data.Id, data2);

            Assert.NotEqual(0, response);
        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DyestuffChemicalUsageReceiptProfile>();
            });
            var mapper = configuration.CreateMapper();

            DyestuffChemicalUsageReceiptViewModel vm = new DyestuffChemicalUsageReceiptViewModel { Id = 1 };
            DyestuffChemicalUsageReceiptModel model = mapper.Map<DyestuffChemicalUsageReceiptModel>(vm);

            Assert.Equal(vm.Id, model.Id);

        }

        [Fact]
        public void ValidateVM()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            var data = new DyestuffChemicalUsageReceiptViewModel()
            {
                UsageReceiptItems = new List<DyestuffChemicalUsageReceiptItemViewModel>()
                {
                    new DyestuffChemicalUsageReceiptItemViewModel()
                    {
                        ColorCode = "code",
                        Prod1Date = DateTimeOffset.UtcNow,
                        Prod2Date = DateTimeOffset.UtcNow,
                        Prod3Date = DateTimeOffset.UtcNow,
                        Prod4Date = DateTimeOffset.UtcNow,
                        Prod5Date = DateTimeOffset.UtcNow,
                        UsageReceiptDetails = new List<DyestuffChemicalUsageReceiptItemDetailViewModel>()
                        {
                            new DyestuffChemicalUsageReceiptItemDetailViewModel()
                            {
                                Name = "name",
                                Prod1Quantity = 1,
                                Prod2Quantity = 1,
                                Prod3Quantity = 1,
                                Prod4Quantity = 1,
                                Prod5Quantity = 1,
                                ReceiptQuantity = 1,
                            }
                        }
                    }
                }
            };


            DyestuffChemicalUsageReceiptFacade facade = new DyestuffChemicalUsageReceiptFacade(serviceProvider, dbContext);
            System.ComponentModel.DataAnnotations.ValidationContext validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(data, serviceProvider, null);
            var response = data.Validate(validationContext);

            Assert.NotEmpty(response);

            data.Date = default(DateTimeOffset);
            response = data.Validate(validationContext);

            Assert.NotEmpty(response);

            data.ProductionOrder = new Production.Lib.ViewModels.Integration.Sales.FinishingPrinting.ProductionOrderIntegrationViewModel()
            {
                Id = 1
            };

            response = data.Validate(validationContext);

            Assert.NotEmpty(response);

            data.StrikeOff = new Lib.ViewModels.StrikeOff.StrikeOffViewModel()
            {
                Id = 1
            };
            response = data.Validate(validationContext);

            Assert.Empty(response);

        }
    }
}
