using Com.Danliris.Service.Finishing.Printing.Lib.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Helpers
{
    public class APIEndpointTest
    {
        [Fact]
        public void Should_Success_Instantiate()
        {
            APIEndpoint.Core = "Core";
            APIEndpoint.Inventory = "Inventory";
            APIEndpoint.Core = "Core";
            APIEndpoint.Purchasing = "Purchasing";
            APIEndpoint.Sales = "Sales";
            APIEndpoint.Production = "Production";

            Assert.Equal("Core", APIEndpoint.Core);
            Assert.Equal("Inventory", APIEndpoint.Inventory);
            Assert.Equal("Production", APIEndpoint.Production);
            Assert.Equal("Purchasing", APIEndpoint.Purchasing);
            Assert.Equal("Sales", APIEndpoint.Sales);
        }
        }
}
