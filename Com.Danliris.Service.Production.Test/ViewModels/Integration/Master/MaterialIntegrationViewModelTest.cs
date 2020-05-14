using Com.Danliris.Service.Finishing.Printing.Lib.ViewModels.Integration.Master;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.ViewModels.Integration.Master
{
    public class MaterialIntegrationViewModelTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            double Price = 1;
            string Code = "Code test";
            string Name = "Name test";
            string Tags = "Tags test";

            MaterialIntegrationViewModel mivm = new MaterialIntegrationViewModel();
            mivm.Price = 1;
            mivm.Code = Code;
            mivm.Name = Name;
            mivm.Tags = Tags;

            Assert.Equal(Price, mivm.Price);
            Assert.Equal(Code, mivm.Code);
            Assert.Equal(Name, mivm.Name);
            Assert.Equal(Tags, mivm.Tags);
        }
    }
}
