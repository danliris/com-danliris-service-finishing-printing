using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Integration.Master
{
    public class UnitViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            int Id = 1;
            string Code = "Code test";
            string Name = "Name test";
            DivisionViewModel dvm = new DivisionViewModel();

            UnitViewModel uvm = new UnitViewModel();
            uvm.Id = 1;
            uvm.Code = Code;
            uvm.Name = Name;
            uvm.Division = dvm;

            Assert.Equal(Id, uvm.Id);
            Assert.Equal(Code, uvm.Code);
            Assert.Equal(Name, uvm.Name);
            Assert.Equal(dvm, uvm.Division);
        }
    }
}
