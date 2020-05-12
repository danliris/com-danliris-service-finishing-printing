using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Integration.Master
{
    public class FinishTypeIntegrationViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            int Id = 1;
            string Code = "Code test";
            string Name = "Name test";
            string Remark = "Remark test";

            FinishTypeIntegrationViewModel ftivm = new FinishTypeIntegrationViewModel();
            ftivm.Id = 1;
            ftivm.Code = Code;
            ftivm.Name = Name;
            ftivm.Remark = Remark;

            Assert.Equal(Id, ftivm.Id);
            Assert.Equal(Code, ftivm.Code);
            Assert.Equal(Name, ftivm.Name);
            Assert.Equal(Remark, ftivm.Remark);
        }
    }
}
