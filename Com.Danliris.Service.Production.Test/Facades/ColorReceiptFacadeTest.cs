using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Facades.ColorReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.BusinessLogic.Implementations.ColorReceipt;
using Com.Danliris.Service.Finishing.Printing.Lib.Models.ColorReceipt;
using Com.Danliris.Service.Finishing.Printing.Test.DataUtils;
using Com.Danliris.Service.Finishing.Printing.Test.Utils;
using Com.Danliris.Service.Production.Lib;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Facades
{
    public class ColorReceiptFacadeTest
        : BaseFacadeTest<ProductionDbContext, ColorReceiptFacade, ColorReceiptLogic, ColorReceiptModel, ColorReceiptDataUtil>
    {
        private const string ENTITY = "FabricQualityControl";
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
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            ColorReceiptFacade facade = new ColorReceiptFacade(serviceProvider, dbContext);

            var data = await DataUtil(facade, dbContext).GetTestData();
            data.ColorReceiptItems = new List<ColorReceiptItemModel>()
            {
                new ColorReceiptItemModel()
                {
                    ProductId = 1,
                    ProductCode ="c",
                    ProductName = "a",
                    Quantity = 1
                }
            };
            var response = await facade.UpdateAsync((int)data.Id, data);

            Assert.NotEqual(0, response);
        }
    }
}
