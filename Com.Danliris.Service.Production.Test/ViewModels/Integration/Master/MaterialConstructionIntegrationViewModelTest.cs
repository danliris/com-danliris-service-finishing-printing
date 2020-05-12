using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Integration.Master
{
    public class MaterialConstructionIntegrationViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            int Id = 1;
            string Code = "Code test";
            string Name = "Name test";
            string Remark = "Remark test";

            MaterialConstructionIntegrationViewModel mcivm = new MaterialConstructionIntegrationViewModel();
            mcivm.Id = 1;
            mcivm.Code = Code;
            mcivm.Name = Name;
            mcivm.Remark = Remark;

            Assert.Equal(Id, mcivm.Id);
            Assert.Equal(Code, mcivm.Code);
            Assert.Equal(Name, mcivm.Name);
            Assert.Equal(Remark, mcivm.Remark);
        }
    }
}
