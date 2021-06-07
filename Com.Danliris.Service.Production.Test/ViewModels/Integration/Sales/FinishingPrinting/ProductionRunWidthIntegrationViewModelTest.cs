using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Sales.FinishingPrinting;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Integration.Sales.FinishingPrinting
{
    public class ProductionRunWidthIntegrationViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            int Id = 1;
            double Value = 100;

            ProductionRunWidthIntegrationViewModel prwivm = new ProductionRunWidthIntegrationViewModel();
            prwivm.Id = 1;
            prwivm.Value = 100;

            Assert.Equal(Id, prwivm.Id);
            Assert.Equal(Value, prwivm.Value);
        }
    }
}
