using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Integration.Master
{
    public class DesignMotiveIntegrationViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            int Id = 1;
            string Code = "Code test";
            string Name = "Name test";

            DesignMotiveIntegrationViewModel dmivm = new DesignMotiveIntegrationViewModel();
            dmivm.Id = 1;
            dmivm.Code = Code;
            dmivm.Name = Name;

            Assert.Equal(Id, dmivm.Id);
            Assert.Equal(Code, dmivm.Code);
            Assert.Equal(Name, dmivm.Name);
        }
    }
}
