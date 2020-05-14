using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Sales.FinishingPrinting;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Integration.Sales.FinishingPrinting
{
    public class ProductionLampStandardIntegrationViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            int Id = 1;
            string Code = "Code test";
            string Name = "Name test";
            string Description = "Description test";
            long LampStandardId = 1;

            ProductionLampStandardIntegrationViewModel plsivm = new ProductionLampStandardIntegrationViewModel();
            plsivm.Id = 1;
            plsivm.Code = Code;
            plsivm.Name = Name;
            plsivm.Description = Description;
            plsivm.LampStandardId = 1;

            Assert.Equal(Id, plsivm.Id);
            Assert.Equal(Code, plsivm.Code);
            Assert.Equal(Name, plsivm.Name);
            Assert.Equal(Description, plsivm.Description);
            Assert.Equal(LampStandardId, plsivm.LampStandardId);
        }
    }
}
