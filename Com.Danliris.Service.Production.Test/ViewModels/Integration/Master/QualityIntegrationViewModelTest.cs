using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Integration.Master
{
    public class QualityIntegrationViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            int Id = 1;
            string Code = "Code test";
            string Name = "Name test";

            QualityIntegrationViewModel qivm = new QualityIntegrationViewModel();
            qivm.Id = 1;
            qivm.Code = Code;
            qivm.Name = Name;

            Assert.Equal(Id, qivm.Id);
            Assert.Equal(Code, qivm.Code);
            Assert.Equal(Name, qivm.Name);
        }
    }
}
