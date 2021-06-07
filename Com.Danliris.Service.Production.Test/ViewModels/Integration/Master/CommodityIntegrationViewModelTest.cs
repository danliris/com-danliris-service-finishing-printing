using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Integration.Master
{
    public class CommodityIntegrationViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            int Id = 1;
            string Code = "Code test";
            string Name = "Name test";

            CommodityIntegrationViewModel civm = new CommodityIntegrationViewModel();
            civm.Id = 1;
            civm.Code = Code;
            civm.Name = Name;

            Assert.Equal(Id, civm.Id);
            Assert.Equal(Code, civm.Code);
            Assert.Equal(Name, civm.Name);
        }
    }
}
