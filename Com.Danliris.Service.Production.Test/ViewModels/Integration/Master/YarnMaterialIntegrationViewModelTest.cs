using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Integration.Master
{
    public class YarnMaterialIntegrationViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            int Id = 1;
            string Code = "Code test";
            string Name = "Name test";
            string Remark = "Remark test";

            YarnMaterialIntegrationViewModel yivm = new YarnMaterialIntegrationViewModel();
            yivm.Id = 1;
            yivm.Code = Code;
            yivm.Name = Name;
            yivm.Remark = Remark;

            Assert.Equal(Id, yivm.Id);
            Assert.Equal(Code, yivm.Code);
            Assert.Equal(Name, yivm.Name);
            Assert.Equal(Remark, yivm.Remark);
        }
    }
}
