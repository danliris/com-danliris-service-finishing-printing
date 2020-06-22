using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Integration.Master
{
    public class StandardTestIntegrationViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            int Id = 1;
            string Code = "Code test";
            string Name = "Name test";
            string Remark = "Remark test";

            StandardTestIntegrationViewModel stivm = new StandardTestIntegrationViewModel();
            stivm.Id = 1;
            stivm.Code = Code;
            stivm.Name = Name;
            stivm.Remark = Remark;

            Assert.Equal(Id, stivm.Id);
            Assert.Equal(Code, stivm.Code);
            Assert.Equal(Name, stivm.Name);
            Assert.Equal(Remark, stivm.Remark);
        }
    }
}
