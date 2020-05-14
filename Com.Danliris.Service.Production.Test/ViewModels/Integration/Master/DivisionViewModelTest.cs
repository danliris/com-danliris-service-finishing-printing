using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Integration.Master
{
    public class DivisionViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            string Code = "Code test";
            string Name = "Name test";

            DivisionViewModel dvm = new DivisionViewModel();
            dvm.Code = Code;
            dvm.Name = Name;

            Assert.Equal(Code, dvm.Code);
            Assert.Equal(Name, dvm.Name);
        }
    }
}
