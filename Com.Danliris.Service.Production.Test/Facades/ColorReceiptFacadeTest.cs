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
        public async void Create_Exception()
        {
            var dbContext = DbContext(GetCurrentMethod());
            var serviceProvider = GetServiceProviderMock(dbContext).Object;

            var facade = new ColorReceiptFacade(serviceProvider, dbContext);

            await Assert.ThrowsAnyAsync<Exception>(() => facade.CreateAsync(null));
        }
    }
}
