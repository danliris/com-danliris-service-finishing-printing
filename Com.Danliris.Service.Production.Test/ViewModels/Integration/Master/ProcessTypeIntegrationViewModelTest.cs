using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Master;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Integration.Master
{
    public class ProcessTypeIntegrationViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            int Id = 1;
            string Code = "Code test";
            string Name = "Name test";
            OrderTypeIntegrationViewModel otivm = new OrderTypeIntegrationViewModel();

            ProcessTypeIntegrationViewModel ptivm = new ProcessTypeIntegrationViewModel();
            ptivm.Id = 1;
            ptivm.Code = Code;
            ptivm.Name = Name;
            ptivm.OrderType = otivm;

            Assert.Equal(Id, ptivm.Id);
            Assert.Equal(Code, ptivm.Code);
            Assert.Equal(Name, ptivm.Name);
            Assert.Equal(otivm, ptivm.OrderType);
        }
    }
}
