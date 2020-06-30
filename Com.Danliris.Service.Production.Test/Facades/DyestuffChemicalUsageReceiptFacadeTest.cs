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
using System.Threading.Tasks;
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
                    ReceiptDate = d.ReceiptDate,
                    Adjs1Date = d.Adjs1Date,
                    Adjs2Date = d.Adjs2Date,
                    Adjs3Date = d.Adjs3Date,
                    Adjs4Date = d.Adjs4Date,
                    DyestuffChemicalUsageReceiptItemDetails = d.DyestuffChemicalUsageReceiptItemDetails.Select(e => new DyestuffChemicalUsageReceiptItemDetailModel()
                    {
                        Index = e.Index,
                        Name = e.Name,
                        Adjs1Quantity = e.Adjs1Quantity,
                        Adjs2Quantity = e.Adjs2Quantity,
                        Adjs3Quantity = e.Adjs3Quantity,
                        Adjs4Quantity = e.Adjs4Quantity,
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
                    ReceiptDate = d.ReceiptDate,
                    Adjs1Date = d.Adjs1Date,
                    Adjs2Date = d.Adjs2Date,
                    Adjs3Date = d.Adjs3Date,
                    Adjs4Date = d.Adjs4Date,
                    DyestuffChemicalUsageReceiptItemDetails = d.DyestuffChemicalUsageReceiptItemDetails.Select(e => new DyestuffChemicalUsageReceiptItemDetailModel()
                    {
                        Index = e.Index,
                        Name = e.Name,
                        Adjs1Quantity = e.Adjs1Quantity,
                        Adjs2Quantity = e.Adjs2Quantity,
                        Adjs3Quantity = e.Adjs3Quantity,
                        Adjs4Quantity = e.Adjs4Quantity,
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
                        ReceiptDate = DateTimeOffset.UtcNow,
                        Adjs1Date = DateTimeOffset.UtcNow,
                        Adjs2Date = DateTimeOffset.UtcNow,
                        Adjs3Date = DateTimeOffset.UtcNow,
                        Adjs4Date = DateTimeOffset.UtcNow,
                        UsageReceiptDetails = new List<DyestuffChemicalUsageReceiptItemDetailViewModel>()
                        {
                            new DyestuffChemicalUsageReceiptItemDetailViewModel()
                            {
                                Index = 1,
                                Name = "name",
                                Adjs1Quantity = 1,
                                Adjs2Quantity = 1,
                                Adjs3Quantity = 1,
                                Adjs4Quantity = 1,
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

            data.Date = DateTimeOffset.UtcNow;
            data.ProductionOrder = null;

            response = data.Validate(validationContext);

            Assert.NotEmpty(response);

            data.ProductionOrder = new Production.Lib.ViewModels.Integration.Sales.FinishingPrinting.ProductionOrderIntegrationViewModel()
            {
                Id = 0
            };

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
                Id = 0
            };
            response = data.Validate(validationContext);

            Assert.NotEmpty(response);

        }

        [Fact]
        public async Task GetByStrikeOff_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            DyestuffChemicalUsageReceiptFacade facade = new DyestuffChemicalUsageReceiptFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = await facade.GetDataByStrikeOff(data.StrikeOffId);

            Assert.NotNull(Response);
        }

        [Fact]
        public async Task GetByStrikeOff_Success_Null()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            DyestuffChemicalUsageReceiptFacade facade = new DyestuffChemicalUsageReceiptFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();

            var Response = await facade.GetDataByStrikeOff(data.StrikeOffId - 1);

            Assert.Null(Response);
        }

        [Fact]
        public void CoverageVM()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            DyestuffChemicalUsageReceiptFacade facade = new DyestuffChemicalUsageReceiptFacade(serviceProvider, dbContext);

            var data = new DyestuffChemicalUsageReceiptViewModel()
            {
                UsageReceiptItems = new List<DyestuffChemicalUsageReceiptItemViewModel>()
                {
                    new DyestuffChemicalUsageReceiptItemViewModel()
                    {
                        UsageReceiptDetails = new List<DyestuffChemicalUsageReceiptItemDetailViewModel>()
                        {
                            new DyestuffChemicalUsageReceiptItemDetailViewModel()
                        }
                    }
                }
            };

            foreach (var item in data.UsageReceiptItems)
            {
                Assert.Null(item.ReceiptDate);
                Assert.Null(item.Adjs1Date);
                Assert.Null(item.Adjs2Date);
                Assert.Null(item.Adjs3Date);
                Assert.Null(item.Adjs4Date);
                Assert.Null(item.ColorCode);
                foreach (var detail in item.UsageReceiptDetails)
                {
                    Assert.Null(detail.Name);
                    Assert.Equal(0, detail.Index);
                    Assert.Equal(0, detail.ReceiptQuantity);
                    Assert.Equal(0, detail.Adjs1Quantity);
                    Assert.Equal(0, detail.Adjs2Quantity);
                    Assert.Equal(0, detail.Adjs3Quantity);
                    Assert.Equal(0, detail.Adjs4Quantity);
                }
            }
        }

        [Fact]
        public void CoverageModel()
        {
            var model = new DyestuffChemicalUsageReceiptModel();
            Assert.ThrowsAny<NotImplementedException>(() => model.Validate(null));

            var itemModel = new DyestuffChemicalUsageReceiptItemModel();
            Assert.ThrowsAny<NotImplementedException>(() => itemModel.Validate(null));

            var detailModel = new DyestuffChemicalUsageReceiptItemDetailModel();
            Assert.ThrowsAny<NotImplementedException>(() => detailModel.Validate(null));
        }
    }
}
