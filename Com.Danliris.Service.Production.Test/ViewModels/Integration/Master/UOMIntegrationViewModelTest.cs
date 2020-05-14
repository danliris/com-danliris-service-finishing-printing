using Com.Danliris.Service.Production.Lib.ViewModels.Integration.Master;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Integration.Master
{
    public class UOMIntegrationViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            int Id = 1;
            string Unit = "Unit test";

            UOMIntegrationViewModel uivm = new UOMIntegrationViewModel();
            uivm.Id = 1;
            uivm.Unit = Unit;

            Assert.Equal(Id, uivm.Id);
            Assert.Equal(Unit, uivm.Unit);
        }
    }
}
