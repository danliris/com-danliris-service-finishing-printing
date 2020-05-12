using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Master;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Integration.Master
{
    public class OrderTypeIntegrationViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            int Id = 1;
            string Code = "Code test";
            string Name = "Name test";

            OrderTypeIntegrationViewModel otivm = new OrderTypeIntegrationViewModel();
            otivm.Id = 1;
            otivm.Code = Code;
            otivm.Name = Name;

            Assert.Equal(Id, otivm.Id);
            Assert.Equal(Code, otivm.Code);
            Assert.Equal(Name, otivm.Name);
        }
    }
}
