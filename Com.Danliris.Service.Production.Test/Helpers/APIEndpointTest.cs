using Com.Danliris.Service.Finishing.Printing.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Helpers
{
  public  class APIEndpointTest
    {
        [Fact]
        public void should_success_instantiate()
        {
            APIEndpoint.Production = "Production";
            APIEndpoint.Purchasing = "Purchasing";
            Assert.Equal("Production", APIEndpoint.Production);
            Assert.Equal("Purchasing", APIEndpoint.Purchasing);
        }
    }
}
