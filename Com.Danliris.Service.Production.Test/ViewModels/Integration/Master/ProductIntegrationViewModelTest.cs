using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Integration.Master
{
    public class ProductIntegrationViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            int Id = 1;
            string Code = "Code test";
            string Name = "Name test";
            double Price = 100;

            ProductIntegrationViewModel pivm = new ProductIntegrationViewModel();
            pivm.Id = 1;
            pivm.Code = Code;
            pivm.Name = Name;
            pivm.Price = Price;

            Assert.Equal(Id, pivm.Id);
            Assert.Equal(Code, pivm.Code);
            Assert.Equal(Name, pivm.Name);
            Assert.Equal(Price, pivm.Price);
        }
    }
}
