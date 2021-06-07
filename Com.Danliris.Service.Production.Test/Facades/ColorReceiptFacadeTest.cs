using AutoMapper;
using Com.Danliris.Service.Finishing.Printing.Lib.AutoMapperProfiles.ColorReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.ColorReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.ColorReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.ColorReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.ColorReceipt;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Facades
{
    public class ColorReceiptFacadeTest
        : BaseFacadeTest<ProductionDbContext, ColorReceiptFacade, ColorReceiptLogic, ColorReceiptModel, ColorReceiptDataUtil>
    {
        private const string ENTITY = "ColorReceipt";
        public ColorReceiptFacadeTest() : base(ENTITY)
        {
        }

        [Fact]
        public virtual async void Create_Technician()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            ColorReceiptFacade facade = new ColorReceiptFacade(serviceProvider, dbContext);


            var response = await facade.CreateTechnician("Test");

            Assert.NotNull(response);
        }

        [Fact]
        public virtual async void Create_TechnicianDouble()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            ColorReceiptFacade facade = new ColorReceiptFacade(serviceProvider, dbContext);


            await facade.CreateTechnician("Test");
            var response = await facade.CreateTechnician("Test2");
            Assert.NotNull(response);
        }

        [Fact]
        public virtual async void Update_Success_2()
        {
            string testName = GetCurrentMethod() + " Update_Success_2";
            var dbContext = DbContext(testName);
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            ColorReceiptFacade facade = new ColorReceiptFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();
            var data2 = new ColorReceiptModel()
            {
                ColorCode = data.ColorCode,
                ColorName = data.ColorName,
                Id = data.Id,
                Remark = data.Remark,
                TechnicianId = data.TechnicianId,
                TechnicianName = data.TechnicianName,
                ColorReceiptItems = data.ColorReceiptItems.Select(s => new ColorReceiptItemModel()
                {
                    ProductCode = s.ProductCode,
                    ProductId = s.ProductId,
                    ProductName = s.ProductName,
                    Quantity = s.Quantity
                }).ToList(),
                Type = data.Type,
                Cloth = data.Cloth,
                DyeStuffReactives = new List<ColorReceiptDyeStuffReactiveModel>()
                {
                    new ColorReceiptDyeStuffReactiveModel()
                    {
                        Name = "test3",
                        Quantity = 1
                    }
                }
            };
            var response = await facade.UpdateAsync((int)data.Id, data2);

            Assert.NotEqual(0, response);
        }

        [Fact]
        public void Mapping_With_AutoMapper_Profiles()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ColorReceiptProfile>();
            });
            var mapper = configuration.CreateMapper();

            ColorReceiptViewModel vm = new ColorReceiptViewModel { Id = 1 };
            ColorReceiptModel model = mapper.Map<ColorReceiptModel>(vm);

            Assert.Equal(vm.Id, model.Id);

        }

        [Fact]
        public virtual void ValidateVM()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            ColorReceiptFacade facade = new ColorReceiptFacade(serviceProvider, dbContext);

            var data = new ColorReceiptViewModel()
            {
                ColorCode = "test",
                Remark = "test",
                Cloth = "test",
                ColorReceiptItems = new List<ColorReceiptItemViewModel>()
                {
                    new ColorReceiptItemViewModel()
                }
            };
            System.ComponentModel.DataAnnotations.ValidationContext validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(data, serviceProvider, null);
            var response = data.Validate(validationContext);

            Assert.True(0 < response.Count());
            Assert.NotEmpty(response);

            data.ColorName = "test";
            validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(data, serviceProvider, null);
            
            response = data.Validate(validationContext);
            Assert.True(0 < response.Count());
            Assert.NotEmpty(response);

            data.Technician = new TechnicianViewModel()
            {
                Name = "a",
                IsDefault = true
            };
            validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(data, serviceProvider, null);
            response = data.Validate(validationContext);

            Assert.True(0 < response.Count());
            Assert.NotEmpty(response);

            data.ChangeTechnician = true;
            validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(data, serviceProvider, null);
            response = data.Validate(validationContext);
            
            Assert.True(0 < response.Count());
            Assert.NotEmpty(response);

            data.NewTechnician = "test";
            data.Type = "type";
            validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(data, serviceProvider, null);
            response = data.Validate(validationContext);
            
            Assert.True(0 < response.Count());
            Assert.NotEmpty(response);

            data.ColorReceiptItems = new List<ColorReceiptItemViewModel>();
            validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(data, serviceProvider, null);
            response = data.Validate(validationContext);
            
            Assert.True(0 < response.Count());
            Assert.NotEmpty(response);

            data.ColorReceiptItems = new List<ColorReceiptItemViewModel>()
            {
                new ColorReceiptItemViewModel()
                {
                    Product = new Lib.ViewModels.Integration.Master.ProductIntegrationViewModel()
                    {
                        Id = 1,
                        Code = "code",
                        Name = "name"
                    },
                    Quantity = 1001
                }
            };
            validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(data, serviceProvider, null);
            response = data.Validate(validationContext);

            Assert.NotEmpty(response);
            Assert.True(0 < response.Count());

            data.ColorReceiptItems = new List<ColorReceiptItemViewModel>()
            {
                new ColorReceiptItemViewModel()
                {
                    Product = new Lib.ViewModels.Integration.Master.ProductIntegrationViewModel()
                    {
                        Id = 1,
                        Code = "code",
                        Name = "name"
                    },
                    Quantity = 3
                }
            };
            validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(data, serviceProvider, null);
            response = data.Validate(validationContext);

            Assert.True(0 < response.Count());
            Assert.NotEmpty(response);

            data.DyeStuffReactives = new List<ColorReceiptDyeStuffReactiveViewModel>();
            validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(data, serviceProvider, null);
            response = data.Validate(validationContext);

            Assert.True(0 < response.Count());
            Assert.NotEmpty(response);

            data.DyeStuffReactives = new List<ColorReceiptDyeStuffReactiveViewModel>()
            {
                new ColorReceiptDyeStuffReactiveViewModel()
                {

                },
                new ColorReceiptDyeStuffReactiveViewModel()
                {
                    Name = "air",
                    Quantity = -1
                }
            };
            validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(data, serviceProvider, null);
            response = data.Validate(validationContext);

            Assert.True(0 < response.Count());
            Assert.NotEmpty(response);

            data.DyeStuffReactives = new List<ColorReceiptDyeStuffReactiveViewModel>()
            {
                new ColorReceiptDyeStuffReactiveViewModel()
                {

                },
                new ColorReceiptDyeStuffReactiveViewModel()
                {
                    Name = "air",
                    Quantity = 1
                }
            };
            validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(data, serviceProvider, null);
            response = data.Validate(validationContext);

            Assert.True(0 < response.Count());
            Assert.NotEmpty(response);

            data.DyeStuffReactives = new List<ColorReceiptDyeStuffReactiveViewModel>()
            {
                new ColorReceiptDyeStuffReactiveViewModel()
                {
                    Name = "test",
                    Quantity = 1
                },
                new ColorReceiptDyeStuffReactiveViewModel()
                {
                    Name = "air",
                    Quantity = 1
                }
            };
            validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(data, serviceProvider, null);
            response = data.Validate(validationContext);

            Assert.True(0 < response.Count());
            Assert.NotEmpty(response);

            ColorReceiptItemViewModel vmItem = new ColorReceiptItemViewModel()
            {
                Product = new Lib.ViewModels.Integration.Master.ProductIntegrationViewModel()
                {
                    Id = 1,
                    Name = "a"
                },
                Quantity = 1
            };

            Assert.True(0 < response.Count());
            Assert.NotEqual(0, vmItem.Quantity);
        }

        [Fact]
        public void Validate_Technician()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            ColorReceiptFacade facade = new ColorReceiptFacade(serviceProvider, dbContext);

            var data = new TechnicianViewModel();
            System.ComponentModel.DataAnnotations.ValidationContext validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(data, serviceProvider, null);
            var response = data.Validate(validationContext);
            Assert.NotEmpty(response);
            Assert.False(data.IsDefault);
        }

        [Fact]
        public virtual async void Get_Technician_Success()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            var facade = new ColorReceiptFacade(serviceProvider, dbContext);
            await facade.CreateTechnician("test");
            var Response = await facade.GetDefaultTechnician();

            Assert.NotEqual(0, Response.Id);
        }
    }
}
