using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Master;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Integration.Master
{
    public class ColorTypeIntegrationViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            int Id = 1;
            string Code = "Code test";
            string Name = "Name test";
            string Remark = "Remark test";

            ColorTypeIntegrationViewModel ctivm = new ColorTypeIntegrationViewModel();
            ctivm.Id = 1;
            ctivm.Code = Code;
            ctivm.Name = Name;
            ctivm.Remark = Remark;

            Assert.Equal(Id, ctivm.Id);
            Assert.Equal(Code, ctivm.Code);
            Assert.Equal(Name, ctivm.Name);
            Assert.Equal(Remark, ctivm.Remark);
        }
    }
}
