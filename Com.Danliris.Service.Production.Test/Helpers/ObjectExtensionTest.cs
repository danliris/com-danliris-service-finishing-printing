using Com.Danliris.Service.Finishing.Printing.Lib.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Finishing.Printing.Test.Helpers
{
 public   class ObjectExtensionTest
    {
        [Fact]
        public void should_Success_Clone()
        {
            //Setup
            var dataRaw = new
            {
                key = "value"
            };

            //Act
            var dataObj = JsonConvert.SerializeObject(dataRaw);
            var resultCopy = dataObj.Clone<string>();

            //Assert
            Assert.Equal(dataObj, resultCopy);
            Assert.NotSame(dataObj, resultCopy);

        }
    }
}
