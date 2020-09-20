using Com.Danliris.Service.Finishing.Printing.Lib.Enums.FabricQualityControlEnums;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Com.Danliris.Service.Finishing.Printing.Lib.Utilities;

namespace Com.Danliris.Service.Finishing.Printing.Test.Helpers
{
  public  class EnumExtensionsTest
    {
        [Fact]
        public void should_Success_GetDisplayName()
        {
            //Act
            var result = FabricQualityControlEnums.GeneralCriteriaName.BelangAbsorbsi.GetDisplayName();

            //Assert
            Assert.Equal("Belang Absorbsi", result);
        }
    }
}
