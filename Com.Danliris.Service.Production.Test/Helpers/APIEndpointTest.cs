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
            var Core = APIEndpoint.Core = "Core";
            var Inventory = APIEndpoint.Inventory = "Inventory";
            var Purchasing = APIEndpoint.Purchasing = "Purchasing";
            var Sales = APIEndpoint.Sales = "Sales";
            var Production =APIEndpoint.Production = "Production";

            Assert.Equal("Core", Core);
            Assert.Equal("Inventory", Inventory);
            Assert.Equal("Production", Production);
            Assert.Equal("Purchasing", Purchasing);
            Assert.Equal("Sales", Sales);
        }
        }
}
