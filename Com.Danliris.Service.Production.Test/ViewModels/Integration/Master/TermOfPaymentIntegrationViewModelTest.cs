using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Integration.Master
{
    public class TermOfPaymentIntegrationViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            int Id = 1;
            string Code = "Code test";
            string Name = "Name test";
            bool IsExport = true;

            TermOfPaymentIntegrationViewModel topivm = new TermOfPaymentIntegrationViewModel();
            topivm.Id = 1;
            topivm.Code = Code;
            topivm.Name = Name;
            topivm.IsExport = IsExport;

            Assert.Equal(Id, topivm.Id);
            Assert.Equal(Code, topivm.Code);
            Assert.Equal(Name, topivm.Name);
            Assert.Equal(IsExport, topivm.IsExport);
        }
    }
}
